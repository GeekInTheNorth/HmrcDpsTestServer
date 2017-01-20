namespace HmrcTpvsProxy.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageDatasets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodingNotice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatasetID = c.Int(nullable: false),
                        MessageType = c.String(),
                        SequenceNo = c.Int(nullable: false),
                        Forename = c.String(),
                        Surname = c.String(),
                        NationalInsuranceNo = c.String(),
                        WorksNumber = c.String(),
                        TaxYear = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        TaxCode = c.String(),
                        TaxRegime = c.String(),
                        TaxBasisNonCumulative = c.String(),
                        GrossTaxableInPreviousEmployment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxPaidInPreviousEmployment = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dataset", t => t.DatasetID, cascadeDelete: true)
                .Index(t => t.DatasetID);
            
            CreateTable(
                "dbo.Dataset",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PayeReference = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentLoanNotice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatasetID = c.Int(nullable: false),
                        MessageType = c.String(),
                        SequenceNo = c.Int(nullable: false),
                        Forename = c.String(),
                        Surname = c.String(),
                        NationalInsuranceNo = c.String(),
                        WorksNumber = c.String(),
                        TaxYear = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false),
                        PlanType = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dataset", t => t.DatasetID, cascadeDelete: true)
                .Index(t => t.DatasetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentLoanNotice", "DatasetID", "dbo.Dataset");
            DropForeignKey("dbo.CodingNotice", "DatasetID", "dbo.Dataset");
            DropIndex("dbo.StudentLoanNotice", new[] { "DatasetID" });
            DropIndex("dbo.CodingNotice", new[] { "DatasetID" });
            DropTable("dbo.StudentLoanNotice");
            DropTable("dbo.Dataset");
            DropTable("dbo.CodingNotice");
        }
    }
}
