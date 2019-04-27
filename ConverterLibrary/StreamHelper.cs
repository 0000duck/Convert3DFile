namespace ConverterLibrary
{
    using System.IO;

    internal static class StreamHelper
    {
        public static string GetStringFromStream(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream))
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

        public static void WriteBytes(Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }

        public static byte[] GetAllBytes(Stream input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}