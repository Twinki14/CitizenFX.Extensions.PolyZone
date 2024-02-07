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
    
    /// <summary>
    /// Converts a <see cref="Vector3"/> to a <see cref="Vector2"/> by dropping the Z level
    /// </summary>
    /// <param name="vector3">The <see cref="Vector3"/> to convert from</param>
    /// <returns>A <see cref="Vector2"/> instantiated from a <see cref="Vector3"/></returns>
    public static Vector2 AsVector2(in this Vector3 vector3) => new() { X = vector3.X, Y = vector3.Y };
    
    /// <summary>
    /// Converts a <see cref="Vector2"/> to a <see cref="Vector3"/> by adding a Z level
    /// </summary>
    /// <param name="vector2">The <see cref="Vector2"/> to convert from</param>
    /// <param name="z">The Z level to add to the new <see cref="Vector3"/>, defaults to max float value</param>
    /// <returns>A <see cref="Vector3"/> instantiated from a <see cref="Vector2"/></returns>
    public static Vector3 AsVector3(in this Vector2 vector2, float z = 2700) => new() { X = vector2.X, Y = vector2.Y, Z = z };
}
