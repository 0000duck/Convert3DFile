namespace ConverterLibrary.MeshConverter
{
    using System;
    using System.Globalization;
    using System.IO;
    using ConverterLibrary.Mesh;

    internal class ObjMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            Mesh mesh = new Mesh
            {
                Content = "1234"
            };

            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(line?.Trim()) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    string[] lineParts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    ProcessLine(lineParts, mesh);
                }
            }

            return mesh;
        }

        Stream IMeshConverter.ToStream(IMesh mesh)
        {
            throw new NotSupportedException();
        }

        private void ProcessLine(string[] lineParts, Mesh mesh)
        {
            switch (lineParts[0])
            {
                case "v":
                    ProcessGeometricVertex(lineParts, mesh);
                    break;
                case "vt":
                    ProcessTextureVertex(lineParts, mesh);
                    break;
                case "vn":
                    ProcessVertexNormal(lineParts, mesh);
                    break;
                case "f":
                    ProcessFace(lineParts, mesh);
                    break;
            }
        }

        private static void ProcessGeometricVertex(string[] lineParts, Mesh mesh)
        {
            if (lineParts.Length < 4)
            {
                return;
            }

            if (!float.TryParse(lineParts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) ||
                !float.TryParse(lineParts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) ||
                !float.TryParse(lineParts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
            {
                return;
            }

            GeometricVertex geometricVertex = new GeometricVertex(x, y, z);

            mesh.GeometricVertices.Add(geometricVertex);
        }

        private static void ProcessTextureVertex(string[] lineParts, Mesh mesh)
        {
        }

        private static void ProcessVertexNormal(string[] lineParts, Mesh mesh)
        {
        }

        private static void ProcessFace(string[] lineParts, Mesh mesh)
        {
        }
    }
}