namespace ConverterTest
{
    using System.IO;
    using ConverterLibrary;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class StreamConverterTest
    {
        [TestMethod]
        public void Convert()
        {
            // Arrange
            IMesh mesh = Mock.Of<IMesh>();

            Mock<IMeshConverter> objMeshConverterMock = new Mock<IMeshConverter>();
            objMeshConverterMock.Setup(c => c.FromStream(It.IsAny<Stream>())).Returns(mesh);
            Mock<IMeshConverter> stlMeshConverterMock = new Mock<IMeshConverter>();
            stlMeshConverterMock.Setup(c => c.ToStream(mesh)).Returns(StreamHelper.GetStreamFromString("TestContent"));

            Mock<IMeshConverterFactory> meshConverterFactoryMock = new Mock<IMeshConverterFactory>();
            meshConverterFactoryMock.Setup(m => m.CreateMeshConverter(FileFormat.Obj)).Returns(objMeshConverterMock.Object);
            meshConverterFactoryMock.Setup(m => m.CreateMeshConverter(FileFormat.Stl)).Returns(stlMeshConverterMock.Object);

            IStreamConverter streamConverter = new StreamConverter(meshConverterFactoryMock.Object);

            Stream sourceStream = new MemoryStream();
            Stream destStream = new MemoryStream();

            // Act
            streamConverter.Convert(sourceStream, FileFormat.Obj, destStream, FileFormat.Stl);

            // Assert
            meshConverterFactoryMock.Verify(f => f.CreateMeshConverter(FileFormat.Obj));
            meshConverterFactoryMock.Verify(f => f.CreateMeshConverter(FileFormat.Stl));

            objMeshConverterMock.Verify(c => c.FromStream(sourceStream));
            stlMeshConverterMock.Verify(c => c.ToStream(mesh));

            destStream.Position = 0;
            Assert.AreEqual("TestContent", StreamHelper.GetStringFromStream(destStream));

            sourceStream.Dispose();
            destStream.Dispose();
        }
    }
}
