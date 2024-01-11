using NetTopologySuite.Geometries;
using NetTopologySuite.Shape.Random;
using NetTopologySuite.Triangulate;

namespace PolyZone.Benchmark.Tools;

public class RandomPolygonBuilder
{
    private readonly Random _rnd = new(17);

    public static Geometry Build(int numberOfPoints, double x1Extent = 0, double x2Extent = 100, double y1Extent = 0, double y2Extent = 100)
    {
        var builder = new RandomPolygonBuilder(numberOfPoints, x1Extent, x2Extent, y1Extent, y2Extent);
        return builder.CreatePolygon();
    }

    private readonly GeometryFactory _geomFact = new();
    private readonly int _numPoints;
    private readonly Geometry _voronoi;

    public RandomPolygonBuilder(int numberOfPoints, double x1Extent = 0, double x2Extent = 100, double y1Extent = 0, double y2Extent = 100)
    {
        _numPoints = numberOfPoints;
        Envelope extent = new(x1Extent, x2Extent, y1Extent, y2Extent);
        var sites = RandomPoints(extent, numberOfPoints);
        _voronoi = VoronoiDiagram(sites, extent);
    }

    private Geometry CreatePolygon()
    {
        var cellsSelect = Select(_voronoi, _numPoints / 2);
        var poly = cellsSelect.Union();
        return poly;
    }

    private Geometry Select(Geometry geoms, int n)
    {
        var selection = new List<Geometry>();
        // add all the geometries
        for (var i = 0; i < geoms.NumGeometries; i++)
        {
            selection.Add(geoms.GetGeometryN(i));
        }
        // toss out random ones to leave n
        while (selection.Count > n)
        {
            var index = (int)(selection.Count * _rnd.NextDouble());
            selection.RemoveAt(index);
        }
        return _geomFact.BuildGeometry(selection);
    }

    private Geometry RandomPoints(Envelope extent, int nPts)
    {
        var shapeBuilder = new RandomPointsBuilder(_geomFact)
        {
            Extent = extent,
            NumPoints = nPts
        };
        return shapeBuilder.GetGeometry();
    }

    private static Geometry VoronoiDiagram(Geometry sitesGeom, Envelope extent)
    {
        var builder = new VoronoiDiagramBuilder();
        builder.SetSites(sitesGeom);
        builder.ClipEnvelope = extent;
        builder.Tolerance = 0.0001;
        Geometry diagram = builder.GetDiagram(sitesGeom.Factory);
        return diagram;
    }
}
