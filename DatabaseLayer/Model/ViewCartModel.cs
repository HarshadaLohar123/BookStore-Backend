using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class ViewCartModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BooksQuantity { get; set; }
        public BookModel Bookmodel { get; set; }
    }
}
