namespace ConverterLibrary
{
    using System.Collections.Generic;
    using System.IO;
    using ConverterLibrary.MeshOperation;

    public interface IStreamConverter
    {
        void Convert(Stream sourceStream, FileFormat sourceFileFormat, Stream destStream, FileFormat destFileFormat, IEnumerable<IMeshOperation> meshOperations);
    }
}