namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Models;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town existingTown = context.Towns
                    .AsNoTracking()
                    .Where(t => t.Name == townName)
                    .FirstOrDefault();

                if (existingTown != null)
                {
                    throw new ArgumentException($"Town {townName} was already added!");
                }
                else
                {
                    Town town = new Town
                    {
                        Name = townName,
                        Country = country
                    };

                    context.Towns.Add(town);
                    context.SaveChanges();
                }

                return townName + " was added successfully!";
            }
        }
    }
}