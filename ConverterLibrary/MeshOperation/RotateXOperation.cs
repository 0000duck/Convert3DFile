namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class RotateXOperation : MeshOperationBase
    {
        private readonly float angle;

        public RotateXOperation(float angle, Action<string> log) : base(log)
        {
            this.angle = angle;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.RotateX(angle);
            Log($"Rotation X by {angle.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}