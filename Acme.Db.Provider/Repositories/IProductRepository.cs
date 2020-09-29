using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;

namespace Acme.Db.Provider.Repositories {
    public interface IProductRepository {
        void DeductStock(string ProductCode, int Qty);

        IEnumerable<ProductEntity> get(int Page, int RecordPerPage);

        IEnumerable<ProductEntity> Find(string ProductCode);

        void DataInitilization();
    }
}
