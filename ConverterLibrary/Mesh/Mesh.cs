namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;
    using System.Linq;

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

        IEnumerable<Triangle> IMesh.GetTriangles()
        {
            foreach (Face face in Faces)
            {
                foreach (Triangle triangle in GetTriangles(face))
                {
                    yield return triangle;
                }
            }
        }

        public List<Vertex> GeometricVertices { get; }

        public List<TextureVertex> TextureVertices { get; }

        public List<Vector> VertexNormals { get; }

        public List<Face> Faces { get; }

        private static IEnumerable<Triangle> GetTriangles(Face face)
        {
            List<FaceElement> faceElements = face.FaceElements.ToList();

            if (faceElements.Count < 3)
            {
                yield break;
            }

            Vertex v1 = faceElements[0].V;

            for (int i = 2; i < faceElements.Count; i++)
            {
                Vertex v2 = faceElements[i - 1].V;
                Vertex v3 = faceElements[i].V;
                yield return new Triangle(v1, v2, v3);
            }
        }
    }
}