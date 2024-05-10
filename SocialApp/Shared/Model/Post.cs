using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Model
{
    [Table("posts")]
    public class Post : IEquatable<Post>
    {
        [Column("id")] public int Id { get; set; }
        [Column("likes")] public int likes { get; set; }
        public int Author_Id { get; set; }
        [Column("header")] public string? Header { get; set; }
        [Column("content")] public string? Content { get; set; }
        [Column("creation_time")] public DateTime? Date { get; set; }

        [ForeignKey("Author_Id")] public User? Author { get; set; }

        public bool Equals(Post? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id
                   && Author_Id == other.Author_Id
                   && Header == other.Header
                   && Content == other.Content
                   && Date.ToString() == other.Date.ToString()
                   && Equals(Author, other.Author);
        }
    }
}