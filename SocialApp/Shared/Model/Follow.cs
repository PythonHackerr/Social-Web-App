using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Model;

[Table("follows")]
public class Follow
{
    public int Id { get; set; }
    public int? Follower { get; set; }
    public int? Following { get; set; }

    [ForeignKey("Follower")] public User? FollowerUser { get; set; }
    [ForeignKey("Following")] public User? FollowingUser { get; set; }
}