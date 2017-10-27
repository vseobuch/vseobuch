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
                    Microsoft.Win32.OpenFileDialog ofp = new Microsoft.Win32.OpenFileDialog();
                    if ((bool)ofp.ShowDialog())
                        readStudent_in_School(ofp.FileName);       
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

                    Microsoft.Win32.OpenFileDialog ofp1 = new Microsoft.Win32.OpenFileDialog();
                    if((bool)ofp1.ShowDialog())                    
                        readStudent_in_Building(ofp1.FileName);
                    excelApp.Quit();
                    break;
                default:
                    Close();
                    break;
            }
        }
        /// <summary>
        /// Метод для запису студентів, школи і студентів в школі
        /// </summary>
        private void readStudent_in_School(string path)
        {
            List<string> letter = new List<string>();
            letter = letters();            
            excelApp = new Excel.Application();
            //excelApp.Visible = true;            
            excelappworkbook = excelApp.Workbooks.Open(
                path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            excelSheets = excelappworkbook.Worksheets;
            excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(1);
            School school = new School();            
            List<KeyValuePair<Student, string>> students = new List<KeyValuePair<Student, string>>();
            List<int> startRead = ofsetRowColumnExcel(1);
            int startRow = startRead[1]+1;
            int startColumn = startRead[0];
            school.Name = Convert.ToString((excelWorksheet.get_Range(letter[startColumn] + startRow, Type.Missing)).Value2);
            for (int i=0;i>-1;i++)
            {
                int j = startColumn+1;
                string row = (i+startRow).ToString();
                Student student = new Student();
                string schoolClass = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                student.LastName = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                student.FirstName = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                student.Surname = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string day = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string month = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string year = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                if(day!=null&&month!=null&&year!=null)
                {
                    DateTime date = DateTime.Parse(day + "." + month + "." + year);
                    student.Birthday = date;
                }
                else
                {
                    //не вказана дата народження треба обробити при вставці в БД
                } 
                if(excelWorksheet.get_Range(letter[j] + row, Type.Missing).Value2!=null)
                {
                    string sex = ((string)(Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2))).ToLower();

                    if (sex.Contains("ж"))
                        student.Sex = true;
                    else
                        student.Sex = false;
                }
                
                if (student.LastName != null || student.Surname != null || student.FirstName != null)
                {
                    students.Add(new KeyValuePair<Student, string>(student, schoolClass));                    
                }                    
                else
                {
                    startRead = ofsetRowColumnExcel(Convert.ToInt32(row));
                    if (startRead[0] != -1)
                    {
                        startRow = startRead[1] + 1;
                        j = startRead[0];
                        i = -1;
                    }
                    else
                        i = -2;//Вихід з циклу
                }
            }            
            excelApp.Quit();
            ConnectionDb db = new ConnectionDb("Students1");            
            ConnectionDb.ImportFromExelSchool(students, school.Name);
        }

        private void readStudent_in_Building(string path)
        {
            ConnectionDb db = new ConnectionDb("Students1");
            List<string> letter = new List<string>();
            letter = letters();            
            excelApp = new Excel.Application();
            //excelApp.Visible = true;           
            excelappworkbook = excelApp.Workbooks.Open(
                path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            excelSheets = excelappworkbook.Worksheets;
            excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(1);
            List<int> startRead = ofsetRowColumnExcel(1);            
            int startRow = startRead[1]+1;
            int startColumn = startRead[0]+2;            
            for (int i = 0;i>-1 ; i++)
            {
                int j = startColumn;
                string row = (i + startRow).ToString();
                Student student = new Student();                
                student.LastName = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                student.FirstName = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                student.Surname = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string day = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string month = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string year = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                if (day != null && month != null && year != null)
                {
                    DateTime date = DateTime.Parse(day + "." + month + "." + year);
                    student.Birthday = date;
                }
                else
                {
                    //не вказана дата народження треба обробити при вставці в БД
                }
                if (excelWorksheet.get_Range(letter[j] + row, Type.Missing).Value2 != null)
                {
                    string sex = ((string)(Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2))).ToLower();

                    if (sex.Contains("ж"))
                        student.Sex = true;
                    else
                        student.Sex = false;
                }               
                string district= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string nameStreet= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string numberBilding= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string letterBilding= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                if (letterBilding != null)
                    numberBilding += letterBilding;
                string numberFlat= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string letterFlat= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                if (letterFlat != null)
                    numberFlat += letterFlat;
                j++;//поле категорія
                string nameLKP= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                Address address = new Address()
                {
                    NumberBuilding = numberBilding,
                    Street = nameStreet,
                    NameLKP = nameLKP,
                    district = new District()
                    {
                        Name = district
                    }                    
                };
                if (student.LastName != null || student.Surname != null || student.FirstName != null)
                     ConnectionDb.ImportFromExelBuilding(student, address, numberFlat);                   
                else
                {
                    startRead = ofsetRowColumnExcel(Convert.ToInt32(row));
                    if (startRead[0] != -1)
                    {
                        startRow = startRead[1] + 1;
                        j = startRead[0];
                        i = -1;
                    }
                    else
                        i = -2;
                }
            }
            excelApp.Quit();           
        }

        public List<int> ofsetRowColumnExcel(int row_Start)//Визначення зміщення для початку читання файлу
        {
            int numberRow;
            List<string> letter = new List<string>();
            letter = letters();
            List<int> ofset_row_column = new List<int>();
            for(int i=0;i<10;i++)
            {
                for(int j=1;j<21;j++)
                {
                    numberRow = j + row_Start;
                    string start = Convert.ToString((excelWorksheet.get_Range(letter[i] + numberRow.ToString(), Type.Missing)).Value2);
                    if (start != null)
                    {
                        start = start.ToLower();
                        if (start.Contains("заклад"))
                        {
                            ofset_row_column.Add(i);
                            ofset_row_column.Add(j + row_Start);
                            return ofset_row_column;
                        }
                    }
                }
            }
            ofset_row_column.Add(-1);
            return ofset_row_column;
        }

        public List<string> letters()
        {
            List<string> letter = new List<string>();
            char a = 'A';
            for (int i = 0; i < 26; i++)
            {
                letter.Add(a.ToString());
                a++;
            }
            return letter;
        }
    }
}
