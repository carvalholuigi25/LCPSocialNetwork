using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Context
{
    public class SeedData
    {
        private readonly ModelBuilder mb;
        public SeedData(ModelBuilder mb)
        {
            this.mb = mb;
        }

        public void Seed(bool isGonnaSeedDefData = false)
        {
            if(isGonnaSeedDefData)
            {
                List<Users>? DefUsers = new List<Users>() {
                    new Users
                    {
                        Id = 1,
                        Username = "luigicar96",
                        Password = "1234",
                        Email = "luiscarvalho239@gmail.com",
                        Pin = "1234",
                        FirstName = "Luis",
                        LastName = "Carvalho",
                        Displayname = "Luis Carvalho",
                        Country = "Portugal",
                        Image = "/assets/images/users/luigi.png",
                        Cover = "/assets/images/users/c_luigi.png",
                        Role = UserRole.superadmin,
                        TypeFriend = TypeFriend.friend,
                        Status = UserStatus.online,
                        PrivacyStatus = UserPrivacyStatus.publictxt,
                        DateBirthday = "1996-06-04T00:00:00",
                        DateRegistered = "2022-08-19T10:30:00",
                        Info = null,
                        FriendsList = null
                    },
                    new Users
                    {
                        Id = 2,
                        Username = "guest",
                        Password = "1234",
                        Email = "guest@localhost.loc",
                        Pin = "1234",
                        FirstName = "Guest",
                        LastName = "Convidado",
                        Displayname = "Guest Convidado",
                        Country = "Italy",
                        Image = "/assets/images/users/guest.png",
                        Cover = "/assets/images/users/c_guest.png",
                        Role = UserRole.user,
                        TypeFriend = TypeFriend.friend,
                        Status = UserStatus.offline,
                        PrivacyStatus = UserPrivacyStatus.privatetxt,
                        DateBirthday = "1995-05-03T00:00:00",
                        DateRegistered = "2022-08-31T15:50:00",
                        Info = null,
                        FriendsList = null
                    }
                };

                List <UsersToken> DefUsersTokens = new List<UsersToken>()
                {
                    new UsersToken
                    {
                        UsersTokenId = 1,
                        Username = "luigicar96",
                        Email = "luiscarvalho239@gmail.com",
                        Password = null,
                        Pin = null,
                        Displayname = "Luis Carvalho",
                        DateCreated = DateTime.UtcNow,
                        DateExp = DateTime.UtcNow.AddMonths(1).ToString(),
                        Token = "",
                        UsersId = 1
                    },
                    new UsersToken
                    {
                        UsersTokenId = 2,
                        Username = "guest",
                        Email = "guest@localhost.loc",
                        Password = null,
                        Pin = null,
                        Displayname = "Guest Convidado",
                        DateCreated = DateTime.UtcNow,
                        DateExp = DateTime.UtcNow.AddMonths(1).ToString(),
                        Token = "",
                        UsersId = 2
                    }
                };

                mb.Entity<Users>().HasData(DefUsers);
                mb.Entity<UsersToken>().HasData(DefUsersTokens);
            }
        }
    }
}
