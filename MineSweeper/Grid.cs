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
                    boxes[i,j] = new Box(false);
                }
            }
            for (i = 0; i < boxes.GetLength(0); i++)
            {
                for (j = 0; j < boxes.GetLength(1); j++)
                {
                    if (!boxes[i,j].isBomb)
                        boxes[i, j].SetNum(GetNumOfBombs(i, j));

                }
            }
        }

        private int GetNumOfBombs(int iBox, int jBox) // calculates number of bombs surrounding an empty box
        {
            int numOfBombs = 0;

            for (int i = iBox - 1; i <= iBox + 1; i++)
            {
                for (int j = jBox - 1; j <= jBox + 1; j++)
                {

                    if (i >= 0 && i < 10 && j >= 0 && j < 10)
                    {
                        if (boxes[i, j].isBomb)
                            numOfBombs++;
                    }
                }
            }
            return numOfBombs;
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
            if (IsOutOfBounds(i, j))
                return;

            Box box = boxes[i, j];

            if (box.isFlagged)
                return;

            if (box.isBomb)
                gridState = State.Over;

            if (box.num == 0)
                RevealNeighbors(i, j);
            else
                box.Reveal();
        }

        private void RevealNeighbors(int i, int j)
        {
            if (i < 0 || i > 9 || j < 0 || j > 9)
                return;
            if (boxes[i, j].num != 0)
                return;
            if (!boxes[i, j].isHidden)
                return;

            boxes[i, j].Reveal();

            RevealNeighbors(i + 1, j);
            RevealNeighbors(i, j + 1);
            RevealNeighbors(i - 1, j);
            RevealNeighbors(i, j - 1);
        }

        public void RightClick(int i, int j)
        {
            if (gridState != State.Playing)
                return;
            if (IsOutOfBounds(i, j))
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
                    if (boxes[i, j].isFlagged)
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Blue);

                    else if (boxes[i, j].isHidden)
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Gray);

                    else if (boxes[i, j].isBomb)
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.Red);

                    else // box is empty and revealed
                    {
                        Raylib.DrawRectangle(offsetX + boxSize * i, offsetY + boxSize * j, boxSize, boxSize, Color.White);
                        Raylib.DrawText(boxes[i, j].num.ToString(), offsetX + boxSize * i, offsetY + boxSize * j, 50, Color.Green);

                    }
                    // border
                    Raylib.DrawRectangleLines(offsetX + boxSize * i, offsetY + boxSize * j, boxSize,boxSize, Color.Black);
                    
                }
            }
        }
        public void RevealAll()
        {
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                for (int j = 0; j < boxes.GetLength(1); j++)
                {
                    if (boxes[i, j].isFlagged && !boxes[i,j].isBomb)
                        boxes[i, j].ToggleFlag();
                    boxes[i, j].Reveal();
                }
            }
        }
        public bool IsOutOfBounds(int i, int j)
        {
            return !(i >= 0 && i < 10 && j >= 0 && j < 10);
        }
    }
}