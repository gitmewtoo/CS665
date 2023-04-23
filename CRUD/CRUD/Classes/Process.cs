using System.Windows;

namespace CRUD
{
    public class Process
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public float Temp { get; set; }
        public float Volume { get; set; }

        public Process(int id, string name, float temp, float volume)
        {
            Id = id;
            Desc = name;
            Temp = temp;
            Volume = volume;
        }
    }

}