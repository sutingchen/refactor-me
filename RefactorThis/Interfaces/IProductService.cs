using refactor_me.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Interfaces
{
    public interface IProductService : IGenericService<Product>
    {
        Task<Product> GetByName(string name);
    }
}
