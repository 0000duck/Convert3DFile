namespace ConverterLibrary.MeshConverter
{
    using System;
    using System.IO;
    using ConverterLibrary.Mesh;

    internal class ObjMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            string content = string.Empty;

            while (true)
            {
                int b = stream.ReadByte();

                if (b == -1)
                {
                    break;
                }

                content += b.ToString();
            }

            return new Mesh
            {
                Content = content
            };
        }

        Stream IMeshConverter.ToStream(IMesh mesh)
        {
            throw new NotSupportedException();
        }
    }
}