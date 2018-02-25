namespace LifeTime.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MVVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TablePlanneryDays", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.TablePlanneryDays", "Task_Id", c => c.Int());
            AddColumn("dbo.TablePlanneryDays", "MainViewModelView_Id", c => c.Int());
            CreateIndex("dbo.TablePlanneryDays", "Task_Id");
            CreateIndex("dbo.TablePlanneryDays", "MainViewModelView_Id");
            AddForeignKey("dbo.TablePlanneryDays", "Task_Id", "dbo.TablePlanneryDays", "Id");
            AddForeignKey("dbo.TablePlanneryDays", "MainViewModelView_Id", "dbo.TablePlanneryDays", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TablePlanneryDays", "MainViewModelView_Id", "dbo.TablePlanneryDays");
            DropForeignKey("dbo.TablePlanneryDays", "Task_Id", "dbo.TablePlanneryDays");
            DropIndex("dbo.TablePlanneryDays", new[] { "MainViewModelView_Id" });
            DropIndex("dbo.TablePlanneryDays", new[] { "Task_Id" });
            DropColumn("dbo.TablePlanneryDays", "MainViewModelView_Id");
            DropColumn("dbo.TablePlanneryDays", "Task_Id");
            DropColumn("dbo.TablePlanneryDays", "Discriminator");
        }
    }
}
