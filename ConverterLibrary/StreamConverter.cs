namespace ConverterLibrary
{
    using System.IO;
    using ConverterLibrary.MeshConverter;

    internal class StreamConverter : IStreamConverter
    {
        private readonly IMeshConverterFactory meshConverterFactory;

        public StreamConverter(IMeshConverterFactory meshConverterFactory)
        {
            this.meshConverterFactory = meshConverterFactory;
        }

        public void Convert(Stream sourceStream, FileFormat sourceFileFormat, Stream destStream, FileFormat destFileFormat)
        {
            IMeshConverter sourceMeshConverter = meshConverterFactory.CreateMeshConverter(sourceFileFormat);
            IMeshConverter destMeshConverter = meshConverterFactory.CreateMeshConverter(destFileFormat);

            string mesh = sourceMeshConverter.FromStream(sourceStream);
            using (Stream stream = destMeshConverter.ToStream(mesh))
            {
                stream.CopyTo(destStream);
            }
        }
    }
}
