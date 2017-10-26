using System;

namespace VseobuchLviv.DadaBase
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public bool Sex { get; set; }
        public DateTime Birthday { get; set; }
        public override string ToString() => FirstName + " " + LastName + " " + SurName;        
    }
}
