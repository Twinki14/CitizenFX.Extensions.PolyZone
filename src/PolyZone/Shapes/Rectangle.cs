using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

public class Rectangle(in Vector2 upperLeft, in Vector2 bottomRight) : IRectangle
{
    public Vector2 UpperLeft { get; } = upperLeft;
    public Vector2 BottomRight { get; } = bottomRight;
    
    /// <inheritdoc cref="ISpatial2dShape.Contains"/>
    public bool Contains(in Vector2 point)
    {
        return point.X >= UpperLeft.X && point.X <= BottomRight.X &&
               point.Y >= UpperLeft.Y && point.Y <= BottomRight.Y;
    }

    /// <inheritdoc cref="ISpatial2dShape.DistanceFrom"/>
    public float DistanceFrom(in Vector2 point)
    {
        // Calculate the distance using Euclidean distance formula
        var dx = Math.Max(Math.Max(UpperLeft.X - point.X, 0), point.X - BottomRight.X);
        var dy = Math.Max(Math.Max(UpperLeft.Y - point.Y, 0), point.Y - BottomRight.Y);

        return (float) Math.Sqrt(dx * dx + dy * dy);    
    }
}
