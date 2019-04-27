namespace ConverterLibrary.MeshOperation
{
    using System;
    using ConverterLibrary.Mesh;

    public class CalculateSurfaceAreaOperation : IMeshOperation
    {
        private readonly Action<string> log;

        public CalculateSurfaceAreaOperation(Action<string> log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        void IMeshOperation.Execute(IMesh mesh)
        {
            log($"Surface area: {mesh.GetSurfaceArea()}");
        }
    }
}