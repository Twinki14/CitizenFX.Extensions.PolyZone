using CitizenFX.Core;
using PolyZone.Internal;
using PolyZone.Shapes;
using PolyZone.Zones.Interfaces;

namespace PolyZone.Zones;

/// <summary>
/// A 2d circle zone in the world
/// </summary>
/// <param name="center">The <see cref="Vector2"/> position of the center</param>
/// <param name="radius">The circle zones radius starting from the center</param>
public class CircleZone(in Vector2 center, float radius) : Circle(in center, radius), IZone
{
    /// <summary>
    /// A 2d circle, constructed from a center x, y and radius
    /// </summary>
    /// <param name="centerX">Center x point</param>
    /// <param name="centerY">Center y point</param>
    /// <param name="radius">The circles radius starting from the center</param>
    public CircleZone(float centerX, float centerY, float radius) : this(new Vector2 { X = centerX, Y = centerY }, radius)
    {
        
    }

    /// <inheritdoc />
    public bool Contains(in Entity entity) => Contains(entity.Position.ToVector2());

    /// <inheritdoc />
    public float DistanceFrom(in Entity entity) => DistanceFrom(entity.Position.ToVector2());
}
