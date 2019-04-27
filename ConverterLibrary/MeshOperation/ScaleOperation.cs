namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class ScaleOperation : MeshOperationBase
    {
        private readonly float factor;

        public ScaleOperation(float factor, Action<string> log) : base(log)
        {
            this.factor = factor;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.Scale(factor);
            Log($"Scaling by {factor.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}