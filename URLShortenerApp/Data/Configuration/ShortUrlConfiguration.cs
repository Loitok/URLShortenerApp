using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLShortenerApp.Models;

namespace URLShortenerApp.Data.Configuration
{
    public class ShortUrlConfiguration : IEntityTypeConfiguration<ShortUrlModel>
    {
        public void Configure(EntityTypeBuilder<ShortUrlModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OriginalUrl)
                .IsRequired();
        }
    }
}
