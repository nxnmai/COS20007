namespace DrawingBattleArena
{
    public class Bot
    {
        private int _baseHealth;
        private int _inkPoints;
        private Random _random;
        private double _attackFrequency;

        public Bot()
        {
            _baseHealth = 500;
            _inkPoints = 100;
            _random = new Random();
            _attackFrequency = 3.0;
        }

        public Shape CreateRandomShape()
        {
            ShapeType[] types = { ShapeType.Rectangle, ShapeType.Circle, ShapeType.Line, ShapeType.Triangle };
            ShapeType type = types[_random.Next(types.Length)];
            int cost = type switch
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

            Shape shape = type switch
            {
                ShapeType.Triangle => new MyTriangle(),
                ShapeType.Rectangle => new MyRectangle(),
                ShapeType.Circle => new MyCircle(),
                ShapeType.Line => new MyLine(),
                _ => null
            };

            if (shape != null)
            {
                _inkPoints -= cost;
                if (type == ShapeType.Circle)
                {
                    shape.X = (float)_random.NextDouble() * 400;
                }
                else
                {
                    shape.X = 400 + (float)_random.NextDouble() * 400;
                }

                shape.Y = (float)_random.NextDouble() * 600;

                if (shape is MyLine line)
                {
                    line.EndX = line.X;
                    line.EndY = line.Y + 100;
                }
                shape.Owner = this;
            }
            return shape;
        }

        public void PerformAttack(Canvas canvas)
        {
            Shape shape = CreateRandomShape();
            if (shape != null)
            {
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
            if (eventType == "BotFrenzy")
            {
                _attackFrequency *= (double)data;
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

        public double AttackFrequency
        { 
            get
            {
                return _attackFrequency;
            }
        }
    }
}
