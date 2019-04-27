namespace ConverterTest
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
            List<string> logs = new List<string>();
            void LogAction(string s) => logs.Add(s);

            IEnumerable<IMeshOperation> meshOperations = new IMeshOperation[]
            {
                new CalculateVolumeOperation(LogAction),
                new CalculateSurfaceAreaOperation(LogAction),
                new TranslateXOperation(1f, LogAction),
                new TranslateYOperation(-2f, LogAction),
                new TranslateZOperation(3f, LogAction),
                new RotateXOperation(30f, LogAction),
                new RotateYOperation(-15f, LogAction),
                new RotateZOperation(5f, LogAction),
                new ScaleOperation(2f, LogAction),
                new CalculateVolumeOperation(LogAction),
                new CalculateSurfaceAreaOperation(LogAction)
            };

            // Act
            streamConverter.Convert(sourceStream, FileFormat.Obj, destStream, FileFormat.Stl, meshOperations);

            // Assert
            Assert.AreEqual(11, logs.Count);
            Assert.AreEqual("Volume: 8.00", logs[0]);
            Assert.AreEqual("Surface area: 24.00", logs[1]);
            Assert.AreEqual("Translation X by 1.00 done", logs[2]);
            Assert.AreEqual("Translation Y by -2.00 done", logs[3]);
            Assert.AreEqual("Translation Z by 3.00 done", logs[4]);
            Assert.AreEqual("Rotation X by 30.00 done", logs[5]);
            Assert.AreEqual("Rotation Y by -15.00 done", logs[6]);
            Assert.AreEqual("Rotation Z by 5.00 done", logs[7]);
            Assert.AreEqual("Scaling by 2.00 done", logs[8]);
            Assert.AreEqual("Volume: 64.00", logs[9]);
            Assert.AreEqual("Surface area: 96.00", logs[10]);

            sourceStream.Dispose();
            destStream.Dispose();
        }
    }
}
