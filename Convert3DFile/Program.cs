namespace Convert3DFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ConverterLibrary;
    using ConverterLibrary.MeshOperation;

    internal class Program
    {
        private const string Usage = "Usage: Convert3DFile sourceFileName destFileName [operation1 [operation2 [operation3 ...]]]\r\n" +
                                     "sourceFileName: path of an existing source file\r\n" +
                                     "destFileName: path of a non-existing destination file\r\n" +
                                     "operations:\r\n" +
                                     "  volume: print the volume of the mesh\r\n" +
                                     "  surface: print the surface area of the mesh\r\n";

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

            FileConverter fileConverter = new FileConverter();

            Console.WriteLine("Converting...");

            try
            {
                fileConverter.Convert(sourceFileFullPath, destFileFullPath, GetMeshOperations(args.Skip(2)));
            }
            catch
            {
                Console.WriteLine("An error occurred during conversion, exiting.");
                return;
            }

            Console.WriteLine("Success.");
        }

        private static IEnumerable<IMeshOperation> GetMeshOperations(IEnumerable<string> args)
        {
            List<string> argsList = args.ToList();
            void Log(string s) => Console.WriteLine(s);

            for (int i = 0; i < argsList.Count; i++)
            {
                string arg = argsList[i];

                switch (arg.ToLower())
                {
                    case "volume":
                        yield return new CalculateVolumeOperation(Log);
                        break;
                    case "surface":
                        yield return new CalculateSurfaceAreaOperation(Log);
                        break;
                }
            }
        }
    }
}
