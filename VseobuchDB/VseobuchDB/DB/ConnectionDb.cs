using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VseobuchDB.DB
{
    public class ConnectionDb
    {
        static MyDbContext db;

        public ConnectionDb() { db = new MyDbContext(); }

        public ConnectionDb(string conn) { db = new MyDbContext(conn); }

        static Student AddStuddent(Student stu)
        {
            Student returnSt = db.Students.FirstOrDefault((x) => x.FirstName == stu.FirstName &&
              x.LastName == stu.LastName && x.Surname == stu.Surname &&
              x.Birthday == stu.Birthday);
            if (returnSt == null)
            {
                returnSt= db.Students.Add(stu);
               // db.SaveChanges();                            
            }
            return returnSt;
        }

    public    static Student AddStuddent(string firstName, string lastName, string surname, DateTime birthday, bool sex)
        {
            return AddStuddent(new Student()
            { FirstName = firstName, LastName = lastName, Surname = surname, Birthday = birthday, Sex = sex });
        }

        static List<Student> AddRangeStuddent(List<Student> stu)
        {
            List<Student> liststud = new List<Student>();
            foreach (var a in stu)
            {
                liststud.Add(AddStuddent(a));                   
            }
            db.SaveChanges();
            return liststud;
        }
/// <summary>
/// метод для вставки студентів і студентів в школі!!!
/// </summary>
/// <param name="stu"></param>
/// <param name="sch"></param>
/// <returns></returns>
     public   static int ImportFromExelSchool(List<KeyValuePair<Student, string>> Pair_student_class, string school) 
        {
            
          //  List<Student> lStu = AddRangeStuddent(stu);//отримуємо список студентів вставлених в базу даних(з ІД) 
            List<Student> lStu = new List<Student>();
            foreach(var aa in Pair_student_class)
            {
                lStu.Add(aa.Key);
            }
            //
            lStu = AddRangeStuddent(lStu);//отримуємо список студентів вставлених в базу даних(з ІД) 
            //
            db.Addresses.ToList();
            db.Districts.ToList();
            db.Citys.ToList();
            School sch=  db.Schools.FirstOrDefault(x => x.Name == school);
            int i=0;
            if (sch == null)
            {
                // sch=db.Schools.AddNewSchool()   //метод який добавляє нову школу
            }
            for(int j=0; j<lStu.Count;j++)
            {
                Student st = new Student();
                st = lStu[i];
                db.Students_In_School.Add(new Student_In_School() { graduation=DateTime.Parse("1/1/2000"),  student = st, SchoolClass= Pair_student_class[i].Value, school = sch });
                i++;                
            }
            db.SaveChanges();
            return i;
        }

        public static int ImportFromExelBuilding(Student student_, Address address_, string FlatNumber_) 
        {
           student_= AddStuddent(student_);           
            Address address2= db.Addresses.FirstOrDefault(x => x.Street == address_.Street && x.NumberBuilding == address_.NumberBuilding);
            if (address2 == null)
            {
                address2 = db.Addresses.Add(address_);
            }
            db.SaveChanges();                                 
            db.Students_In_Building.Add(new Student_In_Building() { graduation=DateTime.Parse("1/1/1977"), student = student_, address=address2, FlatNumber=FlatNumber_ });
            db.SaveChanges();//
            return 1;
        }


    }
}
