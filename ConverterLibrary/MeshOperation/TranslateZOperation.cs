namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class TranslateZOperation : MeshOperationBase
    {
        private readonly float delta;

        public TranslateZOperation(float delta, Action<string> log) : base(log)
        {
            this.delta = delta;
        }

        public override void Execute(IMesh mesh)
        {
            mesh.Translate(0f, 0f, delta);
            Log($"Translation Z by {delta.ToString("0.00", CultureInfo.InvariantCulture)} done");
        }
    }
}