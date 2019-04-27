namespace ConverterLibrary.MeshConverter
{
    internal class MeshConverterFactory : IMeshConverterFactory
    {
        IMeshConverter IMeshConverterFactory.CreateMeshConverter(FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.Obj:
                    return new ObjMeshConverter();
                case FileFormat.Stl:
                    return new StlMeshConverter();
                default:
                    return null;
            }
        }
    }
}