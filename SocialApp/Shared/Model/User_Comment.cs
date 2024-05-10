using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Model
{
    [Table("users_likedcomments")]
    public class User_Comment
    {
        [Column("id")] public int Id { get; set; }
        [Column("user_id")] public int UserId { get; set; }
        [Column("comment_id")] public int CommentId { get; set; }
    }
}