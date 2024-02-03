using CitizenFX.Core;
using PolyZone.Zones.Interfaces;

namespace PolyZone.Extensions;

public static class EntityExtensions
{
    /// <inheritdoc cref="IZone.Contains"/>
    public static bool IsInside(this Entity entity, IZone zone) => zone.Contains(entity);

    /// <inheritdoc cref="IZone.DistanceFrom"/>
    public static float DistanceTo(this Entity entity, IZone zone) => zone.DistanceFrom(entity);
}
