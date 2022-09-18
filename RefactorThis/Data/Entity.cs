using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Data
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

}