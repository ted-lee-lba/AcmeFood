using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;

namespace Acme.Db.Provider.Repositories {
    public interface ICashDenomRepository {
        IEnumerable<CashDenomEntity> getChanges(decimal CashPaid, decimal CartCharges);
    }
}
