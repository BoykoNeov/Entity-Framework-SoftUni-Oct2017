namespace OnlineRadioDatabase
{
    internal class Constants
    {
        internal const int expectedTotalSongsCount = 10;

       internal const int ArtistNameMinLength = 3;
       internal const int ArtistNameMaxLength = 20;
       internal const int SongNameMinLength = 3;
       internal const int SongNameMaxLength = 30;
       internal const int SongMinutesMinCount = 0;
       internal const int SongMinutesMaxCount = 14;
       internal const int SongSecondsMinCount = 0;
       internal const int SongSecondsMaxCount = 59;

       internal const int AuthorNameIndex = 1;
       internal const int SongNameIndex = 2;
       internal const int SongMinutesIndex = 3;
       internal const int SongSecondsIndex = 4;

       internal const char commandSplitSymbol = ';' ;
       internal const char minutesSecondsSplitSymbol = ':' ;
    }
}