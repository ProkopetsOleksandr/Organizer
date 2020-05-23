namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNameFromPlanner : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Planners", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planners", "Name", c => c.String());
        }
    }
}
