using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Model.Model model;
        public ICommand ChoiceButton { get; set; }

        public int AmountChoice
        {
            get
            {
                return model.GetAmount();
            }
            set
            {
                model.SetAmount(value);
                onPropertyChanged();
            }
        }

        public ViewModel()
        {
            model = new Model.Model();
            ChoiceButton = new RelayCommand(choiceButtonExecute);
        }

        public void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void choiceButtonExecute(object o)
        {
            model.SetAmount(AmountChoice);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
