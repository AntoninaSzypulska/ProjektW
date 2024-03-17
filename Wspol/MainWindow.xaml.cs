using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;


namespace Wspol
{
    public partial class MainWindow : Window
    {
        private NumberGenerator numberGenerator = new NumberGenerator();

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = numberGenerator.GenerateRandomNumber();
            ResultText.Text = $"Random number: {randomNumber}";
        }
    }
}
