namespace PhotoShare.Client.Core
{
    using Commands;
    using System;
    using System.Linq;

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
                    Session.CheckValidity();
                    result = AddTownCommand.Execute(commandParameters);
                    break;

                case "modifyuser":
                    Session.CheckValidity();
                    result = ModifyUserCommand.Execute(commandParameters);
                    break;

                case "deleteuser":
                    Session.CheckValidity();
                    result = DeleteUserCommand.Execute(commandParameters);
                    break;

                case "addtag":
                    Session.CheckValidity();
                    result = AddTagCommand.Execute(commandParameters);
                    break;

                case "addfriend":
                    Session.CheckValidity();
                    result = AddFriendCommand.Execute(commandParameters);
                    break;

                case "acceptfriend":
                    Session.CheckValidity();
                    result = AcceptFriendCommand.Execute(commandParameters);
                    break;

                case "addtagto":
                    Session.CheckValidity();
                    result = AddTagToCommand.Execute(commandParameters);
                    break;

                case "listfriends":
                    result = ListFriendsCommand.Execute(commandParameters);
                    break;

                case "createalbum":
                    Session.CheckValidity();
                    result = CreateAlbumCommand.Execute(commandParameters);
                    break;

                case "sharealbum":
                    Session.CheckValidity();
                    result = ShareAlbumCommand.Execute(commandParameters);
                    break;

                case "uploadpicture":
                    Session.CheckValidity();
                    result = UploadPictureCommand.Execute(commandParameters);
                    break;

                case "login":
                    result = LoginCommand.Execute(commandParameters);
                    break;

                case "logout":
                    result = LogoutCommand.Execute();
                    break;

                case "exit":
                    result = ExitCommand.Execute();
                    break;

                default:
                    throw new InvalidOperationException($"Command {command} is not valid!");
            }

            return result;
        }
    }
}