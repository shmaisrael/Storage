using Storage.DAL;
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Repository
{
    public class DetailRepository : GeneralRepository<StorageContext, Detail>
    {
        public override List<Detail> GetAll()
        {
            return Context.Detail.Include("Material").AsEnumerable().Select(detail => new Detail
            {
                Id = detail.Id,
                Name = detail.Name,
                MaterialCount = detail.MaterialCount,
                Material = detail.Material == null ? null : new Material
                {
                    Id = detail.Material.Id,
                    Name = detail.Material.Name
                }
            }).ToList();
        }

        public List<Detail> GetAllWithUsedInProduct()
        {
            return Context.Detail.Include("UsedInProduct").AsEnumerable().Select(detail => new Detail
            {
                Id = detail.Id,
                Name = detail.Name,
                MaterialCount = detail.MaterialCount,
                UsedInProduct = detail.UsedInProduct
            }).ToList();
        }

        public List<Detail> GetAllDropDown()
        {
            return Context.Detail.AsEnumerable().Select(detail => new Detail
            {
                Id = detail.Id,
                Name = detail.Name
            }).ToList();
        }

        public override void Replace(Detail oldEntity, Detail newEntity)
        {
            SharedService.CopyEntity<Detail>(oldEntity, newEntity);
            if (newEntity.Material != null)
            {
                oldEntity.Material = Context.Material.SingleOrDefault(currentMaterial => currentMaterial.Id == newEntity.Material.Id);
            }
            Save();
        }
    }
}