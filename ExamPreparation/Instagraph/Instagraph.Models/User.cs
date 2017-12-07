namespace Instagraph.Models
{
    using System.Collections.Generic;

   public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<User> UsersFollowing { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}