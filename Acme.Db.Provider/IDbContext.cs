using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;

namespace Acme.Db.Provider {
    public interface IDbContext {
        IList<ProductEntity> Products { get; set; }

        IList<CashPoolEntity> CashPool { get; set; }

        IList<CartEntity> Cart { get; set; }

        //IList<>
    }
}
