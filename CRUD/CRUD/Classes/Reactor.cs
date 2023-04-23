using System.Windows;

namespace CRUD
{
    public class Reactor
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public float Temp { get; set; }
        public float Volume { get; set; }

        public Reactor()
        { }

        public Reactor(int id, string name, int buildingId, float temp, float volume)
        {
            Id = id;
            BuildingId = buildingId;
            Name = name;
            Temp = temp;
            Volume = volume;
        }
    }
}