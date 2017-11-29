namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                int albumId = int.Parse(data[0]);

                Album album = context.Albums
                    .Include(a => a.AlbumRoles)
                    .SingleOrDefault(a => a.Id == albumId);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                string username = data[1];

                User user = context.Users
                    .SingleOrDefault(u => u.Username.Equals(username));

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                bool isPermissionValid = Enum.TryParse(data[2], true, out Role permissionRole);

                if (!isPermissionValid)
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                AlbumRole role = new AlbumRole
                {
                    Album = album,
                    User = user,
                    Role = permissionRole
                };

                if (album.AlbumRoles.Any(r => r.UserId == user.Id && r.AlbumId == album.Id))
                {
                    Role currentRole = album.AlbumRoles.Single(r => r.UserId == user.Id && r.AlbumId == album.Id).Role;

                    throw new ArgumentException($"User {username} has already assigned {currentRole.ToString()} role to album {album.Name}.");
                }

                album.AlbumRoles.Add(role);
                context.SaveChanges();

                return $"Username {user.Username} added to album {album.Name} ({permissionRole.ToString()})";
            }
        }
    }
}