using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.BookPlugin.Domains;
using Nop.Plugin.BookPlugin.Models;

namespace Nop.Plugin.BookPlugin.Factories
{
    public partial interface IBookModelFactory
    {
        Task<BookSearchModel> PrepareBookSearchModelAsync(BookSearchModel searchModel);

        Task<BookListModel> PrepareBookListModelAsync(BookSearchModel searchModel);

        Task<BookModel> PrepareBookModelAsync(BookModel model, Book book, bool excludeProperties = false);
    }
}
