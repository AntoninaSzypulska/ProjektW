using Dane;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
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

        public ICommand ChoiceButton { get; set; }
        public ICommand DelateButton { get; set; }

        public ViewModel()
        {
            Logika.Logika logika = new Logika.Logika();
            ChoiceButton = new RelayCommand(CreateKulki);
            DelateButton = new RelayCommand(DelateKulki);
        }

        private void CreateKulki(object parameter)
        {
            if (AmountChoice > 0)
            {
                Logika.Logika logika = new Logika.Logika();
                logika.create(AmountChoice);

                foreach (var kulka in logika.kulkiRepository.GetKulki())
                {
                    Kulki.Add(kulka);
                }
            }
        }

        private void DelateKulki(object parameter)
        {
            Logika.Logika logika = new Logika.Logika();
            logika.remove();

            Kulki.Clear();

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
