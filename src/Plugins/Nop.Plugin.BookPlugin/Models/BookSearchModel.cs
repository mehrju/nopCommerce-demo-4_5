using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.BookPlugin.Models
{
    public partial record BookSearchModel : BaseSearchModel
    {
        [NopResourceDisplayName("Name")]
        public string SearchName { get; set; }

        [NopResourceDisplayName("Author")]
        public string SearchAuthor { get; set; }

        [NopResourceDisplayName("Publish Date")]
        public string SearchPublishDate { get; set; }
    }
}
