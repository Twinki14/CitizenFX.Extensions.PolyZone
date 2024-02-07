using System.Drawing;
using CitizenFX.Core;
using PolyZone.Debug.Drawing;
using PolyZone.Zones;
using Serilog;
using Serilog.Sinks;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace PolyZone.Debug;

public class Script : BaseScript
{
    private readonly CircleZone _circleZone;
    private readonly CuboidZone _cuboidZone;
    private readonly PolygonZone _polygonZone;
    
    public Script()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.FiveM()
            .CreateLogger();
        
        _circleZone = new CircleZone(new Vector2(242, -1312), 15.0f);
        _cuboidZone = new CuboidZone(new Vector3(224, -1269, 31), new Vector3(236, -1260, 41));

        _polygonZone = new PolygonZone(new[]
        {
            new Vector2 { X = 205, Y = -1240 },
            new Vector2 { X = 201, Y = -1261 },
            new Vector2 { X = 199, Y = -1274 },
            new Vector2 { X = 239, Y = -1287 },
            new Vector2 { X = 243, Y = -1251 }
        }, 0, 80);
    }
    

    [Tick]
    public async Task OnTick()
    {
        /*
        _circleZone.DrawDebug();
        _cuboidZone.DrawDebug();
        */

        _polygonZone.DrawDebug();
    }
}
