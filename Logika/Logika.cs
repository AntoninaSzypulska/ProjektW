using Dane;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly System.Timers.Timer MoveTimer;
        private readonly List<(Kulka, Kulka)> collisionPairs;
        private DataLogger dataLogger;
        private System.Timers.Timer logTimer;

        public Logika()
        {
            this.plansza = new Plansza();
            kulkiRepository = new KulkiRepository();
            MoveTimer = new System.Timers.Timer();
            collisionPairs = new List<(Kulka, Kulka)>();
            dataLogger = new DataLogger();
        }
     

        private void setTimer()
        {
            logTimer = new System.Timers.Timer(500);
            logTimer.Elapsed += onTimedEvent;
            logTimer.AutoReset = true;
            logTimer.Enabled = true;
        }

        private void onTimedEvent(Object source, ElapsedEventArgs e)
        {
            LogKulkiData();
        }

        public Kulka createKulka()
        {

            int width = plansza.GetWidth;
            int height = plansza.GetHeight;

            int minWaga = 50;
            int maxWaga = 160;
            int minSrednica = 25;
            int maxSrednica = 70;

            int waga = (int)((minWaga + (random.NextDouble() * (maxWaga - minWaga))));
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
                collision = IsColliding(newKulka);
            } while (collision);

            return new Kulka(randomX, randomY, randomXNext, randomYNext, waga, srednica);
        }

        public bool IsColliding(Kulka newKulka)
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
            lock (kulkiRepository) { 
                foreach (Kulka kulka in kulkiRepository.GetKulki())
                {
                    MoveToNextPosition(kulka);
                }
            }
        }

        public async Task MoveToNextPosition(Kulka kulka)
        {
            lock (kulkiRepository)
            {
                
                int width = plansza.GetWidth;
                int height = plansza.GetHeight;

                int topLeftX = plansza.GettopLeftX;
                int topLeftY = plansza.GettopLeftY;

                float promien = kulka.Srednica / 2;

                float updatedX = kulka.X + kulka.VelocityX;
                float updatedY = kulka.Y + kulka.VelocityY;

                bool bounced = false;

                if (updatedX < topLeftX + promien || updatedX > topLeftX + width - promien)
                {
                    kulka.VelocityX = -kulka.VelocityX;
                    updatedX = kulka.X + kulka.VelocityX;
                    bounced = true;
                }

                if (updatedY < topLeftY + promien || updatedY > topLeftY + height - promien)
                {
                    kulka.VelocityY = -kulka.VelocityY;
                    updatedY = kulka.Y + kulka.VelocityY;
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

                                odbicie(kulka, innaKulka);
                                updatedX = kulka.X + kulka.VelocityX;
                                updatedY = kulka.Y + kulka.VelocityY;
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

                    }
                KulkaMoved?.Invoke(this, new KulkaMovedEventArgs(kulka));
            }
        }

        public void odbicie(Kulka kulka1, Kulka kulka2)
        {
            float dx = kulka2.X - kulka1.X;
            float dy = kulka2.Y - kulka1.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (true)
            {
                float nx = dx / distance;
                float ny = dy / distance;

                float tx = -ny;
                float ty = nx;

                float dpTan1 = kulka1.VelocityX * tx + kulka1.VelocityY * ty;
                float dpTan2 = kulka2.VelocityX * tx + kulka2.VelocityY * ty;

                float dpNorm1 = kulka1.VelocityX * nx + kulka1.VelocityY * ny;
                float dpNorm2 = kulka2.VelocityX * nx + kulka2.VelocityY * ny;

                float m1 = (float)((dpNorm1 * (kulka1.Waga - kulka2.Waga) + 2.0 * kulka2.Waga * dpNorm2) / (kulka1.Waga + kulka2.Waga));
                float m2 = (float)((dpNorm2 * (kulka2.Waga - kulka1.Waga) + 2.0 * kulka1.Waga * dpNorm1) / (kulka1.Waga + kulka2.Waga));

                kulka1.VelocityX = tx * dpTan1 + nx * m1;
                kulka1.VelocityY = ty * dpTan1 + ny * m1;
                kulka2.VelocityX = tx * dpTan2 + nx * m2;
                kulka2.VelocityY = ty * dpTan2 + ny * m2;


                float overlap = (float)(0.5 * (distance - kulka1.Srednica / 2 - kulka2.Srednica / 2));
                kulka1.X -= overlap * nx;
                kulka1.Y -= overlap * ny;
                kulka2.X += overlap * nx;
                kulka2.Y += overlap * ny;
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


        public void LogKulkiData()
        {
            var kulkiData = kulkiRepository.GetKulki().Select(kulka => new
            {
                kulka.X,
                kulka.Y,
                Date = DateTime.Now
            });

            dataLogger.LogData(kulkiData);
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
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(10);
                    MoveKulki();
                }
            });
        }

        public void startLogTimer()
        {
            setTimer();
            logTimer.Start();
        }

        public void stopLogTimer()
        {
            logTimer.Stop();
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
