using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

public interface IPolygon
{ 
    bool Contains(Vector2 point);
    float DistanceFrom(in Vector2 point);
}
