namespace ConverterLibrary.MeshConverter
{
    using System.IO;
    using ConverterLibrary.Mesh;

    internal class TxtMeshConverter : IMeshConverter
    {
        IMesh IMeshConverter.FromStream(Stream stream)
        {
            return new Mesh
            {
                Content = StreamHelper.GetStringFromStream(stream)
            };
        }

        Stream IMeshConverter.ToStream(IMesh mesh)
        {
            return StreamHelper.GetStreamFromString(mesh?.Content);
        }
    }
}