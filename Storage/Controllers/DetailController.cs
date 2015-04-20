using Storage.Models;
using Storage.DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity.Infrastructure;
using Storage.Repository;
using System.Web.Http.ModelBinding;
using Storage.Services;
using System.Web.Mvc;
using Storage.App_Start;

namespace Storage.Controllers
{
    public class DetailController : Controller
    {
        private DetailRepository detailRepository = new DetailRepository();

        public ActionResult GetDetail()
        {
            return Json(detailRepository.GetAll());
        }

        public ActionResult GetDropDown()
        {
            return Json(detailRepository.GetAllDropDown());
        }

        public ActionResult RemoveDetail(int id)
        {
            Detail detail = detailRepository.GetBy(currentDetail => currentDetail.Id == id);
            detailRepository.Remove(detail);
            return GetDetail();
        }

        public ActionResult UpdateDetail(Detail detail)
        {
            if (detail.Name != null && detailRepository.GetBy(currentDetail => currentDetail.Name == detail.Name) != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
                return null;
            }
            detailRepository.Replace(detailRepository.GetBy(currentDetail => currentDetail.Id == detail.Id), detail);
            return null;
        }

        public ActionResult CreateDetail(Detail detail)
        {
            if (detail.Name != null && detailRepository.GetBy(currentDetail => currentDetail.Name == detail.Name) != null)
            {
                Response.StatusCode = (Int32)HttpStatusCode.NotFound;
                return null;
            }
            detailRepository.Replace(detailRepository.Create(), detail);
            return GetDetail();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                detailRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
