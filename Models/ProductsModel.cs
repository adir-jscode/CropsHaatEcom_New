namespace CropsHaatEcom_New.Models
{
    
        public class ProductsModel
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string Catagory { get; set; }
            public string Rating { get; set; }
            public string SKUColor { get; set; }
            public string SKUSize { get; set; }
            public string SKUWeight { get; set; }
            public string ShortDetails { get; set; }
            public string Description { get; set; }

            public decimal MarketPrice { get; set; }
            public decimal DiscountRatio { get; set; }
            public decimal HaatPrice { get; set; }

            public string Supplier { get; set; }

            public string FeatureProduct { get; set; }
            public string TopSellingProduct { get; set; }
            public string IsAvailableStockl { get; set; }
            public string Origin { get; set; }

        }

        public class ProductDetailsModel
        {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string MainCatagory { get; set; }
            public string SubCatagory { get; set; }
            public string ReviewRating { get; set; }
            public string SKUColor { get; set; }
            public string SKUSize { get; set; }
            public string SKUWeight { get; set; }
            public string ShortDetails { get; set; }
            public string Description { get; set; }

            public decimal MarketPrice { get; set; }
            public decimal DiscountRatio { get; set; }
            public decimal HaatPrice { get; set; }

            public string Supplier { get; set; }

            public string FeatureProduct { get; set; }
            public string TopSellingProduct { get; set; }
            public string IsAvailableStockl { get; set; }
            public string Origin { get; set; }

        }

        public class ProductSKUSize
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }


        public class Cart
        {

            public string Token { get; set; }
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public int Qty { get; set; }

            public decimal Rate { get; set; }
            public decimal Weight { get; set; }
            public decimal Subtotal { get; set; }


            public static List<Cart> carts { get; set; }
        }


        public class CartDetails
        {

            public int ItemConunt { get; set; } = 0;
            public int QtyCount { get; set; } = 0;
            public decimal Total { get; set; } = 0;
        }

        public class Asset
        {

            public string ID { get; set; }
            public string AssetTable { get; set; }
            public string AsserName { get; set; }
        }

        public class ProductCetegory
        {

            public int ID { get; set; }
            public string MainCategory { get; set; }
            public string SubCategory { get; set; }
        }
        public class ProductReviewModel
        {

            public int ID { get; set; }
            public string ProductID { get; set; }
            public string OrderNumber { get; set; }
            public string Rating { get; set; }
            public string Time { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }


        public class UserModel
        {

            public string ID { get; set; }
            public string FullName { get; set; }
            public string FullAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string District { get; set; }
            public string Area { get; set; }

        }

        public class OrderssModel
        {

            public string OrderID { get; set; }
            public string OrderDateTime { get; set; }
            public string CustomerID { get; set; }
            public string ShippingAddrName { get; set; }
            public string ShippingAddrPhnNum { get; set; }
            public string ShippingAddrAddress { get; set; }
            public string ShippingAddrDis { get; set; }
            public string ShippingAddrArea { get; set; }
            public string PaymentMode { get; set; }
            public string ShippingMethod { get; set; }
            public string Subtotal { get; set; }
            public string TotalWeight { get; set; }
            public string ShippingCharge { get; set; }
            public string GrandTotal { get; set; }
            public string PaidAmoint { get; set; }
            public string Due { get; set; }
            public string Status { get; set; }


        }


        public class ShortOrderDetails
        {

            public string OrderDetailsID { get; set; }
            public string OrderID { get; set; }
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string Rate { get; set; }
            public string Qty { get; set; }
            public string SubTotal { get; set; }


        }
        public class OrderTrackingModel
        {

            public string ID { get; set; }
            public string OrderID { get; set; }
            public string OrderStatus { get; set; }
            public string StatusDiscriptions { get; set; }
            public string DateTime { get; set; }


        }
    }
