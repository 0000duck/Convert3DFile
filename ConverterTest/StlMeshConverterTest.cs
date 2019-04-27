namespace ConverterTest
{
    using System.IO;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StlMeshConverterTest
    {
        [TestMethod]
        public void ToStream()
        {
            // Arrange
            IMesh mesh = CreateMesh();
            IMeshConverter meshConverter = new StlMeshConverter();

            // Act
            Stream stream = meshConverter.ToStream(mesh);
            
            // Assert
            byte[] bytes = GetAllBytes(stream);
            Assert.AreEqual(80, bytes.Length);

            stream.Dispose();
        }

        private static Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();

            GeometricVertex v1 = new GeometricVertex(0f, 0f, 0f);
            GeometricVertex v2 = new GeometricVertex(2f, 3f, 4f);
            GeometricVertex v3 = new GeometricVertex(8f, -1f, -3f);

            mesh.GeometricVertices.AddRange(new[] { v1, v2, v3 });
            mesh.Faces.Add(new Face(new []{ new FaceElement(v1, null, null), new FaceElement(v2, null, null), new FaceElement(v3, null, null) }));

            return mesh;
        }

        public static byte[] GetAllBytes(Stream input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}