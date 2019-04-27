namespace ConverterLibrary.Mesh
{
    using System.Collections.Generic;

    internal class Face
    {
        public IEnumerable<FaceElement> FaceElements { get; }

        public Face(IEnumerable<FaceElement> faceElements)
        {
            FaceElements = faceElements;
        }
    }
}