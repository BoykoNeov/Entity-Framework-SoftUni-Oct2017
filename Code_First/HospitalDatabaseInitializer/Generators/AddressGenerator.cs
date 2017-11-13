namespace P01_HospitalDatabase.Generators
{
    using System;

    public class AddressGenerator
    {
        private static Random rnd = new Random();

        private static string[] townNames =
        {
            "Kinecardines",
            "Whitebridge ",
            "Nuxvar",
            "Laencaster",
            "Lowestoft",
            "Brickelwhyte",
            "Skegness",
            "Temby",
            "Dalmellington",
            "Ormkirk",
            "Bludhaven",
            "Carrickmacross",
            "Norfolk",
            "Groombridge",
            "Coruscant",
            "Zionmatown",
            "Stepford",
            "AngelGrove",
            "Sunnyvalle",
            "Bedrock",
            "Middlemarch",
            "Lavender",
            "Waterdeep",
            "Ballymascanlan",
            "Amberg",
            "Bayreuth",
            "Sofia",
            "Plovdiv",
            "Gabrovo",
            "Omsk",
            "Petropavlovsk",
            "Ankober",
            "Redmond",
            "Spokane",
            "Shady Cove",
            "Monte Karlo",
            "Joachim Gruevo",
            "Maglij",
            "Polski Trambej",
            "Malo Tyrnovo",
            "Koprivhstica",
         };
        //private static string[] townNames = File.ReadAllLines("<INSERT DIR HERE>");
        private static string[] streetNames = 
        {
            "Somerset Drive",
            "Prospect Street",
            "Highland Avenue",
            "Windsor Court",
            "College Avenue",
            "Green Street",
            "Colonial Avenue",
            "Elm Avenue",
            "Durham Road",
            "Rose Street",
            "6th Street North",
            "Brandywine Drive",
            "Madison Avenue",
            "Route 10",
            "Main Street East",
            "11th Avenue",
            "Greenfinch Street",
            "Molberg Avenue",
            "Thomas DeQuincy Boulevard",
            "Huxley Avenue",
            "Hodgekin Street",
            "Delaware Boulevard",
            "Ciderset Drive",
            "Jump Street",
            "Evergreen Terrace",
            "Baker Street",
            "Paper Street",
            "Susame Street",
            "Wisteria Lane",
            "Coronation Street",
            "Rainey Street",
            "Spooner Street",
            "Queen Boulevard",
            "Orchid Avenue",
            "Skid Row",
            "Campus Lane",
            "Devonshire Road",
            "King Street",
            "Passeig de Gracia",
            "Drakmoore",
            "Moskovska",
            "Last Hope",
            "Mangart",
            "Lepenica",
            "Ulica Dropala",
            "Flowers",
            "Kastaneda",
            };
        //private static string[] streetNames = File.ReadAllLines("<INSERT DIR HERE>");

        internal static string NewAddress()
        {
            string townName = townNames[rnd.Next(townNames.Length)];
            string streetName = streetNames[rnd.Next(streetNames.Length)];
            int number = rnd.Next(1, 100);

            return $"{townName}, {streetName} {number}";
        }
    }
}
