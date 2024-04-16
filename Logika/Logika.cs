using Dane;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logika
{
    public class Logika : LogikaAPI
    {
        public Plansza plansza;
        public KulkiRepository kulkiRepository;

        public Logika()
        {
            this.plansza = new Plansza();
            kulkiRepository = new KulkiRepository();
        }

        public Kulka createKulka()
        {
            int width = plansza.GetWidth;
            int height = plansza.GetHeight;
            Random random = new Random();

            float randomX = (float)random.NextDouble() * width;
            float randomY = (float)random.NextDouble() * height;

            return new Kulka(randomX, randomY);
        }

        public void create(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Kulka kulka = createKulka();
                kulkiRepository.add(kulka);
            }
        }

        public void remove()
        {
            if (kulkiRepository != null)
            {
                kulkiRepository.Clear();
            }
        }

        public void start() { 

        }

    }
}
