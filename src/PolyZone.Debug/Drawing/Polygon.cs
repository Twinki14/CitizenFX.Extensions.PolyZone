using System.Drawing;
using CitizenFX.Core;
using PolyZone.Extensions;
using PolyZone.Zones;

namespace PolyZone.Debug.Drawing;

public static class PolygonExtensions
{
    public static void Draw(this PolygonZone polygon, in Color color, in Color lineColor)
    {
        var minZ = polygon.MinZ;
        var maxZ = polygon.MaxZ;

        var drawCrisscross = maxZ - minZ <= 80;
        
        for (var i = 0; i < polygon.Points.Count - 1; i++)
        {
            var currentPoint = polygon.Points[i];
            var nextPoint = polygon.Points[i + 1];

            var currentMinZ = currentPoint.AsVector3(minZ);
            var currentMaxZ = currentPoint.AsVector3(maxZ);

            var nextMinZ = nextPoint.AsVector3(minZ);
            var nextMaxZ = nextPoint.AsVector3(maxZ);
            
            World.DrawPoly(currentMaxZ, nextMaxZ, nextMinZ, color);
            World.DrawPoly(nextMinZ, currentMinZ, currentMaxZ, color);
            
            World.DrawPoly(nextMinZ, nextMaxZ, currentMaxZ, color);
            World.DrawPoly(currentMaxZ, currentMinZ, nextMinZ, color);
            
            // Edge / Pillar
            World.DrawLine(currentMinZ, currentMaxZ, lineColor);
            
            // Edges
            World.DrawLine(currentMaxZ, nextMaxZ, lineColor);
            World.DrawLine(currentMinZ, nextMinZ, lineColor);
            
            // Crisscross
            if (drawCrisscross)
            {
                World.DrawLine(currentMaxZ, nextMinZ, lineColor);
                World.DrawLine(nextMaxZ, currentMinZ, lineColor);
            }
        }
        
        var start = polygon.Points.ElementAt(polygon.Points.Count - 1);
        var end = polygon.Points.ElementAt(0);
        
        var startMinZ = start.AsVector3(minZ);
        var startMaxZ = start.AsVector3(maxZ);

        var endMinZ = end.AsVector3(minZ);
        var endMaxZ = end.AsVector3(maxZ);
        
        World.DrawPoly(endMaxZ, endMinZ, startMinZ, color);
        World.DrawPoly(startMaxZ, startMinZ, endMaxZ, color);
        
        World.DrawPoly(startMinZ, endMinZ, endMaxZ, color);
        World.DrawPoly(endMaxZ, startMinZ, startMaxZ, color);
        
        // Edge / Pillar
        World.DrawLine(startMaxZ, startMinZ, lineColor);
            
        // Edges
        World.DrawLine(startMaxZ, endMaxZ, lineColor);
        World.DrawLine(startMinZ, endMinZ, lineColor);
        
        // Crisscross
        if (drawCrisscross)
        {
            World.DrawLine(startMaxZ, endMinZ, lineColor);
            World.DrawLine(endMaxZ, startMinZ, lineColor);
        }
    }

    public static void DrawDebug(this PolygonZone polygonZone, Entity? comparativeEntity = null)
    {
        comparativeEntity ??= Game.Player.Character;

        const int alpha = 45;
        var insideColor = Color.FromArgb(alpha, 0, 255, 0);
        var outsideColor = Color.FromArgb(alpha, 255, 0, 0);
        var lineColor = Color.FromArgb(175, 255, 255, 255);

        var isInside = comparativeEntity.IsInside(polygonZone);
        var color = isInside ? insideColor : outsideColor;
        
        polygonZone.Draw(color, lineColor);
        
        Internal.Drawing.Draw3dText(polygonZone.Points.First().AsVector3(35), $"Distance: { comparativeEntity.DistanceTo(polygonZone) }", Color.FromArgb(0, 255, 0), 20.0f);
    }
}
