namespace ConverterLibrary.MeshOperation
{
    using System;
    using System.Globalization;
    using ConverterLibrary.Mesh;

    public class CalculateSurfaceAreaOperation : MeshOperationBase
    {
        public CalculateSurfaceAreaOperation(Action<string> log) : base(log)
        { }

        public override void Execute(IMesh mesh)
        {
            Log($"Surface area: {mesh.GetSurfaceArea().ToString("0.00", CultureInfo.InvariantCulture)}");
        }
    }
}