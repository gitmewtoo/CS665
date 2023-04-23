using System.Windows;

namespace CRUD
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public Building()
        {

        }
        public Building(int id, string name, string address, string city, string state, string zip, string phone)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            Phone = phone;
        }
    }
}