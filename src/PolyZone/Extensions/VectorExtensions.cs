using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Extensions;

public static class VectorExtensions
{
    /// <inheritdoc cref="IShape2d.Contains"/>
    public static bool IsInside(this Vector2 vector2, in IShape2d shape) => shape.Contains(vector2);
    
    /// <inheritdoc cref="IShape2d.DistanceFrom"/>
    public static float DistanceTo(this Vector2 vector2, in IShape2d shape) => shape.DistanceFrom(vector2);
    
    /// <inheritdoc cref="IShape3d.Contains"/>
    public static bool IsInside(this Vector3 vector2, in IShape3d shape) => shape.Contains(vector2);
    
    /// <inheritdoc cref="IShape3d.DistanceFrom"/>
    public static float DistanceTo(this Vector3 vector3, in IShape3d shape) => shape.DistanceFrom(vector3);
}
