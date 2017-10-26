using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VseobuchDB
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<District> districts { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class District
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    //public class Street
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public override string ToString()
    //    {
    //        return Name;
    //    }
    //}

    //public class Strit_Rigeon
    //{
    //    public int ID { get; set; }
    //    public Strit Name_Strit { get; set; }
    //    public Rigeon Name_Rigeon { get; set; }
    //    public override string ToString()
    //    {
    //        return Name_Strit.ToString() + "\nРайон: " + Name_Rigeon.ToString();
    //    }
    //}

     public class Address
    {
        public int ID { get; set; }
        public string NumberBuilding { get; set; }
        public string Street { get; set; }
        public District district { get; set; }
        public string NameLKP { get; set; }
        public override string ToString()
        {
            return "";
        }
    }

    //public class Building
    //{
    //    public int ID { get; set; }
    //    public string FlatNumber { get; set; }
    //    public Address address { get; set; }
    //    public override string ToString()
    //    {
    //        return FlatNumber.ToString();
    //    }
    //}

    public class School
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Address address { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Sex { get; set; }
        public override string ToString() => FirstName + LastName + Surname;
    }

    public class Student_In_School
    {
        public int ID { get; set; }
        public Student student { get; set; }
        public School school { get; set; }
        public string SchoolClass { get; set; }
        public DateTime graduation { get; set; }
        public override string ToString()
        {
            return "s";
        }
    }

    public class Student_In_Building
    {
        public int ID { get; set; }
        public Student student { get; set; }
        public Address address { get; set; }
        public string FlatNumber { get; set; }
        public DateTime graduation { get; set; }
        public override string ToString()
        {
            return "s";
        }
    }

    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("Students") { }
        public MyDbContext(string str) : base(str) { }

        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<District> Districts { get; set; }
      //  public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
      //  public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_In_School> Students_In_School { get; set; }
        public virtual DbSet<Student_In_Building> Students_In_Building { get; set; }
    }
}
