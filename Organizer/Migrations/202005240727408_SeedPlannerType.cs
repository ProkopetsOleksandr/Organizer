namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedPlannerType : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO PlannerTypes(Name) 
                VALUES
                    ('�������'),
                    ('��������'),
                    ('���������'),
                    ('�������'),
                    ('��������');
            ");
        }
        
        public override void Down()
        {
        }
    }
}
