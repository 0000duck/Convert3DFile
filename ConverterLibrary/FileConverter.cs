namespace ConverterLibrary
{
    using System;
    using System.IO;

    public class FileConverter : IFileConverter
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

            Convert(sourceFileName, sourceFileFormat, destFileName, destFileFormat);
        }

        private void Convert(string sourceFileName, FileFormat sourceFileFormat, string destFileName, FileFormat destFileFormat)
        {
            File.Copy(sourceFileName, destFileName);
        }

        private static FileFormat GetFileFormatFromExtension(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName)?.ToLower();

            switch (fileExtension)
            {
                case "txt":
                    return FileFormat.Txt;
                default:
                    return FileFormat.Unknown;
            }
        }
    }
}
