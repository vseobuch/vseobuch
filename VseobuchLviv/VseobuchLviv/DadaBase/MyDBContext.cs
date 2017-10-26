using System.Data.Entity;


namespace VseobuchLviv.DadaBase
{
    class MyDBContext:DbContext
    {
        public MyDBContext() : base("Students2") { }
        public MyDBContext(string str):base(str) { }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student_in_Building> Students_in_Building { get; set; }
        public virtual DbSet<Student_in_School> Students_in_School { get; set; }
    }
}
