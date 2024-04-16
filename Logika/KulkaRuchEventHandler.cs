using Dane;
using System;
using System.Threading;

namespace Logika
{
    public class KulkaRuchEventHandler
    {
        private Kulka kulka;
        public event EventHandler<KulkaRuchEventHandler> KulkaRuchInfo;
        public KulkiRepository kulkiRepository;

        public KulkaRuchEventHandler(Kulka kulka, KulkiRepository kulkiRepository)
        {
            this.kulka = kulka;
            this.kulkiRepository = kulkiRepository;
        }

        private void OnPositionChange(KulkaRuchEventHandler movement)
        {
            KulkaRuchInfo(this, movement);
        }

        public void RuchKulka()
        {

            OnPositionChange(this);

            Thread.Sleep(10);
        }

    }
}
