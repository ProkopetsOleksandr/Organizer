namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStatus : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Status(name) VALUES ('�����'), ('� ��������'), ('�����������'), ('������������'), ('����������');");
        }
        
        public override void Down()
        {
        }
    }
}
