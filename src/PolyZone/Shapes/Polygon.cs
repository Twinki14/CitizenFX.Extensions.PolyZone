using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

/// <summary>
/// A 2d polygonal shape, made up of points in a sequential order
/// </summary>
/// <param name="points">A list of <see cref="Vector2"/> in sequential order, to make-up a polygonal shape</param>
public class Polygon(IReadOnlyList<Vector2> points) : IPolygon
{
    public IReadOnlyList<Vector2> Points { get; } = points;
    
    /// <inheritdoc cref="ISpatial2dShape.Contains"/>
    public bool Contains(in Vector2 point) => Contains(point, Points);

    /// <inheritdoc cref="ISpatial2dShape.DistanceFrom"/>
    public float DistanceFrom(in Vector2 point) => DistanceFrom(point, Points);

    // https://web.archive.org/web/20210225074947/http://geomalgorithms.com/a03-_inclusion.html
    private static bool Contains(in Vector2 point, IReadOnlyList<Vector2> polygon)
    {
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

    private static float DistanceFrom(in Vector2 point, in IReadOnlyList<Vector2> polygon)
    {
        var minDistance = float.MaxValue;

        for (var i = 0; i < polygon.Count; i++)
        {
            var nextIndex = (i + 1) % polygon.Count;

            // Find the closest point on the line segment
            var closestPoint = ClosestPointOnLineSegment(point, polygon[i], polygon[nextIndex]);

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
    
    private static Vector2 ClosestPointOnLineSegment(Vector2 p, Vector2 a, Vector2 b)
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
    
    private static float IsLeft(Vector2 a, Vector2 b, Vector2 c)
    {
        return (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
    }
    
    private static float Distance(in Vector2 value1, in Vector2 value2)
    {
        var x = value1.X - value2.X;
        var y = value1.Y - value2.Y;

        return (float) Math.Sqrt(x * x + y * y);
    }
    
    private static float Dot(Vector2 left, Vector2 right)
    {
        return (left.X * right.X) + (left.Y * right.Y);
    }
}
