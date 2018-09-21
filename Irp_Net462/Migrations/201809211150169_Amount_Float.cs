namespace Irp_Net462.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Amount_Float : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "Amount", c => c.Single(nullable: false));
            AlterColumn("dbo.Payments", "CurrentTarif", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "CurrentTarif", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Payments", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
