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
using VseobuchDB;
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
            ConnectionDb db = new ConnectionDb("Students");
            
            City city = new City();
            city = db.GetCity()[0];
            itemStart.Tag = city;
            itemStart.Items.Add("*");
            ShowTreeView(itemStart);
           // db.
        }

        private void UploadFile2(object sender, RoutedEventArgs e)
        {
            ConnectionDb db = new ConnectionDb("Students");
            dataGrid.ItemsSource = db.NotFoundStudent();
        }

        private void UploadFile(object sender, RoutedEventArgs e)
        {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                if ((bool)openFileDialog.ShowDialog())
                {
                    ConnectionDb db = new ConnectionDb("Students");
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

        private void itemStart_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeItem = new TreeViewItem();
            treeItem = (TreeViewItem)e.Source;                             
            ShowTreeView(treeItem);            
        }

        private void ShowTreeView(TreeViewItem treeItem)
        {
            ConnectionDb db = new ConnectionDb("Students");
            TreeViewItem item;
            treeItem.Items.Clear();
            if(treeItem.Tag is City)
            {                
                item = new TreeViewItem();
                item.Header = ((City)treeItem.Tag).Name;                
                item.Tag = db.GetDistrict();
                item.Items.Add("*");
                treeItem.Items.Add(item);
                return;
            }
            if(treeItem.Tag is List<District>)
            {
                foreach(District d in (List<District>)treeItem.Tag)
                {
                    item = new TreeViewItem();
                    item.Header = d.Name;
                    item.Tag = db.GetSchoolInDistrict(d.ID);
                    item.Items.Add("*");
                    treeItem.Items.Add(item);                    
                }
                return;
            }
            if(treeItem.Tag is List<School>)
            {
                foreach(School s in (List<School>)treeItem.Tag)
                {
                    item = new TreeViewItem();
                    item.Header = s.Name;
                    item.Tag = s.ID.ToString();
                    item.Items.Add("*");
                    treeItem.Items.Add(item);                    
                }
                return;
            }
            if(treeItem.Tag is string)
            {
                List<Student> students = db.GetSudentsinSchool(Convert.ToInt32(treeItem.Tag));
                dataGrid.ItemsSource = students;
            }
        }
    }
}
