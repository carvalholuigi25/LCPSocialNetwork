using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IUsersToken
    {
        public List<UsersToken>? GetUsersToken();
        public UsersToken GetUsersTokenDetails(int id);
        public void AddUsersToken(UsersToken usersToken);
        public void UpdateUsersToken(UsersToken user);
        public UsersToken DeleteUsersToken(int? id = 1);
        public bool CheckUsersToken(int id);
        public string ResetAI(int id = 0);
    }
}
