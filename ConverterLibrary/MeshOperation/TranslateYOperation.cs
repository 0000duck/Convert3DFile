namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class TranslateYOperation : MeshOperationBase
    {
        private readonly float delta;

        public TranslateYOperation(float delta, Action<string> log) : base(log)
        {
            this.delta = delta;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.Translate(0f, delta, 0f);
            Log($"Translation Y by {delta.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}