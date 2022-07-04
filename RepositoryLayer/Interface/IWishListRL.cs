using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        public WishListModel AddWishList(WishListModel wishlistModel, int userId);
        public bool DeleteWishList(int WishlistId, int userId);
        public List<ViewWishListModel> GetWishlistDetailsByUserid(int userId);

    }
}
