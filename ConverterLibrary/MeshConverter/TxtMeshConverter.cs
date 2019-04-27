namespace ConverterLibrary.MeshConverter
{
    using System.IO;
    using ConverterLibrary.Mesh;

    internal class TxtMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream))
            {
                string content = streamReader.ReadToEnd();

                return new Mesh
                {
                    Content = content
                };
            }
        }

        Stream IMeshConverter.ToStream(IMesh mesh)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            if (mesh != null)
            {
                writer.Write(mesh.Content);
            }


            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}