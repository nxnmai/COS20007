using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class MyLine : Shape
    {
        private float _endX, _endY;

        public MyLine() : base(0.0f, 0.0f, Color.Yellow)
        {
            _endX = X;
            _endY = Y + 200;
        }

        public override void Draw()
        {
            SplashKit.DrawLine(Color, X, Y, _endX, _endY);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointOnLine(pt, SplashKit.LineFrom(X, Y, _endX, _endY), 5);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Line");
            base.SaveTo(writer);
            writer.WriteLine(_endX);
            writer.WriteLine(_endY);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _endX = reader.ReadSingle();
            _endY = reader.ReadSingle();
        }

        public float EndX
        {
            get
            {
                return _endX;
            }
            set
            {
                _endX = value;
            }
        }

        public float EndY
        {
            get
            {
                return _endY;
            }
            set
            {
                _endY = value;
            }
        }

        public void PerformAction()
        {

        }
    }
}
