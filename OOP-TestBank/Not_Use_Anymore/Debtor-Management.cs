

//เจน เซฟ เสร็จแล้ว หมดหน้าที่


using System;

namespace OOP_Bank
{
    public class Debtor_Management : IDebtor_Interface
    {
        public List<Debtor> debtor {  get; set; }
        public Debtor_Management()
        {
            debtor = new List<Debtor>();
            Load();
            Save();
        }
        public void GenerateDebtor()
        {
            Random random = new Random();
            for (int i = 1; i <= 25; i++)
            {
                debtor.Add(new Debtor
                {
                    Id = i,
                    Name = $"Debtor:{i}",
                    From = random.Next(1, 6),
                    Bbf = random.Next(1000, 10000),
                    Pay = random.Next(1000, 40000),
                    BuyMore = random.Next(0, 80000)
                });
            }
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter("C:/Beta/No.2/OOP-Bank/Debtor.txt"))
            {
                foreach (Debtor debtor in debtor)
                {
                   writer.WriteLine($"{debtor.Id}," +
                   $"{debtor.Name}," +
                   $"{debtor.From}," +
                   $"{debtor.Bbf}," +
                   $"{debtor.Pay}," +
                   $"{debtor.BuyMore}");
                }
            }
            Console.WriteLine("Save");
        }

        public void Load()
        {
            if (File.Exists("C:/Beta/No.2/OOP-Bank/Debtor.txt"))
            {
                Random random = new Random();
                string[] lines = File.ReadAllLines("C:/Beta/No.2/OOP-Bank/Debtor.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                        debtor.Add(new Debtor 
                        { 
                            Id = Convert.ToInt32(parts[0]),
                            Name = parts[1],
                            From = Convert.ToInt32(parts[2]),
                            Bbf = Convert.ToDouble(parts[3]),
                            Pay = Convert.ToDouble(parts[4]),
                            BuyMore = Convert.ToDouble(parts[5])
                        });
                }
            }
            else
            {
                Random random = new Random();
                for (int i = 1; i <= 20; i++)
                {
                    debtor.Add(new Debtor
                    {
                        Id = i,
                        Name = $"Debtor {i}",
                        From = random.Next(1,6),
                        Bbf = random.Next(1000, 10000),
                        Pay = random.Next(1000, 40000),
                        BuyMore = random.Next(0, 80000)
                    });
                }
            }
        }
    }
}
