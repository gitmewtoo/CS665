using System.Windows;

namespace CRUD
{
    public class ProcessReactant
    {
        public int ProcessId { get; set; }
        public int ReagentId { get; set; }
        public string Name { get; set; }
        public float Temp { get; set; }
        public float Volume { get; set; }

        public ProcessReactant(int processId, int reagentId, string name, float temp, float volume)
        {
            ProcessId = processId;
            ReagentId = reagentId;
            Name = name;
            Temp = temp;
            Volume = volume;
        }
    }
}