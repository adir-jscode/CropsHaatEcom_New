using CropsHaatEcom_New.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Dynamic;

namespace CropsHaatEcom_New.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
		private void UpdateLayout()
		{

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
		public IActionResult Index()
        {


			UpdateLayout();




			dynamic LandingData = new ExpandoObject();
			LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
			LandingData.MixProducts = GetData.GetMixProducts();
			LandingData.FeaturedProducts = GetData.GetFeaturedProducts();
			//LandingData.SlidinngData = GetData.GetAsset("TopSlider");

			string key = "HeaderCokiee";
			string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

			var data = Request.Cookies[key];
			if (data == null)
			{
				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddDays(7);
				Response.Cookies.Append("HeaderCokiee", value, options);
				RedirectToAction("index");

			}
			else
			{
				ViewBag.IP = data;
			}


			//string URL= CityBankIPG.CityBankCreateOrder(12.00,"CHI20222211");
			//string URL= BkashPGW.CreatePayment("01770618575", "100", "CHI20222211");
			//string URL= IPG.getGrandToken();





			//return Redirect(URL);


			System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
			return View(LandingData);
		}





        public IActionResult LiveTagSearch(string SearchKey)
        {
            if (SearchKey == null)
            {
                return PartialView(new List<ProductDetailsModel>());
            }

            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  Top(8) * FROM [dbo].[ProductDetailsCatagory]  where [IsAvailableStockl]!='2' and ProductName like '%" + SearchKey + "%'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];


                    List<ProductDetailsModel> productdetails = new List<ProductDetailsModel>();

                    productdetails = (from DataRow dr in dt.Rows
                                      select new ProductDetailsModel()
                                      {

                                          //StudentName = dr["StudentName"].ToString(),
                                          //Address = dr["Address"].ToString(),
                                          //MobileNo = dr["MobileNo"].ToString()

                                          ProductId = dr["ProductId"].ToString(),
                                          ProductName = dr["ProductName"].ToString(),
                                          SubCatagory = dr["Catagory"].ToString(),
                                          ReviewRating = dr["Rating"].ToString(),
                                          SKUColor = dr["SKUColor"].ToString(),
                                          SKUSize = dr["SKUSize"].ToString(),
                                          SKUWeight = dr["SKUWeight"].ToString(),
                                          ShortDetails = dr["ShortDetails"].ToString(),
                                          Description = dr["Description"].ToString(),
                                          MarketPrice = Convert.ToDecimal(dr["MarketPrice"].ToString()),
                                          DiscountRatio = Convert.ToDecimal(dr["DiscountRatio"].ToString()),
                                          HaatPrice = Convert.ToDecimal(dr["HaatPrice"].ToString()),
                                          Supplier = dr["Supplier"].ToString(),
                                          FeatureProduct = dr["FeatureProduct"].ToString(),
                                          TopSellingProduct = dr["TopSellingProduct"].ToString(),
                                          IsAvailableStockl = dr["IsAvailableStockl"].ToString(),
                                          Origin = dr["Origin"].ToString(),
                                          MainCatagory = dr["MainCategory"].ToString()


                                      }).ToList();

                    return PartialView(productdetails);


                }
            }




        }

        public IActionResult bkashPaymentCallback()
        {



            return View();

        }
        //public IActionResult Sandbox()
        //{

        //    string URL = CityBankIPG.CityBankCreateOrder(12.00, "CHI20222211");

        //    return Redirect(URL);

        //}
        public IActionResult Delegates()
        {
            UpdateLayout();


            dynamic LandingData = new ExpandoObject();
            LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
            LandingData.FeaturedProducts = GetData.GetFeaturedProducts();
            //  LandingData.SlidinngData = GetData.GetAsset("TopSlider");

            string key = "HeaderCokiee";
            string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

            var data = Request.Cookies[key];
            if (data == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("HeaderCokiee", value, options);
                RedirectToAction("index");

            }
            else
            {
                ViewBag.IP = data;
            }
            //return Redirect(BkashPGW.CreateApplicationPayment("sho", "3.00", "CHDAF230620221621206058"));

            return View(LandingData);

            //TempData["error"] = "Please Try Again.";
            //TempData["Header"] = "Recruitment is not started!";
            //return RedirectToAction("index");

        }
        [HttpPost]
        public IActionResult Delegates(
            string FullName,
            string FullAddress,
            string District,
            string Area,
            string ZipCode,
            string PhoneNumber,
            string Email,
            string NIDnumber

            )
        {
            UpdateLayout();


            dynamic LandingData = new ExpandoObject();
            LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
            LandingData.FeaturedProducts = GetData.GetFeaturedProducts();
            //  LandingData.SlidinngData = GetData.GetAsset("TopSlider");

            string key = "HeaderCokiee";
            string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

            var data = Request.Cookies[key];
            if (data == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("HeaderCokiee", value, options);
                RedirectToAction("index");

            }
            else
            {
                ViewBag.IP = data;
            }
            string ApplicationID = "CHDAF" + value;
            PostData.InsertDeligatesApplication(ApplicationID, FullName, FullAddress, District, Area, ZipCode, PhoneNumber
                , Email, NIDnumber);
            //return Redirect(BkashPGW.CreateApplicationPayment(FullName, "299.00", ApplicationID));

            //TempData["Header"] = "Application Submited";
            //TempData["Success"] = "ID: CHDAF" + value;
            //return View(LandingData);


            return RedirectToAction("index");

        }



        public IActionResult Franchise()
        {
            //UpdateLayout();


            //dynamic LandingData = new ExpandoObject();
            //LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
            //LandingData.FeaturedProducts = GetData.GetFeaturedProducts();
            ////  LandingData.SlidinngData = GetData.GetAsset("TopSlider");

            //string key = "HeaderCokiee";
            //string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

            //var data = Request.Cookies[key];
            //if (data == null)
            //{
            //    CookieOptions options = new CookieOptions();
            //    options.Expires = DateTime.Now.AddDays(7);
            //    Response.Cookies.Append("HeaderCokiee", value, options);
            //    RedirectToAction("index");

            //}
            //else
            //{
            //    ViewBag.IP = data;
            //}


            //return View(LandingData);

            TempData["error"] = "Please Try Again.";
            TempData["Header"] = "Recruitment is not started!";
            return RedirectToAction("index");

        }
        public IActionResult QuickCartAdd(string name, int Qty, string productID, decimal rate, decimal Weight, string StockRequest)
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


                    TempData["success"] = "( " + name + " )" + " Added To Cart Succesully ";
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

                    return RedirectToAction("Index");
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


                    TempData["success"] = "( " + name + " )" + " Added To Cart Succesully ";
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

                    return RedirectToAction("Index");
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
                    TempData["Header"] = "Stock Request Succesfully";




                    UpdateLayout();

                    List<ProductDetailsModel> productDetails = new List<ProductDetailsModel>();



                    dynamic LandingData = new ExpandoObject();
                    productDetails = GetData.GetProductdetails(productID);
                    LandingData.ProductDetails = productDetails;

                    var ProductCategory = productDetails.Select(x => x.SubCatagory).FirstOrDefault();
                    //LandingData.FeaturedProducts = GetData.GetTopSellingProducts();
                    LandingData.RecomendedProducts = GetData.GetRecomendedProducts(ProductCategory);
                    LandingData.ProductsReview = GetData.GetProductReviwes(productID);

                    return RedirectToAction("Index");
                }

            }



        }

        public IActionResult ReturnandRefundPolicy()
        {
            UpdateLayout();


            // string datetime = DateTime.Now.ToString();


            dynamic LandingData = new ExpandoObject();
            LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
            LandingData.FeaturedProducts = GetData.GetFeaturedProducts();

            string key = "HeaderCokiee";
            string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

            var data = Request.Cookies[key];
            if (data == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("HeaderCokiee", value, options);
                RedirectToAction("index");

            }
            else
            {
                ViewBag.IP = data;
            }

            return View(LandingData);
        }

        public IActionResult AboutUS()
        {
            UpdateLayout();


            // string datetime = DateTime.Now.ToString();


            dynamic LandingData = new ExpandoObject();
            LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
            LandingData.FeaturedProducts = GetData.GetFeaturedProducts();

            string key = "HeaderCokiee";
            string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

            var data = Request.Cookies[key];
            if (data == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("HeaderCokiee", value, options);
                RedirectToAction("index");

            }
            else
            {
                ViewBag.IP = data;
            }

            return View(LandingData);
        }
        public IActionResult TermsAndConditions()
        {




            UpdateLayout();


            // string datetime = DateTime.Now.ToString();


            dynamic LandingData = new ExpandoObject();
            LandingData.TopSellingProducts = GetData.GetTopSellingProducts();
            LandingData.FeaturedProducts = GetData.GetFeaturedProducts();

            string key = "HeaderCokiee";
            string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

            var data = Request.Cookies[key];
            if (data == null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("HeaderCokiee", value, options);
                RedirectToAction("index");

            }
            else
            {
                ViewBag.IP = data;
            }










            return View(LandingData);

        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}