using System.Windows;
using Blackjack.Engine.ViewModels;

namespace Blackjack.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EnvironmentVM _envViewModel = new EnvironmentVM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _envViewModel;
            _envViewModel.Setup();
        }

        private void hitBtn_Click(object sender, RoutedEventArgs e)
        {
            _envViewModel.Hit();
        }

        private void standBtn_Click(object sender, RoutedEventArgs e)
        {
            _envViewModel.Stand();
        }

        private void dealBtn_Click(object sender, RoutedEventArgs e)
        {
            _envViewModel.Deal();
        }
    }
}
