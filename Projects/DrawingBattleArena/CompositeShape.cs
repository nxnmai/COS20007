using SplashKitSDK;

namespace DrawingBattleArena
{
    public class CompositeShape : Shape
    {
        private List<Shape> _shapes;
        private int _combinedHealth;

        public CompositeShape(float x, float y, Color color) : base(x, y, color)
        {
            _shapes = new List<Shape>();
            _combinedHealth = 50;
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
            shape.Owner = this.Owner;
            _combinedHealth += shape.Health; 
        }

        public override void Draw()
        {
            foreach (var shape in _shapes)
            {
                shape.Draw();
            }
            if (Selected)
            {
                DrawOutline();
            }
        }

        public override bool IsAt(Point2D pt)
        {
            return _shapes.Any(shape => shape.IsAt(pt));
        }

        public override void DrawOutline()
        {
            foreach (var shape in _shapes)
            {
                shape.DrawOutline();
            }
        }

        public int Health
        {
            get
            {
                return _combinedHealth;
            }
            set
            {
                _combinedHealth = value;
            }
        }

        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}