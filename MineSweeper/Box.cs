using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    internal class Box
    {
        public bool isBomb { get; private set; }
        public bool isHidden { get; private set; }
        public bool isFlagged { get; private set; }
        

        public Box(bool isBomb)
        {
            this.isBomb = isBomb;
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
    }
}
