using CitizenFX.Core;
using PolyZone.Extensions;
using PolyZone.Shapes;
using PolyZone.Zones.Interfaces;

namespace PolyZone.Zones;

/// <summary>
/// A 2d zone constructed with a list of <see cref="Vector2"/> points and optionally a MinZ and MaxZ of the overall <see cref="PolygonZone"/>
/// </summary>
/// <param name="points">List of <see cref="Vector2"/> points to construct the underlying <see cref="Polygon"/> with</param>
/// <param name="minZ">MinZ of the overall <see cref="PolygonZone"/>, defaults to <see cref="Map.MinZ"/></param>
/// <param name="maxZ">MaxZ of the overall <see cref="PolygonZone"/>, defaults to <see cref="Map.MaxZ"/></param>
public class PolygonZone(in IReadOnlyList<Vector2> points, float minZ = Map.MinZ, float maxZ = Map.MaxZ) : Polygon(points, minZ: minZ, maxZ), IZone
{
    /// <inheritdoc />
    public bool Contains(in Entity entity) => base.Contains(entity.Position);

    /// <inheritdoc />
    /// <remarks>Does NOT account for the Z level of the <see cref="Entity"/></remarks>
    public float DistanceFrom(in Entity entity) => base.DistanceFrom(entity.Position.AsVector2());
}
