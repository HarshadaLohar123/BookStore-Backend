using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public CartModel AddCart(CartModel cart, int userId)
        {
            try
            {
                return this.cartRL.AddCart(cart, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RemoveFromCart(int CartId)
        {
            try
            {
                return this.cartRL.RemoveFromCart(CartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewCartModel> GetCartDetailsByUserid(int UserId)
        {
            try
            {
                return this.cartRL.GetCartDetailsByUserid(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CartModel UpdateCart(int CartId, CartModel cartModel, int UserId)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId, cartModel, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
