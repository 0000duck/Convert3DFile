namespace ConverterLibrary.MeshConverter
{
    internal class MeshConverterFactory : IMeshConverterFactory
    {
        IMeshConverter IMeshConverterFactory.CreateMeshConverter(FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.Txt:
                    return new TxtMeshConverter();
                default:
                    return null;
            }
        }
    }
}