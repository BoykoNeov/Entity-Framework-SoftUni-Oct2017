namespace OnlineRadioDatabase
{
    using System;

   internal class Messages
    {
        public static void InvalidSongException()
        {
            throw new Exception("Invalid song.");
        }

        public static void InvalidArtistNameException()
        {
            throw new Exception("Artist name should be between 3 and 20 symbols.");
        }

        public static void InvalidSongNameException()
        {
            throw new Exception("Song name should be between 3 and 30 symbols.");
        }

        public static void InvalidSongLengthException()
        {
            throw new Exception("Invalid song length.");
        }

        public static void InvalidSongMinutesException()
        {
            throw new Exception("Song minutes should be between 0 and 14.");
        }

        public static void InvalidSongSecondsException()
        {
            throw new Exception("Song seconds should be between 0 and 59.");
        }
    }
}