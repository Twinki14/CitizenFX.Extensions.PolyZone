using System.Collections.Generic;
using System.Linq;
using System.Text;
using CitizenFX.Core;
using GeoJSON.Text;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using PolyZone.Shapes.Interfaces;
using Polygon = PolyZone.Shapes.Polygon;

namespace PolyZone.Tests.Internal;

internal static class Helpers
{
    internal static string GetPolygonArrayString(List<IPolygon> polygons)
    {
        var resultBuilder = new StringBuilder();

        for (var index = 0; index < polygons.Count; index++)
        {
            var polygon = polygons[index];
            
            resultBuilder.AppendLine($"[{index}]");

            foreach (var point in polygon.Points)
            {
                resultBuilder.AppendLine($"  X: {point.X}, Y: {point.Y}");
            }
        }

        return resultBuilder.ToString();
    }
    
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
                    var polygon = feature.Geometry as GeoJSON.Text.Geometry.Polygon;
                        
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
