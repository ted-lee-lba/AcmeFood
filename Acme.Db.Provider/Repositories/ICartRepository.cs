using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;

namespace Acme.Db.Provider.Repositories {
    public interface ICartRepository {
        IEnumerable<CartEntity> getMyCart(Guid CartId);

        IEnumerable<CartEntity> Add2MyCart(Guid CartId, string productcode);

        IEnumerable<CartEntity> DelMyCartItem(Guid CartId, string productcode);

        decimal getTotalPrice(Guid CartId);

        void clearMyCart(Guid uid);
    }
}
