using Dane;
using System;

namespace Logika
{
    public class Handler    //próba stworzenia EvenyHandler dla ruchu kulek
    {
        private Kulka kulka;
        public event EventHandler<KulkaRuchEventArgs> KulkaRuchInfo;
        public KulkiRepository kulkiRepository;

        public Handler(Kulka kulka, KulkiRepository kulkiRepository)
        {
            this.kulka = kulka;
            this.kulkiRepository = kulkiRepository;
        }

        public void RuchKulka(float newX, float newY)
        {
            float originalX = kulka.X;
            float originalY = kulka.Y;

            kulka.move(newX, newY);

            if (originalX != newX || originalY != newY)
            {
                OnPositionChange(newX, newY);
            }
        }

        protected virtual void OnPositionChange(float newX, float newY)
        {
            KulkaRuchEventArgs eventArgs = new KulkaRuchEventArgs(newX, newY);

            KulkaRuchInfo?.Invoke(this, eventArgs);
        }
    }

    public class KulkaRuchEventArgs : EventArgs
    {
        public float NewX { get; }
        public float NewY { get; }

        public KulkaRuchEventArgs(float newX, float newY)
        {
            this.NewX = newX;
            this.NewY = newY;
        }
    }
}
