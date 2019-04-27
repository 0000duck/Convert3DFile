namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;

    public interface IMesh
    {
        IEnumerable<Vertex> GeometricVertices { get; }

        IEnumerable<TextureVertex> TextureVertices { get; }

        IEnumerable<Vector> VertexNormals { get; }

        IEnumerable<Face> Faces { get; }

        IEnumerable<Triangle> GetTriangles();

        float GetVolume();

        float GetSurfaceArea();

        void Translate(float x, float y, float z);

        void RotateX(float degrees);

        void RotateY(float degrees);

        void RotateZ(float degrees);

        void Scale(float factor);
    }
}