using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 3d cuboid shape
/// </summary>
public class Cuboid : ICuboid
{
    public float Length { get; }
    public float Width { get; }
    public float Height { get; }
    
    public float MinX { get; }
    public float MinY { get; }
    public float MinZ { get; }
    
    public float MaxX { get; }
    public float MaxY { get; }
    public float MaxZ { get; }
    
    public Vector3 Center { get; }
    
    /// <summary>
    /// Calculated corners of the <see cref="Cuboid"/>, starts with the starting vertex and ends with the ending vertex
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
    public Vector3[] Corners { get; }
    
    /// <summary>
    /// A 3d cuboid shape, constructed from a starting vertex point with a defined length, width, and height 
    /// </summary>
    /// <param name="startingVertex">Starting vertex point</param>
    /// <param name="length">Length of the cuboid</param>
    /// <param name="width">Width of the cuboid</param>
    /// <param name="height">Height of the cuboid</param>
    public Cuboid(in Vector3 startingVertex, float length, float width, float height)
    {
        Length = length;
        Width = width;
        Height = height;
        
        Corners = 
        [
            // Upper face
            startingVertex,                                                                                     // Corner 0: Upper Left
            new Vector3 { X = startingVertex.X + length, Y = startingVertex.Y, Z = startingVertex.Z },          // Corner 1: Upper Right
            new Vector3 { X = startingVertex.X + length, Y = startingVertex.Y + width, Z = startingVertex.Z },  // Corner 2: Lower Right
            new Vector3 { X = startingVertex.X, Y = startingVertex.Y + width, Z = startingVertex.Z },           // Corner 3: Lower Left

            // Lower face
            new Vector3 { X = startingVertex.X, Y = startingVertex.Y, Z = startingVertex.Z + height },                      // Corner 4: Upper Left
            new Vector3 { X = startingVertex.X + length, Y = startingVertex.Y, Z = startingVertex.Z + height },             // Corner 5: Upper Right
            new Vector3 { X = startingVertex.X + length, Y = startingVertex.Y + width, Z = startingVertex.Z + height },     // Corner 6: Lower Right
            new Vector3 { X = startingVertex.X, Y = startingVertex.Y + width, Z = startingVertex.Z + height }               // Corner 7: Lower Left
        ];
        
        MinX = Corners.Min(c => c.X);
        MinY = Corners.Min(c => c.Y);
        MinZ = Corners.Min(c => c.Z);
        
        MaxX = Corners.Max(c => c.X);
        MaxY = Corners.Max(c => c.Y);
        MaxZ = Corners.Max(c => c.Z);

        Center = new Vector3 { X = (MinX + MaxX) / 2, Y = (MinY + MaxY) / 2, Z = (MinZ + MaxZ) / 2 };
    }
    
    /// <summary>
    /// A 3d cuboid shape, constructed from a starting point and a ending point
    /// </summary>
    /// <param name="startingVertex">Starting top-face upper-left-vertex point</param>
    /// <param name="endingVertex">Ending bottom-face lower-right-vertex point</param>
    public Cuboid(in Vector3 startingVertex, in Vector3 endingVertex)
    {
        Length = Math.Abs(endingVertex.X - startingVertex.X);
        Width = Math.Abs(endingVertex.Y - startingVertex.Y);
        Height = Math.Abs(endingVertex.Z - startingVertex.Z);
        
        MinX = Math.Min(startingVertex.X, endingVertex.X);
        MinY = Math.Min(startingVertex.Y, endingVertex.Y);
        MinZ = Math.Min(startingVertex.Z, endingVertex.Z);
        
        Corners =
        [
            // Upper face
            new Vector3 { X = MinX, Y = MinY, Z = MinZ },                           // Corner 0: Upper Left
            new Vector3 { X = MinX + Length, Y = MinY, Z = MinZ },                  // Corner 1: Upper Right
            new Vector3 { X = MinX + Length, Y = MinY + Width, Z = MinZ },          // Corner 2: Lower Right
            new Vector3 { X = MinX, Y = MinY + Width, Z = MinZ },                   // Corner 3: Lower Left

            // Lower face
            new Vector3 { X = MinX, Y = MinY, Z = MinZ + Height },                  // Corner 4: Upper Left
            new Vector3 { X = MinX + Length, Y = MinY, Z = MinZ + Height },         // Corner 5: Upper Right
            new Vector3 { X = MinX + Length, Y = MinY + Width, Z = MinZ + Height }, // Corner 6: Lower Right
            new Vector3 { X = MinX, Y = MinY + Width, Z = MinZ + Height }           // Corner 7: Lower Left
        ];
        
        MaxX = Corners.Max(c => c.X);
        MaxY = Corners.Max(c => c.Y);
        MaxZ = Corners.Max(c => c.Z);
        
        Center = new Vector3 { X = (MinX + MaxX) / 2, Y = (MinY + MaxY) / 2, Z = (MinZ + MaxZ) / 2 };
    }
    
    /// <summary>
    /// A 3d cuboid shape, constructed from a 8 <see cref="Vector3"/> corners in a specific pattern:
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
    /// <param name="corners">8 <see cref="Vector3"/> corners, in a very specific pattern</param>
    /// <exception cref="InvalidOperationException">When the array isn't what's expected</exception>
    public Cuboid(in Vector3[] corners)
    {
        if (corners.Length != 8)
        {
            throw new InvalidOperationException($"Constructing {nameof(Cuboid)}'s with {nameof(corners)} requires a {nameof(Vector3)} array length of exactly 8");
        }

        var startingVertex = corners.First();
        var endingVertex = corners.Last();
        
        Length = Math.Abs(endingVertex.X - startingVertex.X);
        Width = Math.Abs(endingVertex.Y - startingVertex.Y);
        Height = Math.Abs(endingVertex.Z - startingVertex.Z);

        Corners = corners;
        
        MinX = Corners.Min(c => c.X);
        MinY = Corners.Min(c => c.Y);
        MinZ = Corners.Min(c => c.Z);
        
        MaxX = Corners.Max(c => c.X);
        MaxY = Corners.Max(c => c.Y);
        MaxZ = Corners.Max(c => c.Z);
        
        Center = new Vector3 { X = (MinX + MaxX) / 2, Y = (MinY + MaxY) / 2, Z = (MinZ + MaxZ) / 2 };
    }
    
    /// <inheritdoc />
    public bool Contains(in Vector3 point)
    {
        // Check if the point is within the bounds defined by the corners of the cuboid
        return 
            point.X >= MinX && point.X <= MaxX &&
            point.Y >= MinY && point.Y <= MaxY && 
            point.Z >= MinZ && point.Z <= MaxZ;
    }
    
    /// <inheritdoc />
    public float DistanceFrom(in Vector3 point)
    {
        if (Contains(point))
        {
            return 0f;
        }
        

        var distanceX = Math.Max(0, Math.Max(MinX - point.X, point.X - MaxX));
        var distanceY = Math.Max(0, Math.Max(MinY - point.Y, point.Y - MaxY));
        var distanceZ = Math.Max(0, Math.Max(MinZ - point.Z, point.Z - MaxZ));

        // Calculate Euclidean distance
        return (float) Math.Sqrt(distanceX * distanceX + distanceY * distanceY + distanceZ * distanceZ);
    }
}
