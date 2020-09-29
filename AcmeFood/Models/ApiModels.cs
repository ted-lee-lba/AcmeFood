using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcmeFood.Models {
    public class ProductModel {
        public int ProductId { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string ProductDescr { get; set; }

        public int AvailableQty { get; set; }

        public string ProductImg { get; set; }

        public decimal Price { get; set; }
    }

    public class CartModel {
        public string ProductCode { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public string ProductImg { get; set; }
    }

    public class GetProductOutputModel {
        public IEnumerable<ProductModel> Products { get; set; }
    }

    public class GetProductInputModel {
        public string ProductCode { get; set; }
    }

    public class AddCartInputModel {
        
        public string ProductCode { get; set; }
    }

    public class AddCartOutputModel {
        public ProductModel Product { get; set; }

        public IEnumerable<CartModel> Carts { get; set; }
    }

    public class DelCartItemInputModel {

        public string ProductCode { get; set; }

    }

    public class DelCartItemOutputModel {
        public ProductModel Product { get; set; }

        public IEnumerable<CartModel> Carts { get; set; }

    }

    public class GetCartOutputModel {
        public IEnumerable<CartModel> Carts { get; set; }
    }

    public class CashDenomModel {
        public int Qty { get; set; }

        public decimal Value { get; set; }

        public string DispDescr { get; set; }

        public decimal TotalCash {
            get {
                return this.Qty * this.Value;
            }
        }
    }

    public class MyCashOutputModel {
        public IEnumerable<CashDenomModel> CashDenom { get; set; }
    }

    public class RefundOutputModel {
        public IEnumerable<CashDenomModel> CashDenom { get; set; }

    }

    public class CheckOutConfirmationInputModel {
        public string PaymentMethod { get; set; }
    }

    public class CheckOutConfirmationOutputModel {
        public decimal TotalPrice { get; set; }
        
        public decimal CCChargePerc { get; set; }

        public decimal CCChargeAmt { get; set; }

        public decimal PaymentPaid { get; set; }

        public IEnumerable<CashDenomModel> ChangeDenom { get; set; }
    }

    public class CheckoutInputModel {
        public string PaymentMethod { get; set; }
    }

    public class CheckOutOutputModel {
        public IEnumerable<CashDenomModel> CashDenom { get; set; }
    }

    public class CheckOutConfirmedInputModel {
        public string PaymentMethod { get; set; }

        public string CreditCardNo { get; set; }
    }

    public class CheckOutConfirmedOutputModel {
        public IEnumerable<CashDenomModel> CashDenom { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }

        public IEnumerable<CartModel> Carts { get; set; }

    }

    public class GetCashPoolOutputModel {
        public IEnumerable<CashDenomModel> CashDenom { get; set; }
    }
}