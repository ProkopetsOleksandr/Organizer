namespace Organizer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategory : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO Categories(Name) 
                VALUES 
                    ('Список целей на год'),
                    ('3 достижения года'),
                    ('3 достижения месяца'),
                    ('3 достижения недели'),
                    ('3 достижения дня'),
                    ('Ежедневные задания'),
                    ('Остальные задания (следует)'),
                    ('Остальные задания (могу)'),
                    ('Не забыть!!!'),
                    ('Ближайшее время'),
                    ('Позже'),
                    ('Не рассортированные');
            ");
        }
        
        public override void Down()
        {
        }
    }
}
