using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBackTidsregistrering.Domain.Entities;

namespace WebBackTidsregistrering.Persistance.Configuration
{
    public class RegistrationsConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("RegistrationID");
        }
    }
}