using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IAnimes
    {
        public List<Animes>? GetAnimes();
        public Animes GetAnimesDetails(int id);
        public void AddAnimes(Animes animes);
        public void UpdateAnimes(Animes animes);
        public Animes DeleteAnimes(int id);
        public void ResetAIById(string? tblname = "Anime", int? id = 0);
        public bool CheckAnimes(int id);
    }
}
