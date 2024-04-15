using CallForPapers.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CallForPapers.Infrastructure.Configuration;

public class ApplicationConfiguration: IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        //  генерация UUID 
        builder
            .Property(e => e.Id)
            .HasDefaultValueSql("uuid_generate_v4()");
        
        //  генерация даты 
        builder
            .Property(u => u.CreateDate)
            .HasDefaultValue(DateTime.Now);
        
        builder
            .Property(u => u.SendDate)
            .HasDefaultValue(null);
        
        builder
            .ToTable(t => t.HasCheckConstraint("ValidApplication", "user_id IS NOT NULL AND (activity IS NOT NULL  OR name IS NOT NULL OR description IS NOT NULL OR plan IS NOT NULL )"));
    }
}