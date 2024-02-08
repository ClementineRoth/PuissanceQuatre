using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PuissanceQuatre.Model
{
    internal class Grid
    {
        private static readonly int DEFAULT_HEIGHT = 6;
        private static readonly int DEFAULT_WIDTH = 7;

        public int WidthSize { get; init; }
        public int HeightSize { get; init; }


        public int[,] Tab { get; private set; }

        public Grid()
        {
            WidthSize = DEFAULT_WIDTH;
            HeightSize = DEFAULT_HEIGHT;

            this.InitTab();
        }
        public Grid(int width, int height)
        {
            WidthSize = width;
            HeightSize = height;

            this.InitTab();
        }

        public void InitTab()
        {
            this.Tab = new int[HeightSize, WidthSize];
        }

        public void PlaceToken(int color, int position)
        {
            this.Tab[0,position] = color;
            for(int i = 1; i < this.Tab.GetLength(0); i++)
            {
                if (Tab[i, position] == 0)
                {
                    Tab[i-1, position] = 0;
                    Tab[i, position] = color;
                }
                else break;
            }
        }
        public bool CheckAlignement(int color)
        {
            for(int i = this.HeightSize-1; i>=0; i--)
            {
                for(int j = 0;j < this.Tab.GetLength(1); j++)
                {           
                    if (Tab[i,j]==color 
                        && (CheckLineAlignement(color,i,j) 
                            || CheckCollonneAlignement(color, i, j)
                            || CheckDiagonalAlignement(color, i, j)))
                        return true;
                }
                
            }
            return false;
        }
        private bool CheckLineAlignement(int color, int heightPos,int widthPos)
        {
            if (widthPos >= WidthSize - 3) 
                return false;

            int val1 = this.Tab[heightPos, widthPos + 1];
            int val2 = this.Tab[heightPos, widthPos + 2];
            int val3 = this.Tab[heightPos, widthPos + 3];

            return TestVals(color, val1, val2, val3);
        }

        private bool CheckCollonneAlignement(int color, int heightPos, int widthPos)
        {
            if (heightPos <= 3) 
                return false;

            int val1 = this.Tab[heightPos - 1, widthPos];
            int val2 = this.Tab[heightPos - 2, widthPos];
            int val3 = this.Tab[heightPos - 3, widthPos];

            return TestVals(color, val1, val2, val3);
        }

        private bool CheckDiagonalAlignement(int color, int heightPos, int widthPos)
        {
            if (heightPos <= 3)
                return false;

            if(widthPos < this.WidthSize - 3)
            {
                int val1 = this.Tab[heightPos - 1, widthPos + 1];
                int val2 = this.Tab[heightPos - 2, widthPos + 2];
                int val3 = this.Tab[heightPos - 3, widthPos + 3];

                if (TestVals(color, val1, val2, val3)) return true;
            }

            if(widthPos > 3)
            {
                int val1 = this.Tab[heightPos - 1, widthPos - 1];
                int val2 = this.Tab[heightPos - 2, widthPos - 2];
                int val3 = this.Tab[heightPos - 3, widthPos - 3];

                if (TestVals(color, val1, val2, val3)) return true;
            }

            return false;
        }
        private bool TestVals(int color, int val1, int val2, int val3)
        {
            return val1 == color && val2 == color && val3 == color;
        }

        public bool CheckPlacementInCollonne(int position)
        {
            if (this.Tab[0, position] != 0)
            {
                Console.WriteLine("la collonne est pleine");
                return false;
            }
            return true;
        }
    }
}
