namespace ConverterLibrary.Mesh
{
    internal class Triangle
    {
        public Triangle(VertexNormal vn, GeometricVertex v1, GeometricVertex v2, GeometricVertex v3)
        {
            Vn = vn;
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public VertexNormal Vn { get; }

        public GeometricVertex V1 { get; }

        public GeometricVertex V2 { get; }

        public GeometricVertex V3 { get; }
    }
}