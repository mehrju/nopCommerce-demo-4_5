using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.BookWidget.Components
{
    [ViewComponent(Name = "Bs23_WidgetShowBooks")]
    public class BooksViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/Nop.Plugin.BookWidget/Areas/Admin/Views/Book/List.cshtml");
        }
    }
}
