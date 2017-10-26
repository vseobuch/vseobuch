using System;

namespace VseobuchLviv.DadaBase
{
    public class Student_in_School
    {
        public int ID { get; set; }
        public Student Student { get; set; }
        public School School { get; set; }
        public string SchoolClass { get; set; }
        public DateTime StartStudy { get; set; }
        public override string ToString() => Student.ToString();        
    }
}
