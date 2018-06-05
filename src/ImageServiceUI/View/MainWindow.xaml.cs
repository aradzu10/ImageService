using ImageServiceUI.ViewModel;
using System.Windows;

namespace ImageServiceUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();          
        }
    }
}
