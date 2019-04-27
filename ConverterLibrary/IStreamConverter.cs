namespace ConverterLibrary
{
    using System.IO;

    public interface IStreamConverter
    {
        void Convert(Stream sourceStream, FileFormat sourceFileFormat, Stream destStream, FileFormat destFileFormat);
    }
}