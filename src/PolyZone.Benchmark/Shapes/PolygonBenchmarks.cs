using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using CitizenFX.Core;
using PolyZone.Benchmark.Tools;
using PolyZone.Shapes;

namespace PolyZone.Benchmark.Shapes;

[SimpleJob(RuntimeMoniker.Net47)]
[SimpleJob(RuntimeMoniker.NetCoreApp20)]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[RPlotExporter]
public class PolygonBenchmarks
{
    [Params(1000, 50000)]
    public int Polygons;
    
    [Params(100, 250, 500)]
    public int PointsPer;

    private Polygon[] _polygons = [];
    
    [GlobalSetup]
    public void Setup()
    {
        _polygons = new Polygon[Polygons];
        
        for (var i = 0; i < Polygons; i++)
        {
            var netPolygon = RandomPolygonBuilder.Build(PointsPer, -1000, 1000, -1000, 1000);
            
            var points = netPolygon.Coordinates
                .Select(netPolygonCoordinate => new Vector2 { X = (float)netPolygonCoordinate.X, Y = (float)netPolygonCoordinate.Y })
                .ToList();

            _polygons[i] = new Polygon(points);
        }
    }

    [Benchmark]
    public void PolygonContains()
    {
        var testPoint = new Vector2 { X = 0, Y = 0 };
        foreach (var polygon in _polygons)
        {
            polygon.Contains(testPoint);
        }
    }
    
    [Benchmark]
    public void PolygonContains_Parallel()
    {
        var testPoint = new Vector2 { X = 0, Y = 0 };
        
        Parallel.ForEach(_polygons, polygon =>
        {
            polygon.Contains(testPoint);
        });
    }
}
