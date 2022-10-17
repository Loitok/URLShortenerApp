using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortenerApp.Models;

namespace URLShortenerApp.Data.Configuration
{
    public class UserMasterConfiguration : IEntityTypeConfiguration<UserMasterModel>
    {
        public void Configure(EntityTypeBuilder<UserMasterModel> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.HasMany(x => x.ShortUrlModels)
                .WithOne(x => x.UserMaster)
                .HasForeignKey(x => x.UserId);
        }
    }
}
