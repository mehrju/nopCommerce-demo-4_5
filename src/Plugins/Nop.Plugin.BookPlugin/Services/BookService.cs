using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Data;
using Nop.Plugin.BookPlugin.Domains;
using Nop.Services.Common;

namespace Nop.Plugin.BookPlugin.Services
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

        public virtual async Task<IPagedList<Book>> GetAllBooksAsync(string name = null, string author = null, string publishDate = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var books = await _bookRepository.GetAllPagedAsync(query =>
            {
                query = query.Where(c => !c.Deleted);

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(c => c.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(author))
                {
                    query = query.Where(c => c.Author.Contains(author));
                }

                if (!string.IsNullOrWhiteSpace(publishDate))
                {
                    query = query.Where(c => c.PublishDate.Contains(publishDate));
                }

                query = query.OrderBy(c => c.Name);

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
