using CarSharingOnlineASP.Models;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace CarSharingOnlineASP.Data
{
    public class RentsJSRepository : IRentsJSRepository
    {
        private List<Rent> rents;
        private readonly string filePath = "Data/rents.json";

        public RentsJSRepository()
        {
            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                rents = JsonSerializer.Deserialize<List<Rent>>(jsonString) ?? new List<Rent>();
            }
            else
            {
                rents = new List<Rent>();
                Save();
            }
        }

        public List<Rent> GetAll()
        {
            return rents;
        }

        public Rent TryGetById(Guid id)
        {
            return rents.FirstOrDefault(x => x.Id == id);
        }

        public List<Rent> GetByUserId(Guid userId)
        {
            return rents.Where(x => x.UserID == userId).ToList();
        }

        public void Add(Rent rent)
        {
            rents.Add(rent);
            Save();
        }

        public void Update(Rent rent)
        {
            var existingRent = rents.FirstOrDefault(x => x.Id == rent.Id);
            if (existingRent != null)
            {
                var index = rents.IndexOf(existingRent);
                rents[index] = rent;
                Save();
            }
        }

        public void Save()
        {
            string updatedJsonString = JsonSerializer.Serialize(rents,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            File.WriteAllText(filePath, updatedJsonString, System.Text.Encoding.UTF8);
        }
    }
} 