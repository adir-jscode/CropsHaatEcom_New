using CropsHaatEcom_New.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace CropsHaatEcom_New.Controllers
{
    public class CheckOutController : Controller
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

        public IActionResult Cart()
        {



            UpdateLayout();
            var data2 = Request.Cookies["HeaderCokiee"];
            ViewBag.IP = data2;
            List<Cart> Tempcartsdata = Globals.GetCartData();
            List<Cart> cartsdata = new List<Cart>();

            foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
            {

                cartsdata.Add(cart);

            }

            ViewBag.Subtotal = cartsdata.Sum(c => c.Subtotal);
            ViewBag.TotalWeight = cartsdata.Sum(c => c.Weight);
            ViewBag.Grandtotal = cartsdata.Sum(c => c.Subtotal);


            dynamic LandingData = new ExpandoObject();
            LandingData.CartDetails = cartsdata;
            //   LandingData.FeaturedProducts = GetData.GetTopSellingProducts();

            return View(LandingData);


        }



        [HttpPost]
        public IActionResult Cart(string id, int quantity)
        {
            string idd = id + " Qty";
            var data2 = Request.Cookies["HeaderCokiee"];
            ViewBag.IP = data2;
            List<Cart> Tempcartsdata = Globals.GetCartData();
            List<Cart> cartsdata = new List<Cart>();

            foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
            {

                cartsdata.Add(cart);

            }
            foreach (var cart in Globals.GetCartData().Where(w => w.ProductId == id && w.Token == data2))
            {
                decimal baseweight = cart.Weight / cart.Qty;
                cart.Qty = quantity;
                cart.Subtotal = cart.Rate * cart.Qty;
                cart.Weight = baseweight * cart.Qty;

            }

            ViewBag.Subtotal = cartsdata.Sum(c => c.Subtotal);
            ViewBag.TotalWeight = cartsdata.Sum(c => c.Weight);
            ViewBag.Grandtotal = cartsdata.Sum(c => c.Subtotal);

            return RedirectToAction("Cart");


        }







        public IActionResult DeleteFromCart(string id)
        {
            string idd = id + " Qty";
            var data2 = Request.Cookies["HeaderCokiee"];
            ViewBag.IP = data2;
            List<Cart> Tempcartsdata = Globals.GetCartData();
            List<Cart> cartsdata = new List<Cart>();

            foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
            {

                cartsdata.Add(cart);

            }
            foreach (var cart in Globals.GetCartData().Where(w => w.ProductId == id && w.Token == data2))
            {
                cart.Token = "deleted";


            }

            ViewBag.Subtotal = cartsdata.Sum(c => c.Subtotal);
            ViewBag.TotalWeight = cartsdata.Sum(c => c.Weight);
            ViewBag.Grandtotal = cartsdata.Sum(c => c.Subtotal);
            TempData["warning"] = "Selected Product Removed From Cart ";
            TempData["Header"] = "Product Removed From Cart";
            return RedirectToAction("Cart");


        }




        public IActionResult Checkout()
        {
            string Data = Request.Cookies["UserIDCokiee"];
            if (Data == null)
            {
                TempData["info"] = "Create Or login To your Account.";
                TempData["Header"] = "Login To Your Account First";
                return RedirectToAction("Index", "Account");
            }
            UpdateLayout();

            var data2 = Request.Cookies["HeaderCokiee"];

            List<Cart> Tempcartsdata = Globals.GetCartData();
            List<Cart> cartsdata = new List<Cart>();

            foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
            {

                cartsdata.Add(cart);

            }

            if (cartsdata.Count == 0)
            {
                TempData["error"] = "Please Add product To cart";
                TempData["Header"] = "Empty Cart";

                return RedirectToAction("Index", "Home");
            }
            dynamic LandingData = new ExpandoObject();
            LandingData.CartDetails = cartsdata;

            decimal Subtotal = cartsdata.Sum(c => c.Subtotal);
            ViewBag.Subtotal = Subtotal;
            ViewBag.TotalWeight = cartsdata.Sum(c => c.Weight);
            decimal Shipping;

            if (Subtotal >= 500)
            {
                Shipping = 0;
                ViewBag.Shipping = Shipping;
            }
            else
            {
                int TotalWeight = Convert.ToInt32(Math.Ceiling(cartsdata.Sum(c => c.Weight)));

                if (TotalWeight == 1)
                {
                    Shipping = TotalWeight * 80;
                    ViewBag.Shipping = Shipping;
                }
                else
                {
                    Shipping = 80 + ((TotalWeight - 1) * 30);
                    ViewBag.Shipping = Shipping;
                }


            }

            ViewBag.Grandtotal = cartsdata.Sum(c => c.Subtotal) + Shipping;
            //   LandingData.FeaturedProducts = GetData.GetTopSellingProducts();

            return View(LandingData);


        }

        public IActionResult PaymentCreator(string OrderID, string GrandTotal, string ipg)
        {
            string CustomerID = Request.Cookies["UserIDCokiee"];
            if (ipg == "Bkash")
            {
                string Responce = BkashPGW.CreatePayment(CustomerID, GrandTotal, OrderID);
                //string Responce = BkashPGW.CreatePayment("01770618575", "100", "CHI20222211");
                if (int.TryParse(Responce, out _))
                {
                    return RedirectToAction("BkashError", "Payments", new { ErrorCode = Responce });
                }
                return Redirect(Responce);
            }
            else
            {
                PostData.InsertApiResponder("CityBank", "debuger", "inside Citybank");
                return Redirect(CityBankIPG.CityBankCreateOrder(Convert.ToDouble(GrandTotal), OrderID));

            }
        }

        [HttpPost]


        public IActionResult ConfirmOrder(
             string ShippingFullName,
             string ShippingPhoneNumber,
             string ShippingFullAddress,
             string ShippingDistrict,
             string ShippingArea,
             string PaymentMode



              )
        {



            string OrderID = "CHI" + DateTime.UtcNow.AddHours(6).ToString("ddMMyyHH") + GetData.TotalOderINaday();
            string OrderDateTime = DateTime.UtcNow.AddHours(6).ToString();
            string CustomerID = Request.Cookies["UserIDCokiee"];






            string ShippingMethod = "Standard";
            string Subtotal;
            string TotalWeight;
            string ShippingCharge;
            string GrandTotal;
            string PaidAmoint = "0";
            string Due;
            string Status = "Order Placed";

            if (ShippingFullName == null)
            {
                string key1 = "UserIDCokiee";
                string key2 = "UserFullNameCokiee";
                string key3 = "UserFullAddressCokiee";
                string key4 = "UserAreaCokiee";
                string key5 = "UserPhoneNumberCokiee";
                string key6 = "UserDistrictCokiee";


                ShippingFullName = Request.Cookies[key2];
                ShippingPhoneNumber = Request.Cookies[key5];
                ShippingFullAddress = Request.Cookies[key3];
                ShippingDistrict = Request.Cookies[key6];
                ShippingArea = Request.Cookies[key4];

            }




            string Data = Request.Cookies["UserIDCokiee"];
            if (Data == null)
            {
                TempData["info"] = "Create Or login To your Account.";
                TempData["Header"] = "Login To Your Account First";
                return RedirectToAction("Index", "Account");
            }
            UpdateLayout();

            var data2 = Request.Cookies["HeaderCokiee"];

            List<Cart> Tempcartsdata = Globals.GetCartData();
            List<Cart> cartsdata = new List<Cart>();

            foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
            {

                cartsdata.Add(cart);

            }
            if (cartsdata.Count > 0)
            {
                dynamic LandingData = new ExpandoObject();
                LandingData.CartDetails = cartsdata;

                decimal Sub = cartsdata.Sum(c => c.Subtotal);
                Subtotal = Sub.ToString();

                decimal weig = cartsdata.Sum(c => c.Weight);
                TotalWeight = weig.ToString();

                if (Sub >= 500)
                {
                    ShippingCharge = "0";
                }
                else
                {

                    int TotalWeight2 = (int)Math.Ceiling(Convert.ToDecimal(TotalWeight));
                    if (TotalWeight2 == 1)
                    {
                        ShippingCharge = (TotalWeight2 * 80).ToString();

                    }
                    else
                    {
                        ShippingCharge = (80 + ((TotalWeight2 - 1) * 30)).ToString();

                    }


                }

                GrandTotal = (Convert.ToDecimal(Sub) + Convert.ToDecimal(ShippingCharge)).ToString();




                PostData.InsertOrderMasterTbl(OrderID, OrderDateTime, CustomerID,
                    ShippingFullName, ShippingPhoneNumber, ShippingFullAddress, ShippingDistrict,
                    ShippingArea, PaymentMode, ShippingMethod, Subtotal, TotalWeight, ShippingCharge,
                    GrandTotal, PaidAmoint, GrandTotal, Status);



                foreach (var cart in Globals.GetCartData().Where(w => w.Token == data2))
                {
                    PostData.InsertOrderDetailsTable(OrderID, cart.ProductId, cart.Rate.ToString(), cart.Qty.ToString(), cart.Subtotal.ToString());

                }

                PostData.InsertOrderTrackingMaster(OrderID, "Order Placed", "Order Placed By User");
                string msgString = "Your Order is placed Succesfully." +
                                        "\nOrderID: " + OrderID +
                                        "\nTotal Order Amount BDT "
                                    + GrandTotal + " TK. \nWe Will Call You ASAP to Confirm. " +
                                    "You can Track Your Order at \n" + "" +
                                    "https://www.cropshaat.com/Account/Trackorder/" + OrderID +
                                    "\nThank You For Shopping In CropsHaat" +
                                    " \nRegards,\nCrops Haat";


                SMS.SendQuickSms(Request.Cookies["UserPhoneNumberCokiee"], msgString);

                string AdminMsg = "New Order Placecd. OrderID\n" + OrderID +
                                 "\nAmount " + GrandTotal;
                SMS.SendQuickSmsAdmin(AdminMsg);



                string key = "HeaderCokiee";
                Response.Cookies.Delete(key, new CookieOptions()
                {
                    Secure = true,
                });


                string value = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");
                TempData["success"] = "Order ID: " + OrderID;
                TempData["Header"] = "Order Confirmed";
                var data = Request.Cookies[key];
                if (data == null)
                {
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append("HeaderCokiee", value, options);

                    ViewBag.IP = data;
                }
                else
                {
                    ViewBag.IP = data;
                }

                //if (PaymentMode == "Bkash")
                //{
                //    string Responce = BkashPGW.CreatePayment(CustomerID, GrandTotal, OrderID);
                //    //string Responce = BkashPGW.CreatePayment("01770618575", "100", "CHI20222211");
                //    if (int.TryParse(Responce, out _))
                //    {
                //        return RedirectToAction("BkashError", "Payments", new { ErrorCode = Responce });
                //    }
                //    return Redirect(Responce);
                //}


                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Please Add product To cart";
                TempData["Header"] = "Empty Cart";

                return RedirectToAction("Index", "Home");
            }



        }
    }
}
