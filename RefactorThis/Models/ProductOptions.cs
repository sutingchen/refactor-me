using refactor_me.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class ProductOptions
    {
        public IEnumerable<ProductOption> Items { get; set; }
    }
}