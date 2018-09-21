namespace Irp_Net462.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentPeriod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentPeriod", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PaymentPeriod");
        }
    }
}
