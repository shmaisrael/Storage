using Storage.Models;
using Storage.Repository;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Storage.Controllers
{
    public class OrderController : Controller
    {        
        private ProductRepository productRepository = new ProductRepository();
        private DetailRepository detailRepository = new DetailRepository();
        private MaterialRepository materialRepository = new MaterialRepository();

        public ActionResult GetOrder()
        {
            List<Material> materials = materialRepository.GetAllWithUsedInDetails().ToList();
            List<Detail> details = detailRepository.GetAllWithUsedInProduct().ToList();
            List<Product> products = productRepository.GetAll().ToList();

            List<Order> ordersCount;
            List<Order> ordersInformation;
            OrderService.FormOrders(materials, details, products, out ordersCount, out ordersInformation);

            HttpContext.Application["ORDERS_INFORMATION"] = ordersInformation;
            return Json(ordersCount);
        }

        public ActionResult BuyProduct(int productId)
        {
            List<Order> ordersInformation = HttpContext.Application["ORDERS_INFORMATION"] as List<Order>;
            List<MaterialCount> materialNeeded = ordersInformation != null ? ordersInformation.SingleOrDefault(currentOrderInformation => currentOrderInformation.ProductId == productId).MaterialNeeded : null;
            if (materialNeeded == null || OrderService.BuyProduct(materialNeeded, materialRepository))
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
            }
            return GetOrder();
        }
    }
}