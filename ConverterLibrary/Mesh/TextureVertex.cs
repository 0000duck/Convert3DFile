namespace ConverterLibrary.Mesh
{
    using System.Diagnostics;

    [DebuggerDisplay("({U}, {V})")]
    public class TextureVertex
    {
        public TextureVertex(float u, float v)
        {
            U = u;
            V = v;
        }

        public float U { get; }

        public float V { get; }
    }
}