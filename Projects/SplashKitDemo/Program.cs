using System;
using SplashKitSDK;

namespace SplashKitDemo
{
    public class Program
    {
        public static void Main()
        {
            new Window("Test Window", 800, 600);

            SplashKit.DrawText("Student Name: Nguyen Xuan Nang Mai. Student ID: 105549980.", Color.Black, 50, 50);
            SplashKit.RefreshScreen();
            SplashKit.Delay(980);
        }
    }
}
