using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class ViewWishListModel
    {
        public int WishlistId { get; set; }
        public int userId { get; set; }
        public int bookId { get; set; }
        public BookModel Bookmodel { get; set; }
    }
}
