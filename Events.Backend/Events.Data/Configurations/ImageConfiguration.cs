using Events.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Events.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FilePath)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}