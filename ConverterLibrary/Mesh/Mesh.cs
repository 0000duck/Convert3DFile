namespace ConverterLibrary.Mesh
{
    using System;
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

        float IMesh.GetVolume()
        {
            return Math.Abs(
                ((IMesh)this).GetTriangles()
                .Select(t => SignedVolumeOfTriangle(t.V1, t.V2, t.V3))
                .Sum());
        }

        float IMesh.GetSurfaceArea()
        {
            return (float)((IMesh)this).GetTriangles().Select(GetTriangleArea).Sum();
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

        public float SignedVolumeOfTriangle(Vertex p1, Vertex p2, Vertex p3)
        {
            float v321 = p3.X * p2.Y * p1.Z;
            float v231 = p2.X * p3.Y * p1.Z;
            float v312 = p3.X * p1.Y * p2.Z;
            float v132 = p1.X * p3.Y * p2.Z;
            float v213 = p2.X * p1.Y * p3.Z;
            float v123 = p1.X * p2.Y * p3.Z;
            return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
        }

        private static double GetTriangleArea(Triangle triangle)
        {
            double a = DistanceBetween(triangle.V1, triangle.V2);
            double b = DistanceBetween(triangle.V2, triangle.V3);
            double c = DistanceBetween(triangle.V3, triangle.V1);
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        private static double DistanceBetween(Vertex a, Vertex b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            double dz = a.Z - b.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}