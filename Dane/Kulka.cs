using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dane
{
    public class Kulka : INotifyPropertyChanged
    {
        private float firstX;
        private float firstY;
        private float xK;
        private float yK;
        private float xNext;
        private float yNext;
        private float speed;
        
        private Random random;
        private Plansza plansza;

        public Kulka(float x, float y, float nx, float xy)
        {
            this.firstX = x;
            this.firstY = y;
            this.xK = x;
            this.yK = y;
            this.xNext = x;
            this.yNext = y;
        }

        public float X
        {
            get { return xK; }
            set
            {
                if (xK != value)
                {
                    xK = value;
                    OnPropertyChanged(nameof(X));
                }
            }
        }

        public float Y
        {
            get { return yK; }
            set
            {
                if (yK != value)
                {
                    yK = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }

        public float XNext
        {
            get { return xNext; }
            set
            {
                if (xNext != value)
                {
                    xNext = value;
                    OnPropertyChanged(nameof(XNext));
                }
            }
        }

        public float YNext
        {
            get { return yNext; }
            set
            {
                if (yNext != value)
                {
                    yNext = value;
                    OnPropertyChanged(nameof(YNext));
                }
            }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                if (speed != value)
                {
                    speed = value;
                    OnPropertyChanged(nameof(Speed));
                }
            }
        }

        public float fX
        {
            get { return firstX; }
            set
            {
                if(firstX != value)
                {
                    firstX = value;
                    OnPropertyChanged(nameof(fX));
                }
            }
        }

        public float fY
        {
            get { return firstY; }
            set
            {
                if (firstY != value)
                {
                    firstY = value;
                    OnPropertyChanged(nameof(fY));
                }
            }
        }

        //co zrobiłem
        public void move(float newX, float newY)
        {
            X = newX;
            Y = newY;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
