namespace P01_HospitalDatabase.Generators
{
    //using System.IO;

    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Data.Models;

    class MedicamentGenerator
    {
        internal static void InitialMedicamentSeed(HospitalContext context)
        {
            var medicamentNames = new string[]
            {
                "Abarelix",
                "Betamethadol",
                "Biseptol",
                "Dypraxa",
                "Ciclobenzaprina",
                "Curam",
                "Diethyltryptamine",
                "Diclofenaco",
                "Disflatyl",
                "Nine Flower Jade Dew Pill",
                "Phoenix Tears",
                "Duvadilan",
                "Efedrinal",
                "Flanax",
                "Hibernol",
                "Fluimucil",
                "Iocane powder",
                "Navidoxine",
                "Drug JJ-180",
                "Provolanaproxalidamine C",
                "Nistatin",
                "Olfen",
                "Papaverine",
                "Pentrexyl",
                "Masiform D",
                "Primolut Nor",
                "Primperan",
                "Selenine",
                "Serum 114",
                "Blinkmoth Serum",
                "Propoven",
                "Retinax 5",
                "Reglin",
                "Imobatine",
                "Miracurall",
                "Neodextraline Solution",
                "Nectar Infusion",
                "Terramicina Oftalmica",
                "Ultran",
                "Viartril-S",
                "Dipipanone",
                "Chamalla Extract",
                "Ethylmethylthiambutene",
                "Etonitazene",
                "Bittamucine",
                "Comanapracil",
                "Daylightium",
                "Etoxeridine",
                "Axelavier",
                "Furethidine",
                "Alprazaline",
                "Hydroxypethidine",
                "Special Kei",
                "Adravil",
                "Ketobemidon",
                "Prozium",
                "Levomoramid",



            };
            //var medicamentNames = File.ReadAllLines("<INSERT DIR HERE>");

            for (int i = 0; i < medicamentNames.Length; i++)
            {
                context.Medicaments.Add(new Medicament() { Name = medicamentNames[i] });
            }

            context.SaveChanges();
        }

        public static void Generate(string medicamentName, HospitalContext context)
        {
            context.Medicaments.Add(new Medicament() { Name = medicamentName });
        }
    }
}
