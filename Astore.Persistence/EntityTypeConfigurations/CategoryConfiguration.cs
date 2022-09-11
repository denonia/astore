﻿using Astore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astore.Persistence.EntityTypeConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);
        builder.HasIndex(category => category.Id).IsUnique();
        builder.Property(category => category.Name).IsRequired();
    }
}