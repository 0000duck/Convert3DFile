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
            List<Triangle> triangles = mesh.GetTriangles().ToList();

            byte[] numberOfTriangles = BitConverter.GetBytes(triangles.Count);
            StreamHelper.WriteBytes(stream, numberOfTriangles);

            foreach (Triangle triangle in triangles)
            {
                Vector normal = FindNormal(triangle.V1, triangle.V2, triangle.V3);
                WriteTriangle(stream, triangle, normal);
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

        private static void WriteTriangle(Stream stream, Triangle triangle, Vector normal)
        {
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(normal.I));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(normal.J));
            StreamHelper.WriteBytes(stream, BitConverter.GetBytes(normal.K));
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