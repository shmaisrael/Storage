using Storage.Models;
using Storage.DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using Storage.Repository;
using Storage.Services;

namespace Storage.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository productRepository = new ProductRepository();

        public ActionResult GetProduct()
        {
            return Json(productRepository.GetAll());
        }

        public ActionResult RemoveProduct(int id)
        {
            Product product = productRepository.GetBy(currentProduct => currentProduct.Id == id);
            productRepository.Remove(product);
            return GetProduct();
        }

        public ActionResult UpdateProduct(Product product)
        {
            if (product.Name != null && productRepository.GetBy(currentProduct => currentProduct.Name == product.Name) != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
                return null;
            }
            productRepository.Replace(productRepository.GetBy(currentProduct => currentProduct.Id == product.Id), product);
            return null;
        }

        public ActionResult CreateProduct(Product product)
        {
            if (product.Name != null && productRepository.GetBy(currentProduct => currentProduct.Name == product.Name) != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
                return null;
            }
            productRepository.Replace(productRepository.Create(), product);
            return GetProduct();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
