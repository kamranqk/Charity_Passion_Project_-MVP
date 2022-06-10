namespace PassionProjectChariity_s2022.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donationfund : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "FundID", c => c.Int(nullable: false));
            CreateIndex("dbo.Donations", "FundID");
            AddForeignKey("dbo.Donations", "FundID", "dbo.Funds", "FundID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "FundID", "dbo.Funds");
            DropIndex("dbo.Donations", new[] { "FundID" });
            DropColumn("dbo.Donations", "FundID");
        }
    }
}
