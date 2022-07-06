using BusinessLayer.Interface;
using DatabaseLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL:IOrderBL
    {
        private readonly IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public string AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ViewOrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this.orderRL.GetAllOrder(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
