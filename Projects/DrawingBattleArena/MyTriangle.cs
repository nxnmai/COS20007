using SplashKitSDK;

namespace DrawingBattleArena
{
    public class MyTriangle : Shape
    {
        private const float _size = 70;
        private const float _duration = 10.0f;

        public MyTriangle() : base(0.0f, 0.0f, Color.DeepPink)
        {
            // No health, relies on duration for disappearance
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            Point2D p1 = new Point2D { X = X, Y = Y - _size / 2 };
            Point2D p2 = new Point2D { X = X - _size / 2, Y = Y + _size / 2 };
            Point2D p3 = new Point2D { X = X + _size / 2, Y = Y + _size / 2 };
            SplashKit.FillTriangle(Color.DeepPink, X, Y - (_size / 2), X - (_size / 2), Y + (_size / 2), X + (_size / 2), Y + (_size / 2));
        }

        public override bool IsAt(Point2D pt)
        {
            double x1 = X;
            double y1 = Y - _size / 2;
            double x2 = X - _size / 2;
            double y2 = Y + _size / 2;
            double x3 = X + _size / 2;
            double y3 = Y + _size / 2;
            double px = pt.X;
            double py = pt.Y;

            double area = 0.5 * Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)));

            double area1 = 0.5 * Math.Abs((px * (y2 - y3) + x2 * (y3 - py) + x3 * (py - y2)));
            double area2 = 0.5 * Math.Abs((x1 * (py - y3) + px * (y3 - y1) + x3 * (y1 - py)));
            double area3 = 0.5 * Math.Abs((x1 * (y2 - py) + x2 * (py - y1) + px * (y1 - y2)));

            return Math.Abs(area - (area1 + area2 + area3)) < 0.01;
        }

        public override void DrawOutline()
        {
            SplashKit.DrawTriangle(Color.Black, X, Y - _size / 2, X - _size / 2, Y + _size / 2, X + _size / 2, Y + _size / 2);
        }

        public float Duration
        {
            get
            {
                return _duration;
            }
        }
    }
}