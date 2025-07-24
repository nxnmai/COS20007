using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class MyCircle : Shape
    {
        private int _radius;

        public MyCircle() : base(0.0f, 0.0f, Color.Red)
        {
            _radius = 112;
        }

        public override void Draw()
        {
            SplashKit.FillCircle(Color, X, Y, _radius);
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
            writer.WriteLine(_radius);
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

        public void PerformAction()
        {
        }
    }
}
