namespace WebAPI.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimagePage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Image", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "Image");
        }
    }
}
