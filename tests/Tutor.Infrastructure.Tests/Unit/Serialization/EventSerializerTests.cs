﻿using Shouldly;
using System.Collections.Generic;
using Tutor.Core.BuildingBlocks.EventSourcing;
using Tutor.Core.DomainModel.KnowledgeComponents.Events.AssessmentItemEvents;
using Tutor.Infrastructure.Serialization;
using Xunit;

namespace Tutor.Infrastructure.Tests.Unit.Serialization
{
    public class EventSerializerTests
    {
        [Theory]
        [MemberData(nameof(Events))]
        public void Serializes_and_deserializes_events(DomainEvent @event)
        {
            var serialized = EventSerializer.Serialize(@event);
            var deserialized = EventSerializer.Deserialize(serialized);

            deserialized.ShouldNotBeNull();
            deserialized.ShouldBeOfType(@event.GetType());
            foreach (var property in @event.GetType().GetProperties())
            {
                property.GetValue(deserialized).ShouldBe(property.GetValue(@event));
            }
        }

        public static IEnumerable<object[]> Events() => new List<object[]>
        {
            new object[]
            {
                new AssessmentItemSelected()
                {
                    KnowledgeComponentId = 1,
                    AssessmentItemId = 2,
                    LearnerId = 3
                }
            },
            new object[]
            {
                new AssessmentItemAnswered()
            }
        };
    }
}
