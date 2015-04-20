using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Storage.Models
{
    public class Detail : BaseModel
    {
        public int? MaterialCount { get; set; }
        public virtual Material Material { get; set; }
        public virtual ICollection<Product> UsedInProduct { get; set; }
    }
}