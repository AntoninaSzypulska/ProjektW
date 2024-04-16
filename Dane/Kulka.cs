using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    public class Kulka
    {
        private float xK;
        private float yK;
        private float xNext;
        private float yNext;
        private float speed;
        private Random random;
        private Plansza plansza;

        public Kulka(float x, float y)
        {
            this.xK = x;
            this.yK = y;
        }

        public float X => xK;   //do odczytu

        public float Y => yK;

        public float XNext => xNext;

        public float YNext => yNext;


        //co zrobiłem
        public void move(float newX, float newY)
        {
            xK = newX;
            yK = newY;
        }

        public float getX () { return xK;}
        public float getY () { return yK;}
        public void setX (float newK) {  xK = newK; }
        public void setY (float newY) {  yK = newY; }
        public float getXNext () { return xNext;}
        public float getYNext () { return yNext;}
        public void setXNext (float newXNext) {  xNext = newXNext; }
        public void setYNext (float newYNext) {  yNext = newYNext; }
        public float getSpeed () { return speed;}
        public void setSpeed (float speed) {  this.speed = speed; }

    }



}
