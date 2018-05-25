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
            
            //LogGrid.Items.Add(logMessage);
            /* 
            --- client test --- 
            Client client = new Client(6145);
            client.ConnectToServer();
            string obj = client.ReadFromServer();
            Settings setting = Settings.Deserialize(obj);
            int x = 5; */
        }
    }
}
