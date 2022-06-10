namespace PassionProjectChariity_s2022.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sponsordonation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sponsors",
                c => new
                    {
                        SponsorID = c.Int(nullable: false, identity: true),
                        SponsorFirstName = c.String(),
                        SponsorLastName = c.String(),
                        Address = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.SponsorID);
            
            CreateTable(
                "dbo.SponsorDonations",
                c => new
                    {
                        Sponsor_SponsorID = c.Int(nullable: false),
                        Donation_DonationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sponsor_SponsorID, t.Donation_DonationID })
                .ForeignKey("dbo.Sponsors", t => t.Sponsor_SponsorID, cascadeDelete: true)
                .ForeignKey("dbo.Donations", t => t.Donation_DonationID, cascadeDelete: true)
                .Index(t => t.Sponsor_SponsorID)
                .Index(t => t.Donation_DonationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SponsorDonations", "Donation_DonationID", "dbo.Donations");
            DropForeignKey("dbo.SponsorDonations", "Sponsor_SponsorID", "dbo.Sponsors");
            DropIndex("dbo.SponsorDonations", new[] { "Donation_DonationID" });
            DropIndex("dbo.SponsorDonations", new[] { "Sponsor_SponsorID" });
            DropTable("dbo.SponsorDonations");
            DropTable("dbo.Sponsors");
        }
    }
}
