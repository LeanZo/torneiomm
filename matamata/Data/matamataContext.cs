using Microsoft.EntityFrameworkCore;
using matamata.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace matamata.Data
{
    public class matamataContext : DbContext
    {
        public static matamataContext Context; 
        public matamataContext(DbContextOptions<matamataContext> options)
            : base(options)
        {
        }

        public DbSet<Torneio> Torneio { get; set; }
        public DbSet<Time> Time { get; set; }
        public DbSet<Partida> Partida { get; set; }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            matamataContext.Context = new matamataContext(serviceProvider.GetRequiredService<DbContextOptions<matamataContext>>());
        }
    }
}