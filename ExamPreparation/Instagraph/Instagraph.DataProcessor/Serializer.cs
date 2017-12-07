using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var outputUsers = context.Posts
                 .Include(p => p.Comments)
                 .Include(p => p.Picture)
                 .Include(p => p.User)
                 .Where(p => p.Comments.Count == 0)
                 .Select(p => new
                 {
                     Id = p.Id,
                     Picture = p.Picture.Path,
                     User = p.User.Username
                 })
                 .OrderBy(p => p.Id)
                 .ToArray();

            string result = JsonConvert.SerializeObject(outputUsers, Newtonsoft.Json.Formatting.Indented);
            // System.Console.WriteLine(result);
            return result;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var popularUsers = context.Users
                //   .Include(u => u.Posts)
                //   .ThenInclude(p => p.Comments)
                // .ThenInclude(p => p.User)
                //   .Include(u => u.Followers)
                // .Where(u => u.Posts.Any(po => po.Comments.Any(c => u.Followers.Any(f => f.FollowerId == c.UserId))))
                .Where(u => u.Followers.Any(f => u.Posts.Any(p => p.Comments.Any(c => c.UserId == f.FollowerId))))
                .Select(u => new
                {
                    Username = u.Username,
                    Followers = u.Followers.Count()
                })
                .OrderBy(u => u.Followers)
                .ToArray();

            string result = JsonConvert.SerializeObject(popularUsers, Newtonsoft.Json.Formatting.Indented);
            // System.Console.WriteLine(result);
            return result;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var usersAndPost = context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    MostComments = u.Posts.OrderByDescending(p => p.Comments.Count).FirstOrDefault() != null ? u.Posts.OrderByDescending(p => p.Comments.Count).FirstOrDefault().Comments.Count : 0
                })
                .OrderByDescending(u => u.MostComments)
                .ThenBy(u => u.Username)
                .ToArray();

            XDocument xUsersAndPost = new XDocument(new XElement("users"));
            // alternative 
            // XDocument xUsersAndPost = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            // at the end:
            // return doc.ToString();

            foreach (var item in usersAndPost)
            {
                XElement user = new XElement("user");

                user.Add(new XElement("Username", item.Username));
                user.Add(new XElement("MostComments",item.MostComments));

                xUsersAndPost.Root.Add(user);
            }

            string result = XMLSerializer.SerializeXML(xUsersAndPost, true);
            return result;
        }
    }
}