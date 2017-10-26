using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VseobuchLviv.DadaBase;

namespace VseobuchLviv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyDBContext db = new MyDBContext();
            Student stu = new Student() { FirstName = "sssss", LastName = "fffffff", SurName = "eeeeee", Sex = false,
            Birthday=new DateTime(2017,12,2)};
            Student stu1 = new Student()
            {
                FirstName = "sssss",
                LastName = "fffffff",
                SurName = "eeeeee",
                Sex = false,
                Birthday = new DateTime(2017, 12, 2)
            };
            Student stu2 = new Student()
            {
                FirstName = "sssssoooo",
                LastName = "fffffffggg",
                SurName = "eeeeeekkk",
                Sex = false,
                Birthday = new DateTime(2017, 12, 2)
            };
            List<Student> st = new List<Student>();
            st = db.Students.AddRange(new List<Student>() { stu, stu1, stu2 }).ToList();
            //Student stud = new Student();
            //stud = db.Students.Add(stu);
            //db.Students.add
            db.SaveChanges();
            Student stud = new Student();
            //db.Cities.Add(new City { Name = "Lviv" });
            //db.SaveChanges();
            // ObservableCollection<District> dist = new ObservableCollection<District>();
            //dist.Add( new District { Name = "Шевченківський",ID=1 });
            //dist.Add( new District { Name = "Личаківський",ID=2 });
            //db.Districts.Add(new District { Name = "Шевченківський" });
            //db.SaveChanges();
            //db.Districts.Add(new District { Name = "Личаківський" });
            //db.Cities.Where(x => x.ID == 1).FirstOrDefault().District = new ObservableCollection<District>();
            //db.Cities.Where(x => x.ID == 1).FirstOrDefault().District.Add(new District { Name = "Галицький2" } );
            //District d = new District();
            //d = db.Districts.Where(x => x.ID == 3).FirstOrDefault();
            //db.Cities.Where(x => x.ID == 1).FirstOrDefault().District = new ObservableCollection<District>();
            //db.Cities.Where(x => x.ID == 1).FirstOrDefault().District.Add(new District { Name = "Залізничний" });
            //db.Srteets.Add(new Street { Name = "Городоцька" });
            //Street s = new Street();
            //s = db.Srteets.Where(x => x.ID == 1).FirstOrDefault();
            //db.Addresses.Add(new Address { numberBuilding = "25", nameDistrict = d, nameStreet = s });
            // db.Cities.Where(x => x.ID == 1).FirstOrDefault();
            //db.Districts.ToList();
            //db.SaveChanges();            

        }
    }
}
