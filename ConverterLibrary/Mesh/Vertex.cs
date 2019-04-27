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
            double radius = Math.Sqrt(Y * Y + Z * Z);
            double currentAngle = Math.Acos(Y / radius);

            if (double.IsNaN(currentAngle))
            {
                return;
            }

            // deg to rad
            double newAngle = currentAngle + (Math.PI / 180) * angle;

            Y = (float)(radius * Math.Cos(newAngle));
            Z = (float)(radius * Math.Sin(newAngle));
        }

        public void RotateY(float angle)
        {
            double radius = Math.Sqrt(Z * Z + X * X);
            double currentAngle = Math.Acos(Z / radius);

            if (double.IsNaN(currentAngle))
            {
                return;
            }

            // deg to rad
            double newAngle = currentAngle + (Math.PI / 180) * angle;

            Z = (float)(radius * Math.Cos(newAngle));
            X = (float)(radius * Math.Sin(newAngle));
        }

        public void RotateZ(float angle)
        {
            double radius = Math.Sqrt(X * X + Y * Y);
            double currentAngle = Math.Acos(X / radius);

            if (double.IsNaN(currentAngle))
            {
                return;
            }

            // deg to rad
            double newAngle = currentAngle + (Math.PI / 180) * angle;

            X = (float)(radius * Math.Cos(newAngle));
            Y = (float)(radius * Math.Sin(newAngle));
        }

        public void Scale(float factor)
        {
            X *= factor;
            Y *= factor;
            Z *= factor;
        }
    }
}