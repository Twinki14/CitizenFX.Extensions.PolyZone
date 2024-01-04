using System.Collections.Generic;
using CitizenFX.Core;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using PolyZone.Shapes;
using PolyZone.Tests.Internal;
using Xunit.Abstractions;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace PolyZone.Tests.Shapes;

public class PolygonTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public PolygonTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Polygon_A_IsInside_ShouldPassTest()
    {
        var insidePoints = new[]
        {
            new Vector2 { X = 9.11417f, Y = 4.19913f }, // H (9.11417,4.19913)
            new Vector2 { X = 9.08464f, Y = 4.1949f }, // I (9.08464,4.1949)
            new Vector2 { X = 9.04999f, Y = 4.19522f }, // J (9.04999,4.19522)
            new Vector2 { X = 8.79974f, Y = 4.15845f }, // K (8.79974,4.15845)
            new Vector2 { X = -1f, Y = 3f }, // L (-1.22244,2.8594)
            new Vector2 { X = -3.44097f, Y = 0.18371f }, // M (-3.44097,0.18371)
            new Vector2 { X = -3.26505f, Y = -4.55097f }, // N (-3.26505,-4.55097)
            new Vector2 { X = -2f, Y = -8f }, // O (-3,-8)
            new Vector2 { X = 2.26106f, Y = -9.30121f }, // P (2.26106,-9.30121)
        };
        
        List<Vector2> points =
        [
            new() { X = 9.12f, Y = 4.2f }, // A (9.12,4.2)
            new() { X = -3.98f, Y = 3.32f }, // B (-3.98,3.32)
            new() { X = -4.56966f, Y = -3.5142f }, // C (-4.56966,-3.5142)
            new() { X = -3.56008f, Y = -9.12483f }, // D (-3.56008,-9.12483)
            new() { X = 3.75525f, Y = -9.88615f }, // E (3.75525,-9.88615)
            new() { X = -2.36844f, Y = -2.83563f }, // F (-2.36844,-2.83563)
            new() { X = -0.5f, Y = 2.06f } // G (-0.5,2.06)
        ];
        
        var polygon = new Polygon(points);

        foreach (var point in insidePoints)
        {
            point.Should().BeInside(polygon);
        }
        
        var distance = polygon.DistanceFrom(new() { X = -1f, Y = 3.6f });

        distance.Should().BeGreaterThan(0);
    }
}
