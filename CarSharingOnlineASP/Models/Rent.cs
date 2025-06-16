namespace CarSharingOnlineASP.Models
{
    public class Rent
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public RentStatus Status { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }

        public Rent()
        {
            Id = Guid.NewGuid();
            Status = RentStatus.Active;
        }
    }

    public enum RentStatus
    {
        Active,
        Completed,
        Cancelled
    }
}
