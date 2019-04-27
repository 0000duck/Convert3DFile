namespace ConverterLibrary.Mesh
{
    internal class FaceElement
    {
        public FaceElement(Vertex v, TextureVertex vt, Vector vn)
        {
            V = v;
            Vt = vt;
            Vn = vn;
        }

        public Vertex V { get; }

        public TextureVertex Vt { get; }

        public Vector Vn { get; }
    }
}