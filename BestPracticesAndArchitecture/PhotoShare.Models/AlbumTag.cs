namespace PhotoShare.Models
{
    public class AlbumTag
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as AlbumTag;

            if (item == null)
            {
                return false;
            }

            return (this.AlbumId.Equals(item.AlbumId) && this.TagId.Equals(item.TagId));
        }

        public override int GetHashCode()
        {
            int hash = 997;
            hash = hash * 19 + this.AlbumId.GetHashCode();
            hash = hash * 19 + this.TagId.GetHashCode();

            return hash;
        }
    }
}