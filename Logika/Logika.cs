using Dane;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
        private readonly List<(Kulka, Kulka)> collisionPairs;

        public Logika()
        {
            this.plansza = new Plansza();
            kulkiRepository = new KulkiRepository();
            MoveTimer = new Timer();
            collisionPairs = new List<(Kulka, Kulka)>();
        }

        public Kulka createKulka()
        {
            int width = plansza.GetWidth;
            int height = plansza.GetHeight;

            int minWaga = 50;
            int maxWaga = 160;
            int minSrednica = 25;
            int maxSrednica = 70;

            int waga = (int)((minWaga + (random.NextDouble() * (maxWaga - minWaga))) / 2);
            int srednica = (int)(minSrednica + (random.NextDouble() * (maxSrednica - minSrednica)));

            float marginX = width * 0.1f;
            float marginY = height * 0.1f;
            float randomX, randomY, randomXNext, randomYNext;
            bool collision;

            do
            {
                randomX = marginX + (float)random.NextDouble() * (width - 2 * marginX);
                randomY = marginY + (float)random.NextDouble() * (height - 2 * marginY);
                randomXNext = marginX + (float)random.NextDouble() * (width - 2 * marginX);
                randomYNext = marginY + (float)random.NextDouble() * (height - 2 * marginY);

                Kulka newKulka = new Kulka(randomX, randomY, randomXNext, randomYNext, waga, srednica);
                collision = IsCollidingWithExistingKulkas(newKulka);
            } while (collision);

            return new Kulka(randomX, randomY, randomXNext, randomYNext, waga, srednica);
        }

        private bool IsCollidingWithExistingKulkas(Kulka newKulka)
        {
            foreach (var existingKulka in kulkiRepository.GetKulki())
            {
                float dx = newKulka.X - existingKulka.X;
                float dy = newKulka.Y - existingKulka.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                if (distance < newKulka.Srednica / 2 + existingKulka.Srednica / 2)
                {
                    return true;
                }
            }
            return false;
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

            if (updatedX - promien <= topLeftX || updatedX + promien >= topLeftX + width)
            {
                kulka.XNext = kulka.fX - vectorX;
                vectorX = kulka.XNext - kulka.fX;
                velocityX = vectorX / 100;
                updatedX = kulka.X + velocityX;
                bounced = true;
            }

            if (updatedY - promien <= topLeftY || updatedY + promien >= topLeftY + height)
            {
                kulka.YNext = kulka.fY - vectorY;
                vectorY = kulka.YNext - kulka.fY;
                velocityY = vectorY / 100;
                updatedY = kulka.Y + velocityY;
                bounced = true;
            }

            foreach (Kulka innaKulka in kulkiRepository.GetKulki())
            {
                if (innaKulka != kulka)
                {
                    float dx = updatedX - innaKulka.X;
                    float dy = updatedY - innaKulka.Y;
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                    var pair = (kulka, innaKulka);
                    var reversePair = (innaKulka, kulka);

                    if (distance < promien + innaKulka.Srednica / 2)
                    {
                        if (!collisionPairs.Contains(pair) && !collisionPairs.Contains(reversePair))
                        {

                            kulka.XNext = kulka.X;
                            kulka.YNext = kulka.Y;
                            innaKulka.XNext = innaKulka.X;
                            innaKulka.YNext = innaKulka.Y;

                            collisionPairs.Add(pair);
                        }
                    }
                    else
                    {
                        collisionPairs.Remove(pair);
                        collisionPairs.Remove(reversePair);
                    }
                }
            }

            if (kulka.X != kulka.XNext || kulka.Y != kulka.YNext)
            {
                kulka.move(updatedX, updatedY);
                KulkaMoved?.Invoke(this, new KulkaMovedEventArgs(kulka));
            }
        }


        /*if (kulka.X != kulka.XNext || kulka.Y != kulka.YNext)
        {
            updatedX = kulka.X + velocityX;
            updatedY = kulka.Y + velocityY;

            kulka.move(updatedX, updatedY);

            KulkaMoved?.Invoke(this, new KulkaMovedEventArgs(kulka));
        }*/




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
