using Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Storage.DAL
{
    public class StorageInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StorageContext>
    {
        protected override void Seed(StorageContext context)
        {
            var material = new List<Material>
            {
                new Material{Id=0,Name="Iron",Count=100},
                new Material{Id=1,Name="Wood",Count=50}
            };
            material.ForEach(data => context.Material.Add(data));

            var detail = new List<Detail>
            {
                new Detail{Id=0,Name="List",Material=material[0],MaterialCount=10},
                new Detail{Id=1,Name="Nail",Material=material[0],MaterialCount=1},
                new Detail{Id=2,Name="Board",Material=material[1],MaterialCount=10},
                new Detail{Id=3,Name="Blank",Material=material[0],MaterialCount=5}
            };
            detail.ForEach(data => context.Detail.Add(data));

            List<Product> product = new List<Product>
            {
                new Product{Id=0,Name="Roof",Detail=new List<Detail>(){detail[0], detail[1]}},
                new Product{Id=1,Name="Hammer",Detail=new List<Detail>(){detail[3], detail[2]}},
                new Product{Id=2,Name="Chair",Detail=new List<Detail>(){detail[2]}}
            };
            product.ForEach(data => context.Product.Add(data));

            context.SaveChanges();
        }
    }
}