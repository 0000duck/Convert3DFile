namespace ConverterTest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using ConverterLibrary;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using ConverterLibrary.MeshOperation;
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
            streamConverter.Convert(sourceStream, FileFormat.Obj, destStream, FileFormat.Stl, Enumerable.Empty<IMeshOperation>());

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

        [TestMethod]
        public void Convert_Shuttle()
        {
            // Arrange
            IStreamConverter streamConverter = new StreamConverter(new MeshConverterFactory());

            Stream sourceStream = new FileStream(@"Resources\shuttle.obj", FileMode.Open);
            Stream destStream = new MemoryStream();

            // Act
            streamConverter.Convert(sourceStream, FileFormat.Obj, destStream, FileFormat.Stl, Enumerable.Empty<IMeshOperation>());

            // Assert
            destStream.Position = 0;
            byte[] actualDestBytes = StreamHelper.GetAllBytes(destStream);

            Stream expectedStream = new FileStream(@"Resources\shuttle.stl", FileMode.Open);
            byte[] expectedDestBytes = StreamHelper.GetAllBytes(expectedStream);

            CollectionAssert.AreEqual(expectedDestBytes, actualDestBytes);

            sourceStream.Dispose();
            destStream.Dispose();
            expectedStream.Dispose();
        }

        [TestMethod]
        public void Convert_MeshOperations()
        {
            // Arrange
            IStreamConverter streamConverter = new StreamConverter(new MeshConverterFactory());

            Stream sourceStream = new FileStream(@"Resources\cube2.obj", FileMode.Open);
            Stream destStream = new MemoryStream();
            StringBuilder log = new StringBuilder();

            IEnumerable<IMeshOperation> meshOperations = new IMeshOperation[]
            {
                new CalculateVolumeOperation(s => log.Append(s + "\r\n")),
                new CalculateSurfaceAreaOperation(s => log.Append(s + "\r\n"))
            };

            // Act
            streamConverter.Convert(sourceStream, FileFormat.Obj, destStream, FileFormat.Stl, meshOperations);

            // Assert
            Assert.AreEqual("Volume: 8\r\nSurface area: 24\r\n", log.ToString());

            sourceStream.Dispose();
            destStream.Dispose();
        }
    }
}
