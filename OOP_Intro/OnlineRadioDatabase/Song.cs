namespace OnlineRadioDatabase
{
   internal class Song
    {
        private string _name;
        private string _artist;
        private int _songMinutes;
        private int _songSeconds;

        internal Song() { }

        internal Song(string artistName, string songName, int minutes, int seconds)
        {
            this.Artist = artistName;
            this.Name = songName;
            this.SongMinutes = minutes;
            this.SongSeconds = seconds;
        }

        public int SongSeconds
        {
            get
            {
                return _songSeconds;
            }
            set
            {
                if (!Validator.IsSongSecondsValueValid(value))
                {
                    Messages.InvalidSongSecondsException();
                }

                _songSeconds = value;
            }
        }

        public int SongMinutes
        {
            get
            {
                return _songMinutes;
            }
            set
            {
                if (!Validator.IsSongMinutesValueValid(value))
                {
                    Messages.InvalidSongMinutesException();
                }
                _songMinutes = value;
            }
        }

        public string Artist
        {
            get
            {
                return _artist;
            }
            set
            {
                if (!Validator.IsArtistNameValid(value))
                {
                    Messages.InvalidArtistNameException();
                }
                _artist = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!Validator.IsSongNamevalid(value))
                {
                    Messages.InvalidSongNameException();
                }
                _name = value;
            }
        }
    }
}