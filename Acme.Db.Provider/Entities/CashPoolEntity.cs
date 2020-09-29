using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Db.Provider.Entities {
    public class CashPoolEntity {
        public Guid PoolId { get; set; }

        public decimal Value { get; set; }
        
        public int Qty { get; set; }
    }
}
