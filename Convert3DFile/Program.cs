namespace Convert3DFile
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
                                     "  volume\tprint the volume of the mesh\r\n" +
                                     "  surface\tprint the surface area of the mesh\r\n" +
                                     "  tx <delta>\ttranslate along x by <delta>\r\n" +
                                     "  ty <delta>\ttranslate along y by <delta>\r\n" +
                                     "  tz <delta>\ttranslate along z by <delta>\r\n" +
                                     "  rx <angle>\trotate around x by <angle> (deg)\r\n" +
                                     "  ry <angle>\trotate around y by <angle> (deg)\r\n" +
                                     "  rz <angle>\trotate around z by <angle> (deg)\r\n" +
                                     "  s <factor>\tscale by <factor>\r\n";

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
            Queue<string> argsQueue = new Queue<string>(args);
            List<IMeshOperation> meshOperations = new List<IMeshOperation>();

            void Log(string s) => Console.WriteLine(s);

            while (argsQueue.Any())
            {
                string arg = argsQueue.Dequeue();
                switch (arg.ToLower())
                {
                    case "volume":
                        meshOperations.Add(new CalculateVolumeOperation(Log));
                        break;
                    case "surface":
                        meshOperations.Add(new CalculateSurfaceAreaOperation(Log));
                        break;
                    case "tx":
                    case "ty":
                    case "tz":
                        AddTranslationOperation(arg, argsQueue, meshOperations, Log);
                        break;
                    case "rx":
                    case "ry":
                    case "rz":
                        AddRotationOperation(arg, argsQueue, meshOperations, Log);
                        break;
                    case "s":
                        AddScaleOperation(argsQueue, meshOperations, Log);
                        break;
                }
            }

            return meshOperations;
        }

        private static void AddTranslationOperation(string arg, Queue<string> argsQueue, List<IMeshOperation> meshOperations, Action<string> log)
        {
            if (!argsQueue.Any())
            {
                return;
            }

            string nextArg = argsQueue.Dequeue();
            if (!float.TryParse(nextArg, NumberStyles.Float, CultureInfo.InvariantCulture, out float delta))
            {
                return;
            }

            switch (arg)
            {
                case "tx":
                    meshOperations.Add(new TranslateXOperation(delta, log));
                    return;
                case "ty":
                    meshOperations.Add(new TranslateYOperation(delta, log));
                    return;
                case "tz":
                    meshOperations.Add(new TranslateZOperation(delta, log));
                    return;
            }
        }

        private static void AddRotationOperation(string arg, Queue<string> argsQueue, List<IMeshOperation> meshOperations, Action<string> log)
        {
            if (!argsQueue.Any())
            {
                return;
            }

            string nextArg = argsQueue.Dequeue();
            if (!float.TryParse(nextArg, NumberStyles.Float, CultureInfo.InvariantCulture, out float angle))
            {
                return;
            }

            switch (arg)
            {
                case "rx":
                    meshOperations.Add(new RotateXOperation(angle, log));
                    return;
                case "ry":
                    meshOperations.Add(new RotateYOperation(angle, log));
                    return;
                case "rz":
                    meshOperations.Add(new RotateZOperation(angle, log));
                    return;
            }
        }

        private static void AddScaleOperation(Queue<string> argsQueue, List<IMeshOperation> meshOperations, Action<string> log)
        {
            if (!argsQueue.Any())
            {
                return;
            }

            string nextArg = argsQueue.Dequeue();
            if (!float.TryParse(nextArg, NumberStyles.Float, CultureInfo.InvariantCulture, out float factor))
            {
                return;
            }

            meshOperations.Add(new ScaleOperation(factor, log));
        }
    }
}
