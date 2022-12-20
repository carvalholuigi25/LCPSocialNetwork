using lcpsnapi.Classes;
using Microsoft.AspNetCore.Mvc;

namespace lcpsnapi.Interfaces
{
    public interface IUsers
    {
        public List<Users>? GetUsers();
        public Users GetUsersDetails(int id);
        public void AddUsers(Users user);
        public void UpdateUsers(Users user);
        public Users DeleteUsers(int id);
        public Task<ActionResult<UsersToken?>> DoUserLog([FromBody] UsersTokenLogin? users);
        public Task<ActionResult<UsersToken?>> DoUserReg([FromBody] UsersToken? users, Enums.UserRole? role = Enums.UserRole.user);
        public bool CheckIfPasswordIsValid(string password, string hashedpassword);
        public bool CheckUsers(int id);
        public bool CheckUsersIfNull(int? id);
    }
}
