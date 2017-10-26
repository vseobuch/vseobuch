namespace VseobuchLviv.DadaBase
{
    public class School
    {
        public int ID { get; set; }
        public int numberSchool { get; set; }
        public string NameSchool { get; set; }
        public Address AddressScchool { get; set; }
        public override string ToString() => numberSchool.ToString() + " " + NameSchool;        
    }
}
