using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BirthdayProject;
using BirthdayProject.ViewModels;

namespace BirthdayProject.Repositories
{
    internal class PersonRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CSPersonStorage");
        private static readonly string[] names = {
            "John", "Alice", "Michael", "Emma", "William", "Olivia",
            "James", "Sophia", "Daniel", "Isabella", "Matthew", "Charlotte",
            "David", "Emily", "Andrew", "Mia", "Joseph", "Amelia",
            "Christopher", "Ella", "Joshua", "Avery", "Nicholas", "Grace",
            "Alexander"
        };
        private static readonly string[] surnames = {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis",
            "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas",
            "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia",
            "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee"
        };
        public PersonRepository()
        {
            if (!Directory.Exists(BaseFolder)) { 
                 Directory.CreateDirectory(BaseFolder);
                for (int i = 1; i <= 50; i++)
                {
                    Random rnd = new Random();
                    int randomYear = rnd.Next(1904, 2024);
                    int randomMonth = rnd.Next(1, 13);
                    int randomDay = rnd.Next(1, 29);
                    string randomName = names[rnd.Next(0, names.Length)];
                    string randomSurname = surnames[rnd.Next(0, surnames.Length)];  
                    DateTime randomBirthday = new DateTime(randomYear, randomMonth, randomDay);
                    _ = AddOrUpdateAsync(new Person(randomName, randomSurname, i + "@gmail.com",randomBirthday));
                }
            }
               

        }

        public async Task AddOrUpdateAsync(Person obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Email), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<Person> GetAsync(string login)
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, login);

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Person>(stringObj);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = await sw.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }

        public List<Person> GetAll()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }
        public void Remove(Person person)
        {
            File.Delete(Path.Combine(BaseFolder, person.Email));
        }
        public void RemoveSelected()
        {
            File.Delete(Path.Combine(BaseFolder, "selected"));
        }
        public Person GetSelected()
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, "selected");

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = sw.ReadToEnd();
            }

            return JsonSerializer.Deserialize<Person>(stringObj);
        }
        
        public async Task SetNewSelected(Person obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, "selected"), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

    }
 
}
