using System.Collections.Generic;
using CitizenFX.Core;
using FluentAssertions;
using PolyZone.Shapes;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace PolyZone.Tests.Shapes;

public class PolygonTests
{
    [Fact]
    public void Polygon_A_IsInside_ShouldPassTest()
    {
        Vector2 fPointOutside = new() { X = -5.120505f, Y = 1.105695f }; // F
        Vector2 gPointInside = new() { X = -5.1205f, Y = 1.10569f }; // G
        
        Vector2 iPointOutside = new() { X = -6.1472f, Y = -0.6536f }; // I
        Vector2 hPointInside = new() { X = -6.1476f, Y = -0.6536f }; // H
        
        IReadOnlyList<Vector2> points =
        [
            new() { X = 7.00776f, Y = 1.24414f }, // A
            new() { X = -5.46056f, Y = 1.10181f }, // B
            new() { X = -6.99775f, Y = -2.82657f }, // C
            new() { X = -0.30813f, Y = -2.6273f }, // D
            new() { X = -7.39628f, Y = -0.57772f }, // E
            new() { X = -5.1175f, Y = 1.10583f }, // F
        ];

        var polygon = new Polygon(points);

        polygon.IsInside(fPointOutside).Should().BeFalse();
        polygon.IsInside(gPointInside).Should().BeTrue();

        polygon.IsInside(iPointOutside).Should().BeFalse();
        polygon.IsInside(hPointInside).Should().BeTrue();

        
        var distance = polygon.DistanceTo(new() { X = -1f, Y = 3.6f });

        distance.Should().BeGreaterThan(0);
    }
}
