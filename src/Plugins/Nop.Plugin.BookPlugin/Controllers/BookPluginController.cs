using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Security;
using Nop.Plugin.BookPlugin.Factories;
using Nop.Plugin.BookPlugin.Services;
using Nop.Plugin.BookPlugin.Models;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Plugin.BookPlugin.Domains;

namespace Nop.Plugin.BookPlugin.Controllers
{
    public class BookPluginController : BasePluginController
    {
        #region Fields

        private readonly IBookService _bookService;
        private readonly IPermissionService _permissionService;
        private readonly IBookModelFactory _bookModelFactory;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region ctor

        public BookPluginController(IBookService bookService,
            IPermissionService permissionService,
            IBookModelFactory bookModelFactory,
            INotificationService notificationService,
            ILocalizationService localizationService)
        {
            _bookService = bookService;
            _permissionService = permissionService;
            _bookModelFactory = bookModelFactory;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }

        #endregion

        #region method

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public virtual async Task<IActionResult> List()
        {
            //prepare model
            //var model = await _bookService.GetAllBooksAsync();
            var model = await _bookModelFactory.PrepareBookSearchModelAsync(new BookSearchModel());

            return View("~/Plugins/Nop.Plugin.BookPlugin/Areas/Admin/Views/Book/List.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> BookList(BookSearchModel searchModel)
        {
            //prepare model
            var model = await _bookModelFactory.PrepareBookListModelAsync(searchModel);

            return Json(model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public virtual async Task<IActionResult> Create()
        {
            //prepare model
            var model = await _bookModelFactory.PrepareBookModelAsync(new BookModel(), null);

            return View("~/Plugins/Nop.Plugin.BookPlugin/Areas/Admin/Views/Book/Create.cshtml", model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual async Task<IActionResult> Create(BookModel model, bool continueEditing, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var book = model.ToEntity<Book>();
                await _bookService.InsertBookAsync(book);

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = book.Id });
            }

            //prepare model
            model = await _bookModelFactory.PrepareBookModelAsync(model, null, true);

            return View(model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public virtual async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null || book.Deleted)
                return RedirectToAction("List");

            //prepare model
            var model = await _bookModelFactory.PrepareBookModelAsync(null, book);
            model.Id = id;

            return View("~/Plugins/Nop.Plugin.BookPlugin/Areas/Admin/Views/Book/Edit.cshtml", model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual async Task<IActionResult> Edit(BookModel model, bool continueEditing, IFormCollection form)
        {
            var book = await _bookService.GetBookByIdAsync(model.Id);
            if (book == null || book.Deleted)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                try
                {
                    book.Name = model.Name;
                    book.Author = model.Author;
                    book.PublishDate = model.PublishDate;

                    await _bookService.UpdateBookAsync(book);

                    _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Showbookwidget.Updated"));

                    if (!continueEditing)
                        return RedirectToAction("List");

                    return RedirectToAction("Edit", new { id = book.Id });
                }
                catch (Exception ex)
                {
                    _notificationService.ErrorNotification(ex.Message);
                }
            }

            //prepare model
            model = await _bookModelFactory.PrepareBookModelAsync(model, book, true);

            //if we got this far, something failed, redisplay form
            return View("~/Plugins/Nop.Plugin.BookPlugin/Areas/Admin/Views/Book/Edit.cshtml", model);
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return RedirectToAction("List");

            try
            {
                await _bookService.DeleteBookAsync(book);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Showbookwidget.Deleted"));

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                _notificationService.ErrorNotification(ex.Message);
                return RedirectToAction("Edit", new { id = book.Id });
            }
        }

        #endregion
    }
}
