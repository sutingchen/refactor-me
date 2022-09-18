using refactor_me.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class Products
    {
        public IEnumerable<Product> Items { get; set; }
    }
}