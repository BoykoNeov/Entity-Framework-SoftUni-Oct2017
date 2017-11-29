namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class RegisterUserCommand
    {
        // RegisterUser <username> <password> <repeat-password> <email> optional:<borntown> optional:<currenttown>
        public static string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            string bornTownName = null;
            if (data.Length >= 5)
            {
               bornTownName = data[4];
            }

            string currentTownName = null;
            if (data.Length >= 6)
            {
                currentTownName = data[5];
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town currentTown = context.Towns
                    .Where(t => t.Name == currentTownName)
                    .FirstOrDefault();

                Town bornTown = context.Towns
                    .Where(t => t.Name == bornTownName)
                    .FirstOrDefault();

                User user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now,
                    LastTimeLoggedIn = DateTime.Now,
                    CurrentTown = currentTown,
                    BornTown = bornTown
                };

                User existingUser = context.Users
                    .AsNoTracking()
                    .Where(u => u.Username == user.Username)
                    .FirstOrDefault();

                if (existingUser != null)
                {
                    throw new InvalidOperationException($"Username {user.Username} is already taken!");
                }
                else
                {
                    if (password != repeatPassword)
                    {
                        throw new ArgumentException("Passwords do not match!");
                    }

                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }

            return "User " + username + " was registered successfully!";
        }
    }
}