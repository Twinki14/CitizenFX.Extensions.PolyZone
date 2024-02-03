using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using PolyZone.Extensions;
using PolyZone.Zones;

namespace PolyZone.Debug.Drawing;

public static class CircleDrawingExtensions
{
    /// <summary>
    /// Draws the circle in the world for the player, uses <see cref="API.GetGroundZFor_3dCoord"/> to determine the Z level
    /// </summary>
    /// <param name="circleZone">The <see cref="CircleZone"/></param>
    /// <param name="color">The <see cref="Color"/></param>
    public static void Draw(this CircleZone circleZone, in Color color) => circleZone.Draw(color, out _);

    public static void Draw(this CircleZone circleZone, in Color color, out Vector3 centerOfCircle)
    {
        var groundZ = 999.0f;
        
        API.GetGroundZFor_3dCoord(circleZone.Center.X, circleZone.Center.Y, groundZ, ref groundZ, false);

        centerOfCircle = new Vector3(circleZone.Center.X, circleZone.Center.Y, groundZ);
        
        World.DrawMarker(MarkerType.VerticalCylinder, 
            centerOfCircle, 
            Vector3.Zero,
            Vector3.Zero,
            new Vector3(circleZone.Radius * 2, circleZone.Radius * 2, 0.5f), 
            color);
    }

    public static void DrawDebug(this CircleZone circleZone, Entity? comparativeEntity = null)
    {
        comparativeEntity ??= Game.Player.Character;

        const int alpha = 150;
        var insideColor = Color.FromArgb(alpha, 0, 255, 0);
        var outsideColor = Color.FromArgb(alpha, 255, 0, 0);

        var isInside = comparativeEntity.IsInside(circleZone);
        var color = isInside ? insideColor : outsideColor;
        
        circleZone.Draw(color, out var centerOfCircle);
        
        Internal.Drawing.Draw3dText(centerOfCircle, $"Distance: { comparativeEntity.DistanceTo(circleZone) }", color, 5.0f);
    }
}
