namespace RawData
{
    class Tire
    {
        private double pressure;
        private int age;

        public Tire() { }

        public Tire(int age, double pressure)
        {
            this.Age = age;
            this.Pressure = pressure;
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public double Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }
    }
}