using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Text;

using Acme.Db.Provider.Entities;
using Acme.Db.Provider.Repositories;
using AcmeFood.Models;
using AcmeFood.Handler;

namespace AcmeFood.Controllers {
    public class AcmeApiController : ApiController {
        [System.Web.Http.HttpPost]
        public GetProductOutputModel get([FromBody]GetProductInputModel model) {
            var _repoProduct = DependencyResolver.Current.GetService<IProductRepository>();
            var _lstProduct = _repoProduct.Find(model.ProductCode);
            var _model = new GetProductOutputModel();
            _model.Products = _lstProduct.Select(c =>
                new ProductModel {
                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    ProductDescr = c.ProductDescr,
                    AvailableQty = c.AvailableQty,
                    Price = c.Price,
                    ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            );
            return _model;
        }

        public AddCartOutputModel Add2Cart([FromBody]AddCartInputModel model) {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCart = DependencyResolver.Current.GetService<ICartRepository>();
            var _lstCartItem = _repoCart.Add2MyCart(UniqId, model.ProductCode);
            var _model = new AddCartOutputModel();
            _model.Carts = _lstCartItem.Select(c =>
                new CartModel {
                    ProductCode = c.ProductCode,
                    Qty = c.Qty,
                    Price = c.Price,
                    TotalPrice = c.Price * c.Qty
                    //ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            );

            var _repoProduct = DependencyResolver.Current.GetService<IProductRepository>();
            _model.Product = _repoProduct.Find(model.ProductCode).Select(c =>
                new ProductModel {
                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    ProductDescr = c.ProductDescr,
                    AvailableQty = c.AvailableQty,
                    Price = c.Price,
                    ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            ).FirstOrDefault();
            return _model;
        }

        public DelCartItemOutputModel DelCartItem([FromBody]DelCartItemInputModel model) {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCart = DependencyResolver.Current.GetService<ICartRepository>();
            var _lstCartItem = _repoCart.DelMyCartItem(UniqId, model.ProductCode);
            var _model = new DelCartItemOutputModel();
            _model.Carts = _lstCartItem.Select(c =>
                new CartModel {
                    ProductCode = c.ProductCode,
                    Qty = c.Qty,
                    Price = c.Price,
                    TotalPrice = c.Price * c.Qty
                    //ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            );

            var _repoProduct = DependencyResolver.Current.GetService<IProductRepository>();
            _model.Product = _repoProduct.Find(model.ProductCode).Select(c =>
                new ProductModel {
                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    ProductDescr = c.ProductDescr,
                    AvailableQty = c.AvailableQty,
                    Price = c.Price,
                    ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            ).FirstOrDefault();
            return _model;
        }

        [System.Web.Http.HttpGet]
        public GetCartOutputModel GetCart() {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCart = DependencyResolver.Current.GetService<ICartRepository>();

            var _model = new GetCartOutputModel();
            _model.Carts = _repoCart.getMyCart(UniqId).Select(c =>
                new CartModel {
                    ProductCode = c.ProductCode,
                    Qty = c.Qty,
                    Price = c.Price,
                    TotalPrice = c.Price * c.Qty
                    //ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            );
            return _model;
        }

        [System.Web.Http.HttpPost]
        public GetCashPoolOutputModel GetCashPool() {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCashPool = DependencyResolver.Current.GetService<ICashPoolRepository>();
            var _lstRefund = _repoCashPool.Refund(UniqId);

            var _model = new GetCashPoolOutputModel();
            _model.CashDenom = _lstRefund.Select(c => new CashDenomModel {
                Value = c.Value,
                Qty = c.Qty,
                DispDescr = (c.Value < 1) ? (c.Value * 100).ToString("0") + "\u00A2" : "$" + c.Value.ToString()
            });
            return _model;
        }

        [System.Web.Http.HttpPost]
        public MyCashOutputModel ResetCashDenom() {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCashPool = DependencyResolver.Current.GetService<ICashPoolRepository>();
            _repoCashPool.ResetPool(UniqId);

            var _model = new MyCashOutputModel();
            _model.CashDenom = _repoCashPool.getMyPool(UniqId).Select(c=>new CashDenomModel {
                Value = c.Value,
                Qty = c.Qty,
                DispDescr = (c.Value < 1)? (c.Value * 100).ToString("0") + "\u00A2" : "$" + c.Value.ToString()
            });
            return _model;
        }

        [System.Web.Http.HttpPost]
        public RefundOutputModel Refund() {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCashPool = DependencyResolver.Current.GetService<ICashPoolRepository>();
            var _lstRefund = _repoCashPool.Refund(UniqId);

            var _model = new RefundOutputModel();
            _model.CashDenom = _lstRefund.Select(c => new CashDenomModel {
                Value = c.Value,
                Qty = c.Qty,
                DispDescr = (c.Value < 1) ? (c.Value * 100).ToString("0") + "\u00A2" : "$" + c.Value.ToString()
            });
            return _model;
        }

        [System.Web.Http.HttpPost]
        public CheckOutConfirmationOutputModel CheckOutConfirmation(CheckOutConfirmationInputModel model) {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _model = new CheckOutConfirmationOutputModel();
            var _repoCashPool = DependencyResolver.Current.GetService<ICashPoolRepository>();
            var _repoCart = DependencyResolver.Current.GetService<ICartRepository>();
            var _repoCashDenom = DependencyResolver.Current.GetService<ICashDenomRepository>();

            _model.PaymentPaid = _repoCashPool.getTotalCash(UniqId);
            _model.TotalPrice = _repoCart.getTotalPrice(UniqId);

            if (model.PaymentMethod.Equals("cash", StringComparison.OrdinalIgnoreCase)) {
                _model.ChangeDenom = _repoCashDenom.getChanges(_model.PaymentPaid, _model.TotalPrice).Select(c =>
                        new CashDenomModel {
                            Value = c.Value,
                            Qty = c.Qty,
                            DispDescr = (c.Value < 1) ? (c.Value * 100).ToString("0") + "\u00A2" : "$" + c.Value.ToString()
                        }
                    );
            } else if (model.PaymentMethod.Equals("cc", StringComparison.OrdinalIgnoreCase)) {
                _model.CCChargePerc = 0.05M;
                _model.CCChargeAmt = _model.TotalPrice * _model.CCChargePerc;
                _model.ChangeDenom = _repoCashDenom.getChanges(_model.PaymentPaid, 0).Select(c =>
                        new CashDenomModel {
                            Value = c.Value,
                            Qty = c.Qty,
                            DispDescr = (c.Value < 1) ? (c.Value * 100).ToString("0") + "\u00A2" : "$" + c.Value.ToString()
                        }
                    );
            }

            return _model;
        }

        [System.Web.Http.HttpPost]
        public CheckOutConfirmedOutputModel CheckOutConfirmed(CheckOutConfirmedInputModel model) {
            var UniqId = new Guid(Request.Properties[CookieHandler.CookieName].ToString());

            var _repoCart = DependencyResolver.Current.GetService<ICartRepository>();
            var _repoCashPool = DependencyResolver.Current.GetService<ICashPoolRepository>();
            var _repoProduct = DependencyResolver.Current.GetService<IProductRepository>();

            //start the cart items check out process and compose vending machine sales record.
            //Not implemented yet

            //clear the data in temp table
            _repoCart.clearMyCart(UniqId);
            _repoCashPool.clearPool(UniqId);

            //return model
            var _model = new CheckOutConfirmedOutputModel();
            _model.Products = _repoProduct.Find("").Select(c =>
                new ProductModel {
                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    ProductDescr = c.ProductDescr,
                    AvailableQty = c.AvailableQty,
                    Price = c.Price,
                    ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            );
            _model.Carts = _repoCart.getMyCart(UniqId).Select(c =>
                new CartModel {
                    ProductCode = c.ProductCode,
                    Qty = c.Qty,
                    Price = c.Price,
                    TotalPrice = c.Price * c.Qty
                    //ProductImg = "http://localhost:56573/Images/" + c.ProductImg
                }
            );
            _model.CashDenom = _repoCashPool.getMyPool(UniqId).Select(c => new CashDenomModel {
                Value = c.Value,
                Qty = c.Qty,
                DispDescr = (c.Value < 1) ? (c.Value * 100).ToString("0") + "\u00A2" : "$" + c.Value.ToString()
            });
            return _model;
        }
    }
}
