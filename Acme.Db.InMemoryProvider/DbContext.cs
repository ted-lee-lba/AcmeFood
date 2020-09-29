using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;

namespace Acme.Db.Provider.InMemory {
    public class DbContext : IDbContext {
        public DbContext() {
            this.Products = new List<ProductEntity>();
            this.Cart = new List<CartEntity>();
            this.CashPool = new List<CashPoolEntity>();
        }

        public IList<ProductEntity> Products { get; set; }

        public IList<CashPoolEntity> CashPool { get; set; }

        public IList<CartEntity> Cart { get; set; }
    }
}
