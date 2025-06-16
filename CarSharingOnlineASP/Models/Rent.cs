namespace CarSharingOnlineASP.Models
{
    public class Rent
    {
        public Guid UserID { get; set; }
        public Car car { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }
    }
}
