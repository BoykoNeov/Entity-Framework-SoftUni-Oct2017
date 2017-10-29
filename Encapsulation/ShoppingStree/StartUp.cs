namespace ShoppingSpree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            string[] personsInput = Console.ReadLine().Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            string[] productsInput = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, Person> persons = new Dictionary<string, Person>();
            Dictionary<string, Product> products = new Dictionary<string, Product>();

            foreach (string pers in personsInput)
            {
                string[] personArgs = pers.Split(new char[] { '=' });
                Person currentPerson = new Person();

                try
                {
                    currentPerson.Name = personArgs[0];
                    currentPerson.Money = decimal.Parse(personArgs[1]);
                    persons.Add(personArgs[0], currentPerson);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            foreach (string prod in productsInput)
            {
                string[] productArgs = prod.Split(new char[] { '=' });
                Product currentProduct = new Product();

                try
                {
                    currentProduct.Name = productArgs[0];
                    currentProduct.Price = decimal.Parse(productArgs[1]);
                    products.Add(productArgs[0], currentProduct);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            string programInput = string.Empty;
            while ((programInput = Console.ReadLine()) != "END")
            {
                string[] inputParams = programInput.Split(new char[] { ' ' });

                string personName = inputParams[0];
                string productName = inputParams[1];

                Person currentPerson = persons[personName];
                Product currentProduct = products[productName];

                if (currentPerson.Money >= currentProduct.Price)
                {
                    currentPerson.bagOfProducts.Add(currentProduct);
                    Console.WriteLine($"{personName} bought {productName}");
                    currentPerson.Money -= currentProduct.Price;
                }
                else
                {
                    Console.WriteLine($"{personName} can't afford {productName}");
                }
            }

            foreach (KeyValuePair<string, Person> personKVP in persons)
            {
                if (personKVP.Value.bagOfProducts.Count > 0)
                {
                    Console.WriteLine($"{personKVP.Key} - {string.Join(", ", personKVP.Value.bagOfProducts.Select(x=> x.Name))}");
                }
                else
                {
                    Console.WriteLine($"{personKVP.Key} - Nothing bought");
                }
            }
        }
    }
}