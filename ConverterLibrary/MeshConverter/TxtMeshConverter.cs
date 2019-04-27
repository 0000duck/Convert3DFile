namespace ConverterLibrary.MeshConverter
{
    using System.IO;

    public class TxtMeshConverter : IMeshConverter
    {
        public string FromStream(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public Stream ToStream(string mesh)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(mesh);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}