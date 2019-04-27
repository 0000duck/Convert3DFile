namespace ConverterLibrary.MeshConverter
{
    internal class MeshConverterFactory : IMeshConverterFactory
    {
        public IMeshConverter CreateMeshConverter(FileFormat fileFormat)
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