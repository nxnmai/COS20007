using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace DrawMultipleShapes
{
    public class MyLine : Shape
    {
        private float _endX;
        private float _endY;

        public MyLine(Color color, float x, float y) : base(color)
        {
            X = x;
            Y = y;
            _endX = x + 80;
            _endY = y + 80;
        }

        public MyLine() : this(Color.Red, 0.0f, 0.0f)
        { 
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.DrawLine(Color, X, Y, _endX, _endY);
        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, X, Y, 5);
            SplashKit.FillCircle(Color.Black, _endX, _endY, 5);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointOnLine(pt, SplashKit.LineFrom(X, Y, _endX, _endY), 10);
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
    }
}
