namespace ConverterLibrary.MeshConverter
{
    using System;
    using System.IO;
    using ConverterLibrary.Mesh;

    public class StlMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            throw new NotSupportedException();
        }

        Stream IMeshConverter.ToStream(IMesh mesh)
        {
            Stream stream = new MemoryStream();

            byte[] header = new byte[80];
            stream.Write(header, 0, header.Length);

            stream.Position = 0;
            return stream;
        }
    }
}