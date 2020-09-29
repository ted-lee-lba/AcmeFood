using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Db.Provider.Entities {
    public class ProductEntity {
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ProductDescr { get; set; }

        public int AvailableQty { get; set; }

        public decimal Price { get; set; }

        public string ProductImg { get; set; }
    }
}
