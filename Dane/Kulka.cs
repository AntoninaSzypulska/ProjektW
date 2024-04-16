using System;

namespace Dane
{
    public class Kulka
    {
        private float xK;
        private float yK;

        public Kulka(float x, float y)
        {
            this.xK = x;
            this.yK = y;
        }

        public float X => xK;   //do odczytu

        public float Y => yK;
    }



}
