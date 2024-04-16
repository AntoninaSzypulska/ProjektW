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
        public Random random = new Random();
        //private Ruch ruch; //emm

        public Logika()
        {
            this.plansza = new Plansza();
            kulkiRepository = new KulkiRepository();
            //this.ruch = new Ruch(plansza); //emm
        }

        public Kulka createKulka()
        {
            int width = plansza.GetWidth;
            int height = plansza.GetHeight;


            float randomX = (float)random.NextDouble() * width;
            float randomY = (float)random.NextDouble() * height;
            //float randomXNew = (float)random.NextDouble() * width;
            //float randomYNew = (float)random.NextDouble() * height;

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

        public void MoveKulki()
        {
            foreach (Kulka kulka in kulkiRepository.GetKulki())
            {
                MoveToNextPosition(kulka);
            }
        }

        public void MoveToNextPosition(Kulka kulka) //gdzie ma być ruch zrobiony?
        {
            /*(float newX, float newY) = ruch.NextPosition();*/
            float nextX = kulka.getXNext(), nextY = kulka.getYNext();

            if (nextX == kulka.getX() && nextY == kulka.getY())
            {
                (nextX, nextY) = NextPosition();
                kulka.setXNext(nextX);
                kulka.setYNext(nextY);
                kulka.setSpeed((float)(random.NextDouble() * (2.0f - 0.5f) + 0.5f));
            }
            if (kulka.getX() != nextX || kulka.getY() != nextY)
            {
                float vektorX = kulka.getXNext() - kulka.getX();
                float vectorY = kulka.getYNext() - kulka.getY();
                float velocityX = vektorX / ((float) kulka.getSpeed() * 100);
                float velocityY = vectorY / (kulka.getSpeed() * 100);

                float updatedX = kulka.getX() + velocityX;
                float updatedY = kulka.getY() + velocityY;

                kulka.move(updatedX, updatedY);

                /*System.Threading.Thread.Sleep(50);*/
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

        }
    }
}
