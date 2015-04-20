using Storage.Models;
using Storage.DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
using System.Web.Http.ModelBinding;
using Storage.Services;
using Storage.Repository;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Web;

namespace Storage.Controllers
{
    public class MaterialController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private MaterialRepository materialRepository = new MaterialRepository();

        public ActionResult GetMaterial()
        {
            return Json(materialRepository.GetAll());
        }

        public ActionResult GetDropDown()
        {
            return Json(materialRepository.GetAllDropDown());
        }

        public ActionResult RemoveMaterial(int id)
        {
            Material material = materialRepository.GetByWithUsedInDetail(currentMaterial => currentMaterial.Id == id);
            materialRepository.Remove(material);
            return GetMaterial();
        }

        public ActionResult UpdateMaterial(Material material)
        {
            if (material.Name != null && materialRepository.GetBy(currentMaterial => currentMaterial.Name == material.Name) != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
                return null;
            }
            materialRepository.Replace(materialRepository.GetBy(currentMaterial => currentMaterial.Id == material.Id), material);
            return null;
        }

        public ActionResult CreateMaterial(Material material)
        {
            if (material.Name != null && materialRepository.GetBy(currentMaterial => currentMaterial.Name == material.Name) != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
                return null;
            }
            materialRepository.Replace(materialRepository.Create(), material);
            return GetMaterial();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                materialRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
