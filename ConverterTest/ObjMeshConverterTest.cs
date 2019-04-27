namespace ConverterTest
{
    using System.IO;
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
            Stream stream = new MemoryStream(new byte[] { 1, 2, 3, 4 });

            // Act
            IMesh mesh = meshConverter.FromStream(stream);

            // Assert
            Assert.AreEqual("1234", mesh.Content);
        }
    }
}