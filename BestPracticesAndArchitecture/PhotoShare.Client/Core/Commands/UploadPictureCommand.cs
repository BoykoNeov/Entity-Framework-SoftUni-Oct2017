namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var albumName = data[0];
            var pictureTitle = data[1];
            var picturePath = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var album = context.Albums
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(ar => ar.User)
                    .SingleOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                var picture = new Picture()
                {
                    Title = pictureTitle,
                    Path = picturePath,
                    Album = album
                };

                context.Pictures.Add(picture);
                context.SaveChanges();

                return $"Picture {pictureTitle} added to {albumName}!";
            }
        }
    }
}