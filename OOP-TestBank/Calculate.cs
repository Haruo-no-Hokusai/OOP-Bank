
using OOP_Bank;

namespace OOP_TestBank
{
    internal class Calculate : Bank
    {
        public List<Debtor> debtor { get; set; }
        public List<SubBank> subBank { get; set; }
        public List<Bank> bank { get; set; }
        public List<double> SumInterest {  get; set; }
        public List<double> SumBcf {  get; set; }

        public Calculate()
        {
            debtor = new List<Debtor>();
            subBank = new List<SubBank>();
            bank = new List<Bank>();
            SumInterest = new List<double>();
            SumBcf = new List<double>();

            LoadDebtor();
            LoadSubBank();
            LoadBank();
            debtor = GroupDebtor();
        }
        public void LoadDebtor()
        {
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
                    BuyMore = Convert.ToDouble(parts[5]),
                    Interest = CalculateInterest(Convert.ToDouble(parts[3]), Convert.ToDouble(parts[4])),
                    Bcf = CalculatBcf(Convert.ToDouble(parts[3]), Convert.ToDouble(parts[4]),Interest, Convert.ToDouble(parts[5]))
                });
            }
        }
        public void LoadSubBank() 
        {
            string[] lines = File.ReadAllLines("C:/Beta/No.2/OOP-Bank/SubBank.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                subBank.Add(new SubBank
                {
                    Id = Convert.ToInt32(parts[0]),
                    Location = parts[1],
                    City = parts[2]
                });
            }
        }
        public void LoadBank()
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

        public double CalculateInterest(double Bbf,double Pay)
        {
            double Interest = (Bbf - Pay) * GetRate();
            SumInterest.Add(Interest);
            return Interest;
        }

        public double CalculatBcf(double Bbf,double Pay,double Interest,double More)
        {
            double Bcf = Bbf - Pay + Interest + More;
            SumBcf.Add(Bcf);
            return Bcf;
        }
        public double CalculateProfit()
        {
            double Profit =(debtor.Sum(p => p.Pay)+debtor.Sum(p => p.Interest))-subBank.Sum(p => p.Bbf);
            return Profit;
        }
        public List<Debtor> GroupDebtor() 
        { 
            var GroupDebtor = debtor.GroupBy(p => p.From).ToList();
            List<Debtor> resultList = new List<Debtor>();
            foreach (var group in GroupDebtor)
            {
                foreach (var debtor in group)
                {
                    resultList.Add(debtor);
                }
            }
            return resultList;
        }
        public double CalculateVariance(int id,string type)
        {
            List<double> data = new List<double>();
            foreach(var debtor in debtor)
            {
                if (debtor.From == id)
                {
                    switch (type)
                    {
                        case "1":data.Add(debtor.Interest); break;
                        case "2":data.Add(debtor.BuyMore); break;
                        case "3":data.Add(debtor.Bcf); break;
                    }
                }
            }
            double mean = data.Average();
            double sumSquaredDifferences = data.Sum(x => Math.Pow(x - mean, 2));
            double populationVariance = sumSquaredDifferences / data.Count;
            data.Clear();
            return populationVariance = Math.Sqrt(populationVariance);
        }

        public void Display()
        {
            foreach (var item in bank) 
            {
                SetAccumulate(debtor.Sum(p => p.Bbf));
                Console.WriteLine($"Id:{item.Id} Name:{item.Name} Type:{item.Type} Country:{item.Legion} Accumulate:{GetAccumulate()} Profit:{CalculateProfit()}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            foreach (var subBank in subBank)
            {
                Console.WriteLine($"Bank Id:{subBank.Id} Location:{subBank.Location} City:{subBank.City}");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine($"Id\tName\t\tBrought forward\tPayment\tInterest\tMore\tCarry forward");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                var SelectKey = GroupDebtor().Where(p => p.From == subBank.Id);
                
                foreach (var debtor in debtor)
                {
                    if (debtor.From == subBank.Id)
                    {
                        Console.WriteLine($"{debtor.Id}\t{debtor.Name}\t{debtor.Bbf}\t\t{debtor.Pay}\t{debtor.Interest:f2}\t\t{debtor.BuyMore}\t{debtor.Bcf:f2}");
                    }
                }
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine($"Sum:{SelectKey.Sum(p => p.Interest),50:f2}\t\t{SelectKey.Sum(p => p.BuyMore):f2}\t{SelectKey.Sum(p => p.Bcf):f2}");
                Console.WriteLine($"Average:{SelectKey.Average(p => p.Interest),45:f2}\t\t{SelectKey.Average(p => p.BuyMore):f2}\t{SelectKey.Average(p => p.Bcf):f2}");
                Console.WriteLine($"Variance:{CalculateVariance(subBank.Id,"1"),44:f2}\t\t{CalculateVariance(subBank.Id, "2"):f2}\t{CalculateVariance(subBank.Id, "3"):f2}");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
