using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Data.Mapping;
using Nop.Plugin.BookWidget.Models;

namespace Nop.Plugin.BookWidget.Data
{
    public partial class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(Book), "BS_Books" },
        };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(Book),"Name"),"Book_Name"},
            {(typeof(Book),"Author"),"Book_Author"},
            {(typeof(Book),"PublishDate"),"Book_PublishDate"},
        };
    }
}
