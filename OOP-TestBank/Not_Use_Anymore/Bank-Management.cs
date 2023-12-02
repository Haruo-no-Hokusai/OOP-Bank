

//เจน เซฟ เสร็จแล้ว หมดหน้าที่


using OOP_TestBank;
using System.Numerics;

namespace OOP_Bank
{
    internal class Bank_Management : IBank_Interface
    {
        public List<Bank> bank { get; set; }
        public Bank_Management() 
        {
            bank = new List<Bank>();
            Calculate calculate = new Calculate();
            Load();
            Save();
        }
        public void GenerateBank()
        {
            Random random = new Random();
            bank.Add(new Bank
            {
                Id = 1,
                Name = "Thaipanish",
                Legion = "Thai",
                Profit = random.Next(1000000, 10000000),
            });
        }

        public void Load()
        {
            string[] lines = File.ReadAllLines("C:/Beta/No.2/OOP-Bank/Bank.txt");
            foreach (string line in lines) 
            {
                string[] parts = line.Split(',');
                bank.Add(new Bank()
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Type = parts[2],
                    Legion = parts[3],
                    Profit = Convert.ToDouble(parts[5]),
                });
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter("C:/Beta/No.2/OOP-Bank/Bank.txt"))
            {
                foreach (Bank Bank in bank)
                {
                    writer.WriteLine($"{Bank.Id}," +
                    $"{Bank.Name}," +
                    $"{Bank.Type}," +
                    $"{Bank.Legion}," +
                    $"{Bank.GetAccumulate()}," +
                    $"{Bank.Profit}");
                }
            }
            Console.WriteLine("Save");
        }
    }
}
