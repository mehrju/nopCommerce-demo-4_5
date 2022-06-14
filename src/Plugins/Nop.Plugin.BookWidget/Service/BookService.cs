using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Data;
using Nop.Plugin.BookWidget.Models;
using Nop.Services.Common;

namespace Nop.Plugin.BookWidget.Service
{
    public partial class BookService : IBookService
    {
        #region Fields

        private readonly IGenericAttributeService _genericAttributeService;
        private readonly INopDataProvider _dataProvider;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<GenericAttribute> _gaRepository;

        #endregion

        #region Ctor

        public BookService(IGenericAttributeService genericAttributeService,
            INopDataProvider dataProvider,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            IRepository<Book> bookRepository,
            IRepository<GenericAttribute> gaRepository)
        {
            _genericAttributeService = genericAttributeService;
            _dataProvider = dataProvider;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _bookRepository = bookRepository;
            _gaRepository = gaRepository;
        }

        #endregion

        #region Methods

        public virtual async Task<IPagedList<Book>> GetAllBooksAsync(string name = null, string author = null, DateTime? publishDate = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var books = await _bookRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(c => !c.Deleted);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query
                        .Join(_gaRepository.Table, x => x.Id, y => y.EntityId,
                            (x, y) => new { Customer = x, Attribute = y })
                        .Where(z => z.Attribute.KeyGroup == nameof(Book) &&
                                    z.Attribute.Key == BookDefaults.NameAttribute &&
                                    z.Attribute.Value.Contains(name))
                        .Select(z => z.Customer);

                if (!string.IsNullOrWhiteSpace(author))
                    query = query
                        .Join(_gaRepository.Table, x => x.Id, y => y.EntityId,
                            (x, y) => new { Customer = x, Attribute = y })
                        .Where(z => z.Attribute.KeyGroup == nameof(Book) &&
                                    z.Attribute.Key == BookDefaults.AuthorAttribute &&
                                    z.Attribute.Value.Contains(author))
                        .Select(z => z.Customer);

                return query;
            }, pageIndex, pageSize, getOnlyTotalCount);

            return books;
        }

        public virtual async Task DeleteBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            book.Deleted = true;

            await _bookRepository.UpdateAsync(book, false);
            await _bookRepository.DeleteAsync(book);
        }

        public virtual async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.GetByIdAsync(bookId,
                cache => cache.PrepareKeyForShortTermCache(NopEntityCacheDefaults<Book>.ByIdCacheKey, bookId));
        }

        public virtual async Task<IList<Book>> GetBooksByIdsAsync(int[] bookIds)
        {
            return await _bookRepository.GetByIdsAsync(bookIds, includeDeleted: false);
        }

        public virtual async Task InsertBookAsync(Book book)
        {
            await _bookRepository.InsertAsync(book);
        }

        public virtual async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        #endregion
    }
}
