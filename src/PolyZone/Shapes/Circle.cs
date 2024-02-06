using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 2d circle, constructed from a center point and radius
/// </summary>
/// <param name="center">The <see cref="Vector2"/> position of the center</param>
/// <param name="radius">The circles radius starting from the center</param>
public class Circle(in Vector2 center, float radius) : ICircle
{
    public Vector2 Center { get; } = center;
    public float Radius { get; } = radius;
    
    /// <summary>
    /// A 2d circle, constructed from a center x, y and radius
    /// </summary>
    /// <param name="centerX">Center x point</param>
    /// <param name="centerY">Center y point</param>
    /// <param name="radius">The circles radius</param>
    public Circle(float centerX, float centerY, float radius) : this(new Vector2 { X = centerX, Y = centerY }, radius)
    {
        
    }

    /// <inheritdoc cref="ISpatial2dShape.Contains"/>
    public bool Contains(in Vector2 point)
    {
        // Calculate the distance from the center of the circle to the given point
        var distance = (float) Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));

        // Check if the distance is less than or equal to the radius
        return distance <= Radius;
    }

    /// <inheritdoc cref="ISpatial2dShape.DistanceFrom"/>
    public float DistanceFrom(in Vector2 point)
    {
        // Calculate the distance from the center of the circle to the given point
        var distance = (float)Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));

        // Subtract the radius to get the distance from the circumference
        return Math.Max(distance - Radius, 0);
    }
}
