using System.Drawing;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace PolyZone.Debug.Internal;

internal static class Drawing
{
    internal static void Draw3dText(Vector3 pos, string text, Color color, float size = 0.35f) 
    {
        float _x = 0;
        float _y = 0;
        
        var onScreen = API.World3dToScreen2d(pos.X, pos.Y, pos.Z, ref _x, ref _y);
        var pCoords = API.GetGameplayCamCoords();
            
        var distance = API.GetDistanceBetweenCoords(pCoords.X,pCoords.Y,pCoords.Z, pos.X, pos.Y, pos.Z, true);
        var txtScale = 1 / distance * 2;
        var fov = 1 / API.GetGameplayCamFov() * 100;
        var scale = txtScale * fov * size;

        if (onScreen) 
        {
            API.SetTextScale(0.0f, scale);
            API.SetTextFont(4);
            API.SetTextProportional(true);
            API.SetTextColour(color.R, color.G, color.B, color.A);
            API.SetTextDropShadow();
            API.SetTextEdge(0, 0, 0, 0, 150);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.SetTextCentre(false);
            API.AddTextComponentString(text);
            API.DrawText(_x, _y);
        }
    }
}
