using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.BookPlugin.Domains;
using Nop.Plugin.BookPlugin.Models;
using Nop.Plugin.BookPlugin.Services;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.BookPlugin.Factories
{
    public partial class BookModelFactory : IBookModelFactory
    {
        private readonly IBookService _bookService;

        public BookModelFactory(IBookService bookService)
        {
            _bookService = bookService;
        }

        public virtual async Task<BookListModel> PrepareBookListModelAsync(BookSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var books = await _bookService.GetAllBooksAsync(
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize,
                name: searchModel.SearchName,
                author: searchModel.SearchAuthor,
                publishDate: searchModel.SearchPublishDate);

            var model = await new BookListModel().PrepareToGridAsync(searchModel, books, () =>
            {
                return books.SelectAwait(book =>
                {
                    var bookModel = book.ToModel<BookModel>();

                    bookModel.Name = book.Name;
                    bookModel.Author = book.Author;
                    bookModel.PublishDate = book.PublishDate;

                    return new ValueTask<BookModel>(bookModel);
                });
            });

            return model;
        }

        public virtual Task<BookModel> PrepareBookModelAsync(BookModel model, Book book, bool excludeProperties = false)
        {
            if (book != null)
            {
                //fill in model values from the entity
                model ??= new BookModel();

                model.Name = book.Name;
                model.Author = book.Author;
                model.PublishDate = book.PublishDate;

                //whether to fill in some of properties
                if (!excludeProperties)
                {
                    model.Name = book.Name;
                    model.Author = book.Author;
                    model.PublishDate = book.PublishDate;
                }
            }            

            return Task.FromResult(model);
        }

        public Task<BookSearchModel> PrepareBookSearchModelAsync(BookSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }
    }
}
