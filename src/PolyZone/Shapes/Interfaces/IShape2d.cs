using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

public interface IShape2d
{
    /// <summary>
    /// Spatially tests a given <see cref="Vector2"/> position to determine if it lies within the shape
    /// </summary>
    /// <param name="point">A <see cref="Vector2"/> position</param>
    /// <returns>True if the <see cref="Vector2"/> position is inside the shape</returns>
    bool Contains(in Vector2 point);
    
    /// <summary>
    /// Calculates the float-accurate distance from a given <see cref="Vector2"/> position to the shape
    /// </summary>
    /// <param name="point">A <see cref="Vector2"/> position</param>
    /// <returns>The float-accurate distance from a <see cref="Vector2"/> to the shape</returns>
    float DistanceFrom(in Vector2 point);
}
