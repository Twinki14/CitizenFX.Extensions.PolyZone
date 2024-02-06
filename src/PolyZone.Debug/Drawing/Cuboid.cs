using System.Drawing;
using CitizenFX.Core;
using PolyZone.Extensions;
using PolyZone.Zones;

namespace PolyZone.Debug.Drawing;

public static class CuboidDrawingExtensions
{
    public static void Draw(this CuboidZone cuboidZone, in Color color, in Color lineColor, bool drawEdgesInside = false)
    {
        if (drawEdgesInside)
        {
            // Draw edges from the perspective of the inside
            World.DrawPoly(cuboidZone.Corners[0], cuboidZone.Corners[1], cuboidZone.Corners[2], color); // Top face
            World.DrawPoly(cuboidZone.Corners[2], cuboidZone.Corners[3], cuboidZone.Corners[0], color);

            World.DrawPoly(cuboidZone.Corners[7], cuboidZone.Corners[6], cuboidZone.Corners[5], color); // Bottom face
            World.DrawPoly(cuboidZone.Corners[5], cuboidZone.Corners[4], cuboidZone.Corners[7], color);

            World.DrawPoly(cuboidZone.Corners[5], cuboidZone.Corners[1], cuboidZone.Corners[0], color); // Side edges
            World.DrawPoly(cuboidZone.Corners[4], cuboidZone.Corners[5], cuboidZone.Corners[0], color);
            World.DrawPoly(cuboidZone.Corners[6], cuboidZone.Corners[2], cuboidZone.Corners[1], color);
            World.DrawPoly(cuboidZone.Corners[5], cuboidZone.Corners[6], cuboidZone.Corners[1], color);
            World.DrawPoly(cuboidZone.Corners[7], cuboidZone.Corners[3], cuboidZone.Corners[2], color);
            World.DrawPoly(cuboidZone.Corners[6], cuboidZone.Corners[7], cuboidZone.Corners[2], color);
            World.DrawPoly(cuboidZone.Corners[4], cuboidZone.Corners[0], cuboidZone.Corners[3], color);
            World.DrawPoly(cuboidZone.Corners[7], cuboidZone.Corners[4], cuboidZone.Corners[3], color);
        }
        else
        {
            // Draw edges from the perspective of the outside
            World.DrawPoly(cuboidZone.Corners[2], cuboidZone.Corners[1], cuboidZone.Corners[0], color); // Top face
            World.DrawPoly(cuboidZone.Corners[0], cuboidZone.Corners[3], cuboidZone.Corners[2], color);
        
            World.DrawPoly(cuboidZone.Corners[4], cuboidZone.Corners[5], cuboidZone.Corners[6], color); // Bottom face
            World.DrawPoly(cuboidZone.Corners[4], cuboidZone.Corners[6], cuboidZone.Corners[7], color);

            World.DrawPoly(cuboidZone.Corners[0], cuboidZone.Corners[1], cuboidZone.Corners[5], color); // Side edges
            World.DrawPoly(cuboidZone.Corners[0], cuboidZone.Corners[5], cuboidZone.Corners[4], color);
            World.DrawPoly(cuboidZone.Corners[1], cuboidZone.Corners[2], cuboidZone.Corners[6], color);
            World.DrawPoly(cuboidZone.Corners[1], cuboidZone.Corners[6], cuboidZone.Corners[5], color);
            World.DrawPoly(cuboidZone.Corners[2], cuboidZone.Corners[3], cuboidZone.Corners[7], color);
            World.DrawPoly(cuboidZone.Corners[2], cuboidZone.Corners[7], cuboidZone.Corners[6], color);
            World.DrawPoly(cuboidZone.Corners[3], cuboidZone.Corners[0], cuboidZone.Corners[4], color);
            World.DrawPoly(cuboidZone.Corners[3], cuboidZone.Corners[4], cuboidZone.Corners[7], color);
        }
        
        // Draw lines for the upper face
        for (var i = 0; i < 4; i++)
        {
            World.DrawLine(cuboidZone.Corners[i], cuboidZone.Corners[(i + 1) % 4], lineColor);
        }

        // Draw lines connecting upper and lower faces
        for (var i = 0; i < 4; i++)
        {
            World.DrawLine(cuboidZone.Corners[i], cuboidZone.Corners[i + 4], lineColor);
        }

        // Draw lines for the lower face
        for (var i = 4; i < 8; i++)
        {
            World.DrawLine(cuboidZone.Corners[i], cuboidZone.Corners[(i + 1) % 4 + 4], lineColor);
        }
    }
    
    public static void DrawDebug(this CuboidZone cuboidZone, Entity? comparativeEntity = null)
    {
        comparativeEntity ??= Game.Player.Character;

        const int alpha = 45;
        var insideColor = Color.FromArgb(alpha, 0, 255, 0);
        var outsideColor = Color.FromArgb(alpha, 255, 0, 0);
        var lineColor = Color.FromArgb(175, 255, 255, 255);

        var isInside = comparativeEntity.IsInside(cuboidZone);
        var color = isInside ? insideColor : outsideColor;
        
        cuboidZone.Draw(color, lineColor, isInside);
        
        Internal.Drawing.Draw3dText(cuboidZone.Center, $"Distance: { comparativeEntity.DistanceTo(cuboidZone) }", color, 5.0f);
    }
}
