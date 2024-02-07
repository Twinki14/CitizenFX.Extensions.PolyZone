using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Zones.Interfaces;

/// <summary>
/// A zone or area in the world represented by an underlying <see cref="IShape2d"/> or <see cref="IShape3d"/>
/// </summary>
public interface IZone
{
    /// <summary>
    /// Spatially tests a given <see cref="Entity"/>'s position to determine if it lies within the <see cref="IZone"/>
    /// </summary>
    /// <param name="entity">A entity, like a vehicle, object, prop, or character</param>
    /// <returns>True if the <see cref="Entity"/>'s position lies within the <see cref="IZone"/></returns>
    public bool Contains(in Entity entity);
    
    /// <summary>
    /// Calculates the float-accurate distance from a <see cref="Entity"/>'s position to the <see cref="IZone"/>
    /// </summary>
    /// <param name="entity">A entity, like a vehicle, object, prop, or character</param>
    /// <returns>The float-accurate distance from a <see cref="Entity"/>'s position to the <see cref="IZone"/></returns>
    public float DistanceFrom(in Entity entity);
}
