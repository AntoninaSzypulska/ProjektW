using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    public class Plansza
    {
        public int width = 740;
        public int height = 340;
        public int topLeftX = 0;
        public int topLeftY = 0;

        public int GetWidth
        {
            get { return width; }
        }

        public int GetHeight
        {
            get { return height; }
        }

        public int GettopLeftX
        {
            get { return topLeftX; }
        }

        public int GettopLeftY
        {
            get { return topLeftY; }
        }
    }
}
