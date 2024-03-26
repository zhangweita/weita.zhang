using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Model
{
    internal class Author
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

    }

    internal class AuthorEntityConfig : IEntityTypeConfiguration<Author>
    {
        void IEntityTypeConfiguration<Author>.Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
