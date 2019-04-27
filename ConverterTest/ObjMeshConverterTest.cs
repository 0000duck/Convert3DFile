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
        }
    }
}