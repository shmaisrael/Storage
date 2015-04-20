using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class Product : BaseModel
    {
        public virtual ICollection<Detail> Detail { get; set; }
    }
}