namespace CarSharingOnlineASP.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostMinute { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        //public bool IsAvailable { get; set; } = true;
        public string Image {  get; set; }

        public Car() 
        {
            Id = Guid.NewGuid();
        }

        public Car(string name, string description, decimal costMinute, string image)
        {
            Name = name;
            Description = description;
            CostMinute = costMinute;
            Image = image;
            Id = Guid.NewGuid();
        }
    }
}
