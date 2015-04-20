using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    public class MaterialCount
    {
        public int MaterialId { get; set; }
        public int? MaterialNeeded { get; set; }
        public int? MaterialExists { get; set; }
    }

    public class Order
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Count { get; set; }
        public List<MaterialCount> MaterialNeeded { get; set; }
    }
}