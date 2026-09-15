using Finbuckle.MultiTenant.EntityFrameworkCore.Stores;
using Infrastructure.Tennacy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Tenancy
{
    public class TenantDbContext(DbContextOptions<TenantDbContext> options)
        : EFCoreStoreDbContext<ABCSchoolTenantInfo>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ABCSchoolTenantInfo>()
                .ToTable("Tenants", "MuliTenancy");
        }
    }
}