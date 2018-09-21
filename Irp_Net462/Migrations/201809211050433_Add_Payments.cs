namespace Irp_Net462.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Payments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CounterCurrent = c.Int(nullable: false),
                        CounterPrev = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentTarif = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                        ReceiptDate = c.DateTime(nullable: false),
                        Order = c.String(nullable: false),
                        Receipt = c.String(nullable: false),
                        PaymentCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PaymentCategories", t => t.PaymentCategoryID, cascadeDelete: true)
                .Index(t => t.PaymentCategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "PaymentCategoryID", "dbo.PaymentCategories");
            DropIndex("dbo.Payments", new[] { "PaymentCategoryID" });
            DropTable("dbo.Payments");
        }
    }
}
