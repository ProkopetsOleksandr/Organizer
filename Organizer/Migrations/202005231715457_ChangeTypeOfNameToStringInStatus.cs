namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfNameToStringInStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Status", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Status", "Name", c => c.Int(nullable: false));
        }
    }
}
