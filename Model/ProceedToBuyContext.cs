using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Model
{
    public class ProceedToBuyContext : DbContext
    {
        public ProceedToBuyContext(DbContextOptions<ProceedToBuyContext> option) : base(option)
        {

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<VendorWishlist> VendorWishlists { get; set; }
       /* public DbSet<CartVendor> CartVendors { get; set; }*/
       //public DbSet<Vendor> Vendor { get; set; }

    }
}
        