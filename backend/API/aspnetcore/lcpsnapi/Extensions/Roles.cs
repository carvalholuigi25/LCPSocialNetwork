using Microsoft.AspNetCore.Authorization;
using static lcpsnapi.Classes.Enums;

namespace lcpsnapi.Extensions
{
    public class AuthMyRoles  :  AuthorizeAttribute
    {
        public AuthMyRoles() => Roles = UserRole.user.ToString() + "," + UserRole.editor.ToString() + "," + UserRole.moderator.ToString() + "," + UserRole.admin.ToString() + "," + UserRole.superadmin.ToString();
    }

    public class AuthOnlyAdmins : AuthorizeAttribute
    {
        public AuthOnlyAdmins() => Roles = UserRole.superadmin.ToString() + "," + UserRole.admin.ToString();
    }

    public class AuthOnlyMods : AuthorizeAttribute
    {
        public AuthOnlyMods() => Roles = UserRole.moderator.ToString();
    }

    public class AuthOnlyEditors : AuthorizeAttribute
    {
        public AuthOnlyEditors() => Roles = UserRole.editor.ToString();
    }

    public class AuthOnlyUsers : AuthorizeAttribute
    {
        public AuthOnlyUsers() => Roles = UserRole.user.ToString();
    }

    public class AuthOnlyGuests : AuthorizeAttribute
    {
        public AuthOnlyGuests() => Roles = UserRole.guest.ToString();
    }

    public class AuthSpecRole : AuthorizeAttribute
    {
        public AuthSpecRole(string[]? role)
        {
            //to use it: 
            //[AuthSpecRole(new string[] { "admin", "moderator" })]

            //to test it:
            //Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJsb2NhbGhvc3QiLCJqdGkiOiI3NjY5ODFjMi05YTY0LTQ5NWItOGJmNi1kMjE4YTBkMGQxOWYiLCJpYXQiOiIyMS8wOS8yMDIyIDA5OjAxOjAyIiwiVXNlcklkIjoiMSIsIkRpc3BsYXluYW1lIjoiTHVpcyBDYXJ2YWxobyIsIlVzZXJuYW1lIjoibHVpZ2ljYXI5NiIsIkVtYWlsIjoibHVpc2NhcnZhbGhvMjM5QGdtYWlsLmNvbSIsIlJvbGUiOiJzdXBlcmFkbWluIiwiZXhwIjoxNjcxNjEzMjYyLCJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QifQ.ER0Ozuf2D8tsQH8g2iH4PqtA5HEe41feb_-YTO47BN8

            if (role?.Length > 0)
            {
                var roleslist = "";

                for (var i = 0; i < role.Length; i++)
                {
                    roleslist += i >= 0 && i < role.Length - 1 ? role[i].ToString() + "," : role[i].ToString();
                }

                if(roleslist.Length > 0)
                {
                    var rolestxt = roleslist.Split(',').ToList();

                    //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rolestxt));

                    if (rolestxt.Count() == 0)
                    {
                        throw new ArgumentException("The roles are empty!");
                    }

                    var myrolest = ""; var ix = 0;

                    foreach(var item in rolestxt)
                    {
                        if(role.Contains(item))
                        {
                            ix++;
                            myrolest += ix >= 0 && ix < rolestxt.Count() ? item + "," : item;
                        }
                    }

                    Roles = myrolest switch
                    {
                        "superadmin" => UserRole.superadmin.ToString(),
                        "admin" => UserRole.admin.ToString(),
                        "moderator" => UserRole.moderator.ToString(),
                        "editor" => UserRole.editor.ToString(),
                        "user" => UserRole.user.ToString(),
                        "guest" => UserRole.user.ToString(),
                        "all" => UserRole.user.ToString() + "," + UserRole.editor.ToString() + "," + UserRole.moderator.ToString() + "," +
                         UserRole.admin.ToString() + "," + UserRole.superadmin.ToString(),
                        _ => UserRole.guest.ToString()
                    };
                }
                else
                {
                    throw new ArgumentException("The roles are empty! Please provide one of them: admin, superadmin, moderator, editor, user, guest or all.");
                }
            }
        }
    }
}
