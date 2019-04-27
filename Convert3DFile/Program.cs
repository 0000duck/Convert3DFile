namespace Convert3DFile
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using ConverterLibrary;

    internal class Program
    {
        private const string Usage = "You must specify an existing source file name and a non-existing destination file name";

        private static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine(Usage);
                return;
            }

            string sourceFileName = args[0];
            if (!File.Exists(sourceFileName))
            {
                Console.WriteLine(Usage);
                return;
            }

            string destFileName = args[1];
            if (File.Exists(destFileName))
            {
                Console.WriteLine(Usage);
                return;
            }

            string sourceFileFullPath = Path.GetFullPath(sourceFileName);
            string destFileFullPath = Path.GetFullPath(destFileName);

            Console.WriteLine($"Source file: {sourceFileFullPath}");
            Console.WriteLine($"Destination file: {destFileFullPath}");

            IFileConverter fileConverter = new FileConverter();

            Console.WriteLine("Converting...");

            try
            {
                fileConverter.Convert(sourceFileFullPath, destFileFullPath);
            }
            catch
            {
                Console.WriteLine("An error occurred during conversion, exiting.");
                return;
            }

            Console.WriteLine("Success.");
        }
    }
}
