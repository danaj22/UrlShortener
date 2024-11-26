using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UrlShortener.Entities.Configuration
{
    public class UrlShorterConfiguration : IEntityTypeConfiguration<UrlShorter>
    {
        public void Configure(EntityTypeBuilder<UrlShorter> builder)
        {
            builder.Property(x => x.Code).HasMaxLength(20);
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
        }
    }
}
