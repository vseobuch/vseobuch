using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace VseobuchDB.DB
{
    public class readwriteExel
    {
        private static Excel.Application excelApp;        
        private static Excel.Workbook excelappworkbook;
        private static Excel.Worksheet excelWorksheet;
        private static Excel.Sheets excelSheets;

        /// <summary>
        /// Метод для запису студентів, школи і студентів в школі
        /// </summary>
        public static void readStudent_in_School(string path)
        {
            //List<string> letter = new List<string>();
            //letter = letters();
            List<string> letter = letters();
            excelApp = new Excel.Application();
            //excelApp.Visible = true;            
            excelappworkbook = excelApp.Workbooks.Open(
                path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            excelSheets = excelappworkbook.Worksheets;
            excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(1);
           // School school = new School();
            List<KeyValuePair<Student, string>> students = new List<KeyValuePair<Student, string>>();
            List<int> startRead = ofsetRowColumnExcel(1);
            int startRow = startRead[1] + 1;
            int startColumn = startRead[0];
            // school.Name = Convert.ToString((excelWorksheet.get_Range(letter[startColumn] + startRow, Type.Missing)).Value2);
            string schoolName = Convert.ToString((excelWorksheet.get_Range(letter[startColumn] + startRow, Type.Missing)).Value2);
            for (int i = 0; i > -1; i++)
            {
                int j = startColumn + 1;
                string row = (i + startRow).ToString();
                Student student = new Student();
                string schoolClass = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
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
            ConnectionDb.ImportFromExelStudentinSchool(students, schoolName);
        }

        public static void readStudent_in_Building(string path)
        {            
            List<string> letter = letters();
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
            int startRow = startRead[1] + 1;
            int startColumn = startRead[0] + 2;
            for (int i = 0; i > -1; i++)
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
                string district = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string nameStreet = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string numberBilding = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string letterBilding = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                if (letterBilding != null)
                    numberBilding += letterBilding;
                string numberFlat = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                string letterFlat = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                if (letterFlat != null)
                    numberFlat += letterFlat;
                j++;//поле категорія
                string nameLKP = Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
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

        private static List<int> ofsetRowColumnExcel(int row_Start)//Визначення зміщення для початку читання файлу
        {
            int numberRow;            
            List<string> letter = letters();
            List<int> ofset_row_column = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 1; j < 21; j++)
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

        private static List<string> letters()
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

        public static void readSchools(string path)
        {
            bool returnschool = true;
            School school;
            Address address;
            District district;
            List<string> letter = letters();
            excelApp = new Excel.Application();           
            excelappworkbook = excelApp.Workbooks.Open(
                path, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            excelSheets = excelappworkbook.Worksheets;
            excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(2);
            int row = 4, j = 0;
            while(returnschool)
            {
                school = new School();
                address = new Address();
                district = new District();
                district.Name= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                school.Name= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                address.district = district;
                address.Street= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                address.NumberBuilding= Convert.ToString((excelWorksheet.get_Range(letter[j++] + row, Type.Missing)).Value2);
                school.address = address;
                if (school.Name != null)
                {
                    ConnectionDb.ImportFromExcelSchool(school);
                    row++;
                    j = 0;
                }
                else
                    returnschool = false;
            }
        }
    }
}
