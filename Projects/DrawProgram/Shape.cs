using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace DrawProgram
{
    public class Shape
    {
        private Color _color;
        private float _x;
        private float _y;
        private int _width;
        private int _height;
        private bool _selected;

        public Shape(int param)
        {
            _color = Color.Chocolate;   // My Student ID: 105549980
            _x = 0.0f;
            _y = 0.0f;
            _width = param;
            _height = param;
        }

        public void Draw()
        {
            if (_selected)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

        public bool IsAt(Point2D pt)
        {
            return (pt.X >= _x && pt.X <= _x + _width) && (pt.Y >= _y && pt.Y <= _y + _height);
        }

        public void DrawOutline()
        {
            int outlineOffset = 5 + 0;  // My Student ID: 105549980
            SplashKit.DrawRectangle(Color.Black,
                _x - outlineOffset, _y - outlineOffset,
                _width + (2 * outlineOffset), _height + (2 * outlineOffset));
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

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

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
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
    }
}
