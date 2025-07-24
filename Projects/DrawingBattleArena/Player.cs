using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class Player
    {
        private int _inkPoints;
        private int _baseHealth;
        private Canvas _canvas;

        public Player(Canvas canvas)
        {
            _canvas = canvas;
            _inkPoints = 100;
            _baseHealth = 500;
        }

        public bool DrawRectangle()
        {
            if (_inkPoints >= 10)
            {
                MyRectangle rect = new MyRectangle();
                rect.X = SplashKit.MouseX();
                rect.Y = SplashKit.MouseY();
                _canvas.AddShape(rect);
                _inkPoints -= 10;
                return true;
            }
            return false;
        }

        public bool DrawCircle()
        {
            if (_inkPoints >= 15)
            {
                MyCircle circle = new MyCircle();
                circle.X = SplashKit.MouseX();
                circle.Y = SplashKit.MouseY();
                _canvas.AddShape(circle);
                _inkPoints -= 15;
                return true;
            }
            return false;
        }

        public bool DrawLine()
        {
            if (_inkPoints >= 20)
            {
                MyLine line = new MyLine();
                line.X = SplashKit.MouseX();
                line.Y = SplashKit.MouseY();
                _canvas.AddShape(line);
                _inkPoints -= 20;
                return true;
            }
            return false;
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
