using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Db.Provider.Entities;
using Acme.Db.Provider.Repositories;
using Acme.Db;

namespace Acme.Db.Provider.InMemory.Repositories {
    public class ProductRepository : IProductRepository {
        private IDbContext _dbContext;
        public ProductRepository(IDbContext dbContext) {
            this._dbContext = dbContext;
        }

        public void DataInitilization() {
            this._dbContext.Products.Clear();
            this._dbContext.Products = new List<ProductEntity> {
                new ProductEntity {
                    ProductCode = "00001",
                    ProductDescr = "Chips Ahoy Cookies",
                    ProductName = "Chips Ahoy Cookies",
                    ProductImg = "ChipAhoy.jpeg",
                    Price = 1,
                },
                new ProductEntity {
                    ProductCode = "00002",
                    ProductDescr = "Orea Cookies",
                    ProductName = "Orea Cookies",
                    ProductImg = "Orea Cookies.jpeg",
                    Price = 0.75M,
                },
                new ProductEntity {
                    ProductCode = "00003",
                    ProductDescr = "Golden Oreo Cookies",
                    ProductName = "Golden Oreo Cookies",
                    ProductImg = "Golden Oreo Cookies.jpeg",
                    Price = 1.75M,
                },
                new ProductEntity {
                    ProductCode = "00004",
                    ProductDescr = "Nutter Butter Cookies",
                    ProductName = "Nutter Butter Cookies",
                    ProductImg = "Nutter Butter.jpeg",
                    Price = 1.35M,
                },
                new ProductEntity {
                    ProductCode = "00005",
                    ProductDescr = "Reese Peanut Butter Cups",
                    ProductName = "Reese Peanut Butter Cups",
                    ProductImg = "Reese Peanut Butter.jpeg",
                    Price = 0.65M,
                },
                new ProductEntity {
                    ProductCode = "00006",
                    ProductDescr = "Hershey Kit Kat",
                    ProductName = "Hershey Kit Kat",
                    ProductImg = "Hershey Kit Kat.jpeg",
                    Price = 0.65M,
                },
                new ProductEntity {
                    ProductCode = "00007",
                    ProductDescr = "Snickers Bar",
                    ProductName = "Snickers Bar",
                    ProductImg = "Snickers.jpeg",
                    Price = 0.85M,
                },
                new ProductEntity {
                    ProductCode = "00008",
                    ProductDescr = "Milky Way Bar",
                    ProductName = "Milky Way Bar",
                    ProductImg = "Milky Way Bar.jpeg",
                    Price = 1.15M,
                },
                new ProductEntity {
                    ProductCode = "00009",
                    ProductDescr = "Twix Bars",
                    ProductName = "Twix Bars",
                    ProductImg = "Twix Bars.jpeg",
                    Price = 0.95M,
                },
                new ProductEntity {
                    ProductCode = "00010",
                    ProductDescr = "Baby Ruth Candy Bar",
                    ProductName = "Baby Ruth Candy Bar",
                    ProductImg = "Baby Ruth Candy Bar.jpeg",
                    Price = 1.45M,
                },
            };
            foreach(var e in this._dbContext.Products) {
                e.AvailableQty = 5;
            }
        }

        public void DeductStock(string ProductCode, int Qty) {
            var _obj = this._dbContext.Products.Where(c => c.ProductCode == ProductCode).FirstOrDefault();
            if (_obj == null) {
                throw new ApplicationException("Sorry. Product not found with product code [" + ProductCode + "]");
            }
            _obj.AvailableQty -= Qty;
        }

        public IEnumerable<ProductEntity> Find(string ProductCode) {
            return this._dbContext.Products.Where(c => c.ProductCode.Contains(ProductCode) || string.IsNullOrEmpty(ProductCode));
        }

        public IEnumerable<ProductEntity> get(int Page, int RecordPerPage) {
            return this._dbContext.Products.Skip((Page - 1) * RecordPerPage).Take(RecordPerPage);
        }


    }
}
