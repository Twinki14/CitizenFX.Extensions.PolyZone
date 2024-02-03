using CitizenFX.Core;

namespace PolyZone.Internal;

internal static class VectorExtensions
{
    internal static Vector2 ToVector2(in this Vector3 vector3) => new() { X = vector3.X, Y = vector3.Y };
}
