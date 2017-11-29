namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            string receiverUsername = data[0];
            string senderUsername = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {

                User receiver = RetrieveUserWithFriends(context, receiverUsername);
                User sender = RetrieveUserWithFriends(context, senderUsername);

                bool alreadyFriends = receiver.FriendsAdded.Any(f => f.Friend.Username.Equals(senderUsername));

                if (alreadyFriends)
                {
                    throw new InvalidOperationException($"{senderUsername} is already a friend to {receiverUsername}!");
                }

                bool addedAsFriend = sender.FriendsAdded.Any(f => f.Friend.Username.Equals(receiverUsername));

                if (!addedAsFriend)
                {
                    throw new InvalidOperationException($"{senderUsername} has not added {receiverUsername} as a friend!");
                }

                Friendship friendship = new Friendship
                {
                    User = receiver,
                    Friend = sender
                };

                receiver.FriendsAdded.Add(friendship);
                context.SaveChanges();
            }

            return $"{receiverUsername} accepted {senderUsername} as a friend.";
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