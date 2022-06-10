namespace PassionProjectChariity_s2022.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funds",
                c => new
                    {
                        FundID = c.Int(nullable: false, identity: true),
                        FundName = c.String(),
                    })
                .PrimaryKey(t => t.FundID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Funds");
        }
    }
}
