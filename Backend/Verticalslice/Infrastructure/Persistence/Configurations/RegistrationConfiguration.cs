using CarShowRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CarShowRegistration.Infrastructure.Persistence.Configurations;
public sealed class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.ToTable("Registrations"); builder.HasKey(x => x.Id);
        builder.Property(x => x.RegistrationNumber).HasMaxLength(30).IsRequired();
        builder.Property(x => x.RegistrationDate).IsRequired();
        builder.Property(x => x.ParticipantName).HasMaxLength(150).IsRequired();
        builder.Property(x => x.ContactNumber).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(300).IsRequired();
        builder.Property(x => x.CarMakeModel).HasMaxLength(100).IsRequired();
        builder.Property(x => x.PlateNumber).HasMaxLength(15).UseCollation("NOCASE").IsRequired();
        builder.Property(x => x.RegistrationFee).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.AmountPaid).HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.RegistrationType).HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
        builder.Ignore(x => x.PaymentStatus);
        builder.HasIndex(x => x.RegistrationNumber).IsUnique();
        builder.HasIndex(x => x.PlateNumber).IsUnique();
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.RegistrationType);
        builder.HasIndex(x => x.Status);
    }
}
