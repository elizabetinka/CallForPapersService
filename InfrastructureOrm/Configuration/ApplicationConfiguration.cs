using InfrastructureOrm.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureOrm.Configuration;

public class ApplicationConfiguration: IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        //  foreign key
        builder
            .HasOne(u => u.user)
            .WithMany(c => c.applications)
            .HasForeignKey(u => u.user_id);
        
        //  генерация UUID 
        builder
            .Property(e => e.id)
            .HasDefaultValueSql("uuid_generate_v4()");
        //  генерация даты 
        builder
            .Property(u => u.added_date)
            .HasDefaultValue(DateTime.Now);
        
        //  enum в виде строки
        //builder
        //    .Property(i => i.activity.activity)
        //    .HasConversion<string>();
        //  Constraint
        builder
            .ToTable(t => t.HasCheckConstraint("ValidApplication", "user_id IS NOT NULL AND (activity IS NOT NULL  OR name IS NOT NULL OR description IS NOT NULL OR plan IS NOT NULL )"));

    }
}