namespace KvantCard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Contact_Id = c.Int(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contact", t => t.Contact_Id)
                .ForeignKey("dbo.Student", t => t.Student_ID)
                .Index(t => t.Contact_Id)
                .Index(t => t.Student_ID);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Street = c.String(),
                        Appartment = c.String(),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.Contact_Id)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocumentSetID = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Parent1ID = c.Int(nullable: false),
                        MentorID = c.Int(nullable: false),
                        LevelID = c.Int(nullable: false),
                        KvantumID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                        ProgramID = c.Int(nullable: false),
                        SchoolID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contact", t => t.Contact_Id)
                .Index(t => t.Contact_Id);
            
            CreateTable(
                "dbo.DictionaryItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.Student_ID)
                .Index(t => t.Student_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parent", "Student_ID", "dbo.Student");
            DropForeignKey("dbo.DictionaryItem", "Student_ID", "dbo.Student");
            DropForeignKey("dbo.Student", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Parent", "Contact_Id", "dbo.Contact");
            DropForeignKey("dbo.Address", "Contact_Id", "dbo.Contact");
            DropIndex("dbo.DictionaryItem", new[] { "Student_ID" });
            DropIndex("dbo.Student", new[] { "Contact_Id" });
            DropIndex("dbo.Address", new[] { "Contact_Id" });
            DropIndex("dbo.Parent", new[] { "Student_ID" });
            DropIndex("dbo.Parent", new[] { "Contact_Id" });
            DropTable("dbo.DictionaryItem");
            DropTable("dbo.Student");
            DropTable("dbo.Address");
            DropTable("dbo.Contact");
            DropTable("dbo.Parent");
        }
    }
}
