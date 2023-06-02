using CropsHaatEcom_New.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Dynamic;

namespace CropsHaatEcom_New.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private IMemoryCache cache;

        public ProductsController(ILogger<ProductsController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            cache = memoryCache;
        }


        public IActionResult Index(string SearchKey = "All Products", int page = 1)
        {
            UpdateLayout();

            ViewBag.SearchKey = SearchKey;
            int LoadPerPage = 32;
            double TotalPage = Math.Ceiling(GetData.allProductCount(SearchKey) / Convert.ToDouble(LoadPerPage));
            ViewBag.TotalPages = TotalPage;

            if (page <= 3)
            {
                ViewBag.StartPage = 1;
            }
            else
            {
                ViewBag.StartPage = page - 2;
            }

            if ((page + 2) > TotalPage)
            {
                ViewBag.Endpage = TotalPage;
            }
            else
            {
                ViewBag.Endpage = page + 2;
            }


            ViewBag.CurrentPage = page;









            dynamic LandingData = new ExpandoObject();
            LandingData.Products = GetData.GridViewSearchProducts(SearchKey, (page - 1) * LoadPerPage, LoadPerPage);




            return View(LandingData);
        }
        [HttpPost]
        public IActionResult Index(string livesearchtags)
        {

            return RedirectToAction("index", "Products", new { SearchKey = livesearchtags });

            // return View(LandingData);
        }



        private void UpdateLayout()
        {
            ViewBag.remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            var data2 = Request.Cookies["HeaderCokiee"];
            ViewBag.IP = data2;
            int itemCount = 0;
            decimal Subtotal = 0;
            List<Cart> Tempcartsdata = Globals.GetCartData();
            List<Cart> cartsdata = new List<Cart>();

            foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
            {

                cartsdata.Add(cart);

            }
            if (cartsdata != null)
            {
                itemCount = cartsdata.Sum(c => c.Qty);
                Subtotal = cartsdata.Sum(c => c.Subtotal);

                ViewBag.Cart = cartsdata;
            }

            if (cartsdata == null)
            {
                if (ViewBag.Cart != null)
                {
                    Globals.cart = ViewBag.Cart;
                }



            }
            ViewBag.FoodCrops = GetData.GetProductSubCategory("Food Crops");
            ViewBag.CashCrops = GetData.GetProductSubCategory("Cash Crops");
            ViewBag.ProcessedCrops = GetData.GetProductSubCategory("Processed crops");
            ViewBag.ItemCount = itemCount;
            ViewBag.SubTotal = Subtotal;

            ViewData["CartCount"] = itemCount;

            string key1 = "UserIDCokiee";
            string key2 = "UserFullNameCokiee";
            string key3 = "UserFullAddressCokiee";
            string key4 = "UserAreaCokiee";
            string key5 = "UserPhoneNumberCokiee";
            string key6 = "UserDistrictCokiee";

            ViewBag.UserID = Request.Cookies[key1];
            ViewBag.UserFullName = Request.Cookies[key2];
            ViewBag.FullAddress = Request.Cookies[key3];
            ViewBag.Area = Request.Cookies[key4];
            ViewBag.PhoneNumber = Request.Cookies[key5];
            ViewBag.District = Request.Cookies[key6];
        }

        public IActionResult Details(string ID)
        {
            UpdateLayout();
            List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();


            dynamic LandingData = new ExpandoObject();
            productDetails = GetData.GetProductdetails(ID);
            LandingData.ProductDetails = productDetails;

            // ViewBag.StripedProductname = Globals.stringSpliter(productDetails.Select(x => x.ProductName).FirstOrDefault(), 1);

            if (productDetails.Count == 0)
            {
                TempData["error"] = "No product Found!";
                TempData["Header"] = "No product Found";
                return RedirectToAction("Index", "Home");
            }


            var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
            //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
            LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
            LandingData.ProductsReview = GetData.GetProductReviwes(ID);

            List<ProductsModel> productsModels = new List<ProductsModel>();
            productsModels = GetData.SimillerProducts(Globals.stringSpliter(productDetails.Select(x => x.ProductName).FirstOrDefault(), 0), productDetails.Select(x => x.ProductName).FirstOrDefault());
            ViewBag.SimilerProductCount = productsModels.Count();
            LandingData.SimillerProducts = productsModels;


            return View(LandingData);

        }
        List<Cart> Cart = new List<Cart>();
        [HttpPost]
        public IActionResult Details(string name, int Qty, string productID, decimal rate, decimal Weight, string StockRequest)
        {

            var data2 = Request.Cookies["HeaderCokiee"];

            List<Cart> cart = new List<Cart>();
            cart = Globals.GetCartData().Where(w => w.ProductId == productID && w.Token == data2).ToList();

            if (StockRequest == "0")
            {
                if (cart.Count > 0)
                {
                    int baseQty = cart[0].Qty;
                    decimal baseweight = cart[0].Weight / baseQty;

                    cart[0].Qty = baseQty + Qty;
                    cart[0].Subtotal = cart[0].Qty * rate;
                    cart[0].Weight = cart[0].Qty * baseweight;


                    TempData["success"] = "( " + name + " )" + " Added To Cart Successfully ";
                    TempData["Header"] = "Product Added To Cart";





                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);

                    return View(LandingData);
                }
                else
                {

                    Cart cart2 = new Cart();
                    cart2.Token = data2;
                    cart2.Qty = Qty;
                    cart2.ProductId = productID;
                    cart2.ProductName = name;
                    cart2.Rate = rate;
                    cart2.Weight = Weight;
                    cart2.Subtotal = rate * Qty;


                    TempData["success"] = "( " + name + " )" + " Added To Cart Successfully ";
                    TempData["Header"] = "Product Added To Cart";


                    Globals.Addincart(cart2);


                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);
                    List<ProductsModel> productsModels = new List<ProductsModel>();
                    productsModels = GetData.SimillerProducts(Globals.stringSpliter(productDetails.Select(x => x.ProductName).FirstOrDefault(), 0), productDetails.Select(x => x.ProductName).FirstOrDefault());
                    ViewBag.SimilerProductCount = productsModels.Count();
                    LandingData.SimillerProducts = productsModels;
                    return View(LandingData);
                }
            }
            else
            {
                string Data = Request.Cookies["UserIDCokiee"];
                if (Data == null)
                {
                    TempData["info"] = "Create Or login To your Account.";
                    TempData["Header"] = "Login To Your Account First to Request Stock";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    string AdminMsg = "New Stock Request Placecd.\nProduct Name:" + name +
                             "\nQty:" + Qty;

                    PostData.InsertStockRequest(productID, Qty.ToString(), Data);

                    SMS.SendQuickSmsAdmin(AdminMsg);

                    TempData["success"] = " We Will Call You ASAP. ";
                    TempData["Header"] = "Stock Request Successfully";




                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);

                    return View(LandingData);
                }

            }



        }

        public IActionResult QuickCartAdd(string name, int Qty, string productID, decimal rate, decimal Weight, string StockRequest, string SearchKey, string page)
        {

            var data2 = Request.Cookies["HeaderCokiee"];

            List<Cart> cart = new List<Cart>();
            cart = Globals.GetCartData().Where(w => w.ProductId == productID && w.Token == data2).ToList();

            if (StockRequest == "0")
            {
                if (cart.Count > 0)
                {
                    int baseQty = cart[0].Qty;
                    decimal baseweight = cart[0].Weight / baseQty;

                    cart[0].Qty = baseQty + Qty;
                    cart[0].Subtotal = cart[0].Qty * rate;
                    cart[0].Weight = cart[0].Qty * baseweight;


                    TempData["success"] = "( " + name + " )" + " Added To Cart Successfully ";
                    TempData["Header"] = "Product Added To Cart";





                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);

                    return RedirectToAction("index", new { SearchKey = SearchKey, page = page });
                }
                else
                {

                    Cart cart2 = new Cart();
                    cart2.Token = data2;
                    cart2.Qty = Qty;
                    cart2.ProductId = productID;
                    cart2.ProductName = name;
                    cart2.Rate = rate;
                    cart2.Weight = Weight;
                    cart2.Subtotal = rate * Qty;


                    TempData["success"] = "( " + name + " )" + " Added To Cart Successfully ";
                    TempData["Header"] = "Product Added To Cart";


                    Globals.Addincart(cart2);


                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);

                    return RedirectToAction("index", new { SearchKey = SearchKey, page = page });
                }
            }
            else
            {
                string Data = Request.Cookies["UserIDCokiee"];
                if (Data == null)
                {
                    TempData["info"] = "Create Or login To your Account.";
                    TempData["Header"] = "Login To Your Account First to Request Stock";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    string AdminMsg = "New Stock Request Placecd.\nProduct Name:" + name +
                             "\nQty:" + Qty;
                    PostData.InsertStockRequest(productID, Qty.ToString(), Data);
                    SMS.SendQuickSmsAdmin(AdminMsg);

                    TempData["success"] = " We Will Call You ASAP. ";
                    TempData["Header"] = "Stock Request Successfully ";




                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);

                    return RedirectToAction("index", new { SearchKey = SearchKey, page = page });
                }

            }



        }


        [HttpPost]
        public IActionResult ProductRequest(string id, int Qty)
        {

            UpdateLayout();

            List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



            dynamic LandingData = new ExpandoObject();
            productDetails = GetData.GetProductdetails(id);
            LandingData.ProductDetails = productDetails;

            var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
            //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
            LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
            LandingData.ProductsReview = GetData.GetProductReviwes(id);

            return View(LandingData);
        }
    }
}
