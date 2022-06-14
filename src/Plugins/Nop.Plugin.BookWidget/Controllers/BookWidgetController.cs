using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Security;
using Nop.Plugin.BookWidget.Service;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.BookWidget.Controllers
{
    public class BookWidgetController : BasePluginController
    {
        #region Fields

        private readonly BookService _bookService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly StoreInformationSettings _storeInformationSettings;

        #endregion

        #region ctor

        public BookWidgetController(BookService bookService,
            IBaseAdminModelFactory baseAdminModelFactory,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IStoreContext storeContext,
            IWorkContext workContext,
            StoreInformationSettings storeInformationSettings)
        {
            _bookService = bookService;
            _baseAdminModelFactory = baseAdminModelFactory; 
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _storeContext = storeContext;
            _workContext = workContext;
            _storeInformationSettings = storeInformationSettings;
        }

        #endregion

        #region method

        public static readonly PermissionRecord ManageCustomers = new() { Name = "Admin area. Manage Books", SystemName = "Bs23_Widget.ShowBooks", Category = "Books" };

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
            if (!await _permissionService.AuthorizeAsync(ManageCustomers))
                return AccessDeniedView();

            //prepare model
            var model = await _bookService.GetAllBooksAsync();

            return View(model);
        }

        #endregion
    }
}
