using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    internal class Box
    {
        public bool isBomb { get; private set; } = false;
        public bool isHidden { get; private set; }
        public bool isFlagged { get; private set; }
        public int num { get; private set; } // -1 if isBomb
        

        public Box(bool isBomb)
        {
            this.isBomb = isBomb;
            this.num = -1;
            isHidden = true;
        }

        public void ToggleFlag()
        {
            isFlagged = !isFlagged;
        }
        public void Reveal()
        {
            isHidden = false;
        }
        public void SetNum(int num)
        {
            this.num = num;
        }
    }
}
