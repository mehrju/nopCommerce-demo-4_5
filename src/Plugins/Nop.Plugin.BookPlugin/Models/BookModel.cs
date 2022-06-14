using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.BookPlugin.Models
{
    public record BookModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
        public bool Deleted { get; set; }
    }
}
