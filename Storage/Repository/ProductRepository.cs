using Storage.DAL;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Repository
{
    public class ProductRepository : GeneralRepository<StorageContext, Product>
    {
        public override List<Product> GetAll()
        {
            return Context.Product.Include("Detail").AsEnumerable().Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Detail = product.Detail.Select(detail => new Detail { Id = detail.Id, Name = detail.Name }).ToList()
            }).ToList();
        }

        public Product GetBy(System.Linq.Expressions.Expression<Func<Product, bool>> predicate)
        {
            return Context.Set<Product>().Include("Detail").SingleOrDefault(predicate);
        }

        public override void Replace(Product oldEntity, Product newEntity)
        {
            SharedService.CopyEntity<Product>(oldEntity, newEntity);
            if (newEntity.Detail != null)
            {
                int[] ids = newEntity.Detail.Select(currentDetail => currentDetail.Id).ToArray();
                oldEntity.Detail = Context.Detail.Where(currentDetail => ids.Contains(currentDetail.Id)).ToList();
            }
            Save();
        }
    }
}