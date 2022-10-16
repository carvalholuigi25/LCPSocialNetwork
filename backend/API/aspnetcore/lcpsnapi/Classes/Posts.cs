using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Classes
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PostId { get; set; }
        public string? Title { get; set; }
        public string? Shortdesc { get; set; }
        public string? Image { get; set; }
        public string? Text { get; set; }
        public Attachments[]? Attachments { get; set; }
        public Status? Status { get; set; }
        public Privacy? Privacy { get; set; }
        public bool? IsFeatured { get; set; }
        public string? DateCreated { get; set; }
        public string? DateModified { get; set; }
        public string? DateDeleted { get; set; }
        public int? UsersId { get; set; }
        public int? ReactsId { get; set; }
    }

    public class PostsReactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ReactsId { get; set; }
        public string? ImageReact { get; set; }
        public ReactType? TypeReact { get; set; }
        public int? Counter { get; set; }
        public int? PostsId { get; set; }
        public int? CommentsId { get; set; }
        public int? RepliesId { get; set; }
        public int? UsersId { get; set; }
        public string? DateReaction { get; set; }
    }

    public class PostsComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? CommentsId { get; set; }
        public string? Desc { get; set; }
        public Status? Status { get; set; }
        public Privacy? Privacy { get; set; }
        public string? DateCreated { get; set; }
        public string? DateModified { get; set; }
        public string? DateDeleted { get; set; }
        public int? UsersId { get; set; }
        public int? RepliesId { get; set; }
        public int? ReactsId { get; set; }
    }

    public class PostsCommentsReplies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? RepliesId { get; set; }
        public string? Desc { get; set; }
        public Status? Status { get; set; }
        public Privacy? Privacy { get; set; }
        public string? DateCreated { get; set; }
        public string? DateModified { get; set; }
        public string? DateDeleted { get; set; }
        public int? UsersId { get; set; }
        public int? CommentsId { get; set; }
        public int? ReactsId { get; set; }
    }
}