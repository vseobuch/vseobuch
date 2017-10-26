namespace VseobuchLviv.DadaBase
{
    public class Building
    {
        public int ID { get; set; }
        public int numberSection { get; set; }
        public Address buildingAddress { get; set; }
        public override string ToString() => buildingAddress.ToString() + " " + numberSection.ToString();
    }
}
