namespace WebAPI.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImgHotTagProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageHotTag", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageHotTag");
        }
    }
}
