﻿namespace ConverterLibrary
{
    using System.IO;

    internal static class StreamHelper
    {
        public static string GetStringFromStream(Stream outputStream)
        {
            using (StreamReader streamReader = new StreamReader(outputStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static Stream GetStreamFromString(string content)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            if (!string.IsNullOrEmpty(content))
            {
                writer.Write(content);
            }

            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}