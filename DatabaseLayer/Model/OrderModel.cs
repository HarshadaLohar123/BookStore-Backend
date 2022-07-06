using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Model
{
    public class OrderModel
    {
        public int BookId { get; set; }
        public int AddressId { get; set; }
        public int BookQuantity { get; set; }

    }
}
