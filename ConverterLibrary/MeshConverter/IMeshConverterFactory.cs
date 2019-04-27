namespace ConverterLibrary.MeshConverter
{
    internal interface IMeshConverterFactory
    {
        IMeshConverter CreateMeshConverter(FileFormat fileFormat);
    }
}