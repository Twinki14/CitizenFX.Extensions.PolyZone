using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Debug.Drawing;

public static class PolygonExtensions
{
    public static IPolygon Draw(this IPolygon polygon, Color color)
    {
        /*for (var i = 0; i < polygon.Points.Count - 1; i++)
        {
            var startPoint = polygon.Points[i];
            var endPoint = polygon.Points[i + 1];

            DrawPoly(startPoint, endPoint, r, g, b, a);
        }*/
        
        var start = polygon.Points.ElementAt(polygon.Points.Count - 1);
        var end = polygon.Points.ElementAt(0);
        
        var bottomLeft = new Vector3(start.X, start.Y, 200);
        var topLeft = new Vector3(start.X, start.Y, 400);
        var bottomRight = new Vector3(end.X, end.Y, 200);
        var topRight = new Vector3(end.X, end.Y, 400);
        
        API.DrawPoly(
            bottomLeft.X, bottomLeft.Y, bottomLeft.Z,
            topLeft.X, topLeft.Y, topLeft.Z,
            bottomRight.X, bottomRight.Y, bottomLeft.Z,
            color.R, color.G, color.B, color.A);
        
        API.DrawPoly(
            topLeft.X, topLeft.Y, topLeft.Z,
            topRight.X, topRight.Y, topRight.Z,
            bottomRight.X, bottomRight.Y, bottomLeft.Z,
            color.R, color.G, color.B, color.A);
                
        API.DrawPoly(
            bottomRight.X, bottomRight.Y, bottomRight.Z,
            topRight.X, topRight.Y, topRight.Z,
            topLeft.X, topLeft.Y, topLeft.Z,
            color.R, color.G, color.B, color.A);
        
        API.DrawPoly(
            bottomRight.X, bottomRight.Y, bottomRight.Z,
            topLeft.X, topLeft.Y, topLeft.Z,
            bottomLeft.X, bottomLeft.Y, bottomLeft.Z,
            color.R, color.G, color.B, color.A);
        
        
        return polygon;
    }
}
