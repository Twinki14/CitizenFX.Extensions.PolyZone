using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using CitizenFX.Core;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using PolyZone.Benchmark.Tools;
using PolyZone.Shapes;

namespace PolyZone.Benchmark.Shapes;

[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
[SimpleJob(RuntimeMoniker.NetCoreApp20)]
[SimpleJob(RuntimeMoniker.Net80)]
[MarkdownExporter]
public class PolygonGeoBenchmark
{
    private List<Polygon> _islands = [];
    private List<Vector2> _points = [];
    
    [GlobalSetup]
    public void Setup()
    {
        var islandsGeoJson = File.ReadAllText("./Data/10m_minor_islands.geojson");
        var pointsGeoJson = File.ReadAllText("./Data/10m_minor_islands_label_points.geojson");
        
        var islandCollection = JsonConvert.DeserializeObject<FeatureCollection>(islandsGeoJson);
        var pointCollection = JsonConvert.DeserializeObject<FeatureCollection>(pointsGeoJson);

        _islands = Helpers.GetPolygonsFromGeoCollection(islandCollection!).ToList();
        _points = Helpers.GetPointsFromGeoCollection(pointCollection!).ToList();
    }

    [Benchmark]
    public void GeoPolygonContains()
    {
        foreach (var island in _islands)
        {
            foreach (var point in _points)
            {
                island.Contains(point);
            }
        }
    }
    
    [Benchmark]
    public void GeoPolygonContains_Parallel()
    {
        Parallel.ForEach(_islands, island =>
        {
            foreach (var point in _points)
            {
                island.Contains(point);
            }
        });
    }
}
