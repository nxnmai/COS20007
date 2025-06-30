using DrawMultipleShapes;
using SplashKitSDK;
using System;

namespace DrawMultipleShapes
{
    public class Program
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }

        public static void Main()
        {
            Window window = new Window("Draw Multiple Shapes Program", 800, 600);
            Drawing myDrawing = new Drawing();
            ShapeKind kindToAdd = ShapeKind.Circle;
            int lineCount = 0;
            int maxLines = 5; // X = 0, reassigned to 5

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                    lineCount = 0;
                }
                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                    lineCount = 0;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                    lineCount = 0;
                }

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    if (kindToAdd == ShapeKind.Line && lineCount >= maxLines)
                    {
                        // Cannot add more lines
                    }
                    else
                    {
                        Shape newShape;

                        switch (kindToAdd)
                        {
                            case ShapeKind.Circle:
                                newShape = new MyCircle();
                                break;

                            case ShapeKind.Line:
                                newShape = new MyLine();
                                lineCount++;
                                break;

                            default:
                                newShape = new MyRectangle();
                                break;
                        }

                        newShape.X = SplashKit.MouseX();
                        newShape.Y = SplashKit.MouseY();
                        myDrawing.AddShape(newShape);
                    }
                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectShapesAt(SplashKit.MousePosition());
                }

                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = SplashKit.RandomColor();
                }

                if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    foreach (Shape s in myDrawing.SelectedShapes)
                    {
                        myDrawing.RemoveShape(s);
                    }
                }

                myDrawing.Draw();
                SplashKit.RefreshScreen();
            }
            while (!window.CloseRequested);
        }
    }
}
