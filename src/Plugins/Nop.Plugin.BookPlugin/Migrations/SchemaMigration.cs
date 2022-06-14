using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.BookPlugin.Domains;

namespace Nop.Plugin.BookPlugin.Migrations
{
    [NopMigration("2022/06/07 11:40:55:1687541", "Nop.Plugin.BookPlugin schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Book>();
        }
    }
}
