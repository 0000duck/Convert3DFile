namespace ConverterLibrary
{
    using System;
    using System.IO;
    using ConverterLibrary.MeshConverter;

    public class FileConverter
    {
        public void Convert(string sourceFileName, string destFileName)
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
                streamConverter.Convert(sourceStream, sourceFileFormat, destStream, destFileFormat);
            }

        }

        private static FileFormat GetFileFormatFromExtension(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName)?.ToLower();

            switch (fileExtension)
            {
                case ".txt":
                    return FileFormat.Txt;
                default:
                    return FileFormat.Unknown;
            }
        }
    }
}
