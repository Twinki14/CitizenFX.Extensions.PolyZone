using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Extensions;

public static class Vector3Extensions
{
    public static bool IsInside(this Vector3 vector2, in ISpatial3dShape shape) => shape.Contains(vector2);
}
