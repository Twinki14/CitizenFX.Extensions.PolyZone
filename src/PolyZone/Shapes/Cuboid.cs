using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 3d cuboid shape, constructed from an upper left 3d point 
/// </summary>
/// <param name="upperLeft">Upper left point</param>
/// <param name="length">Length of the cuboid</param>
/// <param name="width">Width of the cuboid</param>
/// <param name="height">Height of the cuboid</param>
public class Cuboid(in Vector3 upperLeft, float length, float width, float height) : ICuboid
{
    public Vector3 UpperLeft { get; } = upperLeft;
    public float Length { get; } = length;
    public float Width { get; } = width;
    public float Height { get; } = height;
    
    /// <summary>
    /// Calculated corners of the <see cref="Cuboid"/>, starts with the Upper Left
    /// </summary>
    public Vector3[] Corners { get; } =
    [
        // Upper face
        new Vector3 { X = upperLeft.X, Y = upperLeft.Y, Z = upperLeft.Z },  // Upper Left (Front)
        new Vector3 { X = upperLeft.X + length, Y = upperLeft.Y, Z = upperLeft.Z },  // Upper Right (Front)
        new Vector3 { X = upperLeft.X + length, Y = upperLeft.Y + width, Z = upperLeft.Z },  // Lower Right (Front)
        new Vector3 { X = upperLeft.X, Y = upperLeft.Y + width, Z = upperLeft.Z },  // Lower Left (Front)

        // Lower face
        new Vector3 { X = upperLeft.X, Y = upperLeft.Y, Z = upperLeft.Z + height },  // Upper Left (Back)
        new Vector3 { X = upperLeft.X + length, Y = upperLeft.Y, Z = upperLeft.Z + height },  // Upper Right (Back)
        new Vector3 { X = upperLeft.X + length, Y = upperLeft.Y + width, Z = upperLeft.Z + height },  // Lower Right (Back)
        new Vector3 { X = upperLeft.X, Y = upperLeft.Y + width, Z = upperLeft.Z + height }  // Lower Left (Back)
    ];
    
    /// <inheritdoc cref="ISpatial3dShape.Contains"/>
    public bool Contains(in Vector3 point)
    {
        // Check if the point is within the bounds defined by the corners of the cuboid
        return point.X >= UpperLeft.X && point.X <= UpperLeft.X + Length &&
               point.Y >= UpperLeft.Y && point.Y <= UpperLeft.Y + Width &&
               point.Z >= UpperLeft.Z && point.Z <= UpperLeft.Z + Height;
    }
}
