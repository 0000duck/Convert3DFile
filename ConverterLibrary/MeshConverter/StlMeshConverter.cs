namespace ConverterLibrary.MeshConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConverterLibrary.Mesh;

    public class StlMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            throw new NotSupportedException();
        }

        Stream IMeshConverter.ToStream(IMesh mesh)
        {
            Stream stream = new MemoryStream();

            WriteHeader(stream);
            WriteTriangles(stream, mesh);

            stream.Position = 0;
            return stream;
        }

        private static void WriteHeader(Stream stream)
        {
            byte[] header = new byte[80];
            stream.Write(header, 0, header.Length);
        }

        private static void WriteTriangles(Stream stream, IMesh mesh)
        {
            List<Triangle> triangles = GetTriangles(mesh).ToList();

            byte[] numberOfTriangles = BitConverter.GetBytes(triangles.Count);
            StreamHelper.WriteBytes(stream, numberOfTriangles);

            foreach (Triangle triangle in triangles)
            {
                WriteTriangle(stream, triangle);
            }
        }

        private static IEnumerable<Triangle> GetTriangles(IMesh mesh)
        {
            foreach (Face face in mesh.Faces)
            {
                foreach (Triangle triangle in GetTriangles(face))
                {
                    yield return triangle;
                }
            }
        }

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
                Vector vn = FindNormal(v1, v2, v3);
                yield return new Triangle(vn, v1, v2, v3);
            }
        }

        private static Vector FindNormal(Vertex v1, Vertex v2, Vertex v3)
        {
            float a1 = v2.X - v1.X;
            float a2 = v2.Y - v1.Y;
            float a3 = v2.Z - v1.Z;
            float b1 = v3.X - v1.X;
            float b2 = v3.Y - v1.Y;
            float b3 = v3.Z - v1.Z;

            return new Vector(
                a2 * b3 - a3 * b2,
                a3 * b1 - a1 * b3,
                a1 * b2 - a2 * b1);
        }

        private static void WriteTriangle(Stream stream, Triangle triangle)
        {
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.Vn.I));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.Vn.J));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.Vn.K));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V1.X));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V1.Y));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V1.Z));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V2.X));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V2.Y));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V2.Z));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V3.X));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V3.Y));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(triangle.V3.Z));

            byte[] attributeByteCount = new byte[2];
            StreamHelper.WriteBytes(stream, attributeByteCount);
        }
    }
}