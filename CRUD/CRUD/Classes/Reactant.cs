using System.Windows;

namespace CRUD
{
    public class Reactant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public float OrderPoint { get; set; }

        public Reactant()
        {

        }

        public Reactant(int id, string name, float quantity, float orderpoint)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            OrderPoint = orderpoint;
        }
    }

}