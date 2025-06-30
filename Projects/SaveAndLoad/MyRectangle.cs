using SaveAndLoad;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SaveAndLoad
{
    public class MyRectangle : Shape
    {
        private int _width;
        private int _height;

        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            X = x;
            Y = y;
            _width = width;
            _height = height;
        }

        public MyRectangle() : this(Color.Green, 0.0f, 0.0f, 100 + 80, 100 + 80)
        {
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(Color, X, Y, _width, _height);
        }

        public override void DrawOutline()
        {
            int outlineOffset = 5 + 0;
            SplashKit.DrawRectangle(Color.Black,
                X - outlineOffset, Y - outlineOffset,
                _width + (2 * outlineOffset), _height + (2 * outlineOffset));
        }

        public override bool IsAt(Point2D pt)
        {
            return (pt.X >= X && pt.X <= X + _width) && (pt.Y >= Y && pt.Y <= Y + _height);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");
            base.SaveTo(writer);
            writer.WriteLine(Width);
            writer.WriteLine(Height);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            Width = reader.ReadInteger();
            Height = reader.ReadInteger();
        }

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
    }
}
