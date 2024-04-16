using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    public class KulkiRepository : DaneAPI
    {
        public List<Kulka> kulkiL;

        public KulkiRepository()
        {
            kulkiL = new List<Kulka>();
        }

        public KulkiRepository(List<Kulka> kulki)
        {
            kulkiL = kulki;
        }

        public void add(Kulka kulka)
        {
            kulkiL.Add(kulka);
        }

        public List<Kulka> GetKulki()
        {
            return kulkiL;
        }

        public void Clear()
        {
            kulkiL.Clear();
        }
    }
}
