namespace PhotoShare.Client.Core.Commands
{
    using Client.Utilities;
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class AddTagToCommand
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            string albumName = data[0];
            string tagName = TagUtilities.ValidateOrTransform(data[1]);

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Tag tag = context.Tags.SingleOrDefault(t => t.Name == tagName);
                Album album = context.Albums.SingleOrDefault(a => a.Name == albumName);

                if (tag == null || album == null)
                {
                    throw new ArgumentException("Either album or tag do not exists!");
                }

                AlbumTag albumTag = new AlbumTag()
                {
                    AlbumId = album.Id,
                    Album = album,
                    TagId = tag.Id,
                    Tag = tag
                };

                if (context.AlbumTags.Any(at => at == albumTag))
                {
                    throw new InvalidOperationException($"Tag {tag.Name} is already added to album {album.Name} !");
                }

                album.AlbumTags.Add(albumTag);
                context.SaveChanges();
            }

            return $"Tag {tagName} added to {albumName}";
        }
    }
}