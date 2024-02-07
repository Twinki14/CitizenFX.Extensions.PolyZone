using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

public interface IShape3d
{
    /// <summary>
    /// Spatially tests a given <see cref="Vector3"/> position to determine if it lies within the shape
    /// </summary>
    /// <param name="point">A <see cref="Vector3"/> position</param>
    /// <returns>True if the <see cref="Vector3"/> position is inside the shape</returns>
    bool Contains(in Vector3 point);
    
    /// <summary>
    /// Calculates the float-accurate distance from a given <see cref="Vector3"/> position to the shape
    /// </summary>
    /// <param name="point">A <see cref="Vector3"/> position</param>
    /// <returns>The float-accurate distance from a <see cref="Vector3"/> to the shape</returns>
    float DistanceFrom(in Vector3 point);
}
