using Storage.DAL;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Repository
{
    public class MaterialRepository : GeneralRepository<StorageContext, Material>
    {
        public override List<Material> GetAll()
        {
            return Context.Material.AsEnumerable().Select(material => new Material
            {
                Id = material.Id,
                Name = material.Name,
                Count = material.Count
            }).ToList();
        }

        public List<Material> GetAllWithUsedInDetails()
        {
            return Context.Material.Include("UsedInDetail").AsEnumerable().Select(material => new Material
            {
                Id = material.Id,
                Name = material.Name,
                Count = material.Count,
                UsedInDetail = material.UsedInDetail
            }).ToList();
        }

        public List<Material> GetAllDropDown()
        {
            return Context.Material.AsEnumerable().Select(material => new Material
            {
                Id = material.Id,
                Name = material.Name
            }).ToList();
        }

        public Material GetByWithUsedInDetail(System.Linq.Expressions.Expression<Func<Material, bool>> predicate)
        {
            return Context.Material.Include("UsedInDetail").SingleOrDefault(predicate);
        }
    }
}