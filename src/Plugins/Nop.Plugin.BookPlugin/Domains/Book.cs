using Nop.Core;
using Nop.Core.Domain.Common;

namespace Nop.Plugin.BookPlugin.Domains
{
    public partial class Book : BaseEntity, ISoftDeletedEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string PublishDate { get; set; }
        public bool Deleted { get; set; }
    }
}
