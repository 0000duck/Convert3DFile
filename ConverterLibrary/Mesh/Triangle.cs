﻿namespace ConverterLibrary.Mesh
{
    public class Triangle
    {
        public Triangle(Vertex v1, Vertex v2, Vertex v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public Vertex V1 { get; }

        public Vertex V2 { get; }

        public Vertex V3 { get; }
    }
}