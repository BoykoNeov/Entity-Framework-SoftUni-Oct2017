using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using System.Xml;
using Newtonsoft.Json;

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
                 .OrderBy(p => p.Id);

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
                .OrderByDescending(u => u.Followers);

            string result = JsonConvert.SerializeObject(popularUsers, Newtonsoft.Json.Formatting.Indented);
            // System.Console.WriteLine(result);
            return result;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            throw new NotImplementedException();
        }
    }
}