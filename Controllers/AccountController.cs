using CropsHaatEcom_New.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace CropsHaatEcom_New.Controllers
{
	public class AccountController : Controller
	{
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
			return RedirectToAction("Dashboard");

		}

		public IActionResult SignUp()
		{
			UpdateLayout();


			if (Request.Cookies["UserIDCokiee"] != null)
			{
				return RedirectToAction("Dashboard");
			}

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
				RedirectToAction("Signup");

			}
			else
			{
				// ViewBag.IP = data;
			}


			return View(LandingData);
		}

		public IActionResult UpdateUser()
		{
			UpdateLayout();


			if (Request.Cookies["UserIDCokiee"] == null)
			{
				return RedirectToAction("Dashboard");
			}

			// string datetime = DateTime.Now.ToString();

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




			string key = "HeaderCokiee";
			string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

			var data = Request.Cookies[key];
			if (data == null)
			{
				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddDays(7);
				Response.Cookies.Append("HeaderCokiee", value, options);
				RedirectToAction("Signup");

			}
			else
			{
				// ViewBag.IP = data;
			}


			return View();
		}



		public IActionResult Dashboard()
		{
			UpdateLayout();
			if (Request.Cookies["UserIDCokiee"] == null)
			{
				return RedirectToAction("Login");
			}

			// string datetime = DateTime.Now.ToString();


			dynamic LandingData = new ExpandoObject();
			List<OrderssModel> orderssModels = GetData.GetAllFromOrderMaster(Request.Cookies["UserIDCokiee"]);
			LandingData.Orders = orderssModels;

			ViewBag.TotalOrder = orderssModels.Sum(c => Convert.ToDecimal(c.GrandTotal)).ToString("0.00");
			ViewBag.Totalpoint = (orderssModels.Sum(c => Convert.ToDouble(c.GrandTotal)) * 0.01).ToString("0.00");

			string key = "HeaderCokiee";
			string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");

			var data = Request.Cookies[key];
			if (data == null)
			{
				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddDays(7);
				Response.Cookies.Append("HeaderCokiee", value, options);
				RedirectToAction("Signup");

			}
			else
			{
				// ViewBag.IP = data;
			}


			return View(LandingData);
		}



		public IActionResult Login()
		{
			UpdateLayout();
			if (Request.Cookies["UserIDCokiee"] != null)
			{
				return RedirectToAction("Dashboard");
			}


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
				RedirectToAction("Login");

			}
			else
			{
				// ViewBag.IP = data;
			}


			return View(LandingData);
		}
		public IActionResult ForgotPassword()
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
				RedirectToAction("Login");

			}
			else
			{
				// ViewBag.IP = data;
			}


			return View(LandingData);
		}



		public IActionResult Trackorder(string ID)
		{

			UpdateLayout();

			if (Request.Cookies["UserIDCokiee"] == null)
			{
				TempData["Header"] = "Please Login To your Account!";

				TempData["error"] = "Tracking is not allowed";
				return RedirectToAction("Login");
			}

			// string datetime = DateTime.Now.ToString();

			if (ID != null)
			{
				dynamic LandingData = new ExpandoObject();
				List<OrderTrackingModel> OrderData = new List<OrderTrackingModel>();
				OrderData = GetData.GetOrderTrackingDetails(ID);
				LandingData.OrderTrackingDetails = OrderData;
				LandingData.ShippingAddress = GetData.GetAllFromOrderMasterByOrderID(ID);
				List<OrderssModel> Ordersummary = GetData.GetAllFromOrderMasterByOrderID(ID);




				if (Ordersummary.Count() != 0)
				{
					if (Ordersummary.FirstOrDefault().CustomerID != Request.Cookies["UserIDCokiee"])
					{
						TempData["Header"] = "You Can not track the order";

						TempData["error"] = "This Order is not yours";
						return RedirectToAction("Dashboard");

					}


					ViewBag.PaymentMode = Ordersummary.FirstOrDefault().PaymentMode;
					ViewBag.ShippingMethod = Ordersummary.FirstOrDefault().ShippingMethod;
					ViewBag.Subtotal = Ordersummary.FirstOrDefault().Subtotal;
					ViewBag.TotalWeight = Ordersummary.FirstOrDefault().TotalWeight;
					ViewBag.ShippingCharge = Ordersummary.FirstOrDefault().ShippingCharge;
					ViewBag.GrandTotal = Ordersummary.FirstOrDefault().GrandTotal;
					ViewBag.PaidAmoint = Ordersummary.FirstOrDefault().PaidAmoint;
					ViewBag.Status = Ordersummary.FirstOrDefault().Status;
					ViewBag.ID = Ordersummary.FirstOrDefault().OrderID;
					ViewBag.Date = Ordersummary.FirstOrDefault().OrderDateTime;
				}
				else
				{

					TempData["Header"] = "Invalid Order ID";

					TempData["error"] = "Sorry!";
					return View(LandingData);
				}









				LandingData.OrderDetails = GetData.GetAllfromorderDetailsByOrderID(ID);

				ViewBag.TrackingDataAvailable = OrderData.Count;
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
					//ViewBag.IP = data;
				}


				return View(LandingData);

			}
			else
			{
				dynamic LandingData = new ExpandoObject();
				List<OrderTrackingModel> OrderData = new List<OrderTrackingModel>();

				LandingData.OrderTrackingDetails = OrderData;
				LandingData.ShippingAddress = new List<OrderssModel>();
				List<OrderssModel> Ordersummary = new List<OrderssModel>();


				ViewBag.TrackingDataAvailable = 0;
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
					//ViewBag.IP = data;
				}


				return View(LandingData);
			}

		}

		[HttpPost]
		public IActionResult Trackorder2(string InvID)
		{
			return RedirectToAction("Trackorder", "Account", new { ID = InvID });
		}
		[HttpPost]
		public IActionResult UpdateUserDetails(

			string FullName, string FullAddress,
			string PhoneNumber, string Password,
			string District, string Area)
		{

			if (Password == null)
			{
				Password = Account.GetPassword(PhoneNumber);
			}





			if (Request.Cookies["UserPhoneNumberCokiee"] != PhoneNumber)
			{



				if (Account.PhoneNumberAvaliable(PhoneNumber))
				{




					TempData["error"] = "Please Login with your credentials";
					TempData["Header"] = "Number Already Exists";
					return RedirectToAction("Dashboard");

				}
				else
				{
					Account.UpdateUserDetails(Request.Cookies["UserIDCokiee"], FullName, FullAddress, PhoneNumber, Password, District, Area);




					TempData["success"] = "User Updated Successfully.Please Login with your credentials";
					TempData["Header"] = "User Updated";
					return RedirectToAction("Logout");

				}
			}
			else
			{

				Account.UpdateUserDetails(Request.Cookies["UserIDCokiee"], FullName, FullAddress, PhoneNumber, Password, District, Area);




				TempData["success"] = "User Updated Successfully.Please Login with your credentials";
				TempData["Header"] = "User Updated";
				return RedirectToAction("Logout");

			}




		}

		[HttpPost]
		public IActionResult RegesterNewUser(
			string FullName, string FullAddress,
			string PhoneNumber, string Password,
			string District, string Area)
		{

			if (Account.PhoneNumberAvaliable(PhoneNumber))
			{




				TempData["error"] = "Please Login with your credentials";
				TempData["Header"] = "Number Already Exists";
				return RedirectToAction("DashBoard");

			}
			else
			{
				Account.AddNewUser(FullName, FullAddress, PhoneNumber, Password, District, Area);




				TempData["success"] = "User Created Successfully.Please Login with your credentials";
				TempData["Header"] = "New User Created";
				return RedirectToAction("index");

			}



		}

		[HttpPost]
		public IActionResult RetrivePassword(string PhoneNumber)
		{


			Account.ForgetPassword(PhoneNumber);



			TempData["info"] = "Password Will send to your Registered Number (If Number is Registered!!)";

			TempData["Header"] = "Request Accepted";

			return RedirectToAction("Login");

		}

		public IActionResult Logout()
		{
			string key = "UserIDCokiee";


			Response.Cookies.Delete(key, new CookieOptions()
			{
				Secure = true,
			});

			string key2 = "UserFullNameCokiee";



			Response.Cookies.Delete(key2, new CookieOptions()
			{
				Secure = true,
			});


			string key3 = "UserFullAddressCokiee";


			Response.Cookies.Delete(key3, new CookieOptions()
			{
				Secure = true,
			});


			string key4 = "UserAreaCokiee";


			Response.Cookies.Delete(key4, new CookieOptions()
			{
				Secure = true,
			});
			string key5 = "UserPhoneNumberCokiee";

			Response.Cookies.Delete(key5, new CookieOptions()
			{
				Secure = true,
			});
			string key6 = "UserDistrictCokiee";

			Response.Cookies.Delete(key6, new CookieOptions()
			{
				Secure = true,
			});


			return RedirectToAction("Login");
		}




		[HttpPost]
		public IActionResult LoginValidate(string PhoneNumber, string Password)
		{
			string name = "";

			List<UserModel> User = Account.LoginValidate(PhoneNumber, Password);

			if (User.Count() != 0)
			{




				string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");



				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddDays(2);

				foreach (UserModel user in User)
				{
					string key = "UserIDCokiee";
					var data = Request.Cookies[key];
					if (data == null)
					{
						Response.Cookies.Append(key, user.ID, options);
					}
					else
					{
						Response.Cookies.Delete(key, new CookieOptions()
						{
							Secure = true,
						});
						Response.Cookies.Append(key, user.ID, options);
					}

					string key2 = "UserFullNameCokiee";
					var data2 = Request.Cookies[key2];

					if (data2 == null)
					{
						Response.Cookies.Append(key2, user.FullName, options);
						name = user.FullName;
					}
					else
					{

						Response.Cookies.Delete(key2, new CookieOptions()
						{
							Secure = true,
						});
						Response.Cookies.Append(key2, user.FullName, options);
					}

					string key3 = "UserFullAddressCokiee";
					var data3 = Request.Cookies[key3];
					if (data3 == null)
					{
						Response.Cookies.Append(key3, user.FullAddress, options);
					}
					else
					{
						Response.Cookies.Delete(key3, new CookieOptions()
						{
							Secure = true,
						});
						Response.Cookies.Append(key3, user.FullAddress, options);
					}

					string key4 = "UserAreaCokiee";
					var data4 = Request.Cookies[key4];
					if (data4 == null)
					{
						Response.Cookies.Append(key4, user.Area, options);
					}
					else
					{
						Response.Cookies.Delete(key4, new CookieOptions()
						{
							Secure = true,
						});
						Response.Cookies.Append(key4, user.Area, options);
					}
					string key5 = "UserPhoneNumberCokiee";
					var data5 = Request.Cookies[key5];
					if (data5 == null)
					{
						Response.Cookies.Append(key5, user.PhoneNumber, options);
					}
					else
					{
						Response.Cookies.Delete(key5, new CookieOptions()
						{
							Secure = true,
						});
						Response.Cookies.Append(key5, user.PhoneNumber, options);
					}

					string key6 = "UserDistrictCokiee";
					var data6 = Request.Cookies[key6];
					if (data6 == null)
					{
						Response.Cookies.Append(key6, user.District, options);
					}
					else
					{
						Response.Cookies.Delete(key6, new CookieOptions()
						{
							Secure = true,
						});
						Response.Cookies.Append(key6, user.District, options);
					}
				}












				TempData["success"] = "Welcome. " + name;
				TempData["Header"] = "Login Successfully";
				return RedirectToAction("Dashboard");
			}
			else
			{
				TempData["error"] = "Credentials Not Recognized";
				TempData["Header"] = "Login Unsuccessful";
				return RedirectToAction("Login");
			}








		}
	}
}
