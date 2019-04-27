namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class RotateYOperation : MeshOperationBase
    {
        private readonly float angle;

        public RotateYOperation(float angle, Action<string> log) : base(log)
        {
            this.angle = angle;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.RotateY(angle);
            Log($"Rotation Y by {angle.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}