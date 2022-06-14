using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.BookPlugin.Domains;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.BookPlugin.Models
{
    public partial record BookListModel : BasePagedListModel<BookModel>
    {
    }
}
