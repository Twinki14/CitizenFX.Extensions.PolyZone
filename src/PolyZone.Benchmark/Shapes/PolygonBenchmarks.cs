using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using CitizenFX.Core;
using PolyZone.Benchmark.Tools;
using PolyZone.Shapes;

namespace PolyZone.Benchmark.Shapes;

[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
[SimpleJob(RuntimeMoniker.NetCoreApp20)]
[SimpleJob(RuntimeMoniker.Net80)]
[MarkdownExporter]
public class PolygonBenchmarks
{
    [Params(50000, 100000)]
    public int Polygons;
    
    [Params(500)]
    public int PointsPer;

    private Polygon[] _polygons = [];

    private readonly Random _random = new(123123);
    
    [GlobalSetup]
    public void Setup()
    {
        _polygons = new Polygon[Polygons];
        
        for (var i = 0; i < Polygons; i++)
        {
            var netPolygon = RandomPolygonBuilder.Build(PointsPer, -100, 100, -100, 100);
            
            var points = netPolygon.Coordinates
                .Select(netPolygonCoordinate => new Vector2 { X = (float) netPolygonCoordinate.X, Y = (float) netPolygonCoordinate.Y })
                .ToList();

            _polygons[i] = new Polygon(points);
        }
    }

    [Benchmark]
    public void PolygonContains_PointInside()
    {
        var testPoint = new Vector2 { X = 0, Y = 0 };
        foreach (var polygon in _polygons)
        {
            polygon.Contains(testPoint);
        }
    }
    
    [Benchmark]
    public void PolygonContains_Parallel_PointInside()
    {
        var testPoint = new Vector2 { X = 0, Y = 0 };
        Parallel.ForEach(_polygons, polygon =>
        {
            polygon.Contains(testPoint);
        });
    }
    
    [Benchmark]
    public void PolygonContains_PointOutside()
    {
        var testPoint = new Vector2 { X = -1000, Y = -1000 };
        foreach (var polygon in _polygons)
        {
            polygon.Contains(testPoint);
        }
    }
    
    [Benchmark]
    public void PolygonContains_Parallel_PointOutside()
    {
        var testPoint = new Vector2 { X = -1000, Y = -1000 };
        Parallel.ForEach(_polygons, polygon =>
        {
            polygon.Contains(testPoint);
        });
    }
    
    [Benchmark]
    public void PolygonContains_RandomPoint()
    {
        var testPoint = new Vector2 { X = _random.Next(-150, 150), Y = _random.Next(-150, 150) };
        foreach (var polygon in _polygons)
        {
            polygon.Contains(testPoint);
        }
    }
    
    [Benchmark]
    public void PolygonContains_Parallel_RandomPoint()
    {
        var testPoint = new Vector2 { X = _random.Next(-150, 150), Y = _random.Next(-150, 150) };
        Parallel.ForEach(_polygons, polygon =>
        {
            polygon.Contains(testPoint);
        });
    }
}
