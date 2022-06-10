namespace PassionProjectChariity_s2022.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationID = c.Int(nullable: false, identity: true),
                        DonationDate = c.DateTime(nullable: false),
                        DonationAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Donations");
        }
    }
}
