using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        public WishListModel AddWishList(WishListModel wishlistModel, int userId);
        public bool DeleteWishList(int WishlistId, int userId);
        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId);

    }
}
