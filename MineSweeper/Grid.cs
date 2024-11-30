using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace MineSweeper
{
    internal class Grid
    {
        private Random rand = new Random();
        Box[,] boxes = new Box[10, 10];
        public enum State
        {
            Playing,
            Over,
            Win,
            Paused
        }
        public State gridState;
        
        public Grid()
        {
            gridState = State.Playing;
            int countBombsPlaced = 0;
            int i;
            int j;
            while (countBombsPlaced < 10)
            {
                i = rand.Next(0, 10);
                j = rand.Next(0, 10);
                if (boxes[i,j] == null)
                {
                    boxes[i, j] = new Box(true);
                    countBombsPlaced++;
                }
            }

            for (i = 0; i < boxes.GetLength(0); i++)
            {
                for (j = 0; j < boxes.GetLength(1); j++)
                {
                    if (boxes[i, j] != null)
                        continue;
                    boxes[i, j] = new Box(false);
                }
            }
        }
        public void TogglePause()
        {
            if (gridState == State.Over || gridState == State.Win)
                return;
            if (gridState == State.Playing)
                gridState = State.Paused;
            gridState = State.Playing;
        }
        public void LeftClick(int i, int j)
        {
            if (gridState != State.Playing)
                return;

            Box box = boxes[i, j];

            if (box.isFlagged)
                return;

            box.Reveal();

            if (box.isBomb)
                gridState = State.Over;
        }
        public void RightClick(int i, int j)
        {
            if (gridState != State.Playing)
                return;

            Box box = boxes[i, j];

            if (!box.isHidden)
                return;

            box.ToggleFlag();
        }
        public void DrawGrid(int windowX, int windowY, int offsetX, int offsetY, int boxSize)
        {
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                for (int j = 0; j < boxes.GetLength(1); j++)
                {
                    if (boxes[i,j].isHidden)
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Gray);

                    else if (boxes[i,j].isFlagged)
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Blue);

                    else if (boxes[i,j].isBomb)
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Red);

                    else // box is empty and revealed
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Gray);

                    // border
                    Raylib.DrawRectangleLines(offsetX + boxSize * i, offsetY + boxSize * j, boxSize,boxSize, Color.Black);
                    
                }
            }
        }
    }
}