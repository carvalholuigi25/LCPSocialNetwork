using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IMangas
    {
        public List<Mangas>? GetMangas();
        public Mangas GetMangasDetails(int id);
        public void AddMangas(Mangas mangas);
        public void UpdateMangas(Mangas mangas);
        public Mangas DeleteMangas(int id);
        public void ResetAIById(string? tblname = "Manga", int? id = 0);
        public bool CheckMangas(int id);
    }
}
