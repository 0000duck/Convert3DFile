namespace ConverterLibrary.Mesh
{
    internal class Triangle
    {
        public Triangle(GeometricVertex v1, GeometricVertex v2, GeometricVertex v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public GeometricVertex V1 { get; }

        public GeometricVertex V2 { get; }

        public GeometricVertex V3 { get; }
    }
}