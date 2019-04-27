namespace ConverterLibrary.MeshConverter
{
    using System.IO;

    internal interface IMeshConverter
    {
        string FromStream(Stream stream);

        Stream ToStream(string mesh);
    }
}