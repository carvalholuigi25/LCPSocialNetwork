using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IComicBooks
    {
        public List<ComicBooks>? GetComicBooks();
        public ComicBooks GetComicBooksDetails(int id);
        public void AddComicBooks(ComicBooks comicbooks);
        public void UpdateComicBooks(ComicBooks comicbooks);
        public ComicBooks DeleteComicBooks(int id);
        public void ResetAIById(string? tblname = "ComicBook", int? id = 0);
        public bool CheckComicBooks(int id);
    }
}
