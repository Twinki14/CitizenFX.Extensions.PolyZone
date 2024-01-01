using CitizenFX.Core;

namespace PolyZone.Shapes.Interfaces;

public interface IPolygon
{ 
    bool IsInside(Vector2 point);
    float DistanceTo(in Vector2 point);
}
