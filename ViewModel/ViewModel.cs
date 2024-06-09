using Dane;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;


namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Logika.Logika logika;
        private Model.Model model;

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
        public ICommand StartButton { get; set; }

        public int BoardWidth
        {
            get { return model.GetBoardWidth(); }
        }

        public int BoardHeight
        {
            get { return model.GetBoardHeight(); }
        }

        public ViewModel()
        {
            logika = new Logika.Logika();

            ChoiceButton = new RelayCommand(CreateKulki);
            DelateButton = new RelayCommand(DelateKulki);
            StartButton = new RelayCommand(StartKulki);
        }

        private void CreateKulki(object parameter)
        {
            if (AmountChoice > 0)
            {
                logika.create(AmountChoice);

                foreach (var kulka in logika.kulkiRepository.GetKulki())
                {
                    Kulki.Add(kulka);
                }
            }
        }

        private void DelateKulki(object parameter)
        {
            logika.remove();

            Kulki.Clear();
            logika.stopLogTimer();

        }

        private void StartKulki(object parameter)
        {

            logika.start();
            logika.startLogTimer();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
