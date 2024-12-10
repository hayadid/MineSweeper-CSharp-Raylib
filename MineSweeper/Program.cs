using MineSweeper;
using Raylib_cs;

public static class Program
{
    const int screenWidth = 1000;
    const int screenHeight = 1000;
    const int gridOffsetX = 100;
    const int gridOffsetY = 100;
    const int boxSize = 80;
    public static void Main(String[] args)
    {
        // Initialization
        //--------------------------------------------------------------------------------------


        Grid grid = new Grid();
        string debugText = "None";

        Raylib.InitWindow(screenWidth, screenHeight, "MineSweeper");

        Raylib.SetTargetFPS(60);


        // Main game loop
        while (!Raylib.WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            

            // Draw
            //----------------------------------------------------------------------------------
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.RayWhite);

            grid.DrawGrid(screenWidth, screenHeight, gridOffsetX, gridOffsetY, boxSize);

            if (grid.gridState == Grid.State.Over)
            {
                grid.RevealAll();
                Raylib.DrawText("GAME OVER! 'R' TO RESTART", 10, 10, 50, Color.Red);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                grid = new Grid();
            }
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                int[] posArr = MousePosToGridPos();
                grid.LeftClick(posArr[0], posArr[1]);
            }
            if (Raylib.IsMouseButtonPressed(MouseButton.Right))
            {
                int[] posArr = MousePosToGridPos();
                grid.RightClick(posArr[0], posArr[1]);
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

    public static int[] MousePosToGridPos()
    {
        int iPos = (Raylib.GetMouseX() - gridOffsetX);
        int jPos = (Raylib.GetMouseY() - gridOffsetY);
        if (iPos >= 0 && jPos >= 0) // makes sure that clicking on top left is negative
        {
            iPos /= boxSize;
            jPos /= boxSize;
        }    
        int[] gridPos = { iPos, jPos };
        return gridPos;
    }
}
