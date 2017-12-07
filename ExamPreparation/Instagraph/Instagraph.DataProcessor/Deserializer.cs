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
                bool hasPath = !string.IsNullOrWhiteSpace(pic.Path);

                bool pathIsUnique = false;

                if (hasPath)
                {
                    pathIsUnique = !paths.Contains(pic.Path);
                }

                bool sizeIsBiggerThanZero = pic.Size > 0m;

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
            string result = sb.ToString().Trim();
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
                //  bool userAndFollowerAreDifferent = !userFollowerDTO.Follower.Equals(userFollowerDTO.User);

                if (validInput
                    //&& userAndFollowerAreDifferent
                    )
                {
                    bool userExists = users.ContainsKey(userFollowerDTO.User);
                    bool followerExists = users.ContainsKey(userFollowerDTO.Follower);

                    if (userExists && followerExists)
                    {
                        User user = users[userFollowerDTO.User];
                        User follower = users[userFollowerDTO.Follower];

                        bool pairAlreadyExists = alreadyAddedPairs.Contains(userFollowerDTO.User + userFollowerDTO.Follower);

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
                            sb.AppendLine($"Successfully imported Follower {userFollowerDTO.Follower} to User {userFollowerDTO.User}.");
                            alreadyAddedPairs.Add(userFollowerDTO.User + userFollowerDTO.Follower);

                            continue;
                        }
                    }
                }

                sb.AppendLine("Error: Invalid data.");
            }

            context.SaveChanges();

            string result = sb.ToString().Trim();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            Dictionary<string, User> users = context.Users.ToDictionary(u => u.Username);
            Dictionary<string, Picture> pictures = context.Pictures.ToDictionary(u => u.Path);
            StringBuilder sb = new StringBuilder();

            XDocument xmlPostsDoc = XDocument.Parse(xmlString);
            IEnumerable<XElement> xPosts = xmlPostsDoc.Root.Elements();
            List<Post> posts = new List<Post>();

            foreach (XElement xPost in xPosts)
            {
                string username = xPost.Element("user")?.Value;
                string caption = xPost.Element("caption")?.Value;
                string picturePath = xPost.Element("picture")?.Value;

                bool usernameIsNotNullOrEmpty = !string.IsNullOrWhiteSpace(username);
                bool captionIsNotNullOrEmpty = !string.IsNullOrWhiteSpace(caption);
                bool pictureIsNotNullOrEmpty = !string.IsNullOrWhiteSpace(picturePath);

                bool userExists = false;

                if (usernameIsNotNullOrEmpty)
                {
                    userExists = users.ContainsKey(username);
                }

                bool pictureExists = false;

                if (pictureIsNotNullOrEmpty)
                {
                    pictureExists = pictures.ContainsKey(picturePath);
                }

                if (captionIsNotNullOrEmpty && userExists && pictureExists)
                {
                    User user = users[username];
                    Picture picture = pictures[picturePath];

                    Post newPost = new Post()
                    {
                        Caption = caption,
                        UserId = user.Id,
                        PictureId = picture.Id
                    };

                    posts.Add(newPost);
                    sb.AppendLine($"Successfully imported Post {caption}.");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.AddRange(posts);
            context.SaveChanges();

            string result = sb.ToString().Trim();
            return result;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            Dictionary<int, Post> posts = context.Posts.ToDictionary(p => p.Id);
            Dictionary<string, User> users = context.Users.ToDictionary(u => u.Username);
            StringBuilder sb = new StringBuilder();

            XDocument xmlCommentsDoc = XDocument.Parse(xmlString);
            IEnumerable<XElement> xComments = xmlCommentsDoc.Root.Elements();
            List<Comment> comments = new List<Comment>();

            foreach (XElement xComment in xComments)
            {
                string commentUsername = xComment.Element("user")?.Value;
                bool postIdParseSuccess = int.TryParse(xComment.Element("post")?.Attribute("id")?.Value, out int commentPostId);
                string commentContent = xComment.Element("content")?.Value;

                bool userExists = false;

                if (!string.IsNullOrWhiteSpace(commentUsername))
                {
                    userExists = users.ContainsKey(commentUsername);
                }

                bool postExists = false;

                if (postIdParseSuccess)
                {
                    postExists = posts.ContainsKey(commentPostId);
                }

                bool commentIsValid = false;
                if (!string.IsNullOrWhiteSpace(commentContent) && commentContent.Length <= 250)
                {
                    commentIsValid = true;
                }

                if (userExists && postExists && commentIsValid)
                {
                    User commentingUser = users[commentUsername];
                    Post commentedPost = posts[commentPostId];

                    Comment newComment = new Comment()
                    {
                        UserId = commentingUser.Id,
                        PostId = commentedPost.Id,
                        Content = commentContent
                    };

                    sb.AppendLine($"Successfully imported Comment {commentContent}.");
                    comments.Add(newComment);
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }
    }
}