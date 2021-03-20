namespace WebAPI.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class updateMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "ParentID", c => c.Int(nullable: false, defaultValueSql: "null"));
        }

        public override void Down()
        {
            DropColumn("dbo.Menus", "ParentID");
        }
    }
}