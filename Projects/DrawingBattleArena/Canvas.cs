using SplashKitSDK;
using System.Linq;

namespace DrawingBattleArena
{
    public class Canvas
    {
        private List<Shape> _shapes;
        private readonly Color _background = Color.White;
        private SplashKitSDK.Timer _moveCooldownTimer;
        private bool _canMoveShapes;

        public Canvas()
        {
            _shapes = new List<Shape>();
            _moveCooldownTimer = SplashKit.CreateTimer("moveCooldown");
            _canMoveShapes = true;
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            _shapes.Remove(shape);
        }

        public void Draw()
        {
            SplashKit.ClearScreen(_background);
            foreach (Shape shape in _shapes)
            {
                shape.Draw();
            }
            SplashKit.DrawLine(Color.Black, 400, 0, 400, 600);
        }

        public void SelectShapesAt(Point2D pt)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = shape.IsAt(pt);
            }
        }

        public void HandleShapeMovement()
        {
            if (!_canMoveShapes)
            {
                if (SplashKit.TimerTicks(_moveCooldownTimer) / 1000.0f >= 5.0f)
                {
                    _canMoveShapes = true;
                    SplashKit.ResetTimer(_moveCooldownTimer);
                }
                return;
            }

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                SelectShapesAt(SplashKit.MousePosition());
            }

            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                foreach (Shape shape in _shapes)
                {
                    if (shape.Selected)
                    {
                        Point2D mousePos = SplashKit.MousePosition();
                        shape.X = (float)mousePos.X;
                        shape.Y = (float)mousePos.Y;
                        if (shape is MyLine line)
                        {
                            float dx = (float)mousePos.X - line.X;
                            float dy = (float)mousePos.Y - line.Y;
                            line.EndX = line.X + dx * (line.EndX - line.X) / 100;
                            line.EndY = line.Y + dy * (line.EndY - line.Y) / 100;
                        }
                    }
                }
            }

            if (SplashKit.MouseUp(MouseButton.LeftButton) && _shapes.Any(s => s.Selected))
            {
                _canMoveShapes = false;
                SplashKit.StartTimer(_moveCooldownTimer);
                foreach (Shape shape in _shapes)
                {
                    shape.Selected = false;
                }
            }
        }

        public List<Shape> GetAllShapes()
        {
            return new List<Shape>(_shapes);
        }

        public List<Shape> SelectedShapes()
        {
            return _shapes.FindAll(s => s.Selected);
        }

        public Color Background
        {
            get
            {
                return _background;
            }
        }
    }
}
