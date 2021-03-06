﻿using System;
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
     public   static int ImportFromExelStudentinSchool(List<KeyValuePair<Student, string>> Pair_student_class, string school) 
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
                sch = db.Schools.Add(new School() { Name = school });   //метод який добавляє нову школу
            }
            for(int j=0; j<lStu.Count;j++)
            {
                Student st = new Student();
                st = lStu[i];
                if(db.Students_In_School.FirstOrDefault(x=>x.student.ID==st.ID)==null)
                {
                    db.Students_In_School.Add(new Student_In_School() { graduation = DateTime.Parse("1/1/1970"), student = st, SchoolClass = Pair_student_class[i].Value, school = sch });                    
                }
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
                District distr = db.Districts.FirstOrDefault(x => x.Name == address_.district.Name);
                if (distr == null)
                {
                    City city = db.Citys.FirstOrDefault(x => x.Name == "Львів");
                    db.Districts.FirstOrDefault();
                    city.districts.Add(address_.district);                                   
                }                    
                else                
                    address_.district = distr;               
                address2 = db.Addresses.Add(address_);
            }
            db.SaveChanges();
            if(db.Students_In_Building.FirstOrDefault(x=>x.student.ID==student_.ID)==null)
            {
                db.Students_In_Building.Add(new Student_In_Building() { student = student_, address = address2, FlatNumber = FlatNumber_, graduation = DateTime.Parse("1/1/1970") });
                db.SaveChanges();//
                return 1;
            }
            return 0;
        }

        public static int  ImportFromExcelSchool(School school)
        {            
            Address address = setAddress(school.address);
            School sch = db.Schools.FirstOrDefault(x => x.Name == school.Name && x.address.Street == school.address.Street);
            if (sch == null)
            {
                if (sch.Name == null)
                {
                    db.Addresses.ToList();
                    school.address = address;
                    db.Schools.Add(school);                      
                }
                else
                {
                    db.Addresses.ToList();
                    school.address = address;
                }
                db.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public static District setDistrict(District district)
        {
            District dist = db.Districts.FirstOrDefault(x => x.Name == district.Name);
            if (dist == null)
            {
                dist = db.Districts.Add(district);
                db.SaveChanges();
                return dist;
            }
            else
                return dist;
        }

        public static Address setAddress(Address address)
        {
            db.Districts.ToList();
            Address addr = db.Addresses.FirstOrDefault(x => x.Street == address.Street && x.NumberBuilding == address.NumberBuilding);
            if (addr == null)
            {
                District dist = setDistrict(address.district);
                address.district = dist;
                db.Addresses.Add(address);
                db.SaveChanges();
                return addr;
            }
            else
                return addr;
        }

        public int CountStudents()
        {
            return db.Students.Count();
        }

        public int CountStudentsInBuilding()
        {
            return db.Students_In_Building.Count();
        }

        public int CountStudentsInSchool()
        {
            return db.Students_In_School.Count();
        }

        private bool FoundStudent(Student a)
        {
            Student_In_School aa = db.Students_In_School.FirstOrDefault(x=>x.ID==a.ID);
            var bb=db.Students_In_Building.FirstOrDefault(x => x.ID == a.ID);
            if (aa != null && bb != null)
                return true;
            return false;
        }
        public List<Student> FoundStudent() //повертає список студентів про яких є дані зі школи і жеку
        {
            return db.Students.ToList().Where(x => FoundStudent(x)).ToList();
        }
        public List<Student> NotFoundStudent() //повертає список студентів про яких є дані зі школи і жеку
        {
            return db.Students.ToList().Where(x => !FoundStudent(x)).ToList();
        }

        public List<City> GetCity()
        {
            return db.Citys.ToList();
        }

        public List<District> GetDistrict()
        {
            return db.Districts.ToList();
        }

        public List<School> GetSchool()
        {
            return db.Schools.ToList();
        }

        public List<School> GetSchoolInDistrict(int ID)
        {
            return db.Schools.Where(x => x.address.district.ID == ID).ToList();
        }

        public List<Student> GetSudentsinSchool(int ID)
        {
            db.Students.ToList();
            List<Student_In_School> sin = db.Students_In_School.Where(x => x.school.ID == ID).ToList();
            List<Student> students = new List<Student>();
            foreach (Student_In_School s in sin)
            {               
                students.Add(s.student);
            }                
            return students;
        }
    }
}
