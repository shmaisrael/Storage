using Storage.Models;
using Storage.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Services
{
    public class OrderService
    {
        public static List<MaterialCount> GetMaterialNeeded(List<Material> materials, List<Detail> details, int productId)
        {
            List<MaterialCount> materialsNeeded = new List<MaterialCount>();
            List<Detail> detailsInProduct = details.Where(detail => detail.UsedInProduct.Any(usedInProduct => usedInProduct.Id == productId)).ToList();
            if (detailsInProduct.Count == 0)
            {
                return null;
            }
            foreach (Detail detailInProduct in detailsInProduct)
            {
                Material materialInDetail = materials.SingleOrDefault(material => material.UsedInDetail.Any(usedInDetail => usedInDetail.Id == detailInProduct.Id));
                if (materialInDetail == null)
                {
                    return null;
                }
                int? materialInDetailCount = detailInProduct.MaterialCount;
                if (!materialsNeeded.Any(material => material.MaterialId == materialInDetail.Id))
                {
                    materialsNeeded.Add(new MaterialCount { MaterialId = materialInDetail.Id, MaterialNeeded = materialInDetailCount, MaterialExists = materialInDetail.Count });
                }
                else
                {
                    materialsNeeded.SingleOrDefault(material => material.MaterialId == materialInDetail.Id).MaterialNeeded += materialInDetailCount;
                }
            }
            return materialsNeeded;
        }

        public static int? GetProductCount(List<MaterialCount> materialsNeeded)
        {
            int? count = Int32.MaxValue;
            foreach (MaterialCount materialNeeded in materialsNeeded)
            {
                int? curCount = materialNeeded.MaterialExists / materialNeeded.MaterialNeeded;
                if (curCount < count)
                {
                    count = curCount;
                }
            }
            return count;
        }

        public static void FormOrders(List<Material> materials, List<Detail> details, List<Product> products, out List<Order> ordersCount, out List<Order> ordersInformation)
        {
            ordersCount = new List<Order>();
            ordersInformation = new List<Order>();
            foreach (Product product in products)
            {
                List<MaterialCount> materialNeeded = OrderService.GetMaterialNeeded(materials, details, product.Id);
                int? count = materialNeeded != null ? OrderService.GetProductCount(materialNeeded) : null;
                ordersCount.Add(new Order
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Count = count
                });

                ordersInformation.Add(new Order
                {
                    ProductId = product.Id,
                    MaterialNeeded = materialNeeded
                });
            }
        }

        public static bool BuyProduct(List<MaterialCount> materialsNeeded, MaterialRepository materialRepository)
        {
            bool isMaterialCountNegative = false;
            foreach (MaterialCount materialNeeded in materialsNeeded)
            {
                Material buyMaterial = materialRepository.GetBy(material => material.Id == materialNeeded.MaterialId);
                buyMaterial.Count -= materialNeeded.MaterialNeeded;
                if (buyMaterial.Count < 0)
                {
                    isMaterialCountNegative = true;
                }
            }
            if (!isMaterialCountNegative)
            {
                materialRepository.Save();
            }
            return isMaterialCountNegative;
        }
    }
}