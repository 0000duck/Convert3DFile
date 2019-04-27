namespace ConverterLibrary.Mesh
{
    internal class Triangle
    {
        public Triangle(Vector vn, Vertex v1, Vertex v2, Vertex v3)
        {
            Vn = vn;
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public Vector Vn { get; }

        public Vertex V1 { get; }

        public Vertex V2 { get; }

        public Vertex V3 { get; }
    }
}