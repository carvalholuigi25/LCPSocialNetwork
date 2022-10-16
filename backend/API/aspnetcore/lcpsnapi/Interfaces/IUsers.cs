using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IUsers
    {
        public List<Users>? GetUsers();
        public Users GetUsersDetails(int id);
        public void AddUsers(Users user);
        public void UpdateUsers(Users user);
        public Users DeleteUsers(int id);
        public bool CheckUsers(int id);
    }
}
