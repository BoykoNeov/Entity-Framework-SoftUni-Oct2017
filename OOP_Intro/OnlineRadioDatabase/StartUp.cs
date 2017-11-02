/// <summary>
/// Task 6 from OOP Introduction, Entity Framework Course @ SoftUni
/// Much better quality code this time (not perfect though)
/// </summary>
namespace OnlineRadioDatabase
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            int inputLinesCount = int.Parse(Console.ReadLine());
            Playlist playlist = new Playlist(inputLinesCount);

            for (int i = 0; i < inputLinesCount; i++)
            {
                try
                {
                    string[] commandArgs = Console.ReadLine().Split(new char[] { Constants.commandSplitSymbol }, StringSplitOptions.None);

                    if (commandArgs.Length != 3)
                    {
                        Messages.InvalidSongException();
                    }

                    string artistName = commandArgs[0];
                    string songName = commandArgs[1];
                    Song newSong = new Song();
                    newSong.Artist = artistName;
                    newSong.Name = songName;

                    string songLengthArgsPart = commandArgs[2];

                    if (!Validator.IsSongLengthFormatValid(songLengthArgsPart))
                    {
                        Messages.InvalidSongLengthException();
                    }

                    string[] LengthArgs = songLengthArgsPart.Split(new char[] { Constants.minutesSecondsSplitSymbol }, StringSplitOptions.None);
                    int minutes = int.Parse(LengthArgs[0]);
                    int seconds = int.Parse(LengthArgs[1]);

                    newSong.SongMinutes = minutes;
                    newSong.SongSeconds = seconds;

                    playlist.AddSong(newSong);
                    Console.WriteLine("Song added.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine($"Songs added: {playlist.SongCount()}");

            int totalHours = playlist.TotalLengthInSeconds() / 3600;
            int totalMinutes = (playlist.TotalLengthInSeconds() % 3600) / 60;
            int totalSeconds = playlist.TotalLengthInSeconds() % 60;

            Console.WriteLine($"Playlist length: {totalHours}h {totalMinutes}m {totalSeconds}s ");
        }
    }
}