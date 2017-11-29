namespace PhotoShare.Client.Core.Commands
{
    using Client.Utilities;
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>      

        public static string Execute(string[] data)
        {
            var userName = data[0];
            var albumTitle = data[1];
            var colorToString = data[2];
            var tags = data.Skip(3).Select(tag => TagUtilities.ValidateOrTransform(tag)).ToArray();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                User currentUser = context.Users
                                        .Where(u => u.Username == userName)
                                        .FirstOrDefault();

                if (currentUser == null)
                {
                    throw new ArgumentException($"User {userName} not found!");
                }

                if (context.Albums.SingleOrDefault(a => a.Name == albumTitle) != null)
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                if (Enum.TryParse(colorToString, out Color color))
                {
                    throw new ArgumentException($"Color {colorToString} not found!");
                }

                foreach (var tag in tags)
                {
                    if (context.Tags.SingleOrDefault(t => t.Name == tag) == null)
                    {
                        throw new ArgumentException("Invalid tags!");
                    }
                }

                Album album = new Album
                {
                    Name = albumTitle,
                    BackgroundColor = (Color)color

                };

                context.AlbumRoles.Add(new AlbumRole
                {
                    User = currentUser,
                    Album = album,
                    Role = Role.Owner
                });

                foreach (var tag in tags)
                {
                    var currentTag = context.Tags.SingleOrDefault(t => t.Name == tag);

                    context.AlbumTags.Add(new AlbumTag
                    {
                        Tag = currentTag,
                        Album = album
                    });
                }

                context.SaveChanges();

                return $"Album {albumTitle} successfully created!";
            }
        }
    }
}