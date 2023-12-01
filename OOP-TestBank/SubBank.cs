
namespace OOP_Bank
{
    public class SubBank : Debtor
    {
        public int Id {  get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        protected double Rate = 0.06;

        public double GetRate()
        {
            return Rate;
        }
    }
}
