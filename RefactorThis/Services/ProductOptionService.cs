using refactor_me.Data;
using refactor_me.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace refactor_me.Services
{
    public class ProductOptionService : GenericService<ProductOption>, IProductOptionService
    {
        public ProductOptionService(ApiDatabaseContext apiDatabaseContext) : base(apiDatabaseContext) { }

        public IEnumerable<ProductOption> GetOptionsByProductId(Guid productId)
        {
            return _apiDatabaseContext.ProductOptions.Where(x => x.ProductId.Equals(productId));
        }
    }
}