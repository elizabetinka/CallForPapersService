using InfrastructureOrm.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureOrm.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //  генерация UUID 
        builder
            .Property(e => e.id)
            .HasDefaultValueSql("uuid_generate_v4()");
    }
}