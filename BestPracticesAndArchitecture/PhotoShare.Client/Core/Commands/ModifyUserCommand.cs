namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string result = null;

            string userName = data[0];
            string userProperty = data[1];
            string newUserProperty = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                User currentUser = context.Users
                    .Where(u => u.Username == userName)
                    .FirstOrDefault();

                if (currentUser == null)
                {
                    throw new ArgumentException($"User {userName} not found!");
                }
                else
                {
                    // TODO
                    throw new NotImplementedException();
                }
            }

            return result;
        }
    }
}