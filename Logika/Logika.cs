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
        private Ruch ruch; //emm

        public Logika()
        {
            this.plansza = new Plansza();
            kulkiRepository = new KulkiRepository();
            this.ruch = new Ruch(plansza); //emm
        }

        public Kulka createKulka()
        {
            int width = plansza.GetWidth;
            int height = plansza.GetHeight;
            Random random = new Random();

            float randomX = (float)random.NextDouble() * width;
            float randomY = (float)random.NextDouble() * height;

            float randomXNew = (float)random.NextDouble() * width;
            float randomYNew = (float)random.NextDouble() * height;

            return new Kulka(randomX, randomY, randomXNew, randomYNew);
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
            (float newX, float newY) = ruch.NextPosition();

            while (kulka.X != newX || kulka.Y != newY)
            {
                //kierunek ruchu dla osi X
                float dx = Math.Sign(newX - kulka.X) * 0.1f; 

                //kierunek ruchu dla osi Y
                float dy = Math.Sign(newY - kulka.Y) * 0.1f; 

                float updatedX = kulka.X + dx;
                float updatedY = kulka.Y + dy;

                kulka.move(updatedX, updatedY);

                System.Threading.Thread.Sleep(50);
            }
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

        }
    }
}
