using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DatabaseLayer.Model
{
    public class ViewOrderModel
    {
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BookQuantity { get; set; }
        [JsonIgnore]
        public DateTime OrderDateTime { get; set; }
        public object OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string AuthorName { get; set; }
    }
}
