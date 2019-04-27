namespace ConverterLibrary
{
    using System.IO;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;

    internal class StreamConverter : IStreamConverter
    {
        private readonly IMeshConverterFactory meshConverterFactory;

        public StreamConverter(IMeshConverterFactory meshConverterFactory)
        {
            this.meshConverterFactory = meshConverterFactory;
        }

        void IStreamConverter.Convert(Stream sourceStream, FileFormat sourceFileFormat, Stream destStream, FileFormat destFileFormat)
        {
            IMeshConverter sourceMeshConverter = meshConverterFactory.CreateMeshConverter(sourceFileFormat);
            IMeshConverter destMeshConverter = meshConverterFactory.CreateMeshConverter(destFileFormat);

            IMesh mesh = sourceMeshConverter.FromStream(sourceStream);
            using (Stream stream = destMeshConverter.ToStream(mesh))
            {
                stream.CopyTo(destStream);
            }
        }
    }
}
