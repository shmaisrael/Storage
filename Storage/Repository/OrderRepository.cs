using Storage.DAL;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Repository
{
    public class OrderRepository : GeneralRepository<StorageContext, Order>
    {
        public override List<Order> GetAll()
        {
            return Context.Product.Include("Detail").AsEnumerable().Select(product => new Order
            {
                ProductId = product.Id,
                ProductName = product.Name
            }).ToList();
        }
    }
}