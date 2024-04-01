using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureOrm;

public static class ModelBuilderExtensions
{
    public static void SetDefaultValuesTableName(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var entityClass = entity.ClrType;

            foreach (var property in entityClass.GetProperties())
            {
                if (property.PropertyType == typeof(Guid))
                {
                    var defaultValueSql = "newid()";
                    modelBuilder.Entity(entityClass).Property(property.Name).HasDefaultValueSql(defaultValueSql);
                }
                
            }
        }
    }
}
