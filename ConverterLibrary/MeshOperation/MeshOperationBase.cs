namespace ConverterLibrary.MeshOperation
{
    using System;
    using ConverterLibrary.Mesh;

    public abstract class MeshOperationBase : IMeshOperation
    {
        protected Action<string> Log { get; }

        protected MeshOperationBase(Action<string> log)
        {
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public abstract void Execute(IMesh mesh);
    }
}