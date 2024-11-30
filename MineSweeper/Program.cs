using MineSweeper;
using Raylib_cs;

public static class Program
{
    public static void Main(String[] args)
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 1000;
        const int screenHeight = 1000;
        const int gridOffsetX = 100;
        const int gridOffsetY = 100;
        const int boxSize = 80;


        Grid grid = new Grid();

        Raylib.InitWindow(screenWidth, screenHeight, "MineSweeper");

        Raylib.SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
                                               //--------------------------------------------------------------------------------------

        // Main game loop
        while (!Raylib.WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            // TODO: Update your variables here
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.RayWhite);

            grid.DrawGrid(screenWidth, screenHeight, gridOffsetX, gridOffsetY, boxSize);

            if (Raylib.IsMouseButtonPressed(0))
            {
                grid.LeftClick();
            }

            Raylib.EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        Raylib.CloseWindow();        // Close window and OpenGL context
                                     //--------------------------------------------------------------------------------------

        return;
    }
}
