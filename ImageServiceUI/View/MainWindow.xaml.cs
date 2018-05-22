using ImageServiceUI.Communication;
using ImageServiceUI.DataAnalizer;
using ImageServiceUI.Messages;
using ImageServiceUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
