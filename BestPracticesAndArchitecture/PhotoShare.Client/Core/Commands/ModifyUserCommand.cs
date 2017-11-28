namespace PhotoShare.Client.Core.Commands
{
    using Data;
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
        public static string Execute(string[] data)
        {
            string result = null;

            try
            {
                string userName = data[0];
                string userProperty = data[1].ToLower();
                string newUserPropertyValue = data[2];

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
                        switch (userProperty)
                        {
                            case "password":
                                bool containsLowerCase = false;
                                bool containsDigit = false;

                                foreach (char letter in newUserPropertyValue)
                                {
                                    if (char.IsDigit(letter) && char.IsNumber(letter))
                                    {
                                        containsDigit = true;
                                    }

                                    if (char.IsLower(letter))
                                    {
                                        containsLowerCase = true;
                                    }
                                }

                                if (containsLowerCase && containsDigit)
                                {
                                    currentUser.Password = newUserPropertyValue;
                                    result = $"User {userName} {userProperty} is {newUserPropertyValue}.";
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid Password!", "Password");
                                }
                                break;

                            case "borntown":
                                {
                                    Town newBornTown = context.Towns
                                      .Where(t => t.Name == newUserPropertyValue)
                                      .FirstOrDefault();

                                    if (newBornTown == null)
                                    {
                                        throw new ArgumentException($"Town {newUserPropertyValue} not found!", "Town");
                                    }
                                    else
                                    {
                                        currentUser.BornTown = newBornTown;
                                        result = $"User {userName} {userProperty} is {newUserPropertyValue}.";
                                    }
                                }
                                break;

                            case "currenttown":
                                {
                                    Town newCurrentTown = context.Towns
                                        .Where(t => t.Name == newUserPropertyValue)
                                        .FirstOrDefault();

                                    if (newCurrentTown == null)
                                    {
                                        throw new ArgumentException($"Town {newUserPropertyValue} not found!", "Town");
                                    }
                                    else
                                    {
                                        currentUser.CurrentTown = newCurrentTown;
                                        result = $"User {userName} {userProperty} is {newUserPropertyValue}.";
                                    }
                                }
                                break;

                            default:
                                throw new ArgumentException($"Propery {userProperty} not supported!");
                        }

                        context.SaveChanges();
                    }
                }
            }
            catch (ArgumentException e) when (e.ParamName == "Town" || e.ParamName == "Password")
            {
                string redactedExceptionMessage = e.Message.Substring(0, e.Message.IndexOf("Parameter name:"));
                throw new ArgumentException($"Value {data[2]} not valid. {redactedExceptionMessage}");
            }

            return result;
        }
    }
}