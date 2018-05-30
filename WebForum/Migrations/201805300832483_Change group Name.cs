namespace WebForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangegroupName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupDtoes", "Name", c => c.String());
            DropColumn("dbo.GroupDtoes", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupDtoes", "Text", c => c.String());
            DropColumn("dbo.GroupDtoes", "Name");
        }
    }
}
