using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Model
{
    [Table("comments")]
    public class Comment
    {
        [Column("post_id")] public int PostId { get; set; }
        [Column("id")] public int Id { get; set; }
        [Column("content")] public string? Content { get; set; }
        [Column("creation_time")] public DateTime? Date { get; set; }
        public int? Author_Id { get; set; }
        [ForeignKey("Author_Id")] public User? Author { get; set; }
        [Column("likes")] public int likes { get; set; }
        [Column("Author_Name")] public string? AuthorName { get; set; }
    }
}