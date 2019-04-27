namespace ConverterLibrary.Mesh
{
    using System.Diagnostics;

    [DebuggerDisplay("({X}, {Y}, {Z})")]
    internal class GeometricVertex
    {
        public GeometricVertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; }

        public float Y { get; }

        public float Z { get; }
    }
}