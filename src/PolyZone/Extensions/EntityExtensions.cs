using CitizenFX.Core;
using CitizenFX.Core.Native;
using PolyZone.Zones.Interfaces;

namespace PolyZone.Extensions;

public static class EntityExtensions
{
    /// <inheritdoc cref="IZone.Contains"/>
    public static bool IsInside(this Entity entity, IZone zone) => zone.Contains(entity);

    /// <inheritdoc cref="IZone.DistanceFrom"/>
    public static float DistanceTo(this Entity entity, IZone zone) => zone.DistanceFrom(entity);

    /// <summary>
    /// Returns a array of <see cref="Vector3"/> with a size 8, representing the bounding box of the entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static Vector3[] GetBoundingBox(this Entity entity)
    {
        var handle = entity.Handle;
        
        var min = Vector3.Zero;
        var max = Vector3.Zero;
        
        API.GetModelDimensions((uint) API.GetEntityModel(handle), ref min, ref max);
        
        const float pad = 0.001f;

        return
        [
            // Top
            API.GetOffsetFromEntityInWorldCoords(handle, min.X - pad, min.Y - pad, max.Z + pad),   // [0] - Top - Upper Left (Start)
            API.GetOffsetFromEntityInWorldCoords(handle, max.X + pad, min.Y - pad, max.Z + pad),   // [1] - Top - Upper Right
            API.GetOffsetFromEntityInWorldCoords(handle, max.X + pad, max.Y + pad, max.Z + pad),   // [2] - Top - Lower Right
            API.GetOffsetFromEntityInWorldCoords(handle, min.X - pad, max.Y + pad, max.Z + pad),   // [3] - Top - Lower Left

            // Bottom
            API.GetOffsetFromEntityInWorldCoords(handle, min.X - pad, min.Y - pad, min.Z - pad),   // [4] - Bottom - Upper Left
            API.GetOffsetFromEntityInWorldCoords(handle, max.X + pad, min.Y - pad, min.Z - pad),   // [5] - Bottom - Upper Right
            API.GetOffsetFromEntityInWorldCoords(handle, max.X + pad, max.Y + pad, min.Z - pad),   // [6] - Bottom - Lower Right
            API.GetOffsetFromEntityInWorldCoords(handle, min.X - pad, max.Y + pad, min.Z - pad)    // [7] - Bottom - Lower Left (End)
        ];
    }
}
