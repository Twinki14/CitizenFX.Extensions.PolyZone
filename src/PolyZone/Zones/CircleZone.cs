using CitizenFX.Core;
using PolyZone.Internal;
using PolyZone.Shapes;
using PolyZone.Zones.Interfaces;

namespace PolyZone.Zones;

// IZone or ISpatial2dZone>
// Contains(player / character)
// IZone + IDrawableZone?
// Maybe have a simple constructor option? x, y, radius?

public class CircleZone(in Vector2 center, float radius) : Circle(in center, radius), IZone
{
    public CircleZone(float centerX, float centerY, float radius) : this(new Vector2 { X = centerX, Y = centerY }, radius) { }

    /// <inheritdoc />
    public bool Contains(in Entity entity) => Contains(entity.Position.ToVector2());

    /// <inheritdoc />
    public float DistanceFrom(in Entity entity) => DistanceFrom(entity.Position.ToVector2());
}
