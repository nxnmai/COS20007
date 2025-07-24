using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class MyRectangle : Shape, ICombatShape
    {
        private int _width;
        private int _height;

        public MyRectangle() : base(0.0f, 0.0f, Color.Green)
        {
            _width = 200;
            _height = 200;
        }

        public override void Draw()
        {
            SplashKit.FillRectangle(Color, X, Y, _width, _height);
        }

        public override bool IsAt(Point2D pt)
        {
            return pt.X >= X && pt.X <= X + _width &&
                   pt.Y >= Y && pt.Y <= Y + _height;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");
            base.SaveTo(writer);
            writer.WriteLine(_width);
            writer.WriteLine(_height);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _width = reader.ReadInteger();
            _height = reader.ReadInteger();
        }

        public void PerformAction()
        {

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
    }
}
