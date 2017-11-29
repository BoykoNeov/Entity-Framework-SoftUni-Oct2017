namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Models;
    using PhotoShare.Data;
    using System.Linq;

    public class LoginCommand
    {
        public static string Execute(string[] data)
        {
            if (Session.User != null)
            {
                return "You should logout first!";
            }

            string username = data[0];
            string password = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                User currentUser = context.Users
                    .SingleOrDefault(u => u.Username.Equals(username));

                if (currentUser == null || currentUser.Password != password)
                {
                    throw new ArgumentException($"Invalid username or password!");
                }

                Session.User = currentUser;
                return $"User {username} logged in successfully!";
            }
        }
    }
}