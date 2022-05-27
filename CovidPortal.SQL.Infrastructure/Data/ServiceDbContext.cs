using CovidPortal.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidPortal.SQL.Infrastructure.Data
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
        {
        }

        public virtual DbSet<CovidCountryDetail> CovidCountryDetail { get; set; }
    }
}
