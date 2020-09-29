using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;
using Acme.Db.Provider.Repositories;
using Acme.Db;

namespace Acme.Db.Provider.InMemory.Repositories {
    public class CartRepository : ICartRepository {
        private IDbContext _dbContext;
        public CartRepository(IDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public IEnumerable<CartEntity> Add2MyCart(Guid CartId, string ProductCode) {
            var _repoProduct = new ProductRepository(this._dbContext);
            var _eProduct = _repoProduct.Find(ProductCode).FirstOrDefault();
            if (_eProduct.AvailableQty > 0) {

                var _cartItem = this._dbContext.Cart.Where(c => c.CartId == CartId && c.ProductCode == ProductCode).FirstOrDefault();
                if (_cartItem == null) {
                    this._dbContext.Cart.Add(new CartEntity {
                        CartId = CartId,
                        ProductCode = _eProduct.ProductCode,
                        Price = _eProduct.Price,
                        Qty = 1,
                    });
                } else {
                    _cartItem.Qty += 1;
                }                
                _repoProduct.DeductStock(ProductCode, 1);
            }

            return this._dbContext.Cart.Where(c => c.CartId == CartId);
        }

        public IEnumerable<CartEntity> DelMyCartItem(Guid CartId, string ProductCode) {
            var _cartItem = this._dbContext.Cart.Where(c => c.CartId == CartId && c.ProductCode == ProductCode).FirstOrDefault();
            var _eProduct = this._dbContext.Products.Where(c => c.ProductCode == ProductCode).FirstOrDefault();

            _eProduct.AvailableQty += _cartItem.Qty;

            this._dbContext.Cart.Remove(_cartItem);

            return this._dbContext.Cart.Where(c => c.CartId == CartId);
        }

        public void clearMyCart(Guid CartId) {
            var _lstTemp = this._dbContext.Cart.Where(c => c.CartId == CartId).ToList();
            foreach(var obj in _lstTemp) {
                this._dbContext.Cart.Remove(obj);
            }
        }

        public IEnumerable<CartEntity> getMyCart(Guid CartId) {
            return this._dbContext.Cart.Where(c => c.CartId == CartId);
        }

        public decimal getTotalPrice(Guid CartId) {
            return this._dbContext.Cart.Where(c => c.CartId == CartId).Sum(c => c.Price * c.Qty);
        }
    }
}
