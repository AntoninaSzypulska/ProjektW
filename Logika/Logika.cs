using Dane;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;

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

            int minWaga = 50;
            int maxWaga = 160;
            int minSrednica = 25;
            int maxSrednica = 70;

            int waga = (int)(minWaga + (random.NextDouble() * (maxWaga - minWaga)));
            int srednica = (int)(minSrednica + (random.NextDouble() * (maxSrednica - minSrednica)));

            float marginX = width * 0.1f;
            float marginY = height * 0.1f;
            float randomX = marginX + (float)random.NextDouble() * (width - 2 * marginX);
            float randomY = marginY + (float)random.NextDouble() * (height - 2 * marginY);
            float randomXNext = marginX + (float)random.NextDouble() * (width - 2 * marginX);
            float randomYNext = marginY + (float)random.NextDouble() * (height - 2 * marginY);

            return new Kulka(randomX, randomY, randomXNext, randomYNext, waga, srednica);
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

        public void MoveToNextPosition(Kulka kulka)
        {
            float width = plansza.GetWidth;
            float height = plansza.GetHeight;
            float topLeftX = plansza.GettopLeftX;
            float topLeftY = plansza.GettopLeftY;

            float vectorX = kulka.XNext - kulka.fX;
            float vectorY = kulka.YNext - kulka.fY;
            float velocityX = vectorX / 100;
            float velocityY = vectorY / 100;
            float promien = kulka.Srednica / 2;

            float updatedX = kulka.X + velocityX;
            float updatedY = kulka.Y + velocityY;

            bool bounced = false;

            if (updatedX - promien < topLeftX || updatedX + promien > topLeftX + width)
            {
                velocityX = -velocityX;
                updatedX = kulka.X + velocityX;
                kulka.XNext = kulka.fX - vectorX;
                bounced = true;
            }

            if (updatedY - promien < topLeftY || updatedY + promien > topLeftY + height)
            {
                velocityY = -velocityY;
                updatedY = kulka.Y + velocityY;
                kulka.YNext = kulka.fY - vectorY;
                bounced = true;
            }

            if (bounced)
            {
                vectorX = kulka.XNext - kulka.fX;
                vectorY = kulka.YNext - kulka.fY;
                velocityX = vectorX / 100;
                velocityY = vectorY / 100;
            }

            if (kulka.X != kulka.XNext || kulka.Y != kulka.YNext)
            {
                updatedX = kulka.X + velocityX;
                updatedY = kulka.Y + velocityY;

                kulka.move(updatedX, updatedY);

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

            MoveTimer.Elapsed += MoveTimer_Elapsed;
            MoveTimer.Start();
        }

        private void MoveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
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
