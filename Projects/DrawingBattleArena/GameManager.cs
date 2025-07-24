using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace DrawingBattleArena
{
    public class GameManager
    {
        private Player _player;
        private Bot _bot;
        private Canvas _canvas;
        private int _gameTime;
        private const int MAX_TIME = 120;

        public GameManager()
        {
            _canvas = new Canvas();
            _player = new Player(_canvas);
            _bot = new Bot(_canvas);
            _gameTime = 0;
        }

        public void Update()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if (_player.DrawRectangle()) { }
                else if (_player.DrawCircle()) { }
                else if (_player.DrawLine()) { }
            }

            if (SplashKit.Rnd(100) < 20)
            {
                _bot.DrawShape();
            }

            foreach (Shape playerShape in _player.Canvas.Shapes)
            {
                if (playerShape is ICombatShape playerCombatShape)
                {
                    foreach (Shape botShape in _bot.Canvas.Shapes)
                    {
                        if (botShape.IsAt(new Point2D { X = playerShape.X, Y = playerShape.Y }))
                        {
                            playerCombatShape.PerformAction();
                            if (botShape is ICombatShape botCombatShape)
                            {
                                botCombatShape.PerformAction();
                            }
                            if (botShape is MyRectangle || botShape is MyLine)
                            {
                                _bot.TakeDamage(10);    // Example of bot taking damage
                            }
                        }
                    }
                }
            }

            foreach (Shape botShape in _bot.Canvas.Shapes)
            {
                if (botShape is ICombatShape botCombatShape)
                {
                    foreach (Shape playerShape in _player.Canvas.Shapes)
                    {
                        if (playerShape.IsAt(new Point2D { X = botShape.X, Y = botShape.Y }))
                        {
                            botCombatShape.PerformAction();
                            if (playerShape is ICombatShape playerCombatShape)
                            {
                                playerCombatShape.PerformAction();
                            }
                            if (playerShape is MyRectangle || playerShape is MyLine)
                            {
                                _player.TakeDamage(10); // Example of player taking damage
                            }
                        }
                    }
                }
            }

            if (SplashKit.TimerTicks("GameTimer") >= 1000)
            {
                _gameTime++;
                SplashKit.ResetTimer("GameTimer");
            }

            _canvas.Draw();
        }

        public bool IsGameOver()
        {
            return _gameTime >= MAX_TIME || _player.BaseHealth <= 0 || _bot.BaseHealth <= 0;
        }

        public string GetWinner()
        {
            if (_player.BaseHealth <= 0 && _bot.BaseHealth <= 0)
            {
                return "Draw";
            }
            else if (_player.BaseHealth <= 0)
            {
                return "Bot wins!";
            }
            else if (_bot.BaseHealth <= 0)
            {
                return "Player wins!";
            }
            else if (_gameTime >= MAX_TIME)
            {
                return _player.BaseHealth > _bot.BaseHealth ? "Player wins!" : "Bot wins!";
            }
            return null;
        }

        public void Run()
        {
            SplashKit.OpenWindow("Drawing Battle Arena", 800, 600);
            SplashKit.CreateTimer("GameTimer");
            while (!IsGameOver())
            {
                Update();
                SplashKit.ProcessEvents();
                SplashKit.Delay(16);    // Approximately 60 FPS
            }
            string winner = GetWinner();
            if (winner != null)
            {
                SplashKit.ClearScreen();
                SplashKit.DrawText(winner, SplashKit.ColorBlack(), 300, 300);
                SplashKit.RefreshScreen();
                SplashKit.Delay(5000);  // Show result for 5 seconds
            }
            SplashKit.FreeAllTimers();
        }
    }
}
