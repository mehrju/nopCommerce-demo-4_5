using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Data;
using Nop.Plugin.BookPlugin.Domains;
using Nop.Plugin.BookPlugin.Services;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.BookPlugin.Components
{
    [ViewComponent(Name = "BookPlugin")]
    public class BooksViewComponent : NopViewComponent
    {
        private readonly IBookService _bookService;
        private readonly IRepository<Book> _bookRepository;
        private readonly INopDataProvider _nopDataProvider;

        public BooksViewComponent(INopDataProvider nopDataProvider,
            IBookService bookService)
        {
            _nopDataProvider = nopDataProvider;
            _bookService = bookService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var model = await _nopDataProvider.GetTable<Book>().ToListAsync();
            model = model.Where(x => !x.Deleted).ToList();
            //var model = await _bookService.GetAllBooksAsync();


            return View("~/Plugins/Nop.Plugin.BookPlugin/Views/Book/List.cshtml", model);
        }
    }
}
