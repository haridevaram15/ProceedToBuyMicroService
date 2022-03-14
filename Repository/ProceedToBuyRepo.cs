using ProceedToBuy.Model;
using ProceedToBuy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    public class ProceedToBuyRepo : IProceedToBuyRepo<Cart>
    {

        IProceedToBuyProvider _provider;
        ProceedToBuyContext _proceedToBuyContext;
        public ProceedToBuyRepo(ProceedToBuyContext proceedToBuyContext, IProceedToBuyProvider provider)
        {
            _provider = provider;
            _proceedToBuyContext = proceedToBuyContext;

        }
        public List<Cart> GetCart()
        {

            return _proceedToBuyContext.Carts.ToList();
        }
        public bool AddToCart(Cart _cart)
        {

            var v = _provider.GetVendors(_cart.ProductId);
            if (v == null)
            {
                AddToWishList(_cart.CustomerId, _cart.ProductId);
                return false;
            }
            else
           {
                _cart.VendorId = v.Id;
                Cart val = _proceedToBuyContext.Carts.SingleOrDefault(c=>c.CustomerId == _cart.CustomerId &&  c.ProductId == _cart.ProductId ) ;
                if (val != null)
                {
                    val.Quantity += _cart.Quantity;
                }
                else
                    _proceedToBuyContext.Carts.Add(_cart);
                _proceedToBuyContext.SaveChanges();
                return true;
            }

            
        }
        public bool AddToWishList(int customerId,int productId)
        {
            
            VendorWishlist vendorWishlist = new VendorWishlist();
            vendorWishlist.CustomerId = customerId;
            vendorWishlist.ProductId = productId;
            vendorWishlist.Quantity =7;
            vendorWishlist.DateAddedToWishlist = DateTime.Now;
            vendorWishlist.VendorId = 6;
            _proceedToBuyContext.VendorWishlists.Add(vendorWishlist);
            _proceedToBuyContext.SaveChanges();
            return true;
        }

        public List<VendorWishlist> GetWishlist(int id)
        {
            return _proceedToBuyContext.VendorWishlists.Where(v => v.CustomerId == id).ToList();
        }

        public bool DeleteCustomerCart(int customerId)
        {
            List<VendorWishlist> Wlist = GetWishlist(customerId);
            foreach(VendorWishlist item in Wlist)
            {
                _proceedToBuyContext.VendorWishlists.Remove(item);
            }

            List<Cart> cart = GetCart();
            foreach (Cart item in cart)
            {
                if (item.CustomerId == customerId)
                    _proceedToBuyContext.Carts.Remove(item);
            }

            _proceedToBuyContext.SaveChanges();

            return true;
        }

        public bool DeleteCartById(int cartId)
        {
            Cart cart = GetCart().SingleOrDefault(x => x.CartId == cartId);
            _proceedToBuyContext.Carts.Remove(cart);
            _proceedToBuyContext.SaveChanges();

            return true;
        }

    }
}
