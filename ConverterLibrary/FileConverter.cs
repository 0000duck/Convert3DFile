namespace ConverterLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConverterLibrary.MeshConverter;
    using ConverterLibrary.MeshOperation;

    public class FileConverter
    {
        public void Convert(string sourceFileName, string destFileName, IEnumerable<IMeshOperation> meshOperations)
        {
            FileFormat sourceFileFormat = GetFileFormatFromExtension(sourceFileName);
            if (sourceFileFormat == FileFormat.Unknown)
            {
                throw new InvalidOperationException("Not supported source file format");
            }

            FileFormat destFileFormat = GetFileFormatFromExtension(destFileName);
            if (destFileFormat == FileFormat.Unknown)
            {
                throw new InvalidOperationException("Not supported destination file format");
            }

            IStreamConverter streamConverter = new StreamConverter(new MeshConverterFactory());

            using (Stream sourceStream = new FileStream(sourceFileName, FileMode.Open))
            using (FileStream destStream = File.Create(destFileName))
            {
                streamConverter.Convert(sourceStream, sourceFileFormat, destStream, destFileFormat, meshOperations ?? Enumerable.Empty<IMeshOperation>());
            }
        }

        private static FileFormat GetFileFormatFromExtension(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName)?.ToLower();

            switch (fileExtension)
            {
                case ".obj":
                    return FileFormat.Obj;
                case ".stl":
                    return FileFormat.Stl;
                default:
                    return FileFormat.Unknown;
            }
        }
    }
}
