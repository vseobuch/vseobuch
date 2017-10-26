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
using System.Data.Entity;
using Excel = Microsoft.Office.Interop.Excel;

using VseobuchDB;
using VseobuchDB.DB;

namespace wpfExel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Excel.Application excelApp;
        private Excel.Window excelWind;
        private Excel.Workbooks excelappworkbooks;
        private Excel.Workbook excelappworkbook;
        private Excel.Worksheet excelWorksheet;
        private Excel.Sheets excelSheets;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            
            int i = Convert.ToInt32(((Button)(sender)).Tag);            
            switch (i)
            {
                case 1:
                    //readStudent_in_School();       
                    //MyDbContext db = new MyDbContext("Students1");
                    //City city = db.Citys.FirstOrDefault();
                    //var dist = db.Districts.ToList();
                    //var student = db.Students.ToList();
                    //var school= db.Schools.ToList();
                    //var addr=db.Addresses.ToList();                    
                    //Student_In_School sis = new Student_In_School()
                    //{
                    //    graduation = DateTime.Parse("12/3/2001"),
                    //    SchoolClass = "5d",
                    //    student = new Student()
                    //    {
                    //        FirstName = "dddddd",
                    //        LastName="fffffff",
                    //        Birthday=DateTime.Parse("1/1/1990"),
                    //        Sex=true,
                    //        Surname="wwwwwwwwww"
                    //    },
                    //    school=new School()
                    //    {
                    //        Name="96",
                    //        address=addr[0]
                    //    }
                    //};
                    //Student_In_School sis = new Student_In_School() { graduation=new DateTime(), SchoolClass = "3a", school = school[0], student = student[2] };
                    //db.Students_In_School.Add(sis);
                    //var address = new Address() { NameLKP = "ЖЕК", district = dist[2], NumberBuilding = "45", Street = "Галицька" };
                    //db.Addresses.Add(address);
                    // city.districts = dist;
                    //city.districts.Add(new District() { Name = "Галицький" });
                    //db.SaveChanges();
                   
                    //db.Students_In_School.ToList();
                   
                    /*excelApp.SheetsInNewWorkbook = 3;
                    excelApp.Workbooks.Add(Type.Missing);
                    excelApp.SheetsInNewWorkbook = 5;
                    excelApp.Workbooks.Add(Type.Missing);
                    //Запрашивать сохранение
                    excelApp.DisplayAlerts = true;
                    //Получаем набор ссылок на объекты Workbook (на созданные книги)
                    excelappworkbooks = excelApp.Workbooks;
                    //Получаем ссылку на книгу 1 - нумерация от 1
                    excelappworkbook = excelappworkbooks[1];
                    //Ссылку можно получить и так, но тогда надо знать имена книг,
                    //причем, после сохранения - знать расширение файла
                    //excelappworkbook=excelappworkbooks["Книга 1"];
                    //Запроса на сохранение для книги не должно быть
                    excelappworkbook.Saved = true;
                    //excelappworkbook.SaveAs();
                    //Используем свойство Count, число Workbook в Workbooks 
                    if (excelappworkbooks.Count > 1)
                    {
                        excelappworkbook = excelappworkbooks[2];
                        //Запрос на сохранение  книги 2  должен быть
                        excelappworkbook.Saved = false;
                    }*/

                    break;
                case 2:
                    excelApp.Quit();
                    break;
                default:
                    Close();
                    break;
            }
        }

        private void readStudent_in_School()
        {
            excelApp = new Excel.Application();
            //excelApp.Visible = true;
            Microsoft.Win32.OpenFileDialog ofp = new Microsoft.Win32.OpenFileDialog();
            ofp.ShowDialog();
            string path = ofp.FileName;
            excelappworkbook = excelApp.Workbooks.Open(
                path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            excelSheets = excelappworkbook.Worksheets;
            excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(1);
            School school = new School();
            List<KeyValuePair<Student, string>> students = new List<KeyValuePair<Student, string>>();            
            for(int i=0;i<3;i++)
            {
                int row = i + 3;
                Student student = new Student();                
                student.LastName = Convert.ToString((excelWorksheet.get_Range("C" + row.ToString(), Type.Missing)).Value2);
                student.FirstName = Convert.ToString((excelWorksheet.get_Range("D" + row.ToString(), Type.Missing)).Value2);
                student.Surname = Convert.ToString((excelWorksheet.get_Range("E" + row.ToString(), Type.Missing)).Value2);
                DateTime date = DateTime.Parse(Convert.ToString((excelWorksheet.get_Range("F" + row.ToString(), Type.Missing)).Value2) + "." +
                    Convert.ToString((excelWorksheet.get_Range("G" + row.ToString(), Type.Missing)).Value2) + "." +
                    Convert.ToString((excelWorksheet.get_Range("H" + row.ToString(), Type.Missing)).Value2));
                student.Birthday = date;
                string sex = ((string)(Convert.ToString((excelWorksheet.get_Range("I" + row.ToString(), Type.Missing)).Value2))).ToLower();

                if (sex.Contains("ж"))
                    student.Sex = true;
                else
                    student.Sex = false;                
                string schoolClass = Convert.ToString((excelWorksheet.get_Range("B" + row.ToString(), Type.Missing)).Value2);                
                students.Add(new KeyValuePair<Student, string>(student, schoolClass));
            }
            school.Name = Convert.ToString((excelWorksheet.get_Range("A" + 3, Type.Missing)).Value2);
            excelApp.Quit();            
            ConnectionDb db = new ConnectionDb("Students1");
            //ConnectionDb.AddRangeStuddent(students);
            ConnectionDb.ImportFromExelSchool(students, school.Name);
            //char a = 'A';
            //int b = Convert.ToInt32(a);
            //b++;
            //a = Convert.ToChar((int)b);

        }
    }
}
