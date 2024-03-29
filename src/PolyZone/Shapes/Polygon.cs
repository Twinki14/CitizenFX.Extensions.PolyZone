﻿using CitizenFX.Core;
using PolyZone.Extensions;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 2d polygonal shape, made up of points in a sequential order
/// </summary>
/// <param name="points">A list of <see cref="Vector2"/> in sequential order, to make-up a polygonal shape</param>
/// <param name="minZ">MinZ of the overall <see cref="Polygon"/>, defaults to <see cref="Map.MinZ"/></param>
/// <param name="maxZ">MaxZ of the overall <see cref="Polygon"/>, defaults to <see cref="Map.MaxZ"/></param>
public class Polygon(in IReadOnlyList<Vector2> points, float minZ = -Map.MinZ, float maxZ = Map.MaxZ) : IPolygon
{
    private Rectangle? _boundingBox;

    public IReadOnlyList<Vector2> Points { get; } = points;

    public float MinZ { get; } = minZ;
    public float MaxZ { get; } = maxZ;

    /// <inheritdoc />
    public bool Contains(in Vector2 point)
    {
        if (_boundingBox is null)
        {
            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;

            foreach (var vertex in Points)
            {
                minX = Math.Min(minX, vertex.X);
                minY = Math.Min(minY, vertex.Y);
                maxX = Math.Max(maxX, vertex.X);
                maxY = Math.Max(maxY, vertex.Y);
            }

            _boundingBox = new Rectangle(new Vector2 { X = minX, Y = minY }, new Vector2 { X = maxX, Y = maxY }, MinZ, MaxZ);
        }
        
        return Contains(point, Points, _boundingBox);
    }

    /// <inheritdoc />
    public bool Contains(in Vector3 point) => point.Z >= MinZ && point.Z <= MaxZ && Contains(point.AsVector2());

    /// <inheritdoc />
    public float DistanceFrom(in Vector2 point)
    {
        if (Contains(point))
        {
            return 0;
        }
        
        var minDistance = float.MaxValue;

        for (var i = 0; i < Points.Count; i++)
        {
            var nextIndex = (i + 1) % Points.Count;

            // Find the closest point on the line segment
            var closestPoint = ClosestPointOnLineSegment(point, Points[i], Points[nextIndex]);

            // Calculate the distance between the given point and the closest point on the line segment
            var distance = Distance(point, closestPoint);

            // Update the minimum distance if the current distance is smaller
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        return minDistance;
    }

    // https://web.archive.org/web/20210225074947/http://geomalgorithms.com/a03-_inclusion.html
    private static bool Contains(in Vector2 point, IReadOnlyList<Vector2> polygon, in Rectangle boundingBox)
    {
        if (!boundingBox.Contains(point))
        {
            return false;
        }
        
        var windingNumber = 0;
        
        // Loop through all edges of the polygon (considering the last edge connecting the last and first vertices)
        for (var i = 0; i < polygon.Count; i++)
        {
            // Edge from polygon[i] to polygon[(i + 1) % polygon.Count]
            var nextIndex = (i + 1) % polygon.Count;

            // Start polygon[i].Y <= point.Y
            if (polygon[i].Y <= point.Y)
            {
                // An upward crossing
                if (polygon[nextIndex].Y > point.Y && IsLeft(polygon[i], polygon[nextIndex], point) > 0)
                {
                    // P left of edge
                    ++windingNumber;
                }
            }
            // Start polygon[i].Y > point.Y (no test needed)
            else if (polygon[nextIndex].Y <= point.Y && IsLeft(polygon[i], polygon[nextIndex], point) < 0)
            {
                // A downward crossing, P right of edge
                --windingNumber;
            }
        }

        return windingNumber != 0;
    }
    
    private static Vector2 ClosestPointOnLineSegment(in Vector2 p, in Vector2 a, in Vector2 b)
    {
        var ap = new Vector2 { X = p.X - a.X, Y = p.Y - a.Y };
        var ab = new Vector2 { X = b.X - a.X, Y = b.Y - a.Y };

        var t = Dot(ap, ab) / Dot(ab, ab);

        return t switch
        {
            < 0.0f => a,
            > 1.0f => b,
            _ => new Vector2 { X = a.X + t * ab.X, Y = a.Y + t * ab.Y }
        };
    }
    
    private static float IsLeft(in Vector2 a, in Vector2 b, in Vector2 c)
    {
        return (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
    }
    
    private static float Distance(in Vector2 value1, in Vector2 value2)
    {
        var x = value1.X - value2.X;
        var y = value1.Y - value2.Y;

        return (float) Math.Sqrt(x * x + y * y);
    }
    
    private static float Dot(in Vector2 left, in Vector2 right)
    {
        return (left.X * right.X) + (left.Y * right.Y);
    }
}
