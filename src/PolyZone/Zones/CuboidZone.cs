using CitizenFX.Core;
using PolyZone.Shapes;
using PolyZone.Zones.Interfaces;

namespace PolyZone.Zones;

/// <summary>
/// A 3d cuboid zone in the world, constructed in several different ways
/// </summary>
public class CuboidZone : Cuboid, IZone
{
    /// <summary>
    /// A 3d cuboid zone in the world, constructed from a starting vertex point with a defined length, width, and height 
    /// </summary>
    /// <inheritdoc />
    public CuboidZone(in Vector3 startingVertex, float length, float width, float height) : base(in startingVertex, length, width, height)
    {
        
    }
    
    /// <summary>
    /// A 3d cuboid zone in the world, constructed from a starting upper top left point and a ending bottom lower right point
    /// </summary>
    /// <inheritdoc />
    public CuboidZone(in Vector3 startingVertex, in Vector3 endingVertex) : base(startingVertex, endingVertex)
    {
        
    }
    
    /// <summary>
    /// A 3d cuboid zone in the world, constructed from a 8 <see cref="Vector3"/> corners in a specific pattern:
    /// <list>
    /// <item>[0] - Top - Upper Left (Start)</item>
    /// <item>[1] - Top - Upper Right</item>
    /// <item>[2] - Top - Lower Right</item>
    /// <item>[3] - Top - Lower Left</item>
    /// <item>[4] - Bottom - Upper Left</item>
    /// <item>[5] - Bottom - Upper Right</item>
    /// <item>[6] - Bottom - Lower Right</item>
    /// <item>[7] - Bottom - Lower Left (End)</item>
    /// </list>
    /// </summary>
    /// <inheritdoc />
    public CuboidZone(in Vector3[] corners) : base(corners)
    {
        
    }

    /// <inheritdoc />
    public bool Contains(in Entity entity) => Contains(entity.Position);

    /// <inheritdoc />
    public float DistanceFrom(in Entity entity) => DistanceFrom(entity.Position);
}
