using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;
using Acme.Db.Provider.Repositories;
using Acme.Db;

namespace Acme.Db.Provider.InMemory.Repositories {
    public class CashDenomRepository : ICashDenomRepository {
        private IDbContext _dbContext;
        public CashDenomRepository(IDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public IEnumerable<CashDenomEntity> getChanges(decimal CashPaid, decimal CartCharges) {
            List<CashDenomEntity> _lstCashReturn = new List<CashDenomEntity>();
            decimal _dChange = CashPaid - CartCharges;
            if (_dChange / 5 > 0) {
                _lstCashReturn.Add(new CashDenomEntity { Value = 5, Qty = (int)(_dChange / 5) });
                _dChange = _dChange % 5;
            }
            if (_dChange / 1 > 0) {
                _lstCashReturn.Add(new CashDenomEntity { Value = 1, Qty = (int)(_dChange / 1) });
                _dChange = _dChange % 1;
            }
            if (_dChange / 0.25M > 0) {
                _lstCashReturn.Add(new CashDenomEntity { Value = 0.25M, Qty = (int)(_dChange / 0.25M) });
                _dChange = _dChange % 0.25M;
            }
            if (_dChange / 0.1M > 0) {
                _lstCashReturn.Add(new CashDenomEntity { Value = 0.1M, Qty = (int)(_dChange / 0.1M) });
                _dChange = _dChange % 0.1M;
            }
            if (_dChange / 0.05M > 0) {
                _lstCashReturn.Add(new CashDenomEntity { Value = 0.05M, Qty = (int)(_dChange / 0.05M) });
                _dChange = _dChange % 0.05M;
            }
            return _lstCashReturn;
        }
    }
}
