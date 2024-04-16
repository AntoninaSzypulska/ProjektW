using Dane;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Logika
{
    public class Logika : LogikaAPI
    {
        public event EventHandler<KulkaMovedEventArgs> KulkaMoved;

        public Plansza plansza;
        public KulkiRepository kulkiRepository;
        public Random random = new Random();
        private readonly Timer MoveTimer;

        public Logika()
        {
            this.plansza = new Plansza();
            kulkiRepository = new KulkiRepository();
            MoveTimer = new Timer();

        }

        public Kulka createKulka()
        {
            int width = plansza.GetWidth;
            int height = plansza.GetHeight;


            float randomX = (float)random.NextDouble() * width;
            float randomY = (float)random.NextDouble() * height;
            float randomXNext = (float)random.NextDouble() * width;
            float randomYNext = (float)random.NextDouble() * height;

            return new Kulka(randomX, randomY, randomXNext, randomYNext);
        }

        public void create(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Kulka kulka = createKulka();
                kulkiRepository.add(kulka);
            }
        }

        public void MoveKulki()
        {
            foreach (Kulka kulka in kulkiRepository.GetKulki())
            {
                MoveToNextPosition(kulka);
            }
        }

        public void MoveToNextPosition(Kulka kulka) //gdzie ma być ruch zrobiony?
        {
            float nextX = kulka.XNext;
            float nextY = kulka.YNext;
            float vectorX = kulka.XNext - kulka.fX;
            float vectorY = kulka.YNext - kulka.fY;
            float velocityX = vectorX / (100);
            float velocityY = vectorY / (100);
            if (nextX - kulka.X < 10 && nextY - kulka.Y < 10)
            {
                (nextX, nextY) = NextPosition();
                kulka.XNext = nextX;
                kulka.YNext = nextY;
                kulka.fX = kulka.X;
                kulka.fY = kulka.Y;
                kulka.Speed = (float)(random.NextDouble() * (2.0f - 0.5f) + 0.5f);

                vectorX = kulka.XNext - kulka.X;
                vectorY = kulka.YNext - kulka.Y;
                velocityX = vectorX / (100);
                velocityY = vectorY / (100);
                // Wywołaj zdarzenie, że kulka została przesunięta
                /*KulkaMoved?.Invoke(this, new KulkaMovedEventArgs(kulka));*/
            }

            if (kulka.X != nextX || kulka.Y != nextY)
            {

                float updatedX = kulka.X + velocityX;
                float updatedY = kulka.Y + velocityY;

                kulka.move(updatedX, updatedY);

                // Wywołaj zdarzenie, że kulka została przesunięta
                KulkaMoved?.Invoke(this, new KulkaMovedEventArgs(kulka));
            }
        }

        public (float, float) NextPosition()
        {
            float width = plansza.GetWidth;
            float height = plansza.GetHeight;

            float xNext = (float)random.NextDouble() * width;
            float yNext = (float)random.NextDouble() * height;

            return (xNext, yNext);
        }

        public void remove()
        {
            if (kulkiRepository != null)
            {
                kulkiRepository.Clear();
            }
        }

        public void start()
        {
            MoveTimer.Interval = 10;

            // Dodanie obsługi zdarzenia Elapsed
            MoveTimer.Elapsed += MoveTimer_Elapsed;

            // Uruchomienie timera
            MoveTimer.Start();
        }

        private void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Wywołanie metody MoveKulki przy każdym cyklu timera
            MoveKulki();
        }

        public class KulkaMovedEventArgs : EventArgs
        {
            public Kulka MovedKulka { get; }

            public KulkaMovedEventArgs(Kulka kulka)
            {
                MovedKulka = kulka;
            }
        }
    }
}
