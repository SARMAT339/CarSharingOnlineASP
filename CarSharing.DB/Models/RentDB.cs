using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarSharing.DB.Models;

namespace CarSharing.DB.Models
{
    public class RentDB
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid CarId { get; set; }
        public CarDB Car { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public RentStatus Status { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }

        public enum RentStatus
        {
            Active,
            Completed,
            Cancelled
        }
    }
}
