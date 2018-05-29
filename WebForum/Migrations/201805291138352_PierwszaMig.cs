namespace WebForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PierwszaMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageDtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupDtoes", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            AddColumn("dbo.GroupDtoes", "Text", c => c.String());
            DropColumn("dbo.GroupDtoes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupDtoes", "Name", c => c.String());
            DropForeignKey("dbo.MessageDtoes", "Group_Id", "dbo.GroupDtoes");
            DropIndex("dbo.MessageDtoes", new[] { "Group_Id" });
            DropColumn("dbo.GroupDtoes", "Text");
            DropTable("dbo.MessageDtoes");
        }
    }
}
