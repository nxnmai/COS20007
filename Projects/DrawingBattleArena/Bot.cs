using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class Bot
    {
        private int _inkPoints; 
        private int _baseHealth;
        private Canvas _canvas;

        public Bot(Canvas canvas)
        {
            _canvas = canvas;
            _inkPoints = 100; 
            _baseHealth = 500; 
        }

        public void DrawShape()
        {
            int choice = SplashKit.Rnd(3);
            if (choice == 0 && CanDraw(10))
            {
                MyRectangle rect = new MyRectangle();
                rect.X = SplashKit.Rnd(600);
                rect.Y = SplashKit.Rnd(400);
                _canvas.AddShape(rect);
                _inkPoints -= 10;
            }
            else if (choice == 1 && CanDraw(15))
            {
                MyCircle circle = new MyCircle();
                circle.X = SplashKit.Rnd(600);
                circle.Y = SplashKit.Rnd(400);
                _canvas.AddShape(circle);
                _inkPoints -= 15;
            }
            else if (choice == 2 && CanDraw(20))
            {
                MyLine line = new MyLine();
                line.X = SplashKit.Rnd(600);
                line.Y = SplashKit.Rnd(400);
                _canvas.AddShape(line);
                _inkPoints -= 20;
            }
        }

        public bool CanDraw(int inkCost)
        {
            return _inkPoints >= inkCost;
        }

        public void TakeDamage(int damage)
        {
            _baseHealth -= damage;
            if (_baseHealth < 0)
            {
                _baseHealth = 0;
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

        public Canvas Canvas
        {
            get
            {
                return _canvas;
            }
        }
    }
}
