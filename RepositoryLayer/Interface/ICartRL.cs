using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public CartModel AddCart(CartModel cart, int userId);
        public string RemoveFromCart(int CartId);
        public List<ViewCartModel> GetCartDetailsByUserid(int UserId);
        public CartModel UpdateCart(int CartId, CartModel cartModel, int UserId);

    }
}
