namespace OOP_Bank
{
    public class Bank 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Legion { get; set; }
        private double Accumulate {  get; set; }
        internal double Profit { get; set; }

        public void SetAccumulate(double value)
        {
            Accumulate = value;
        }
        public double GetAccumulate()
        {
            return Accumulate;
        }
    }
}
