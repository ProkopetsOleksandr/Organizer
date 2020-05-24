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
                    ('������ ����� �� ���'),
                    ('3 ���������� ����'),
                    ('3 ���������� ������'),
                    ('3 ���������� ������'),
                    ('3 ���������� ���'),
                    ('���������� �������'),
                    ('��������� ������� (�������)'),
                    ('��������� ������� (����)'),
                    ('�� ������!!!'),
                    ('��������� �����'),
                    ('�����'),
                    ('�� ����������������');
            ");
        }
        
        public override void Down()
        {
        }
    }
}
