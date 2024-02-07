using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

/// <summary>
/// A 2d polygon
/// </summary>
public interface IPolygon : IShape2d
{
    public IReadOnlyList<Vector2> Points { get; }
}
