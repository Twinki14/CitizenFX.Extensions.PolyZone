using CitizenFX.Core;
using PolyZone.Shapes.Interfaces;

namespace PolyZone.Shapes;

public class Box(float x, float y, float z, float length, float width, float height) : IBox
{
    private readonly float _x = x;
    private readonly float _y = y;
    private readonly float _z = z;
    private readonly float _length = length;
    private readonly float _width = width;
    private readonly float _height = height;
    
    /// <summary>
    /// Calculated corners of the <see cref="Box"/>, starts with the Upper Left
    /// </summary>
    public Vector3[] Corners {  get; } =
    [
        // Upper face
        new Vector3 { X = x, Y = y, Z = z },  // Upper Left (Front)
        new Vector3 { X = x + length, Y = y, Z = z },  // Upper Right (Front)
        new Vector3 { X = x + length, Y = y + width, Z = z },  // Lower Right (Front)
        new Vector3 { X = x, Y = y + width, Z = z },  // Lower Left (Front)

        // Lower face
        new Vector3 { X = x, Y = y, Z = z + height },  // Upper Left (Back)
        new Vector3 { X = x + length, Y = y, Z = z + height },  // Upper Right (Back)
        new Vector3 { X = x + length, Y = y + width, Z = z + height },  // Lower Right (Back)
        new Vector3 { X = x, Y = y + width, Z = z + height }  // Lower Left (Back)
    ];

    public Box(in Vector3 upperLeft, float length, float width, float height) : this(upperLeft.X, upperLeft.Y, upperLeft.Z, length, width, height)
    {
        
    }

    public bool Contains(in Vector3 point)
    {
        throw new NotImplementedException();
    }
}
