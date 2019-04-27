namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;

    internal interface IMesh
    {
        IEnumerable<Vertex> GeometricVertices { get; }

        IEnumerable<TextureVertex> TextureVertices { get; }

        IEnumerable<Vector> VertexNormals { get; }

        IEnumerable<Face> Faces { get; }

        IEnumerable<Triangle> GetTriangles();

        float GetVolume();
    }
}