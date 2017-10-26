using System;

namespace VseobuchLviv.DadaBase
{
    public class Student_in_Building
    {
        public int ID { get; set; }
        public Student Student { get; set; }
        public Building Building { get; set; }
        public DateTime StartDate { get; set; }
        public override string ToString() => Student.ToString();
    }
}
