namespace ConverterTest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjMeshConverterTest
    {
        [TestMethod]
        public void FromStream()
        {
            // Arrange
            IMeshConverter meshConverter = new ObjMeshConverter();
            Stream stream = new FileStream(@"Resources\cube.obj", FileMode.Open);

            // Act
            IMesh mesh = meshConverter.FromStream(stream);

            // Assert
            List<GeometricVertex> geometricVertices = mesh.GeometricVertices.ToList();
            Assert.AreEqual(8, geometricVertices.Count);
            Assert.AreEqual(0.0f, geometricVertices.ElementAt(3).X);
            Assert.AreEqual(1.0f, geometricVertices.ElementAt(3).Y);
            Assert.AreEqual(1.0f, geometricVertices.ElementAt(3).Z);

            Assert.IsFalse(mesh.TextureVertices.Any());

            List<VertexNormal> vertexNormals = mesh.VertexNormals.ToList();
            Assert.AreEqual(6, vertexNormals.Count);
            Assert.AreEqual(0.0f, vertexNormals.ElementAt(1).I);
            Assert.AreEqual(0.0f, vertexNormals.ElementAt(1).J);
            Assert.AreEqual(-1.0f, vertexNormals.ElementAt(1).K);

            List<Face> faces = mesh.Faces.ToList();
            Assert.AreEqual(12, faces.Count);
            List<FaceElement> faceElements = faces[9].FaceElements.ToList();
            Assert.AreEqual(3, faceElements.Count);
            Assert.AreEqual(mesh.GeometricVertices.ElementAt(5), faceElements[1].V);
            Assert.IsNull(faceElements[1].Vt);
            Assert.AreEqual(mesh.VertexNormals.ElementAt(3), faceElements[1].Vn);
        }
    }
}