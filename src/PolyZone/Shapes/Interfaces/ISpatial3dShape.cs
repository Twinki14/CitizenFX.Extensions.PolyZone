using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

public interface ISpatial3dShape
{
    /// <summary>
    /// Spatially tests a given <see cref="Vector2"/> to determine if it lies within the shape
    /// </summary>
    /// <param name="point"><see cref="Vector2"/>, otherwise known as a 2d position</param>
    /// <returns>True if the <see cref="Vector2"/> is inside the shape</returns>
    bool Contains(in Vector3 point);
}
