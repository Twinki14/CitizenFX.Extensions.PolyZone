using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

public interface ISpatial2dShape
{
    bool Contains(Vector2 point);
    float DistanceFrom(in Vector2 point);
}
