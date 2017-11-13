namespace P01_HospitalDatabase.Generators
{
    using System;
    //using System.IO;

    public class NameGenerator
    {
        private static string[] firstNames = {
            "Petur",
            "Ivan",
            "Georgi",
            "Alexander",
            "Stefan",
            "Vladimir",
            "Svetoslav",
            "Kaloyan",
            "Mihail",
            "Stamat",
            "Boyko",
            "Vasil",
            "Angel",
            "Atanas",
            "Bogdan",
            "Bogomil",
            "Boris",
            "Borislav",
            "Dimo",
            "Dragan",
            "Dragomir",
            "Grozdan",
            "Kostadin",
            "Krasimir",
            "Lyuben",
            "Milen",
            "Miroslav",
            "Mladen",
            "Momchil",
            "Nayden",
            "Nikola",
            "Radotimir",
            "Yavor",
            "Atila",
        };
        //private static string[] firstNames = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] lastNames = { "Ivanov",
            "Georgiev",
            "Stefanov",
            "Alexandrov",
            "Petrov",
            "Stamatov",
            "Milanov",
            "Milchev",
            "Petrunov",
            "Zemenski",
            "Karaliov",
            "Novikov",
            "Baltov",
            "Veselinov",
            "Cyrkovski",
            "Leonidov",
            "Kiseliov",
            "Zahariev",
            "Diulgerov",
            "Terziiski",
            "Fikov",
            "Otomanski",
            "Enicharov",
            "Jivkov",
            "Varangov",
            "Vorozinski",
            "Robertov",
            "Prokopiev",
        };
        //private static string[] lastNames = File.ReadAllLines("<INSERT DIR HERE>");

        public static string FirstName() => GenerateName(firstNames);
        public static string LastName() => GenerateName(lastNames);

        private static string GenerateName(string[] names)
        {
            Random rnd = new Random();

            int index = rnd.Next(0, names.Length);

            string name = names[index];

            return name;
        }
    }
}
