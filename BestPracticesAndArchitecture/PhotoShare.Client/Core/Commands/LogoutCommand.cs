namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class LogoutCommand
    {
        public static string Execute()
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }
            else
            {
                string username = Session.User.Username;
                Session.User = null;
                return $"User {username} successfully loged out!";
            }
        }
    }
}