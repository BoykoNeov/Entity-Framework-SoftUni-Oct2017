namespace CreatingConstructors
{
    public class Person
    {
        private int age;
        private string name;

        public Person(int inputAge, string inputName)
        {
            this.name = inputName;
            this.age = inputAge;
        }

        public Person(int inputAge)
            : this(inputAge, "No name")
        {
        }

        public Person()
            : this(1, "No name")
        {
        }

        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                this.age = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }
    }
}