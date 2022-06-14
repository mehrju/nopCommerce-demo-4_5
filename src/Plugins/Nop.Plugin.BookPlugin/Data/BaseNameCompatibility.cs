//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Nop.Data.Mapping;
//using Nop.Plugin.BookPlugin.Domains;

//namespace Nop.Plugin.BookPlugin.Data
//{
//    public partial class BaseNameCompatibility : INameCompatibility
//    {
//        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
//        {
//            { typeof(Book), "Books_Plugin" },
//        };

//        public Dictionary<(Type, string), string> ColumnName => new()
//        {
//            { (typeof(Book), nameof(Book.Id)), "Book_Id" },
//            { (typeof(Book), nameof(Book.Name)), "Book_Name" },
//            { (typeof(Book), nameof(Book.Author)), "Book_Author" },
//            { (typeof(Book), nameof(Book.PublishDate)), "Book_PublishDate" }
//        };
//    }
//}
