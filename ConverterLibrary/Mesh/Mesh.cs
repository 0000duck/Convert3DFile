namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;

    internal class Mesh : IMesh
    {
        public Mesh()
        {
            GeometricVertices = new List<GeometricVertex>();
            TextureVertices = new List<TextureVertex>();
            VertexNormals = new List<VertexNormal>();
            Faces = new List<Face>();
        }

        IEnumerable<GeometricVertex> IMesh.GeometricVertices => GeometricVertices;

        IEnumerable<TextureVertex> IMesh.TextureVertices => TextureVertices;

        IEnumerable<VertexNormal> IMesh.VertexNormals => VertexNormals;

        IEnumerable<Face> IMesh.Faces => Faces;

        public List<GeometricVertex> GeometricVertices { get; }

        public List<TextureVertex> TextureVertices { get; }

        public List<VertexNormal> VertexNormals { get; }

        public List<Face> Faces { get; }
    }
}