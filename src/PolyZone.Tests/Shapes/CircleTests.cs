using CitizenFX.Core;
using PolyZone.Shapes;
using PolyZone.Tests.Internal;

namespace PolyZone.Tests.Shapes;

public class CircleTests
{
    [Fact]
    public void CircleA()
    {
        var circle = new Circle(-7.7f, -3.55f, 5f);

        var insidePoints = new[]
        {
            new Vector2 { X = -10.41f, Y = 0.65f },
            new Vector2 { X = -8.54f, Y = -2.25f },
        };
        
        var outsidePoints = new[]
        {
            new Vector2 { X = -7.47788f, Y = -9.43801f },
            new Vector2 { X = -12.74f, Y = -4.31f }
        };

        foreach (var insidePoint in insidePoints)
        {
            insidePoint.Should().BeInside(circle);
        }
        
        foreach (var outsidePoint in outsidePoints)
        {
            outsidePoint.Should().BeOutside(circle);
        }
    }
}
