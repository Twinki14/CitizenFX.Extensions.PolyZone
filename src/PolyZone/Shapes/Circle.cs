using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

public class Circle(float x, float y, float radius) : ICircle
{
    public Circle(in Vector2 center, float radius) : this(center.X, center.Y, radius)
    {
        
    }
    
    public bool Contains(in Vector2 point)
    {
        // Calculate the distance from the center of the circle to the given point
        var distance = (float) Math.Sqrt(Math.Pow(point.X - x, 2) + Math.Pow(point.Y - y, 2));

        // Check if the distance is less than or equal to the radius
        return distance <= radius;
    }

    public float DistanceFrom(in Vector2 point)
    {
        throw new NotImplementedException();
    }
}
