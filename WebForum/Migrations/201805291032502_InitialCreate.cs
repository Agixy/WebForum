namespace WebForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupDtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserDto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDtoes", t => t.UserDto_Id)
                .Index(t => t.UserDto_Id);
            
            CreateTable(
                "dbo.UserDtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupDtoes", "UserDto_Id", "dbo.UserDtoes");
            DropIndex("dbo.GroupDtoes", new[] { "UserDto_Id" });
            DropTable("dbo.UserDtoes");
            DropTable("dbo.GroupDtoes");
        }
    }
}
