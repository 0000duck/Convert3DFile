namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;

    internal class Mesh : IMesh
    {
        public Mesh()
        {
            GeometricVertices = new List<Vertex>();
            TextureVertices = new List<TextureVertex>();
            VertexNormals = new List<Vector>();
            Faces = new List<Face>();
        }

        IEnumerable<Vertex> IMesh.GeometricVertices => GeometricVertices;

        IEnumerable<TextureVertex> IMesh.TextureVertices => TextureVertices;

        IEnumerable<Vector> IMesh.VertexNormals => VertexNormals;

        IEnumerable<Face> IMesh.Faces => Faces;

        public List<Vertex> GeometricVertices { get; }

        public List<TextureVertex> TextureVertices { get; }

        public List<Vector> VertexNormals { get; }

        public List<Face> Faces { get; }
    }
}