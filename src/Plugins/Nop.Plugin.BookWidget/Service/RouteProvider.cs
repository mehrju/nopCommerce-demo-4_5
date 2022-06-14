using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.BookWidget.Service
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(
                "Nop.Plugin.BookWidget.Index", "Plugins/BookWidget/Index",
                    new
                    {
                        controller = "BookWidget",
                        action = "Index",
                        area = AreaNames.Admin
                    });

            //endpointRouteBuilder.MapControllerRoute(
            //    "Nop.Plugin.BookWidget.List", "Admin/BookWidget/List",
            //        new
            //        {
            //            controller = "BookWidget",
            //            action = "List",
            //            area = AreaNames.Admin
            //        });
        }
        public int Priority => -1;
    }
}
