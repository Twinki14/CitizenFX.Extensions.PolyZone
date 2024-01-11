using CitizenFX.Core;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Polygon = PolyZone.Shapes.Polygon;

namespace PolyZone.Benchmark.Tools;

internal static class Helpers
{
    internal static IEnumerable<Vector2> GetPointsFromGeoCollection(FeatureCollection featureCollection)
    {
        var points = new List<Vector2>();
        
        foreach (var feature in featureCollection.Features)
        {
            switch (feature.Geometry.Type)
            {
                case GeoJSONObjectType.Point:
                {
                    var point = feature.Geometry as Point;
                    
                    points.Add(new Vector2 { X = (float) point.Coordinates.Longitude, Y = (float) point.Coordinates.Latitude });

                    break;
                }
            }
        }

        return points;
    }

    internal static IEnumerable<Polygon> GetPolygonsFromGeoCollection(FeatureCollection featureCollection)
    {
        var polygons = new List<Polygon>();
        
        foreach (var feature in featureCollection.Features)
        {
            switch (feature.Geometry.Type)
            {
                case GeoJSONObjectType.MultiPolygon:
                {
                    var multiPolygon = feature.Geometry as MultiPolygon;
                    foreach (var polygon in multiPolygon!.Coordinates)
                    {
                        var points = new List<Vector2>();
                    
                        foreach (var lines in polygon.Coordinates)
                        {
                            foreach (var line in lines.Coordinates)
                            {
                                points.Add(new Vector2 { X = (float) line.Longitude, Y = (float) line.Latitude });
                            }
                        }
                        
                        polygons.Add(new Polygon(points));
                    }
                    break;
                }
                case GeoJSONObjectType.Polygon:
                {
                    var polygon = feature.Geometry as GeoJSON.Net.Geometry.Polygon;
                        
                    var points = new List<Vector2>();
                    
                    foreach (var line in polygon!.Coordinates.First().Coordinates)
                    {
                        points.Add(new Vector2 { X = (float) line.Longitude, Y = (float) line.Latitude });
                    }

                    polygons.Add(new Polygon(points));
                        
                    break;
                }
            }
        }

        return polygons;
    }
}
