
namespace OOP_Bank
{
    internal class SubBank_Management: ISubBank_Interface
    {
        public List<SubBank> subBank {  get; set; }
        public SubBank_Management() 
        {
            subBank = new List<SubBank>();
            Load();
            Save();
        }
        public void GenerateSubBank()
        {
            Random random = new Random();
            for (int i = 1; i <= 5; i++)
            {
                subBank.Add(new SubBank
                {
                    Id = i,
                    Location = $"Location:{random.Next(1, 6)}",
                    City = $"City:{random.Next(1, 5)}"
                });
            }
        }

        public void Load()
        {
            if (File.Exists("C:/Beta/No.2/OOP-Bank/SubBank.txt"))
            {
                Random random = new Random();
                string[] lines = File.ReadAllLines("C:/Beta/No.2/OOP-Bank/SubBank.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    subBank.Add(new SubBank
                    {
                        Id = Convert.ToInt32(parts[0]),
                        Location = parts[1],
                        City = parts[2],
                    });
                }
            }
            else
            {
                Random random = new Random();
                for (int i = 1; i <= 20; i++)
                {
                    subBank.Add(new SubBank
                    {
                        Id = i,
                        Location = $"Location:{random.Next(1, 5)}",
                        City = $"City:{random.Next(1, 5)}"
                    });
                }
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter("C:/Beta/No.2/OOP-Bank/SubBank.txt"))
            {
                foreach (SubBank subBank in subBank)
                {
                    writer.WriteLine($"{subBank.Id}," +
                    $"{subBank.Location}," +
                    $"{subBank.City},");
                }
            }
            Console.WriteLine("Save");
        }
    }
}
