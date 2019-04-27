namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class TranslateXOperation : MeshOperationBase
    {
        private readonly float delta;

        public TranslateXOperation(float delta, Action<string> log) : base(log)
        {
            this.delta = delta;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.Translate(delta, 0f, 0f);
            Log($"Translation X by {delta.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}