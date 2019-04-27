namespace ConverterLibrary.MeshConverter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using ConverterLibrary.Mesh;

    internal class ObjMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            Mesh mesh = new Mesh();

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
            if (lineParts.Length < 3)
            {
                return;
            }

            if (!float.TryParse(lineParts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float u) ||
                !float.TryParse(lineParts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float v))
            {
                return;
            }

            TextureVertex textureVertex = new TextureVertex(u, v);

            mesh.TextureVertices.Add(textureVertex);
        }

        private static void ProcessVertexNormal(string[] lineParts, Mesh mesh)
        {
            if (lineParts.Length < 4)
            {
                return;
            }

            if (!float.TryParse(lineParts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float i) ||
                !float.TryParse(lineParts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float j) ||
                !float.TryParse(lineParts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float k))
            {
                return;
            }

            VertexNormal vertexNormal = new VertexNormal(i, j, k);

            mesh.VertexNormals.Add(vertexNormal);
        }

        private static void ProcessFace(string[] lineParts, Mesh mesh)
        {
            if (lineParts.Length < 4)
            {
                return;
            }

            List<FaceElement> faceElements = new List<FaceElement>();

            for (int i = 1; i < lineParts.Length; i++)
            {
                string linePart = lineParts[i];

                string[] referenceNumberParts = linePart.Split('/');
                if (referenceNumberParts.Length == 3)
                {
                    GeometricVertex v = GetExistingVertex(referenceNumberParts[0], mesh.GeometricVertices);
                    TextureVertex vt = GetExistingVertex(referenceNumberParts[1], mesh.TextureVertices);
                    VertexNormal vn = GetExistingVertex(referenceNumberParts[2], mesh.VertexNormals);

                    faceElements.Add(new FaceElement(v, vt, vn));
                }
                else if (referenceNumberParts.Length == 1)
                {
                    GeometricVertex v = GetExistingVertex(referenceNumberParts[0], mesh.GeometricVertices);

                    faceElements.Add(new FaceElement(v, null, null));
                }
                else
                {
                    return;
                }
            }

            mesh.Faces.Add(new Face(faceElements));
        }

        private static T GetExistingVertex<T>(string referenceNumber, List<T> vertices) where T : class
        {
            if (string.IsNullOrEmpty(referenceNumber))
            {
                return null;
            }

            if (!int.TryParse(referenceNumber, NumberStyles.Integer, CultureInfo.InvariantCulture, out int index))
            {
                return null;
            }

            // one based to zero based
            return vertices[index - 1];
        }
    }
}