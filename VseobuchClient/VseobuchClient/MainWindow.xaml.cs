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
using VseobuchDB.DB;

namespace VseobuchClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            
        }

        private void UploadFile(object sender, RoutedEventArgs e)
        {           
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if((bool)openFileDialog.ShowDialog())
            {
                ConnectionDb db = new ConnectionDb("Students1");
                string path = openFileDialog.FileName;
                if (((string)((MenuItem)e.Source).Tag).Contains("school"))
                {
                    readwriteExel.readStudent_in_School(path);
                }
                else if (((string)((MenuItem)e.Source).Tag).Contains("building"))
                {
                    readwriteExel.readStudent_in_Building(path);
                }
            }
        }
    }
}
