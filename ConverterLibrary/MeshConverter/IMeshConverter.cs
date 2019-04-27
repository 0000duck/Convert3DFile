namespace ConverterLibrary.MeshConverter
{
    using System.IO;
    using ConverterLibrary.Mesh;

    internal interface IMeshConverter
    {
        IMesh FromStream(Stream stream);

        Stream ToStream(IMesh mesh);
    }
}