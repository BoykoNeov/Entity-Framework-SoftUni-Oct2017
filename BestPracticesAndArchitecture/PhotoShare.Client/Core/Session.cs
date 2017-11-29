namespace PhotoShare.Client.Core
{
    using System;
    using PhotoShare.Models;

    public static class Session
    {
        public static User User { get; set; }

        //private static bool IsValid
        //{
        //    get
        //    {
        //        return User != null;
        //    }
        //}

        public static bool CheckValidity()
        {
            if (User == null)
            {
                throw new InvalidOperationException($"Invalid credentials!");
            }
            else
            {
                return true;
            }
        }
    }
}