﻿namespace ConverterTest
{
    using System.IO;
    using System.Linq;
    using ConverterLibrary;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StlMeshConverterTest
    {
        [TestMethod]
        public void ToStream_OneFace_3Vertices()
        {
            // Arrange
            IMesh mesh = CreateMesh3();
            IMeshConverter meshConverter = new StlMeshConverter();

            // Act
            Stream stream = meshConverter.ToStream(mesh);

            // Assert
            byte[] bytes = StreamHelper.GetAllBytes(stream);
            Assert.AreEqual(ComputeExpectedNumberOfBytes(1), bytes.Length);

            byte[] numberOfTriangles = bytes.Skip(80).Take(4).ToArray();
            Assert.AreEqual(1, numberOfTriangles[0]);
            Assert.AreEqual(0, numberOfTriangles[1]);
            Assert.AreEqual(0, numberOfTriangles[2]);
            Assert.AreEqual(0, numberOfTriangles[3]);

            stream.Dispose();
        }

        [TestMethod]
        public void ToStream_OneFace_4Vertices()
        {
            // Arrange
            IMesh mesh = CreateMesh4();
            IMeshConverter meshConverter = new StlMeshConverter();

            // Act
            Stream stream = meshConverter.ToStream(mesh);

            // Assert
            byte[] bytes = StreamHelper.GetAllBytes(stream);
            Assert.AreEqual(ComputeExpectedNumberOfBytes(2), bytes.Length);

            byte[] numberOfTriangles = bytes.Skip(80).Take(4).ToArray();
            Assert.AreEqual(2, numberOfTriangles[0]);
            Assert.AreEqual(0, numberOfTriangles[1]);
            Assert.AreEqual(0, numberOfTriangles[2]);
            Assert.AreEqual(0, numberOfTriangles[3]);

            stream.Dispose();
        }

        private static int ComputeExpectedNumberOfBytes(int triangleCount)
        {
            return 80 // header
                   + 4 // number of triangles
                   + triangleCount * (48 + 2); // 4 * 4 bytes for normal and vertices and 2 bytes for attribute byte count
        }

        [TestMethod]
        public void ToStream_ToFile()
        {
            // Arrange
            IMesh mesh = CreateMesh4();
            IMeshConverter meshConverter = new StlMeshConverter();

            // Act
            using (Stream stream = meshConverter.ToStream(mesh))
            using (FileStream fileStream = File.Create(@"c:\Temp\testout.stl"))
            {
                stream.CopyTo(fileStream);
            }
        }

        private static Mesh CreateMesh3()
        {
            Mesh mesh = new Mesh();

            Vertex v1 = new Vertex(0f, 0f, 0f);
            Vertex v2 = new Vertex(2f, 3f, 4f);
            Vertex v3 = new Vertex(8f, -1f, -3f);

            Vector vn = new Vector(1f, 0f, 0f);

            mesh.GeometricVertices.AddRange(new[] { v1, v2, v3 });
            mesh.Faces.Add(new Face(new[]
            {
                new FaceElement(v1, null, vn),
                new FaceElement(v2, null, vn),
                new FaceElement(v3, null, vn)
            }));

            return mesh;
        }

        private static Mesh CreateMesh4()
        {
            Mesh mesh = new Mesh();

            Vertex v1 = new Vertex(0f, 0f, 0f);
            Vertex v2 = new Vertex(2f, 3f, 4f);
            Vertex v3 = new Vertex(8f, -1f, -3f);
            Vertex v4 = new Vertex(4f, -12f, -3f);

            Vector vn = new Vector(1f, 0f, 0f);

            mesh.GeometricVertices.AddRange(new[] { v1, v2, v3, v4 });
            mesh.Faces.Add(new Face(new[]
            {
                new FaceElement(v1, null, vn),
                new FaceElement(v2, null, vn),
                new FaceElement(v3, null, vn),
                new FaceElement(v4, null, vn)
            }));

            return mesh;
        }
    }
}