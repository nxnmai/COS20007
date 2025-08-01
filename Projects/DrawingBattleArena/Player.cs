using SplashKitSDK;

namespace DrawingBattleArena
{
    public class Player : IObserver
    {
        private int _inkPoints;
        private int _baseHealth;
        private ShapeUpgrade _shapeUpgrade;

        public Player()
        {
            _inkPoints = 100;
            _baseHealth = 500;
            _shapeUpgrade = new ShapeUpgrade();
        }

        public Shape CreateShape(ShapeType shapeType, ShapeConfig config = null)
        {
            int cost = shapeType switch
            {
                ShapeType.Triangle => 5,
                ShapeType.Circle => 10,
                ShapeType.Rectangle => 15,
                ShapeType.Line => 20,
                _ => 0
            };

            if (_inkPoints < cost)
            {
                return null;
            }

            _inkPoints -= cost;

            return shapeType switch
            {
                ShapeType.Rectangle => new MyRectangle(),
                ShapeType.Circle => new MyCircle(),
                ShapeType.Line => new MyLine(),
                ShapeType.Triangle => new MyTriangle(),
                _ => null
            };
        }

        public void PlaceShape(Shape shape, Canvas canvas, Point2D position)
        {
            if (shape != null)
            {
                shape.X = (float)position.X;
                shape.Y = (float)position.Y;
                if (shape is MyLine line)
                {
                    line.EndX = line.X;
                    line.EndY = line.Y + 100;
                }
                shape.Owner = this;
                canvas.AddShape(shape);
            }
        }

        public void UpdateInk(int amount)
        {
            _inkPoints += amount;
            if (_inkPoints < 0)
            {
                _inkPoints = 0;
            }
        }

        public void Update(string eventType, object data)
        {
            if (eventType == "InkSurge")
            {
                _inkPoints = (int)(_inkPoints * (double)data);
            }
        }

        public int InkPoints
        {
            get
            {
                return _inkPoints;
            }
            set
            {
                _inkPoints = value;
            }
        }

        public int BaseHealth
        {
            get
            {
                return _baseHealth;
            }
            set
            {
                _baseHealth = value;
            }
        }

        public ShapeUpgrade ShapeUpgrade
        {
            get
            {
                return _shapeUpgrade;
            }
        }
    }
}
