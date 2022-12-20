using static lcpsnapi.Classes.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using Newtonsoft.Json;

namespace lcpsnapi.Classes
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Displayname { get; set; }
        public string? Email { get; set; }
        public string? Pin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        public string? Image { get; set; }
        public string? Cover { get; set; }
        public UserRole? Role { get; set; }
        public TypeFriend? TypeFriend { get; set; }
        public UserStatus? Status { get; set; }
        public UserPrivacyStatus? PrivacyStatus { get; set; }
        public string? DateBirthday { get; set; }
        public string? DateRegistered { get; set; }
        public List<Friends>? FriendsList { get; set; }
        public Info? Info { get; set; }
        public int? UsersTokenId { get; set; }
    }

    public class Friends
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? FriendsId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Pin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        public string? Image { get; set; }
        public string? Cover { get; set; }
        public UserRole? Role { get; set; }
        public TypeFriend? TypeFriend { get; set; }
        public UserStatus? Status { get; set; }
        public UserPrivacyStatus? PrivacyStatus { get; set; }
        public string? DateBirthday { get; set; }
        public string? DateRegistered { get; set; }
        public int? TotalFriends { get; set; }
        public int? UsersTokenId { get; set; }
    }

    public class Info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? InfoId { get; set; }
        public int? TotalReactions { get; set; }
        public int? TotalComments { get; set; }
        public int? TotalReplies { get; set; }
        public int? TotalShares { get; set; }
        public Posts? LatestPost { get; set; }
        public string? School { get; set; }
        public string? University { get; set; }
        public string? Workorprofession { get; set; }
        public string? About { get; set; }
        public bool? IsSchoolFinished { get; set; }
        public bool? IsUniversityFinished { get; set; }
        public UserPrivacyStatus? Privacy { get; set; }
    }

    public class UsersToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UsersTokenId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Displayname { get; set; }
        public string? Email { get; set; }
        public string? Pin { get; set; }
        public string? Token { get; set; }
        public string? DateExp { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UsersId { get; set; }
    }

    public class UsersTokenLogin
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Pin { get; set; }
    }

    public class UsersTokenInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UsersTokenInfoId { get; set; }
        public DateTime? Date { get; set; }
        public string? Token { get; set; }
        public string? ClaimsInfo { get; set; }
        public bool? IsValid { get; set; }
        public string? Username { get; set; }
        public int? Userid { get; set; }
        public string? Msg { get; set; }
    }
}