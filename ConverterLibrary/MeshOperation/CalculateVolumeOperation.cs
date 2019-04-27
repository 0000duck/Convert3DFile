namespace ConverterLibrary.MeshOperation
{
    using System;
    using ConverterLibrary.Mesh;

    public class CalculateVolumeOperation : IMeshOperation
    {
        private readonly Action<string> log;

        public CalculateVolumeOperation(Action<string> log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        void IMeshOperation.Execute(IMesh mesh)
        {
            log($"Volume: {mesh.GetVolume()}");
        }
    }
}