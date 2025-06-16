using CarSharingOnlineASP.Models;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace CarSharingOnlineASP.Data
{
    public class UsersJSRepository : IUsersJSRepository
    {
        List<User> users;

        public UsersJSRepository()
        {
            var jsonString = File.ReadAllText("Data/users.json");
            users = JsonSerializer.Deserialize<List<User>>(jsonString);
        }

        public void Add(User user)
        {
            users.Add(user);
            Save();
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User TryGetById(Guid id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public User TryGetByEmail(string email)
        {
            return users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserExists(string email)
        {
            return users.Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool ValidateUser(string email, string password)
        {
            var user = TryGetByEmail(email);
            if (user == null)
                return false;

            return password == user.Password;
        }

        public void Updata(UserEdit user)
        {
            var existingProduct = users.FirstOrDefault
                                    (x => x.Id == user.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.FirstName = user.FirstName;
            existingProduct.LastName = user.LastName;
            existingProduct.Age = user.Age;
            existingProduct.Email = user.Email;
            existingProduct.Password = user.Password;
            Save();
        }

        public void Save()
        {
            string updatedJsonString = JsonSerializer.Serialize(users,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText("Data/users.json", updatedJsonString,
                                       System.Text.Encoding.UTF8);
        }
    }
}
