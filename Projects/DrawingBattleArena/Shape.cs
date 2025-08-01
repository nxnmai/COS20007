using SplashKitSDK;

namespace DrawingBattleArena
{
    public abstract class Shape
    {
        private float _x, _y;
        private Color _color;
        private bool _selected;
        private int _health;

        public Shape(float x, float y, Color color)
        {
            _x = x;
            _y = y;
            _color = color;
            _selected = false;
            _health = 20; // Default health value
        }

        public abstract void Draw();

        public abstract bool IsAt(Point2D pt);

        public abstract void DrawOutline();

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        public object Owner
        {
            get;
            set;
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
