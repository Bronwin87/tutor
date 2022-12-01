using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tutor.Core.BuildingBlocks.EventSourcing;
using Tutor.Core.Domain.CourseIteration;
using Tutor.Core.Domain.Knowledge.RepositoryInterfaces;
using Tutor.Core.Domain.KnowledgeMastery;
using Tutor.Core.Domain.KnowledgeMastery.MoveOn;
using Tutor.Core.Domain.LearningUtilities;
using Tutor.Core.Domain.Stakeholders.RepositoryInterfaces;
using Tutor.Core.UseCases.KnowledgeAnalysis;
using Tutor.Core.UseCases.Learning;
using Tutor.Core.UseCases.Learning.Assessment;
using Tutor.Core.UseCases.Learning.Utilities;
using Tutor.Core.UseCases.Management.CourseIteration;
using Tutor.Core.UseCases.Management.Knowledge;
using Tutor.Core.UseCases.Management.Stakeholder;
using Tutor.Core.UseCases.ProgressMonitoring;
using Tutor.Infrastructure;
using Tutor.Infrastructure.Database.EventStore.DefaultEventSerializer;
using Tutor.Infrastructure.Database.Repositories;
using Tutor.Infrastructure.Database.Repositories.CourseIteration;
using Tutor.Infrastructure.Database.Repositories.Knowledge;
using Tutor.Infrastructure.Database.Repositories.LearningUtilities;
using Tutor.Infrastructure.Database.Repositories.Stakeholders;
using Tutor.Infrastructure.EventConfiguration;
using Tutor.Infrastructure.Security;
using Tutor.Infrastructure.Security.Authentication;
using Tutor.Web.Mappings.Knowledge.DTOs.AssessmentItems.ArrangeTasks;
using Tutor.Web.Mappings.Knowledge.DTOs.AssessmentItems.Challenges;
using Tutor.Web.Mappings.Knowledge.DTOs.AssessmentItems.MultiResponseQuestions;
using Tutor.Web.Mappings.Knowledge.DTOs.InstructionalItems;

namespace Tutor.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    private const string CorsPolicy = "_corsPolicy";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructure(Configuration);

        SetupControllers(services);
        SetupServices(services);
        SetupRepositories(services);
    }

    #region Controller Setup
    private static void SetupControllers(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));
        services.AddControllers().AddJsonOptions(SetupJsonOptions);
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy(name: CorsPolicy,
                builder =>
                {
                    builder.WithOrigins(ParseCorsOrigins())
                        .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "access_token")
                        .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");
                });
        });
        SetupAuth(services);
    }

    private static void SetupJsonOptions(JsonOptions options)
    {
        var serializerOptions = options.JsonSerializerOptions;
        serializerOptions.SetupExtensions();
        var registry = serializerOptions.GetDiscriminatorConventionRegistry();
        registry.ClearConventions();
        registry.RegisterConvention(
            new DefaultDiscriminatorConvention<string>(serializerOptions, "typeDiscriminator"));
        registry.RegisterType<AtDto>();
        registry.RegisterType<ChallengeDto>();
        registry.RegisterType<ImageDto>();
        registry.RegisterType<MrqDto>();
        registry.RegisterType<TextDto>();
        registry.RegisterType<VideoDto>();

        registry.RegisterConvention(new AllowedTypesDiscriminatorConvention<string>(
            serializerOptions, EventSerializationConfiguration.EventRelatedTypes, "$discriminator"));
        foreach (var type in EventSerializationConfiguration.EventRelatedTypes.Keys)
        {
            registry.RegisterType(type);
        }
    }
    private static void SetupAuth(IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserDatabaseRepository>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("administratorPolicy", policy => policy.RequireRole("administrator"));
            options.AddPolicy("instructorPolicy", policy => policy.RequireRole("instructor"));
            options.AddPolicy("learnerPolicy", policy => policy.RequireRole("learner"));
        });

        var key = EnvironmentConnection.GetSecret("JWT_KEY") ?? "tutor_secret_key";
        var issuer = EnvironmentConnection.GetSecret("JWT_ISSUER") ?? "tutor";
        var audience = EnvironmentConnection.GetSecret("JWT_AUDIENCE") ?? "tutor-front.com";

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("AuthenticationTokens-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
    }

    private static string[] ParseCorsOrigins()
    {
        var corsOrigins = new[] { "http://localhost:4200" };
        var corsOriginsPath = EnvironmentConnection.GetSecret("SMART_TUTOR_CORS_ORIGINS");
        if (File.Exists(corsOriginsPath))
        {
            corsOrigins = File.ReadAllLines(corsOriginsPath);
        }

        return corsOrigins;
    }
    #endregion

    #region Service Setup
    private void SetupServices(IServiceCollection services)
    {
        SetupLearningServices(services);
        SetupManagementServices(services);

        services.AddScoped<IUnitAnalysisService, UnitAnalysisService>();
    }

    private void SetupLearningServices(IServiceCollection services)
    {
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IStructureService, StructureService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<ISelectionService, SelectionService>();
        services.AddScoped<IEvaluationService, EvaluationService>();
        services.AddScoped<IHelpService, HelpService>();

        services.AddScoped<IFeedbackService, FeedbackService>();
        services.AddScoped<INoteService, NoteService>();

        services.AddScoped<ICourseIterationMonitoringService, CourseIterationMonitoringService>();
        services.AddScoped<IAssessmentItemSelector, LeastCorrectAssessmentItemSelector>();
        SetupMoveOn(services);
    }

    private void SetupMoveOn(IServiceCollection services)
    {
        var moveOnCriteria = Configuration.GetValue<string>("MoveOn");
        var moveOnType = MoveOnResolver.ResolveOrDefault(moveOnCriteria);
        services.AddScoped(typeof(IMoveOnCriteria), moveOnType);
    }

    private static void SetupManagementServices(IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUnitService, UnitService>();

        services.AddScoped<ICourseOwnershipService, CourseOwnershipService>();
        
        services.AddScoped<ILearnerService, LearnerService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
    }
    #endregion

    #region Repository Setup
    private static void SetupRepositories(IServiceCollection services)
    {
        services.AddScoped<IKnowledgeComponentRepository, KnowledgeComponentDatabaseRepository>();
        services.AddScoped<IAssessmentItemRepository, AssessmentItemDatabaseRepository>();
        services.AddScoped<IKnowledgeMasteryRepository, KnowledgeMasteryDatabaseRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackDatabaseRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();

        services.AddScoped<ICourseRepository, CourseDatabaseRepository>();
        services.AddScoped<IUnitRepository, UnitDatabaseRepository>();
        services.AddScoped<IInstructorRepository, InstructorDatabaseRepository>();
        services.AddScoped<IOwnedCourseRepository, OwnedCourseDatabaseRepository>();
        services.AddScoped<ILearnerRepository, LearnerDatabaseRepository>();
        services.AddScoped<IGroupRepository, GroupDatabaseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentDatabaseRepository>();
        services.AddSingleton<IEventSerializer>(
            new DefaultEventSerializer(EventSerializationConfiguration.EventRelatedTypes));
    }
    #endregion

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/error");
        }
        
        app.UseCors(CorsPolicy);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}