namespace OnlineRadioDatabase
{
    using System;

    internal class Validator
    {
        public static bool IsArtistNameValid(string artistName)
        {
            bool isNotNullOrWhiteSpace = !string.IsNullOrWhiteSpace(artistName);
            bool isInValidRange = (artistName.Length >= Constants.ArtistNameMinLength) && (artistName.Length <= Constants.ArtistNameMaxLength);

            return isNotNullOrWhiteSpace && isInValidRange;
        }

        public static bool IsSongNamevalid(string songName)
        {
            bool isNotNullOrWhiteSpace = !string.IsNullOrWhiteSpace(songName);
            bool isInValidRange = (songName.Length >= Constants.SongNameMinLength) && (songName.Length <= Constants.SongNameMaxLength);

            return isNotNullOrWhiteSpace && isInValidRange;
        }

        public static bool IsSongMinutesValueValid(int minutes)
        {
            bool isInValidRange = (minutes >= Constants.SongMinutesMinCount) && (minutes <= Constants.SongMinutesMaxCount);

            return isInValidRange;
        }

        public static bool IsSongSecondsValueValid(int seconds)
        {
            bool isInValidRange = (seconds >= Constants.SongSecondsMinCount) && (seconds <= Constants.SongSecondsMaxCount);

            return isInValidRange;
        }

        public static bool IsSongLengthFormatValid(string songLengthArgument)
        {
            string[] inputs = songLengthArgument.Split(new char[] { Constants.minutesSecondsSplitSymbol }, StringSplitOptions.None);

            if (inputs.Length != 2)
            {
                return false;
            }
            else
            {
                string minutes = inputs[0];
                string seconds = inputs[1];

                bool minutesParsedSuccessfully = int.TryParse(minutes, out int tempMin);
                bool secondsParsedSuccessfully = int.TryParse(seconds, out int tempSec);

                return minutesParsedSuccessfully && secondsParsedSuccessfully;
            }
        }
    }
}
