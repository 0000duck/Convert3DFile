namespace ConverterLibrary
{
    using System.Collections.Generic;
    using System.IO;
    using ConverterLibrary.Mesh;
    using ConverterLibrary.MeshConverter;
    using ConverterLibrary.MeshOperation;

    internal class StreamConverter : IStreamConverter
    {
        private readonly IMeshConverterFactory meshConverterFactory;

        public StreamConverter(IMeshConverterFactory meshConverterFactory)
        {
            this.meshConverterFactory = meshConverterFactory;
        }

        void IStreamConverter.Convert(
            Stream sourceStream,
            FileFormat sourceFileFormat,
            Stream destStream,
            FileFormat destFileFormat,
            IEnumerable<IMeshOperation> meshOperations)
        {
            IMeshConverter sourceMeshConverter = meshConverterFactory.CreateMeshConverter(sourceFileFormat);
            IMeshConverter destMeshConverter = meshConverterFactory.CreateMeshConverter(destFileFormat);

            IMesh mesh = sourceMeshConverter.FromStream(sourceStream);

            foreach (IMeshOperation meshOperation in meshOperations)
            {
                meshOperation.Execute(mesh);
            }

            using (Stream stream = destMeshConverter.ToStream(mesh))
            {
                stream.CopyTo(destStream);
            }
        }
    }
}
