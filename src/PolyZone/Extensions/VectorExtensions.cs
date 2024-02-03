using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Extensions;

public static class VectorExtensions
{
    /// <inheritdoc cref="ISpatial2dShape.Contains"/>
    public static bool IsInside(this Vector2 vector2, in ISpatial2dShape shape) => shape.Contains(vector2);
    
    /// <inheritdoc cref="ISpatial2dShape.DistanceFrom"/>
    public static float DistanceTo(this Vector2 vector2, in ISpatial2dShape shape) => shape.DistanceFrom(vector2);
    
    /// <inheritdoc cref="ISpatial3dShape.Contains"/>
    public static bool IsInside(this Vector3 vector2, in ISpatial3dShape shape) => shape.Contains(vector2);
}
