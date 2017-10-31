namespace RawData
{
    using System.Collections.Generic;
    using System.Linq;

    class Car
    {
        private string model;
        private Engine engine;
        private Cargo cargo;
        private Tire[] tires;

        public Car() { }

        public Car(string model, Cargo cargo, Engine engine, params Tire[] tires)
        {
            this.Model = model;
            this.Cargo = cargo;
            this.Engine = engine;

            this.Tires = new Tire[tires.Length];

            for (int i = 0; i < tires.Length; i++)
            {
                this.Tires[i] = tires[i];
            }
        }

        public Car(string model, Cargo cargo, Engine engine, ICollection<Tire> tires)
        {
            this.Model = model;
            this.Cargo = cargo;
            this.Engine = engine;

            this.Tires = new Tire[tires.Count];
            this.Tires = tires.ToArray();
        }

        public Tire[] Tires
        {
            get { return tires; }
            private set { tires = value; }
        }


        public Cargo Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }


        public Engine Engine
        {
            get { return engine; }
            set { engine = value; }
        }


        public string Model
        {
            get { return model; }
            set { model = value; }
        }

    }
}