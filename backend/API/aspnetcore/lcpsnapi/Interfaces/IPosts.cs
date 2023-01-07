using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IPosts
    {
        public List<Posts>? GetPosts();
        public Posts GetPostsDetails(int id);
        public void AddPosts(Posts post);
        public void UpdatePosts(Posts post);
        public Posts DeletePosts(int id);
        public void ResetAIById(string? tblname = "Post", int? id = 0);
        public bool CheckPosts(int id);
    }
}
