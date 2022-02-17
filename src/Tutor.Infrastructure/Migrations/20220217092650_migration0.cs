﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tutor.Infrastructure.Migrations
{
    public partial class migration0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeComponentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeHints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeHints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstructionalEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeComponentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionalEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Learners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentIndex = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    WorkspacePath = table.Column<string>(type: "text", nullable: true),
                    IamId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Learners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArrangeTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrangeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArrangeTasks_AssessmentEvents_Id",
                        column: x => x.Id,
                        principalTable: "AssessmentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TestSuiteLocation = table.Column<string>(type: "text", nullable: true),
                    SolutionUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_AssessmentEvents_Id",
                        column: x => x.Id,
                        principalTable: "AssessmentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiResponseQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiResponseQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiResponseQuestions_AssessmentEvents_Id",
                        column: x => x.Id,
                        principalTable: "AssessmentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortAnswerQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    AcceptableAnswers = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortAnswerQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortAnswerQuestions_AssessmentEvents_Id",
                        column: x => x.Id,
                        principalTable: "AssessmentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssessmentEventId = table.Column<int>(type: "integer", nullable: false),
                    LearnerId = table.Column<int>(type: "integer", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    CorrectnessLevel = table.Column<double>(type: "double precision", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    SourceCode = table.Column<string[]>(type: "text[]", nullable: true),
                    SubmittedAnswerIds = table.Column<List<int>>(type: "integer[]", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_AssessmentEvents_AssessmentEventId",
                        column: x => x.AssessmentEventId,
                        principalTable: "AssessmentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Caption = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_InstructionalEvents_Id",
                        column: x => x.Id,
                        principalTable: "InstructionalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Texts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Texts_InstructionalEvents_Id",
                        column: x => x.Id,
                        principalTable: "InstructionalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_InstructionalEvents_Id",
                        column: x => x.Id,
                        principalTable: "InstructionalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    UnitId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeComponents_KnowledgeComponents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "KnowledgeComponents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KnowledgeComponents_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArrangeTaskContainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArrangeTaskId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrangeTaskContainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArrangeTaskContainers_ArrangeTasks_ArrangeTaskId",
                        column: x => x.ArrangeTaskId,
                        principalTable: "ArrangeTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeFulfillmentStrategies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodeSnippetId = table.Column<string>(type: "text", nullable: true),
                    ChallengeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeFulfillmentStrategies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeFulfillmentStrategies_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MrqItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MrqId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MrqItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MrqItems_MultiResponseQuestions_MrqId",
                        column: x => x.MrqId,
                        principalTable: "MultiResponseQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArrangeTaskContainerSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubmissionId = table.Column<int>(type: "integer", nullable: false),
                    ArrangeTaskContainerId = table.Column<int>(type: "integer", nullable: false),
                    ElementIds = table.Column<List<int>>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrangeTaskContainerSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArrangeTaskContainerSubmissions_Submissions_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KcMastery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mastery = table.Column<double>(type: "double precision", nullable: false),
                    KnowledgeComponentId = table.Column<int>(type: "integer", nullable: false),
                    LearnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KcMastery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KcMastery_KnowledgeComponents_KnowledgeComponentId",
                        column: x => x.KnowledgeComponentId,
                        principalTable: "KnowledgeComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KcMastery_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArrangeTaskElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArrangeTaskContainerId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrangeTaskElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArrangeTaskElements_ArrangeTaskContainers_ArrangeTaskContai~",
                        column: x => x.ArrangeTaskContainerId,
                        principalTable: "ArrangeTaskContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannedWordsCheckers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    BannedWords = table.Column<List<string>>(type: "text[]", nullable: true),
                    HintId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedWordsCheckers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannedWordsCheckers_ChallengeFulfillmentStrategies_Id",
                        column: x => x.Id,
                        principalTable: "ChallengeFulfillmentStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BannedWordsCheckers_ChallengeHints_HintId",
                        column: x => x.HintId,
                        principalTable: "ChallengeHints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BasicMetricCheckers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicMetricCheckers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicMetricCheckers_ChallengeFulfillmentStrategies_Id",
                        column: x => x.Id,
                        principalTable: "ChallengeFulfillmentStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredWordsCheckers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    RequiredWords = table.Column<List<string>>(type: "text[]", nullable: true),
                    HintId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredWordsCheckers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredWordsCheckers_ChallengeFulfillmentStrategies_Id",
                        column: x => x.Id,
                        principalTable: "ChallengeFulfillmentStrategies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredWordsCheckers_ChallengeHints_HintId",
                        column: x => x.HintId,
                        principalTable: "ChallengeHints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MetricRangeRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetricName = table.Column<string>(type: "text", nullable: true),
                    FromValue = table.Column<double>(type: "double precision", nullable: false),
                    ToValue = table.Column<double>(type: "double precision", nullable: false),
                    HintId = table.Column<int>(type: "integer", nullable: true),
                    MetricCheckerForeignKey = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricRangeRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetricRangeRules_BasicMetricCheckers_MetricCheckerForeignKey",
                        column: x => x.MetricCheckerForeignKey,
                        principalTable: "BasicMetricCheckers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MetricRangeRules_ChallengeHints_HintId",
                        column: x => x.HintId,
                        principalTable: "ChallengeHints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArrangeTaskContainers_ArrangeTaskId",
                table: "ArrangeTaskContainers",
                column: "ArrangeTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrangeTaskContainerSubmissions_SubmissionId",
                table: "ArrangeTaskContainerSubmissions",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrangeTaskElements_ArrangeTaskContainerId",
                table: "ArrangeTaskElements",
                column: "ArrangeTaskContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_BannedWordsCheckers_HintId",
                table: "BannedWordsCheckers",
                column: "HintId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeFulfillmentStrategies_ChallengeId",
                table: "ChallengeFulfillmentStrategies",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_KcMastery_KnowledgeComponentId",
                table: "KcMastery",
                column: "KnowledgeComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_KcMastery_LearnerId",
                table: "KcMastery",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeComponents_ParentId",
                table: "KnowledgeComponents",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeComponents_UnitId",
                table: "KnowledgeComponents",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MetricRangeRules_HintId",
                table: "MetricRangeRules",
                column: "HintId");

            migrationBuilder.CreateIndex(
                name: "IX_MetricRangeRules_MetricCheckerForeignKey",
                table: "MetricRangeRules",
                column: "MetricCheckerForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_MrqItems_MrqId",
                table: "MrqItems",
                column: "MrqId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredWordsCheckers_HintId",
                table: "RequiredWordsCheckers",
                column: "HintId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssessmentEventId",
                table: "Submissions",
                column: "AssessmentEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArrangeTaskContainerSubmissions");

            migrationBuilder.DropTable(
                name: "ArrangeTaskElements");

            migrationBuilder.DropTable(
                name: "BannedWordsCheckers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "KcMastery");

            migrationBuilder.DropTable(
                name: "MetricRangeRules");

            migrationBuilder.DropTable(
                name: "MrqItems");

            migrationBuilder.DropTable(
                name: "RequiredWordsCheckers");

            migrationBuilder.DropTable(
                name: "ShortAnswerQuestions");

            migrationBuilder.DropTable(
                name: "Texts");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "ArrangeTaskContainers");

            migrationBuilder.DropTable(
                name: "KnowledgeComponents");

            migrationBuilder.DropTable(
                name: "Learners");

            migrationBuilder.DropTable(
                name: "BasicMetricCheckers");

            migrationBuilder.DropTable(
                name: "MultiResponseQuestions");

            migrationBuilder.DropTable(
                name: "ChallengeHints");

            migrationBuilder.DropTable(
                name: "InstructionalEvents");

            migrationBuilder.DropTable(
                name: "ArrangeTasks");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "ChallengeFulfillmentStrategies");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "AssessmentEvents");
        }
    }
}
