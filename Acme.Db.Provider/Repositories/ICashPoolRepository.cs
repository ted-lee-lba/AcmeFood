using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;

namespace Acme.Db.Provider.Repositories {
    public interface ICashPoolRepository {
        void ResetPool(Guid PoolId);

        void clearPool(Guid PoolId);

        IEnumerable<CashPoolEntity> getMyPool(Guid PoolId);

        IEnumerable<CashPoolEntity> Refund(Guid PoolId);

        decimal getTotalCash(Guid PoolId);
    }
}
