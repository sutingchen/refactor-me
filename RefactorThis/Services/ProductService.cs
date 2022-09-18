using refactor_me.Data;
using refactor_me.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Product = refactor_me.Data.Product;

namespace refactor_me.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(ApiDatabaseContext apiDatabaseContext) : base(apiDatabaseContext) { }

        public async Task<Product> GetByName(string name)
        {
            return await _apiDatabaseContext.Products.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim())).FirstOrDefaultAsync();
        }
    }
}