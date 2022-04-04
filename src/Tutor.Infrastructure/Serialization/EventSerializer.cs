﻿using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Tutor.Core.BuildingBlocks.EventSourcing;
using Tutor.Core.DomainModel.AssessmentItems.ArrangeTasks;
using Tutor.Core.DomainModel.AssessmentItems.Challenges;
using Tutor.Core.DomainModel.AssessmentItems.MultiResponseQuestions;
using Tutor.Core.DomainModel.AssessmentItems.ShortAnswerQuestions;
using Tutor.Core.DomainModel.KnowledgeComponents.Events.AssessmentItemEvents;
using Tutor.Core.DomainModel.KnowledgeComponents.Events.KnowledgeComponentEvents;
using Tutor.Core.DomainModel.KnowledgeComponents.Events.KnowledgeComponentEvents.SessionLifecycleEvents;

namespace Tutor.Infrastructure.Serialization
{
    public static class EventSerializer
    {
        private static readonly IDictionary<Type, string> eventRelatedTypes = new Dictionary<Type, string>()
        {
            { typeof(AssessmentItemAnswered), "AssessmentItemAnswered" },
            { typeof(SoughtHints), "SoughtChallengeHints" },
            { typeof(SoughtSolution), "SoughtChallengeSolution" },
            { typeof(KnowledgeComponentPassed), "KnowledgeComponentPassed" },
            { typeof(KnowledgeComponentCompleted), "KnowledgeComponentCompleted" },
            { typeof(KnowledgeComponentSatisfied), "KnowledgeComponentSatisfied" },
            { typeof(AssessmentItemSelected), "AssessmentItemSelected" },
            { typeof(SessionLaunched), "SessionLaunched" },
            { typeof(SessionTerminated), "SessionTerminated" },
            { typeof(SessionAbandoned), "SessionAbandoned" },
            { typeof(InstructionalItemsSelected), "InstructionalItemsSelected" },
#region Submissions
            { typeof(ArrangeTaskSubmission), "ArrangeTaskSubmission" },
            { typeof(ChallengeSubmission), "ChallengeSubmission" },
            { typeof(MrqSubmission), "MrqSubmission" },
            { typeof(SaqSubmission), "SaqSubmission" },
#endregion
        };

        public static JsonSerializerOptions SetupEvents(this JsonSerializerOptions options)
        {
            DiscriminatorConventionRegistry registry = options.GetDiscriminatorConventionRegistry();
            registry.RegisterConvention(new AllowedTypesDiscriminatorConvention<string>(options, eventRelatedTypes, "$discriminator"));

            foreach (Type type in eventRelatedTypes.Keys)
            {
                registry.RegisterType(type);
            }

            return options;
        }

        public static JsonSerializerOptions GetEventSerializerOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.SetupExtensions();
            options.GetDiscriminatorConventionRegistry().ClearConventions();
            options.SetupEvents();

            return options;
        }

        public static string Serialize(DomainEvent @event)
        {
            return JsonSerializer.Serialize(@event, GetEventSerializerOptions());
        }

        public static DomainEvent Deserialize(string @event)
        {
            return JsonSerializer.Deserialize<DomainEvent>(@event, GetEventSerializerOptions());
        }
    }
}
