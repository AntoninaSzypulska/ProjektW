using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    public class Ruch
    {
        private Random random;
        private Plansza plansza;

        public Ruch(Plansza plansza)
        {
            this.plansza = plansza;
            this.random = new Random();
        }
        public (float, float) NextPosition()
        {
            float width = plansza.GetWidth;
            float height = plansza.GetHeight;

            float xNext = (float)random.NextDouble() * width;
            float yNext = (float)random.NextDouble() * height;

            return (xNext, yNext);
        }
    }
}
