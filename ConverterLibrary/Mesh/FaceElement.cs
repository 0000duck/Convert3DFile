namespace ConverterLibrary.Mesh
{
    internal class FaceElement
    {
        public FaceElement(GeometricVertex v, TextureVertex vt, VertexNormal vn)
        {
            V = v;
            Vt = vt;
            Vn = vn;
        }

        public GeometricVertex V { get; }

        public TextureVertex Vt { get; }

        public VertexNormal Vn { get; }
    }
}