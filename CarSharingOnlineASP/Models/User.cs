namespace CarSharingOnlineASP.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RightsNumber { get; set; }
        public bool IsAdmin { get; set; } = false;
        public bool IsBlocked { get; set; } = false;

        public User()
        {
            Id = Guid.NewGuid();
        }

        public User(string firstName, string lastName, int age, string email, string password, int rightsNumber, bool isAdmin, bool isBlocked)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Email = email;
            Password = password;
            RightsNumber = rightsNumber;
            IsAdmin = isAdmin;
            IsBlocked = isBlocked;
            Id = Guid.NewGuid();
        }

    }
}
