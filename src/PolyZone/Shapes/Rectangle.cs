using CitizenFX.Core;
using PolyZone.Extensions;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 2d rectangular shape, constructed from an upperLeft and bottomRight point
/// </summary>
/// <param name="upperLeft">Upper left point of the rectangle</param>
/// <param name="bottomRight">Bottom right point of the rectangle</param>
/// <param name="minZ">MinZ of the overall <see cref="Rectangle"/>, defaults to <see cref="Map.MinZ"/></param>
/// <param name="maxZ">MaxZ of the overall <see cref="Rectangle"/>, defaults to <see cref="Map.MaxZ"/></param>
public class Rectangle(in Vector2 upperLeft, in Vector2 bottomRight, float minZ = Map.MinZ, float maxZ = Map.MaxZ) : IRectangle
{
    public Vector2 UpperLeft { get; } = upperLeft;
    public Vector2 BottomRight { get; } = bottomRight;
    
    public float MinZ { get; } = minZ;
    public float MaxZ { get; } = maxZ;

    /// <inheritdoc />
    public bool Contains(in Vector2 point)
    {
        return point.X >= UpperLeft.X && point.X <= BottomRight.X &&
               point.Y >= UpperLeft.Y && point.Y <= BottomRight.Y;
    }
    
    /// <inheritdoc />
    public bool Contains(in Vector3 point)
    {
        return point.X >= UpperLeft.X && point.X <= BottomRight.X &&
               point.Y >= UpperLeft.Y && point.Y <= BottomRight.Y &&
               point.Z >= MinZ && point.Z <= MaxZ;
    }

    /// <inheritdoc />
    public float DistanceFrom(in Vector2 point)
    {
        // Calculate the distance using Euclidean distance formula
        var dx = Math.Max(Math.Max(UpperLeft.X - point.X, 0), point.X - BottomRight.X);
        var dy = Math.Max(Math.Max(UpperLeft.Y - point.Y, 0), point.Y - BottomRight.Y);

        return (float) Math.Sqrt(dx * dx + dy * dy);
    }
}
