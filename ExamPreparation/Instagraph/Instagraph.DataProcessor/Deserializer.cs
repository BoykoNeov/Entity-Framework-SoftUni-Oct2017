using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            Picture[] PicturesFromJSON = JsonConvert.DeserializeObject<Picture[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            HashSet<string> paths = new HashSet<string>();

            foreach (Picture pic in PicturesFromJSON)
            {
                bool hasPath = pic.Path != null;
                bool pathIsUnique = !paths.Contains(pic.Path);
                bool sizeIsBiggerThanZero = pic.Size > 0;

                if (hasPath && pathIsUnique && sizeIsBiggerThanZero)
                {
                    paths.Add(pic.Path);
                    context.Pictures.Add(pic);
                    sb.AppendLine("Successfully imported Picture " + pic.Path + ".");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.SaveChanges();
            string result = sb.ToString();
            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            UserDTO[] UsersFromJSON = JsonConvert.DeserializeObject<UserDTO[]>(jsonString);
            StringBuilder sb = new StringBuilder();

            HashSet<string> usedUsernames = new HashSet<string>();

            Dictionary<string, Picture> pictures = context.Pictures.ToDictionary(k => k.Path);

            foreach (UserDTO userDTO in UsersFromJSON)
            {
                bool usernameIsNullOrEmpty = string.IsNullOrWhiteSpace(userDTO.Username);
                bool passwordIsNullOrEmpty = string.IsNullOrWhiteSpace(userDTO.Password);
                bool profilePictureIsNullEmpty = string.IsNullOrWhiteSpace(userDTO.ProfilePicture);

                bool usernameIsNotUnique = true;
                if (!usernameIsNullOrEmpty)
                {
                    usernameIsNotUnique = usedUsernames.Contains(userDTO.Username);
                }

                bool noSuchProfilePicture = true;
                if (!profilePictureIsNullEmpty)
                {
                    noSuchProfilePicture = !pictures.ContainsKey(userDTO.ProfilePicture);
                }

                if (!usernameIsNullOrEmpty && !passwordIsNullOrEmpty && !profilePictureIsNullEmpty && !usernameIsNotUnique && !noSuchProfilePicture)
                {
                    User newUser = new User()
                    {
                        Username = userDTO.Username,
                        Password = userDTO.Password,
                        ProfilePictureId = pictures[userDTO.ProfilePicture].Id,
                        ProfilePicture = pictures[userDTO.ProfilePicture]
                    };

                    context.Add(newUser);
                    sb.AppendLine($"Successfully imported User {newUser.Username}.");
                    usedUsernames.Add(newUser.Username);
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            UserFollowerDTO[] usersFollowerDTOs = JsonConvert.DeserializeObject<UserFollowerDTO[]>(jsonString);

            Dictionary<string, User> users = context.Users.ToDictionary(u => u.Username);
            StringBuilder sb = new StringBuilder();

            HashSet<string> alreadyAddedPairs = new HashSet<string>();

            foreach (UserFollowerDTO userFollowerDTO in usersFollowerDTOs)
            {
                bool validInput = !string.IsNullOrWhiteSpace(userFollowerDTO.User) && !string.IsNullOrWhiteSpace(userFollowerDTO.Follower);
                bool userAndFollowerAreDifferent = !userFollowerDTO.Follower.Equals(userFollowerDTO.User);

                if (validInput && userAndFollowerAreDifferent)
                {
                    bool userExists = users.ContainsKey(userFollowerDTO.User);
                    bool followerExists = users.ContainsKey(userFollowerDTO.Follower);

                    if (userExists && followerExists)
                    {
                        User user = users[userFollowerDTO.User];
                        User follower = users[userFollowerDTO.Follower];

                        bool pairAlreadyExists = alreadyAddedPairs.Contains(user.Username + follower.Username);

                        if (!pairAlreadyExists)
                        {
                            UserFollower newUserFollower = new UserFollower()
                            {
                                UserId = user.Id,
                                User = user,
                                FollowerId = follower.Id,
                                Follower = follower
                            };

                            context.UsersFollowers.Add(newUserFollower);
                            sb.AppendLine($"Successfully imported Follower {follower.Username} to User {user.Username}");
                            alreadyAddedPairs.Add(user.Username + follower.Username);

                            continue;
                        }
                    }
                }

                sb.AppendLine("Error: Invalid data.");
            }

            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
    }
}