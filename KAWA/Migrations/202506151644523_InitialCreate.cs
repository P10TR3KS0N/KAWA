namespace KAWA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoffeeBeans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        OriginCountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.OriginCountryId, cascadeDelete: true)
                .Index(t => t.OriginCountryId);
            
            CreateTable(
                "dbo.Coffees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        RoastLevel = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BeanId = c.Int(nullable: false),
                        RoasterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoffeeBeans", t => t.BeanId)
                .ForeignKey("dbo.Roasters", t => t.RoasterId)
                .Index(t => t.BeanId)
                .Index(t => t.RoasterId);
            
            CreateTable(
                "dbo.Roasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.CoffeeBeans", "OriginCountryId", "dbo.Countries");
            DropForeignKey("dbo.Coffees", "RoasterId", "dbo.Roasters");
            DropForeignKey("dbo.Roasters", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Coffees", "BeanId", "dbo.CoffeeBeans");
            DropIndex("dbo.Roasters", new[] { "CityId" });
            DropIndex("dbo.Coffees", new[] { "RoasterId" });
            DropIndex("dbo.Coffees", new[] { "BeanId" });
            DropIndex("dbo.CoffeeBeans", new[] { "OriginCountryId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.Roasters");
            DropTable("dbo.Coffees");
            DropTable("dbo.CoffeeBeans");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
