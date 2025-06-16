using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.DB.Models
{
    public class CarDB
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostMinute { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string Image { get; set; }
    }
}
