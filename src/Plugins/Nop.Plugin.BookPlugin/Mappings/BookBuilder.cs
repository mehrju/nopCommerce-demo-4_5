using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.BookPlugin.Domains;

namespace Nop.Plugin.BookPlugin.Mappings
{
    public partial class BookBuilder : NopEntityBuilder<Book>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Book.Name)).AsString(1000).Nullable()
                .WithColumn(nameof(Book.Author)).AsString(1000).Nullable()
                .WithColumn(nameof(Book.PublishDate)).AsString(1000).Nullable();
        }
    }
}
