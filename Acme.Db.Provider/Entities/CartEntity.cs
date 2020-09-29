using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Db.Provider.Entities {
    public class CartEntity {
        public Guid CartId { get; set; }

        public string ProductCode { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }
    }
}
