namespace Laobian.Blog.Models
{
    public class Pagination
    {
        public Pagination(int currentPage, int totalPages)
        {
            CurrentPage = currentPage;
            if (CurrentPage <= 0) CurrentPage = 1;

            if (CurrentPage > totalPages) CurrentPage = totalPages;

            TotalPages = totalPages;
        }

        public int TotalPages { get; }

        public int CurrentPage { get; }
    }
}