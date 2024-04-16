using System;

namespace Dane
{
    public class Kulka
    {
        private float xK;
        private float yK;
        private float xNext;
        private float yNext;

        public Kulka(float x, float y, float x2, float y2)
        {
            this.xK = x;
            this.yK = y;
            this.xNext = x2;
            this.yNext = y2;
        }

        public float X => xK;   //do odczytu

        public float Y => yK;

        public float XNext => xNext;

        public float YNext => yNext;

        public void move(float newX, float newY)
        {
            this.xK = newX;
            this.yK = newY;
        }
    }



}
