
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.NameTranslation;
using System.IO;

namespace GridShared.Data
{
    public static class SharedDbContextUtils
    {
        public static DbProvider DbProvider = DbProvider.SqlServer;

        public static string ConnectionString = "Data Source=DESKTOP-FRJNM5L\\KINFOSERVER;Initial Catalog=Northwind;User Id=sa;Password=147852369;MultipleActiveResultSets=true";

        public static DbContextOptionsBuilder UseGridBlazorDatabase(this DbContextOptionsBuilder optionsBuilder)
        {
            
                
                    return optionsBuilder.UseSqlServer(ConnectionString);
                
              
        
        }
        
        public static void ApplyToModelBuilder(DatabaseFacade databaseFacade, ModelBuilder modelBuilder)
        {
            switch (DbProvider)
            {
                case DbProvider.SqlServer:
                    break;
                case DbProvider.Sqlite:
                default:
                    var mapper = new NpgsqlSnakeCaseNameTranslator();
                    foreach (var entity in modelBuilder.Model.GetEntityTypes())
                    {
                        foreach (var property in entity.GetProperties())
                        {
                            var storeObjectIdentifier = StoreObjectIdentifier.Create(property.DeclaringEntityType, StoreObjectType.Table);
                            if(storeObjectIdentifier.HasValue)
                                property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName(storeObjectIdentifier.Value)));
                        }
                    
                        entity.SetTableName(mapper.TranslateTypeName(entity.GetTableName()));
                    }
                    break;
            }
        }
    }

    public enum DbProvider
    {
        SqlServer,
        Sqlite
    }
}