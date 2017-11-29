namespace PhotoShare.Client.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public static string Execute(string[] data)
        {
            string tag = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Tag currentTag = context.Tags
                    .Where(t => t.Name == tag)
                    .FirstOrDefault();

                if (currentTag != null)
                {
                    throw new ArgumentException($"Tag {currentTag.Name} exists!");
                }
                else
                {
                    context.Tags.Add(new Tag
                    {
                        Name = tag
                    });

                    context.SaveChanges();
                }
            }

            return tag + " was added successfully to database!";
        }
    }
}