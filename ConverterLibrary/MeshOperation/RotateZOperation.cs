namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class RotateZOperation : MeshOperationBase
    {
        private readonly float angle;

        public RotateZOperation(float angle, Action<string> log) : base(log)
        {
            this.angle = angle;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.RotateZ(angle);
            Log($"Rotation Z by {angle.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}