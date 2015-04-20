using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Storage.Models
{
    public class Material : BaseModel
    {
        public int? Count { get; set; }
        public virtual ICollection<Detail> UsedInDetail { get; set; }
    }
}