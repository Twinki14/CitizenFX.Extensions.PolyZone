using CitizenFX.Core;
using PolyZone.Debug.Drawing;
using PolyZone.Zones;
using Serilog;
using Serilog.Sinks;

namespace PolyZone.Debug;

public class Script : BaseScript
{
    private readonly CircleZone _circleZone;
    
    public Script()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.FiveM()
            .CreateLogger();
        
        _circleZone = new CircleZone(new Vector2(242, -1312), 15.0f);
    }

    [Tick]
    public async Task OnTick()
    {
        _circleZone.DrawDebug();
    }
}
