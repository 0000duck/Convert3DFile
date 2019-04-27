namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;

    internal interface IMesh
    {
        string Content { get; set; }

        IEnumerable<GeometricVertex> GeometricVertices { get; }

        IEnumerable<TextureVertex> TextureVertices { get; }

        IEnumerable<VertexNormal> VertexNormals { get; }

        IEnumerable<Face> Faces { get; }
    }
}