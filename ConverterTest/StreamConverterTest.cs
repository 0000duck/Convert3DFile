namespace ConverterTest
{
    using System.IO;
    using ConverterLibrary;
    using ConverterLibrary.MeshConverter;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StreamConverterTest
    {
        [TestMethod]
        public void TxtToTxt()
        {
            // Arrange
            IStreamConverter streamConverter = new StreamConverter(new MeshConverterFactory());

            Stream inputStream = StreamHelper.GetStreamFromString("TestContent");
            MemoryStream outputStream = new MemoryStream();

            // Act
            streamConverter.Convert(inputStream, FileFormat.Txt, outputStream, FileFormat.Txt);

            // Assert
            outputStream.Position = 0;

            Assert.AreEqual("TestContent", StreamHelper.GetStringFromStream(outputStream));

            inputStream.Dispose();
            outputStream.Dispose();
        }
    }
}
