// Licensed to the .NET Foundation under one or more agreements.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Infrastructure.Services.Archive;

namespace RealEstate.Infrastructure.Configuration.Base;

public class ArchiveConfiguration: IEntityTypeConfiguration<ArchiveRecord>
{
    public void Configure(EntityTypeBuilder<ArchiveRecord> builder)
    {
        builder.HasIndex(x => new { x.EntityId, x.EntityName });
    }
}
