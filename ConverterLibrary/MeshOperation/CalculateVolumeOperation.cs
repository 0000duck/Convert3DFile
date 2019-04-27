namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class CalculateVolumeOperation : MeshOperationBase
    {
        public CalculateVolumeOperation(Action<string> log) : base(log)
        { }

        public override void Execute(IMesh mesh)
        {
            Log($"Volume: {mesh.GetVolume().ToString("0.00", CultureInfo.InvariantCulture)}");
        }
    }
}