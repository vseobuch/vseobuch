using System.Collections.ObjectModel;

namespace VseobuchLviv.DadaBase
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ObservableCollection<District> District { get; set; }
        public override string ToString() => Name;        
    }
}
