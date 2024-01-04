using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

/// <summary>
/// Generic interface for spatial 2d shapes
/// </summary>
public interface ISpatial2dShape
{
    /// <summary>
    /// Spatially tests a given <see cref="Vector2"/> to determine if it lies within the shape
    /// </summary>
    /// <param name="point"><see cref="Vector2"/>, otherwise known as a 2d position</param>
    /// <returns>True if the <see cref="Vector2"/> is inside the shape</returns>
    bool Contains(Vector2 point);
    
    /// <summary>
    /// Calculates the float-accurate distance from a <see cref="Vector2"/> to the shape
    /// </summary>
    /// <param name="point"><see cref="Vector2"/>, otherwise known as a 2d position</param>
    /// <returns>The float-accurate distance from a <see cref="Vector2"/> to the shape</returns>
    float DistanceFrom(in Vector2 point);
}
