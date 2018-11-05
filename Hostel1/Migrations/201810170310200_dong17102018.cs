namespace Hostel1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dong17102018 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Square = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                        Description = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
        }
    }
}
