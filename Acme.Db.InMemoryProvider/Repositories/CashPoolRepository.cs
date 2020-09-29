using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;
using Acme.Db.Provider.Repositories;
using Acme.Db;

namespace Acme.Db.Provider.InMemory.Repositories {
    public class CashPoolRepository : ICashPoolRepository {
        private IDbContext _dbContext;
        public CashPoolRepository(IDbContext dbContext) {
            this._dbContext = dbContext;
        }        

        public void ResetPool(Guid PoolId) {
            var _dCashNote = new decimal[] { 5, 1, 0.25M, 0.5M, 0.05M };
            for(int i = 0; i < _dCashNote.Length; i++) {
                var _lstTemp = this._dbContext.CashPool.Where(c => c.PoolId == PoolId && c.Value == _dCashNote[i]);
                if (_lstTemp.Any()) {
                    _lstTemp.First().Qty = 5;
                    continue;
                }

                this._dbContext.CashPool.Add(new CashPoolEntity { PoolId = PoolId, Value = _dCashNote[i], Qty = 5 });
            }
        }

        public IEnumerable<CashPoolEntity> getMyPool(Guid PoolId) {
            return this._dbContext.CashPool.Where(c => c.PoolId == PoolId);
        }

        public IEnumerable<CashPoolEntity> Refund(Guid PoolId) {
            var _lstTemp = this._dbContext.CashPool.Where(c => c.PoolId == PoolId);
            var _lstRefund = _lstTemp.ToList();

            foreach (var obj in _lstTemp) {
                obj.Qty = 0;
            }
            return _lstRefund;
        }

        public decimal getTotalCash(Guid PoolId) {
            return this._dbContext.CashPool.Where(c => c.PoolId == PoolId).Sum(c => c.Qty * c.Value);
        }

        public void clearPool(Guid PoolId) {
            var _lstTemp = this._dbContext.CashPool.Where(c => c.PoolId == PoolId);
            foreach (var obj in _lstTemp) {
                obj.Qty = 0;
            }
        }
    }
}
