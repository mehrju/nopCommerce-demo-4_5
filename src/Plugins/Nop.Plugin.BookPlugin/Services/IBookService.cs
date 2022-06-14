using Nop.Core;
using Nop.Plugin.BookPlugin.Domains;

namespace Nop.Plugin.BookPlugin.Services
{
    public partial interface IBookService
    {
        Task<IPagedList<Book>> GetAllBooksAsync(string name = null, string author = null, string publishDate = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        Task DeleteBookAsync(Book book);

        Task<Book> GetBookByIdAsync(int bookId);

        Task<IList<Book>> GetBooksByIdsAsync(int[] bookIds);

        Task InsertBookAsync(Book book);

        Task UpdateBookAsync(Book book);
    }
}
