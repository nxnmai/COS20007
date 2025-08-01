using SplashKitSDK;

namespace DrawingBattleArena
{
    public class MyRectangle : Shape
    {
        private const int _width = 100;
        private const int _height = 100;
        private int _health;

        public MyRectangle() : base(0.0f, 0.0f, Color.Green)
        {
            _health = 40;
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(Color.Green, X, Y, _width, _height);
        }

        public override void DrawOutline()
        {
            SplashKit.DrawRectangle(Color.Black, X - 2, Y - 2, _width + 4, _height + 4);
        }

        public override bool IsAt(Point2D pt)
        {
            return pt.X >= X && pt.X <= X + _width &&
                   pt.Y >= Y && pt.Y <= Y + _height;
        }

        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Height
        {
            get
            {
                return _height;
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
    }
}
