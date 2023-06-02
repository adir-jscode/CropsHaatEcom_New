using CropsHaatEcom_New.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace CropsHaatEcom_New.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
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