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
    public class MyCircle : Shape
    {
        private int _radius;

        public MyCircle(Color color, int radius) : base(color)
        {
            _radius = radius;
        }

        public MyCircle() : this(Color.Blue, 50 + 80)
        {
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillCircle(Color, X, Y, _radius);
        }

        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, X, Y, _radius + 2);
        }

        public override bool IsAt(Point2D pt)
        {
            double distance = Math.Sqrt(Math.Pow(pt.X - X, 2) + Math.Pow(pt.Y - Y, 2));
            return distance <= _radius;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(Radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            Radius = reader.ReadInteger();
        }

        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }
    }
}
