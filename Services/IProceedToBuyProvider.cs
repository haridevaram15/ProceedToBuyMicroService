using ProceedToBuy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Services
{
    public interface IProceedToBuyProvider
    {
        // public Vendor GetVendors(int productId);
        public Task<Vendor> GetVendors(int productId);
    }
}
