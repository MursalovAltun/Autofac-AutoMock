using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Entities.Configuration
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Id)
                .HasDefaultValueSql("newsequentialid()");

            // DON'T DO SERVER SIDE VALIDATIONS LIKE LENGTH
            builder.Property(entity => entity.Name)
                .IsRequired();

            builder.Property(entity => entity.CompletedAt)
                .HasColumnType("date");

            builder.ToTable("Todos");
        }
    }
}