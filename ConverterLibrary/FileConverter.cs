namespace ConverterLibrary
{
    using System.IO;

    public class FileConverter : IFileConverter
    {
        public void Convert(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }
    }
}
