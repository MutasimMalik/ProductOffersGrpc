using Microsoft.EntityFrameworkCore;
using OfferGrpcService.Models;

namespace OfferGrpcService.Data
{
    public class OfferContext : DbContext
    {
        public OfferContext(DbContextOptions<OfferContext> options) : base(options) { }

        public DbSet<Offer> Offer { get; set; }
    }
}
