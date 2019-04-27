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

            GeometricVertex v1 = faceElements[0].V;
            GeometricVertex v2 = faceElements[1].V;

            for (int i = 2; i < faceElements.Count; i++)
            {
                GeometricVertex v3 = faceElements[i].V;
                yield return new Triangle(v1, v2, v3);
            }
        }

        private static void WriteTriangle(Stream stream, Triangle triangle)
        {
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(0f));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(0f));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(0f));
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