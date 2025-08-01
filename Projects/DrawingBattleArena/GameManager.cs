using SplashKitSDK;
using System.Linq;

namespace DrawingBattleArena
{
    public class GameManager
    {
        private Player _player;
        private Bot _bot;
        private Canvas _canvas;
        private EventManager _eventManager;
        private SplashKitSDK.Timer _gameTimer;
        private SplashKitSDK.Timer _botAttackTimer;
        private SplashKitSDK.Timer _gameElapsedTimer;
        private SplashKitSDK.Timer _inkRegenTimer;
        private ShapeType? _selectedShapeType;
        private List<(MyCircle circle, float creationTime, float age, bool exploding, object owner)> _circles;
        private List<(MyTriangle triangle, float creationTime, float age)> _triangles;
        private CombatStrategy _combatStrategy;
        private string _cautionMessage;
        private float _cautionTime;
        private float _cautionAge;

        // UI button positions & sizes
        private readonly Rectangle _rectButton = new Rectangle { X = 10, Y = 10, Width = 80, Height = 30 };
        private readonly Rectangle _circleButton = new Rectangle { X = 100, Y = 10, Width = 80, Height = 30 };
        private readonly Rectangle _lineButton = new Rectangle { X = 190, Y = 10, Width = 80, Height = 30 };
        private readonly Rectangle _triangleButton = new Rectangle { X = 280, Y = 10, Width = 80, Height = 30 };
        private readonly Rectangle _upgradeButton = new Rectangle { X = 370, Y = 10, Width = 80, Height = 30 };

        public GameManager()
        {
            _player = new Player();
            _bot = new Bot();
            _canvas = new Canvas();
            _eventManager = new EventManager();
            _gameTimer = SplashKit.CreateTimer("gameTimer");
            _botAttackTimer = SplashKit.CreateTimer("botAttackTimer");
            _gameElapsedTimer = SplashKit.CreateTimer("gameElapsedTimer");
            _inkRegenTimer = SplashKit.CreateTimer("inkRegenTimer");
            _selectedShapeType = null;
            _circles = new List<(MyCircle, float, float, bool, object)>();
            _triangles = new List<(MyTriangle, float, float)>();
            _combatStrategy = new CombatStrategy();
            _cautionMessage = null;
            _cautionTime = 0;
            _cautionAge = 0;
            SplashKit.StartTimer(_gameTimer);
            SplashKit.StartTimer(_botAttackTimer);
            SplashKit.StartTimer(_gameElapsedTimer);
            SplashKit.StartTimer(_inkRegenTimer);
            _eventManager.AddObserver(this);
            _eventManager.AddObserver(_player);
            _eventManager.AddObserver(_bot);
            _eventManager.AddObserver(_canvas);

        }

        public void StartGame()
        {
            _player.InkPoints = 100;
            _player.BaseHealth = 500;
            _bot.InkPoints = 100;
            _bot.BaseHealth = 500;
            _canvas = new Canvas();
            _eventManager.AddObserver(_canvas);
            _selectedShapeType = null;
            _circles.Clear();
            _triangles.Clear();
            _cautionMessage = null;
            _cautionTime = 0;
            _cautionAge = 0;
            SplashKit.ResetTimer(_gameElapsedTimer);
            SplashKit.ResetTimer(_inkRegenTimer);
        }

        public void Update()
        {
            float deltaTime = SplashKit.TimerTicks(_gameTimer) / 1000.0f;
            float elapsedTime = SplashKit.TimerTicks(_gameElapsedTimer) / 1000.0f;
            Console.WriteLine("Delta time: " + deltaTime + ", Elapsed time: " + elapsedTime);
            _eventManager.Update();

            if (SplashKit.TimerTicks(_inkRegenTimer) >= 1000.0f)
            {
                int inkRegen = 5;
                Console.WriteLine($"Ink regeneration: {inkRegen} points");
                _player.UpdateInk(inkRegen);
                _bot.UpdateInk(inkRegen);
                SplashKit.ResetTimer(_inkRegenTimer);
            }

            if (SplashKit.TimerTicks(_botAttackTimer) / 1000.0f >= _bot.AttackFrequency)
            {
                Shape shape = _bot.CreateRandomShape();
                if (shape != null)
                {
                    _canvas.AddShape(shape);
                    if (shape is MyCircle circle)
                        _circles.Add((circle, elapsedTime, 0f, false, _bot));
                    else if (shape is MyTriangle triangle)
                        _triangles.Add((triangle, elapsedTime, 0f));
                }
                SplashKit.ResetTimer(_botAttackTimer);
            }

            for (int i = _circles.Count - 1; i >= 0; i--)
            {
                var (circle, creationTime, age, exploding, owner) = _circles[i];
                age += deltaTime;
                Console.WriteLine($"Circle {i} age: {age}, creationTime: {creationTime}, exploding: {exploding}");

                if (age >= 2.0f && !exploding)
                {
                    bool blocked = false;
                    int damage = 20;
                    foreach (Shape target in _canvas.GetAllShapes())
                    {
                        if (target == circle)
                        {
                            continue;
                        }
                        object targetOwner = (target.X < 400) ? (object)_player : _bot;
                        if (owner != targetOwner && IsCollision(circle, target))
                        {
                            _combatStrategy.ApplyCombat(circle, target, owner, targetOwner, _canvas, ref damage, ref blocked);
                        }
                    }

                    if (!blocked && damage > 0)
                    {
                        if (owner is Player)
                        {
                            _bot.BaseHealth -= damage;
                            Console.WriteLine("Bot health reduced to " + _bot.BaseHealth);
                        }
                        else if (owner is Bot)
                        {
                            _player.BaseHealth -= damage;
                            Console.WriteLine("Player health reduced to " + _player.BaseHealth);
                        }
                    }

                    _circles[i] = (circle, creationTime, age, true, owner);
                }

                else if (age >= 2.5f && exploding)
                {
                    _canvas.RemoveShape(circle);
                    _circles.RemoveAt(i);
                    Console.WriteLine("Circle removed at age " + age);
                }
                else
                {
                    _circles[i] = (circle, creationTime, age, exploding, owner);    // Update age
                }
            }

            for (int i = _triangles.Count - 1; i >= 0; i--)
            {
                var (triangle, creationTime, age) = _triangles[i];
                age += deltaTime;
                Console.WriteLine($"Triangle {i} age: {age}, creationTime: {creationTime}");
                if (age >= triangle.Duration)
                {
                    _canvas.RemoveShape(triangle);
                    _triangles.RemoveAt(i);
                    Console.WriteLine("Triangle removed at age " + age);
                }
                else
                {
                    _triangles[i] = (triangle, creationTime, age);    // Update age
                }
            }

            if (_cautionMessage != null)
            {
                _cautionAge += deltaTime;
                Console.WriteLine($"Caution age: {_cautionAge}");
                if (_cautionAge >= 2.0f)
                {
                    _cautionMessage = null;
                    _cautionAge = 0;    // Reset for next caution message
                    Console.WriteLine("Caution message cleared");
                }
            }

            SplashKit.ResetTimer(_gameTimer);
        }

        private bool IsCollision(Shape shape1, Shape shape2)
        {
            if (shape1 is MyCircle circle && shape2 is MyRectangle rect)
            {
                float dx = Math.Max(0, Math.Abs(circle.X - rect.X - rect.Width / 2) - rect.Width / 2);
                float dy = Math.Max(0, Math.Abs(circle.Y - rect.Y - rect.Height / 2) - rect.Height / 2);
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                return distance < circle.Radius;
            }

            else if (shape1 is MyCircle circle2 && shape2 is MyLine line)
            {
                float minX = Math.Min(line.X, line.EndX);
                float maxX = Math.Max(line.X, line.EndX);
                float minY = Math.Min(line.Y, line.EndY);
                float maxY = Math.Max(line.Y, line.EndY);
                float closestX = Math.Max(minX, Math.Min(maxX, circle2.X));
                float closestY = Math.Max(minY, Math.Min(maxY, circle2.Y));
                float dx = circle2.X - closestX;
                float dy = circle2.Y - closestY;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                return distance < circle2.Radius;
            }

            else if (shape1 is MyCircle circle3 && shape2 is MyCircle circle4)
            {
                float dx = circle3.X - circle4.X;
                float dy = circle3.Y - circle4.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                return distance < (circle3.Radius + circle4.Radius);
            }
            return false;
        }

        public void Draw()
        {
            _canvas.Draw();

            SplashKit.FillRectangle(_selectedShapeType == ShapeType.Rectangle ? Color.Green : Color.Gray, _rectButton.X, _rectButton.Y, _rectButton.Width, _rectButton.Height);
            SplashKit.FillRectangle(_selectedShapeType == ShapeType.Circle ? Color.Green : Color.Gray, _circleButton.X, _circleButton.Y, _circleButton.Width, _circleButton.Height);
            SplashKit.FillRectangle(_selectedShapeType == ShapeType.Line ? Color.Green : Color.Gray, _lineButton.X, _lineButton.Y, _lineButton.Width, _lineButton.Height);
            SplashKit.FillRectangle(_selectedShapeType == ShapeType.Triangle ? Color.Green : Color.Gray, _triangleButton.X, _triangleButton.Y, _triangleButton.Width, _rectButton.Height);
            SplashKit.FillRectangle(Color.Gray, _upgradeButton.X, _upgradeButton.Y, _upgradeButton.Width, _rectButton.Height);


            SplashKit.DrawText("Rectangle", Color.Black, _rectButton.X + 5, _rectButton.Y + 10);
            SplashKit.DrawText("Circle", Color.Black, _circleButton.X + 15, _circleButton.Y + 10);
            SplashKit.DrawText("Line", Color.Black, _lineButton.X + 20, _lineButton.Y + 10);
            SplashKit.DrawText("Triangle", Color.Black, _triangleButton.X + 20, _triangleButton.Y + 10);
            SplashKit.DrawText("Upgrade", Color.Black, _upgradeButton.X + 20, _upgradeButton.Y + 10);


            foreach (var (circle, creationTime, age, exploding, _) in _circles)
            {
                if (exploding)
                {
                    if (age < 2.5f)
                    {
                        SplashKit.FillCircle(Color.RGBAColor(255, 0, 0, 128), circle.X, circle.Y, circle.Radius * 2);
                    }
                }
            }

            SplashKit.DrawText($"Player Ink: {_player.InkPoints}", Color.Black, 10, 50);
            SplashKit.DrawText($"Selected: {_selectedShapeType?.ToString() ?? "None"}", Color.Black, 10, 70);
            SplashKit.DrawText($"Upgrade Cost: {_player.ShapeUpgrade.Cost}", Color.Black, 10, 90);

            SplashKit.FillRectangle(Color.Green, 10, 130, 100 * (_player.BaseHealth / 500.0f), 10);
            SplashKit.DrawText("Player's health: " + _player.BaseHealth, Color.Black, 120, 130);
            SplashKit.FillRectangle(Color.Red, 410, 130, 100 * (_bot.BaseHealth / 500.0f), 10);
            SplashKit.DrawText("Bot's health: " + _bot.BaseHealth, Color.Black, 520, 130);

            if (_cautionMessage != null)
            {
                SplashKit.DrawText(_cautionMessage, Color.Red, 300, 300);
            }

            if (_player.BaseHealth <= 0 || _bot.BaseHealth <= 0)
            {
                string winner = _player.BaseHealth <= 0 ? "Bot wins!" : "Player wins!";
                SplashKit.FillRectangle(Color.RGBAColor(0, 0, 0, 128), 0, 0, 800, 600);
                SplashKit.DrawText("Game Over: " + winner, Color.White, 300, 280);
            }
        }

        public void HandleInput()
        {
            Point2D mousePos = SplashKit.MousePosition();
            float elapsedTime = SplashKit.TimerTicks(_gameElapsedTimer) / 1000.0f;

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Console.WriteLine("Mouse clicked detected");

                if (SplashKit.PointInRectangle(mousePos, _rectButton))
                {
                    _selectedShapeType = ShapeType.Rectangle;
                    Console.WriteLine("Selected Rectangle");
                }
                else if (SplashKit.PointInRectangle(mousePos, _circleButton))
                {
                    _selectedShapeType = ShapeType.Circle;
                    Console.WriteLine("Selected Circle");
                }
                else if (SplashKit.PointInRectangle(mousePos, _lineButton))
                {
                    _selectedShapeType = ShapeType.Line;
                    Console.WriteLine("Selected Line");
                }
                else if (SplashKit.PointInRectangle(mousePos, _triangleButton))
                {
                    _selectedShapeType = ShapeType.Triangle;
                }
                else if (SplashKit.PointInRectangle(mousePos, _upgradeButton))
                {
                    var selectedShapes = _canvas.SelectedShapes;
                    if (selectedShapes.Any())
                    {
                        foreach (var shape in selectedShapes)
                        {
                            if (_player.ShapeUpgrade.Upgrade(_player, shape))
                            {
                                _cautionMessage = $"Upgraded {shape.GetType().Name} to level {_player.ShapeUpgrade.Level}";
                            }
                            else
                            {
                                _cautionMessage = $"Not enough ink to upgrade!";
                            }
                            _cautionTime = elapsedTime;
                            _cautionAge = 0;
                        }
                    }
                }

                else if (_selectedShapeType != null)
                {
                    bool validPlacement = (_selectedShapeType == ShapeType.Circle && mousePos.X >= 400) ||
                        (_selectedShapeType == ShapeType.Rectangle && mousePos.X < 400) ||
                        (_selectedShapeType == ShapeType.Line && mousePos.X < 400);

                    if (validPlacement)
                    {
                        Shape shape = _player.CreateShape(_selectedShapeType.Value);
                        if (shape != null)
                        {
                            _player.PlaceShape(shape, _canvas, mousePos);
                            if (shape is MyCircle circle)
                            {
                                _circles.Add((circle, elapsedTime, 0f, false, _player));
                            }
                            else if (shape is MyTriangle triangle)
                            {
                                _triangles.Add((triangle, elapsedTime, 0f));
                            }
                            else if (shape is MyRectangle && _canvas.SelectedShapesAt().Any(s => s is MyLine))
                            {
                                var composite = new CompositeShape(mousePos.X, mousePos.Y, Color.Purple);
                                composite.AddShape(shape);
                                composite.AddShape(_canvas.SelectedShapes().First(s => s is MyLine));
                                _canvas.AddShape(composite);
                                _canvas.RemoveShape(_canvas.SelectedShapes().First(s => s is MyLine));
                            }
                        }
                        Console.WriteLine($"Placed {shape.GetType().Name} at {mousePos.X}, {mousePos.Y}");

                        else
                        {
                            _cautionMessage = "Not enough ink!";
                            _cautionTime = elapsedTime;
                            _cautionAge = 0;
                        }
                    }

                    else
                    {
                        _cautionMessage = $"Cannot place {_selectedShapeType} here!";
                        _cautionTime = elapsedTime;
                        _cautionAge = 0;
                        //_cautionTime = SplashKit.TimerTicks(_gameTimer) / 1000.0f;
                    }
                }
            }

            _canvas.HandleShapeMovement();

            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                SplashKit.CloseWindow("Drawing Battle Arena");
            }
        }

        public void Update(string eventType, object data)
        {
            if (eventType == "InkSurge")
            {
                _cautionMessage = "Ink surge! Ink gain doubled!";
                _cautionTime = SplashKit.TimerTicks(_gameElapsedTimer) / 1000.0f;
                _cautionAge = 0;
            }
            else if (eventType == "Bot Frenzy")
            {
                _cautionMessage = "Bot frenzy! Increased bot attacks!";
                _cautionTime = SplashKit.TimerTicks(_gameElapsedTimer) / 1000.0f;
                _cautionAge = 0;
            }
        }
    }
}


