﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tutor.Infrastructure.Database;

#nullable disable

namespace Tutor.Infrastructure.Migrations
{
    [DbContext(typeof(TutorContext))]
    [Migration("20220131193935_migration0")]
    partial class migration0
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskContainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArrangeTaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArrangeTaskId");

                    b.ToTable("ArrangeTaskContainers");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskContainerSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArrangeTaskContainerId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("ElementIds")
                        .HasColumnType("integer[]");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId");

                    b.ToTable("ArrangeTaskContainerSubmissions");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArrangeTaskContainerId")
                        .HasColumnType("integer");

                    b.Property<string>("Feedback")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArrangeTaskContainerId");

                    b.ToTable("ArrangeTaskElements");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KnowledgeComponentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AssessmentEvents");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.ChallengeHint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChallengeHints");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChallengeId")
                        .HasColumnType("integer");

                    b.Property<string>("CodeSnippetId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("ChallengeFulfillmentStrategies");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.MetricRangeRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("FromValue")
                        .HasColumnType("double precision");

                    b.Property<int?>("HintId")
                        .HasColumnType("integer");

                    b.Property<int?>("MetricCheckerForeignKey")
                        .HasColumnType("integer");

                    b.Property<string>("MetricName")
                        .HasColumnType("text");

                    b.Property<double>("ToValue")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("HintId");

                    b.HasIndex("MetricCheckerForeignKey");

                    b.ToTable("MetricRangeRules");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.MrqItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Feedback")
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("MrqId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MrqId");

                    b.ToTable("MrqItems");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssessmentEventId")
                        .HasColumnType("integer");

                    b.Property<double>("CorrectnessLevel")
                        .HasColumnType("double precision");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AssessmentEventId");

                    b.ToTable("Submissions");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Submission");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KnowledgeComponentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("InstructionalEvents");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<int?>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("UnitId");

                    b.ToTable("KnowledgeComponents");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponentMastery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KnowledgeComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<double>("Mastery")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("KnowledgeComponentId");

                    b.HasIndex("LearnerId");

                    b.ToTable("KcMastery");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Tutor.Core.LearnerModel.Learners.Learner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("IamId")
                        .HasColumnType("text");

                    b.Property<string>("StudentIndex")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Learners");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTask", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.ToTable("ArrangeTasks", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskSubmission", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.Submission");

                    b.HasDiscriminator().HasValue("ArrangeTaskSubmission");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.Challenge", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("SolutionUrl")
                        .HasColumnType("text");

                    b.Property<string>("TestSuiteLocation")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.ToTable("Challenges", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.ChallengeSubmission", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.Submission");

                    b.Property<string[]>("SourceCode")
                        .HasColumnType("text[]");

                    b.HasDiscriminator().HasValue("ChallengeSubmission");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy");

                    b.ToTable("BasicMetricCheckers", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.NameChecker.BannedWordsChecker", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy");

                    b.Property<List<string>>("BannedWords")
                        .HasColumnType("text[]");

                    b.Property<int?>("HintId")
                        .HasColumnType("integer");

                    b.HasIndex("HintId");

                    b.ToTable("BannedWordsCheckers", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.NameChecker.RequiredWordsChecker", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy");

                    b.Property<int?>("HintId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("RequiredWords")
                        .HasColumnType("text[]");

                    b.HasIndex("HintId");

                    b.ToTable("RequiredWordsCheckers", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.Mrq", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.ToTable("MultiResponseQuestions", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.MrqSubmission", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.AssessmentEvents.Submission");

                    b.Property<List<int>>("SubmittedAnswerIds")
                        .HasColumnType("integer[]");

                    b.HasDiscriminator().HasValue("MrqSubmission");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.Image", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent");

                    b.Property<string>("Caption")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.Text", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.ToTable("Texts", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.Video", b =>
                {
                    b.HasBaseType("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.ToTable("Videos", (string)null);
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskContainer", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTask", null)
                        .WithMany("Containers")
                        .HasForeignKey("ArrangeTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskContainerSubmission", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskSubmission", null)
                        .WithMany("Containers")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskElement", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskContainer", null)
                        .WithMany("Elements")
                        .HasForeignKey("ArrangeTaskContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.Challenge", null)
                        .WithMany("FulfillmentStrategies")
                        .HasForeignKey("ChallengeId");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.MetricRangeRule", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.ChallengeHint", "Hint")
                        .WithMany()
                        .HasForeignKey("HintId");

                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", null)
                        .WithMany("MetricRanges")
                        .HasForeignKey("MetricCheckerForeignKey");

                    b.Navigation("Hint");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.MrqItem", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.Mrq", null)
                        .WithMany("Items")
                        .HasForeignKey("MrqId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Submission", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent", null)
                        .WithMany("Submissions")
                        .HasForeignKey("AssessmentEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponent", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponent", null)
                        .WithMany("KnowledgeComponents")
                        .HasForeignKey("ParentId");

                    b.HasOne("Tutor.Core.DomainModel.KnowledgeComponents.Unit", null)
                        .WithMany("KnowledgeComponents")
                        .HasForeignKey("UnitId");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponentMastery", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponent", null)
                        .WithMany("Masteries")
                        .HasForeignKey("KnowledgeComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tutor.Core.LearnerModel.Learners.Learner", null)
                        .WithMany("KnowledgeComponentMasteries")
                        .HasForeignKey("LearnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.LearnerModel.Learners.Learner", b =>
                {
                    b.OwnsOne("Tutor.Core.LearnerModel.Workspaces.Workspace", "Workspace", b1 =>
                        {
                            b1.Property<int>("LearnerId")
                                .HasColumnType("integer");

                            b1.Property<string>("Path")
                                .HasColumnType("text")
                                .HasColumnName("WorkspacePath");

                            b1.HasKey("LearnerId");

                            b1.ToTable("Learners");

                            b1.WithOwner()
                                .HasForeignKey("LearnerId");
                        });

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTask", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTask", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.Challenge", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.AssessmentEvents.Challenges.Challenge", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.NameChecker.BannedWordsChecker", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.ChallengeHint", "Hint")
                        .WithMany()
                        .HasForeignKey("HintId");

                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.NameChecker.BannedWordsChecker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hint");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.NameChecker.RequiredWordsChecker", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.ChallengeHint", "Hint")
                        .WithMany()
                        .HasForeignKey("HintId");

                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.NameChecker.RequiredWordsChecker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hint");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.Mrq", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.Mrq", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.Image", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.InstructionalEvents.Image", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.Text", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.InstructionalEvents.Text", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.InstructionalEvents.Video", b =>
                {
                    b.HasOne("Tutor.Core.DomainModel.InstructionalEvents.InstructionalEvent", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.DomainModel.InstructionalEvents.Video", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskContainer", b =>
                {
                    b.Navigation("Elements");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.AssessmentEvent", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.KnowledgeComponent", b =>
                {
                    b.Navigation("KnowledgeComponents");

                    b.Navigation("Masteries");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.KnowledgeComponents.Unit", b =>
                {
                    b.Navigation("KnowledgeComponents");
                });

            modelBuilder.Entity("Tutor.Core.LearnerModel.Learners.Learner", b =>
                {
                    b.Navigation("KnowledgeComponentMasteries");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTask", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.ArrangeTasks.ArrangeTaskSubmission", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.Challenge", b =>
                {
                    b.Navigation("FulfillmentStrategies");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", b =>
                {
                    b.Navigation("MetricRanges");
                });

            modelBuilder.Entity("Tutor.Core.DomainModel.AssessmentEvents.MultiResponseQuestions.Mrq", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
