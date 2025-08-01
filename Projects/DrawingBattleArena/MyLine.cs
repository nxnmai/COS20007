using SplashKitSDK;

namespace DrawingBattleArena
{
    public class MyLine : Shape
    {
        private const float _length = 100;
        private float _endX;
        private float _endY;
        private int _health;
        private const float _reflectionFactor = 0.5f;

        public MyLine() : base(0.0f, 0.0f, Color.Gold)
        {
            _endX = X;
            _endY = Y + _length;
            _health = 10;
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.DrawLine(Color.Gold, X, Y, _endX, _endY);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointOnLine(pt, SplashKit.LineFrom(X, Y, _endX, _endY), 5);
        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, X, Y, 5);
            SplashKit.FillCircle(Color.Black, _endX, _endY, 5);
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

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        public float ReflectionFactor
        {
            get
            {
                return _reflectionFactor;
            }
        }
    }
}
