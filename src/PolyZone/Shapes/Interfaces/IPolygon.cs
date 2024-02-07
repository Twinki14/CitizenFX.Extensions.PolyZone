using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

/// <summary>
/// A 2d polygon
/// </summary>
public interface IPolygon : IShape2d
{
    public IReadOnlyList<Vector2> Points { get; }
    
    /// <summary>
    /// Spatially tests a given <see cref="Vector3"/> position to determine if it lies within the shape
    /// </summary>
    /// <param name="point">A <see cref="Vector3"/> position</param>
    /// <returns>True if the <see cref="Vector3"/> position is inside the shape</returns>
    bool Contains(in Vector3 point);
}
