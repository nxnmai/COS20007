using SplashKitSDK;

namespace DrawingBattleArena
{
    public class MyCircle : Shape
    {
        private const int _radius = 50;

        public MyCircle() : base(0.0f, 0.0f, Color.Red)
        {

        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillCircle(Color.Red, X, Y, _radius);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointInCircle(pt, SplashKit.CircleAt(X, Y, _radius));
        }

        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, X, Y, _radius + 2);
        }

        public int Radius
        {
            get
            {
                return _radius;
            }
        }
    }
}
