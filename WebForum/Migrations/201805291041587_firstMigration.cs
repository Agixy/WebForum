namespace WebForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupDtoes", "UserDto_Id", "dbo.UserDtoes");
            DropIndex("dbo.GroupDtoes", new[] { "UserDto_Id" });
            CreateTable(
                "dbo.UserDtoGroupDtoes",
                c => new
                    {
                        UserDto_Id = c.Int(nullable: false),
                        GroupDto_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserDto_Id, t.GroupDto_Id })
                .ForeignKey("dbo.UserDtoes", t => t.UserDto_Id, cascadeDelete: true)
                .ForeignKey("dbo.GroupDtoes", t => t.GroupDto_Id, cascadeDelete: true)
                .Index(t => t.UserDto_Id)
                .Index(t => t.GroupDto_Id);
            
            DropColumn("dbo.GroupDtoes", "UserDto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupDtoes", "UserDto_Id", c => c.Int());
            DropForeignKey("dbo.UserDtoGroupDtoes", "GroupDto_Id", "dbo.GroupDtoes");
            DropForeignKey("dbo.UserDtoGroupDtoes", "UserDto_Id", "dbo.UserDtoes");
            DropIndex("dbo.UserDtoGroupDtoes", new[] { "GroupDto_Id" });
            DropIndex("dbo.UserDtoGroupDtoes", new[] { "UserDto_Id" });
            DropTable("dbo.UserDtoGroupDtoes");
            CreateIndex("dbo.GroupDtoes", "UserDto_Id");
            AddForeignKey("dbo.GroupDtoes", "UserDto_Id", "dbo.UserDtoes", "Id");
        }
    }
}
