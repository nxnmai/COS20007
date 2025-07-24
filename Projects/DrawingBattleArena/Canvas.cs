using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class Canvas
    {
        private List<Shape> _shapes;
        private Color _background;

        public Canvas()
        {
            _shapes = new List<Shape>();
            _background = SplashKit.ColorWhite();
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
            SplashKit.RefreshScreen();
        }

        public void SaveTo(StreamWriter writer)
        {
            writer.WriteLine(_shapes.Count);
            foreach (Shape shape in _shapes)
            {
                if (shape is MyRectangle)
                {
                    writer.WriteLine("Rectangle");
                }
                else if (shape is MyCircle)
                {
                    writer.WriteLine("Circle");
                }
                else if (shape is MyLine)
                {
                    writer.WriteLine("Line");
                }
                shape.SaveTo(writer);
            }
        }

        public void LoadFrom(StreamReader reader)
        {
            _shapes.Clear();
            int shapeCount = reader.ReadInteger();
            for (int i = 0; i < shapeCount; i++)
            {
                string shapeType = reader.ReadLine();
                Shape shape = null;
                if (shapeType == "Rectangle")
                {
                    shape = new MyRectangle();
                }
                else if (shapeType == "Circle")
                {
                    shape = new MyCircle();
                }
                else if (shapeType == "Line")
                {
                    shape = new MyLine();
                }
                if (shape != null)
                {
                    shape.LoadFrom(reader);
                    _shapes.Add(shape);
                }
            }
        }

        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }

        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }
    }
}
