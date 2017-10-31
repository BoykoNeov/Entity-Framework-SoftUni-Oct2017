namespace RawData
{
    class Cargo
    {
        private int weight;
        private string cargoType;

        public Cargo() { }
        public Cargo(int weight, string cargoType)
        {
            this.Weight = weight;
            this.CargoType = cargoType;
        }

        public string CargoType
        {
            get { return cargoType; }
            set { cargoType = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
    }
}