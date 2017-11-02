namespace OnlineRadioDatabase
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Playlist
    {
        internal List<Song> _allSongs;

       internal Playlist(int expectedSongCount = Constants.expectedTotalSongsCount)
        {
            this._allSongs = new List<Song>(expectedSongCount);
        }

        internal void AddSong(Song newSong)
        {
            this._allSongs.Add(newSong);
        }

        internal int SongCount()
        {
            return this._allSongs.Count;
        }

        internal int TotalLengthInSeconds()
        {
            int totalMinutesSumInSeconds = this._allSongs.Select(x => x.SongMinutes).Sum() * 60;
            int totalSecondsSum = this._allSongs.Select(x => x.SongSeconds).Sum();
            int result = totalMinutesSumInSeconds + totalSecondsSum;

            return result;
        }
    }
}