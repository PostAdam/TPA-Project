using System.Data.Entity.Migrations;

namespace DataBaseRepository
{
    public class AddMetadataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable( "dbo.Assembly",
                    a => new
                    {
                        Name = a.String( nullable: true ),
                    } )
                .PrimaryKey( a => a.Name );

            CreateTable( "dbo.Namespace",
                    n => new
                    {
                        NamespaceName = n.String( nullable: true ),
                        AssemblyId = n.String( nullable: true )
                    } )
                .PrimaryKey( a => a.NamespaceName )
                .ForeignKey( "dbo.Assembly", n => n.AssemblyId );

/*            CreateTable( "dbo.Type",
                    t => new
                    {
                        TypeName = t.String( nullable: true ),
                        NamespaceName = t.String( nullable: true ),
                        BaseTypeId = t.String( nullable: true ),
                        DeclaringType = t.String( nullable: true ),
                        
                    } )
                .PrimaryKey( a => a.NamespaceName )
                .ForeignKey( "dbo.Assembly", n => n.AssemblyId );*/
        }
    }
}