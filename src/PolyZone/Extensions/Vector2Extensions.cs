using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Extensions;

public static class Vector2Extensions
{
    public static bool IsInside(this Vector2 vector2, in ISpatial2dShape shape) => shape.Contains(vector2);
    public static float DistanceTo(this Vector2 vector2, in ISpatial2dShape shape) => shape.DistanceFrom(vector2);
}
