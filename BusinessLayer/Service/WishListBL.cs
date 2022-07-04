using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
        public class WishListBL:IWishListBL
        {
        private readonly IWishListRL wishRL;

        public WishListBL(IWishListRL wishRL)
        {
            this.wishRL = wishRL;
        }
        public WishListModel AddWishList(WishListModel wishlistModel, int userId)
        {
            try
            {
                return this.wishRL.AddWishList(wishlistModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteWishList(int WishlistId, int userId)
        {
            try
            {
                return this.wishRL.DeleteWishList(WishlistId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId)
        {
            try
            {
                return this.wishRL.GetWishlistDetailsByUserid(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
