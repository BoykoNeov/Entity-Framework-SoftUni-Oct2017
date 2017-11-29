namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string senderUsername = data[0];
            string receiverUsername = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                User sender = RetrieveUserWithFriends(context, senderUsername);
                User receiver = RetrieveUserWithFriends(context, receiverUsername);

                bool alreadyRequested = sender.FriendsAdded.Any(f => f.Friend.Username.Equals(receiverUsername));

                if (alreadyRequested)
                {
                    throw new InvalidOperationException($"{receiverUsername} is already added as a friend to {senderUsername}!");
                }

                Friendship friendship = new Friendship
                {
                    User = sender,
                    Friend = receiver
                };

                sender.FriendsAdded.Add(friendship);
                context.SaveChanges();
            }

            return $"Friend {receiverUsername} added to {senderUsername}.";
        }

        private static User RetrieveUserWithFriends(PhotoShareContext context, string username)
        {
            User retrievedUser = context.Users
                .Include(u => u.FriendsAdded)
                .ThenInclude(f => f.Friend)
                .Include(u => u.AddedAsFriendBy)
                .ThenInclude(f => f.Friend)
                .SingleOrDefault(u => u.Username.Equals(username));

            if (retrievedUser == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }
            else
            {
                return retrievedUser;
            }
        }
    }
}