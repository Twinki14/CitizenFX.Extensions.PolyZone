using CitizenFX.Core;
using PolyZone.Debug.Drawing;
using PolyZone.Zones;
using Serilog;
using Serilog.Sinks;

namespace PolyZone.Debug;

public class Script : BaseScript
{
    private readonly CircleZone _circleZone;
    private readonly CuboidZone _cuboidZone;
    
    public Script()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.FiveM()
            .CreateLogger();
        
        _circleZone = new CircleZone(new Vector2(242, -1312), 15.0f);
        _cuboidZone = new CuboidZone(new Vector3(224, -1269, 31), new Vector3(236, -1260, 41));
    }

    [Tick]
    public async Task OnTick()
    {
        _circleZone.DrawDebug();
        _cuboidZone.DrawDebug();
    }
}
