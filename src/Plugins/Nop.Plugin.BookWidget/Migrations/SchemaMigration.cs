using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.BookWidget.Models;

namespace Nop.Plugin.BookWidget.Migrations
{
    [NopMigration("2022/06/06 14:24:16:2551771", "Nop.Plugin.BookWidget schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Book>();
        }
    }
}
