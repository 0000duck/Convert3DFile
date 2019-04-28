namespace ConverterLibrary.Mesh
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public class Vertex
    {
        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; private set; }

        public float Y { get; private set; }

        public float Z { get; private set; }

        public void Translate(float x, float y, float z)
        {
            X += x;
            Y += y;
            Z += z;
        }

        public void RotateX(float angle)
        {
            // deg to rad
            double angleRad = (Math.PI / 180.0) * angle;

            float newY = (float)(Math.Cos(angleRad) * Y - Math.Sin(angleRad) * Z);
            float newZ = (float)(Math.Sin(angleRad) * Y + Math.Cos(angleRad) * Z);

            Y = newY;
            Z = newZ;
        }

        public void RotateY(float angle)
        {
            // deg to rad
            double angleRad = (Math.PI / 180.0) * angle;

            float newZ = (float)(Math.Cos(angleRad) * Z - Math.Sin(angleRad) * X);
            float newX = (float)(Math.Sin(angleRad) * Z + Math.Cos(angleRad) * X);

            Z = newZ;
            X = newX;
        }

        public void RotateZ(float angle)
        {
            // deg to rad
            double angleRad = (Math.PI / 180.0) * angle;

            float newX = (float)(Math.Cos(angleRad) * X - Math.Sin(angleRad) * Y);
            float newY = (float)(Math.Sin(angleRad) * X + Math.Cos(angleRad) * Y);

            X = newX;
            Y = newY;
        }

        public void Scale(float factor)
        {
            X *= factor;
            Y *= factor;
            Z *= factor;
        }
    }
}