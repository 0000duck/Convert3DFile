namespace ConverterLibrary.Mesh
{
    using System.Diagnostics;

    [DebuggerDisplay("({I}, {J}, {K})")]
    internal class Vector
    {
        public Vector(float i, float j, float k)
        {
            I = i;
            J = j;
            K = k;
        }

        public float I { get; }

        public float J { get; }

        public float K { get; }
    }
}