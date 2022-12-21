using static lcpsnapi.Classes.Enums;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using lcpsnapi.Functions;

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
                        Email = "luiscarvalho239@gmail.com",
                        Password = BC.HashPassword("luigi2023", 11),
                        Pin = BC.HashPassword("2023", 11),
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
                        Email = "guest@localhost.loc",
                        Password = BC.HashPassword("guest1234", 11),
                        Pin = BC.HashPassword("1234", 11),
                        FirstName = "Guest",
                        LastName = "Convidado",
                        Displayname = "Guest Convidado",
                        Country = "Italy",
                        Image = "/assets/images/users/guest.png",
                        Cover = "/assets/images/users/c_guest.png",
                        Role = UserRole.guest,
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
                        Password = BC.HashPassword("luigi1234", 11),
                        Pin = BC.HashPassword("1234", 11),
                        Displayname = "Luis Carvalho",
                        Image = "/assets/images/users/luigi.png",
                        Cover = "/assets/images/users/c_luigi.png",
                        DateCreated = DateTime.UtcNow,
                        DateExp = DateTime.UtcNow.AddMonths(1).ToString(),
                        Token = MyGenTokens.GenTokenOnly("luigicar96", UserRole.superadmin, TokenUnitTime.months, 1),
                        UsersId = 1
                    },
                    new UsersToken
                    {
                        UsersTokenId = 2,
                        Username = "guest",
                        Email = "guest@localhost.loc",
                        Password = BC.HashPassword("guest1234", 11),
                        Pin = BC.HashPassword("1234", 11),
                        Displayname = "Guest Convidado",
                        Image = "/assets/images/users/guest.png",
                        Cover = "/assets/images/users/c_guest.png",
                        DateCreated = DateTime.UtcNow,
                        DateExp = DateTime.UtcNow.AddMonths(1).ToString(),
                        Token = MyGenTokens.GenTokenOnly("guest", UserRole.guest, TokenUnitTime.months, 1),
                        UsersId = 2
                    }
                };

                mb.Entity<Users>().HasData(DefUsers);
                mb.Entity<UsersToken>().HasData(DefUsersTokens);
            }
        }
    }
}
