namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidPeriodToPlanner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planners", "ValidFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.Planners", "ValidTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Planners", "ValidTo");
            DropColumn("dbo.Planners", "ValidFrom");
        }
    }
}
