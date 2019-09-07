using System;

namespace Bridge
{
    public abstract class Shape
    {
        protected IRenderer renderer;
        public string Name { get; set; }

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public override string ToString()
        {
            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
        }
    }

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }
    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get; } = "pixels";
    }

    public class VectorSquare : Square
    {
        public VectorSquare(IRenderer renderer) : base(renderer)
        {
        }
    }

    public class RasterSquare : Square
    {
        public RasterSquare(IRenderer renderer) : base(renderer)
        {
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}
