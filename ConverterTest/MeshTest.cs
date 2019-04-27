namespace ConverterTest
{
    using System.IO;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MeshTest
    {
        [TestMethod]
        public void GetVolume()
        {
            // Arrange
            IMeshConverter meshConverter = new ObjMeshConverter();
            IMesh mesh = meshConverter.FromStream(new FileStream(@"Resources\cube2.obj", FileMode.Open));

            // Act
            float volume = mesh.GetVolume();

            // Assert
            Assert.AreEqual(8f, volume);
        }

        [TestMethod]
        public void GetSurfaceArea()
        {
            // Arrange
            IMeshConverter meshConverter = new ObjMeshConverter();
            IMesh mesh = meshConverter.FromStream(new FileStream(@"Resources\cube2.obj", FileMode.Open));

            // Act
            float surfaceArea = mesh.GetSurfaceArea();

            // Assert
            Assert.AreEqual(24f, surfaceArea);
        }
    }
}