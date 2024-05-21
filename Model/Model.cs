using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Dane;
using Logika;

namespace Model
{
    public class Model : INotifyPropertyChanged
    {
        private Logika.Logika logika;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Kulka> kulki = new ObservableCollection<Kulka>();
        public ObservableCollection<Kulka> Kulki
        {
            get { return kulki; }
            set { kulki = value; OnPropertyChanged(); }
        }

        private int amountChoice;
        public int AmountChoice
        {
            get { return amountChoice; }
            set { amountChoice = value; OnPropertyChanged(); }
        }

        public Model(Logika.Logika logika)
        {
            this.logika = logika;
        }

        public int GetBoardWidth()
        {
            return logika.plansza.GetWidth;
        }

        public int GetBoardHeight()
        {
            return logika.plansza.GetHeight;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}