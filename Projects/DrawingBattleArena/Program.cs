using SplashKitSDK;

namespace DrawingBattleArena
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                SplashKit.OpenWindow("Drawing Battle Arena", 800, 600);
                Console.WriteLine("Window opened, starting game...");

                GameManager gameManager = new GameManager();
                gameManager.StartGame();

                while (!SplashKit.WindowCloseRequested("Drawing Battle Arena"))
                {
                    SplashKit.ProcessEvents();

                    gameManager.Update();
                    gameManager.HandleInput();
                    gameManager.Draw();

                    SplashKit.RefreshScreen(60);
                }

                SplashKit.CloseWindow("Drawing Battle Arena");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
