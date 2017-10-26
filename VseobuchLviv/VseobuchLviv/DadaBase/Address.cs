namespace VseobuchLviv.DadaBase
{
    public class Address
    {
        public int ID { get; set; }
        public string numberBuilding { get; set; }        
        public District nameDistrict { get; set; }
        public Street nameStreet { get; set; }
        public override string ToString() => nameStreet.Name + " " + numberBuilding;
    }
}
