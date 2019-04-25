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
using Connecting;
using System.Configuration;
using SQLCommands;


namespace PMM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            Conn.CreateDatabase(connString);
            Commands.InsertInotDB(connString, 1000, "First", DateTime.Now, DateTime.Now, "Bulki");
        }

        public MainWindow()
        {
            InitializeComponent();
            GetConnection();
        }
    }
}
