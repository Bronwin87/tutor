﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tutor.Infrastructure.Database;

namespace Tutor.Infrastructure.Migrations
{
    [DbContext(typeof(TutorContext))]
    partial class TutorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTaskContainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ArrangeTaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArrangeTaskId");

                    b.ToTable("ArrangeTaskContainers");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTaskElement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ArrangeTaskContainerId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArrangeTaskContainerId");

                    b.ToTable("ArrangeTaskElements");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.ChallengeHint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<int?>("LearningObjectSummaryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ChallengeHints");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ChallengeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChallengeId");

                    b.ToTable("ChallengeFulfillmentStrategies");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.MetricRangeRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ClassMetricCheckerForeignKey")
                        .HasColumnType("integer");

                    b.Property<double>("FromValue")
                        .HasColumnType("double precision");

                    b.Property<int?>("HintId")
                        .HasColumnType("integer");

                    b.Property<int?>("MethodMetricCheckerForeignKey")
                        .HasColumnType("integer");

                    b.Property<string>("MetricName")
                        .HasColumnType("text");

                    b.Property<double>("ToValue")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ClassMetricCheckerForeignKey");

                    b.HasIndex("HintId");

                    b.HasIndex("MethodMetricCheckerForeignKey");

                    b.ToTable("MetricRangeRules");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.LearningObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("LearningObjectSummaryId")
                        .HasColumnType("integer");

                    b.Property<int?>("NodeProgressId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NodeProgressId");

                    b.ToTable("LearningObjects");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Questions.QuestionAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Feedback")
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionAnswers");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.KnowledgeNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("LearningObjective")
                        .HasColumnType("text");

                    b.Property<int>("LectureId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("KnowledgeNodes");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.LearningObjectSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("KnowledgeNodeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("KnowledgeNodeId");

                    b.ToTable("LearningObjectSummaries");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("Tutor.Core.KnowledgeComponentModel.KnowledgeComponents.KnowledgeComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("KnowledgeComponentId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("KnowledgeComponentId");

                    b.ToTable("KnowledgeComponents");
                });

            modelBuilder.Entity("Tutor.Core.KnowledgeComponentModel.KnowledgeComponents.KnowledgeComponentProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("KnowledgeComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<double>("Progress")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("KnowledgeComponentsProgresses");
                });

            modelBuilder.Entity("Tutor.Core.LearnerModel.Learners.Learner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IamId")
                        .HasColumnType("text");

                    b.Property<string>("StudentIndex")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Learners");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Feedback.LearningObjectFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<int>("LearningObjectId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("LearningObjectFeedback");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Progress.NodeProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<int?>("NodeId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.ToTable("NodeProgresses");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Submissions.ArrangeTaskContainerSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ArrangeTaskSubmissionId")
                        .HasColumnType("integer");

                    b.Property<int>("ContainerId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("ElementIds")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.HasIndex("ArrangeTaskSubmissionId");

                    b.ToTable("ArrangeTaskContainerSubmissions");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Submissions.ArrangeTaskSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ArrangeTaskId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ArrangeTaskSubmissions");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Submissions.ChallengeSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ChallengeId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<string[]>("SourceCode")
                        .HasColumnType("text[]");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ChallengeSubmissions");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Submissions.QuestionSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("LearnerId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("SubmittedAnswerIds")
                        .HasColumnType("integer[]");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("QuestionSubmissions");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy");

                    b.ToTable("BasicMetricCheckers");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.NameChecker.BasicNameChecker", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy");

                    b.Property<List<string>>("BannedWords")
                        .HasColumnType("text[]");

                    b.Property<int?>("HintId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("RequiredWords")
                        .HasColumnType("text[]");

                    b.HasIndex("HintId");

                    b.ToTable("BasicNameCheckers");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTask", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.LearningObject");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.ToTable("ArrangeTasks");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.Challenge", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.LearningObject");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("SolutionIdForeignKey")
                        .HasColumnType("integer");

                    b.Property<string>("TestSuiteLocation")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasIndex("SolutionIdForeignKey");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Image", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.LearningObject");

                    b.Property<string>("Caption")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Questions.Question", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.LearningObject");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Text", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.LearningObject");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.ToTable("Texts");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Video", b =>
                {
                    b.HasBaseType("Tutor.Core.ContentModel.LearningObjects.LearningObject");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTaskContainer", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTask", null)
                        .WithMany("Containers")
                        .HasForeignKey("ArrangeTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTaskElement", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTaskContainer", null)
                        .WithMany("Elements")
                        .HasForeignKey("ArrangeTaskContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.Challenge", null)
                        .WithMany("FulfillmentStrategies")
                        .HasForeignKey("ChallengeId");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.MetricRangeRule", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", null)
                        .WithMany("ClassMetricRules")
                        .HasForeignKey("ClassMetricCheckerForeignKey");

                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.ChallengeHint", "Hint")
                        .WithMany()
                        .HasForeignKey("HintId");

                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", null)
                        .WithMany("MethodMetricRules")
                        .HasForeignKey("MethodMetricCheckerForeignKey");

                    b.Navigation("Hint");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.LearningObject", b =>
                {
                    b.HasOne("Tutor.Core.ProgressModel.Progress.NodeProgress", null)
                        .WithMany("LearningObjects")
                        .HasForeignKey("NodeProgressId");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Questions.QuestionAnswer", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Questions.Question", null)
                        .WithMany("PossibleAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.KnowledgeNode", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.Lectures.Lecture", null)
                        .WithMany("KnowledgeNodes")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.LearningObjectSummary", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.Lectures.KnowledgeNode", "KnowledgeNode")
                        .WithMany("LearningObjectSummaries")
                        .HasForeignKey("KnowledgeNodeId");

                    b.Navigation("KnowledgeNode");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.Lecture", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.Lectures.Course", null)
                        .WithMany("Lectures")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.KnowledgeComponentModel.KnowledgeComponents.KnowledgeComponent", b =>
                {
                    b.HasOne("Tutor.Core.KnowledgeComponentModel.KnowledgeComponents.KnowledgeComponent", null)
                        .WithMany("Subcomponents")
                        .HasForeignKey("KnowledgeComponentId");
                });

            modelBuilder.Entity("Tutor.Core.LearnerModel.Learners.Learner", b =>
                {
                    b.OwnsOne("Tutor.Core.LearnerModel.Workspaces.Workspace", "Workspace", b1 =>
                        {
                            b1.Property<int>("LearnerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            modelBuilder.Entity("Tutor.Core.ProgressModel.Progress.NodeProgress", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.Lectures.KnowledgeNode", "Node")
                        .WithMany()
                        .HasForeignKey("NodeId");

                    b.Navigation("Node");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Submissions.ArrangeTaskContainerSubmission", b =>
                {
                    b.HasOne("Tutor.Core.ProgressModel.Submissions.ArrangeTaskSubmission", null)
                        .WithMany("Containers")
                        .HasForeignKey("ArrangeTaskSubmissionId");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.NameChecker.BasicNameChecker", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.ChallengeHint", "Hint")
                        .WithMany()
                        .HasForeignKey("HintId");

                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.ChallengeFulfillmentStrategy", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.NameChecker.BasicNameChecker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hint");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTask", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.LearningObject", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTask", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.Challenge", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.LearningObject", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Challenges.Challenge", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tutor.Core.ContentModel.Lectures.LearningObjectSummary", "Solution")
                        .WithMany()
                        .HasForeignKey("SolutionIdForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solution");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Image", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.LearningObject", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Image", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Questions.Question", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.LearningObject", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Questions.Question", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Text", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.LearningObject", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Text", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Video", b =>
                {
                    b.HasOne("Tutor.Core.ContentModel.LearningObjects.LearningObject", null)
                        .WithOne()
                        .HasForeignKey("Tutor.Core.ContentModel.LearningObjects.Video", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTaskContainer", b =>
                {
                    b.Navigation("Elements");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.Course", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.KnowledgeNode", b =>
                {
                    b.Navigation("LearningObjectSummaries");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.Lectures.Lecture", b =>
                {
                    b.Navigation("KnowledgeNodes");
                });

            modelBuilder.Entity("Tutor.Core.KnowledgeComponentModel.KnowledgeComponents.KnowledgeComponent", b =>
                {
                    b.Navigation("Subcomponents");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Progress.NodeProgress", b =>
                {
                    b.Navigation("LearningObjects");
                });

            modelBuilder.Entity("Tutor.Core.ProgressModel.Submissions.ArrangeTaskSubmission", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.FulfillmentStrategy.MetricChecker.BasicMetricChecker", b =>
                {
                    b.Navigation("ClassMetricRules");

                    b.Navigation("MethodMetricRules");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.ArrangeTasks.ArrangeTask", b =>
                {
                    b.Navigation("Containers");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Challenges.Challenge", b =>
                {
                    b.Navigation("FulfillmentStrategies");
                });

            modelBuilder.Entity("Tutor.Core.ContentModel.LearningObjects.Questions.Question", b =>
                {
                    b.Navigation("PossibleAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
