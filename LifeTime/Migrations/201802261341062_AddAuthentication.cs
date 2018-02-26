namespace LifeTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthentication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TablePlanneryDays", "User_Id", c => c.Int());
            CreateIndex("dbo.TablePlanneryDays", "User_Id");
            AddForeignKey("dbo.TablePlanneryDays", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TablePlanneryDays", "User_Id", "dbo.Users");
            DropIndex("dbo.TablePlanneryDays", new[] { "User_Id" });
            DropColumn("dbo.TablePlanneryDays", "User_Id");
            DropTable("dbo.Users");
        }
    }
}
