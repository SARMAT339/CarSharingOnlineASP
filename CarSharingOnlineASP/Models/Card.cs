﻿namespace CarSharingOnlineASP.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
    }
}