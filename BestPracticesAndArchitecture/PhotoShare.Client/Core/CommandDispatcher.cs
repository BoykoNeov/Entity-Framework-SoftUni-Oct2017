namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using Commands;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0].ToLower();

            commandParameters = commandParameters.Skip(1).ToArray();

            string result = null;

            switch (command)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParameters);
                    break;

                case "addtown":
                    result = AddTownCommand.Execute(commandParameters);
                    break;

                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParameters);
                    break;

                default:
                    throw new InvalidOperationException($"Command {command} is not valid!");
            }

            return result;
        }
    }
}