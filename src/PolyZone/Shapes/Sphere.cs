using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 3d sphere constructed from a center 3d point and a radius
/// </summary>
/// <param name="center">Center of the sphere</param>
/// <param name="radius">Radius of the sphere</param>
public class Sphere(in Vector3 center, float radius) : ISphere
{
    public Vector3 Center { get; } = center;
    public float Radius { get; } = radius;
    
    /// <inheritdoc cref="ISpatial3dShape.Contains"/>
    public bool Contains(in Vector3 point)
    {
        // Calculate the distance from the center of the sphere to the given point
        var distance = (float) Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2) + Math.Pow(point.Z - Center.Z, 2));

        // Check if the distance is less than or equal to the radius
        return distance <= Radius;
    }
}
