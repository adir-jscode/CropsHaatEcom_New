using Newtonsoft.Json;
using System.Net;

namespace CropsHaatEcom_New
{
	public class BkashPGW
	{
		private static string BkashAppkey = "xvLvpzteVMhk6pHFJBSpNG8Vtc";
		private static string BkashAppsecret = "lRvtesimBLlWjXjbLMsZ7PtWU7Zd8ZX2TKjqVkdVSWlNYiOSeb4Z";
		private static string BkashUsername = "01616799729";
		private static string BkashPassword = "BDdOEmHv1+]";
		private static string BkashBase_Url = "https://tokenized.pay.bka.sh/v1.2.0-beta/tokenized/";
		private static string ServerResponceBaseURL = "https://www.cropshaat.com/";
		// private static string ServerResponceBaseURL = "https://localhost:5001/";

		private static string BKashToken;
		private static string ReturnResponce;
		private static string BKashTokenExpireDateTime = "2022-06-10 00:21:50.000";
		private readonly static TimeSpan Timeout = TimeSpan.FromSeconds(30);

		private readonly static TimeSpan TimeBetweenChecks = TimeSpan.FromSeconds(3);



		public static string getGrandToken()
		{


			if (Convert.ToDateTime(BKashTokenExpireDateTime) < DateTime.Now)
			{
				var url = BkashBase_Url + "checkout/token/grant";

				var httpRequest = (HttpWebRequest)WebRequest.Create(url);
				httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
				httpRequest.Method = "POST";

				httpRequest.Accept = ".s";
				httpRequest.ContentType = "application/json";
				httpRequest.Headers["password"] = BkashPassword;
				httpRequest.Headers["username"] = BkashUsername;



				string data = $@"
                        {{
                                ""app_key"": ""{BkashAppkey}"",
                                ""app_secret"": ""{BkashAppsecret}""
                        }}
                        ";

				using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
				{
					streamWriter.Write(data);
				}

				try
				{
					var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
					using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
					{
						var result = streamReader.ReadToEnd();
						dynamic output = JsonConvert.DeserializeObject(result);
						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						BKashToken = output.id_token;
						BKashTokenExpireDateTime = DateTime.Now.AddSeconds(3000).ToString();
						ReturnResponce = output.id_token;
					}
				}
				catch
				{
					getGrandToken();
				}

				return ReturnResponce;
			}
			else
			{
				return BKashToken;
			}




		}




		public static string CreatePayment(string payerReference, string amount, string merchantInvoiceNumber)
		{
			var url = BkashBase_Url + "checkout/create";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
			httpRequest.Method = "POST";

			httpRequest.Accept = ".s";
			httpRequest.ContentType = "application/json";
			httpRequest.Headers["Authorization"] = getGrandToken();
			httpRequest.Headers["X-App-Key"] = BkashAppkey;



			string data = $@"
                        {{
                                 ""mode"": ""0011"",
                                 ""payerReference"": ""{payerReference}"",
                                 ""callbackURL"": ""{ServerResponceBaseURL + "payments/BkashExecutePayment"}"",
                                 ""amount"": ""{amount}"",
                                 ""currency"": ""BDT"",
                                 ""intent"": ""sale"",
                                 ""merchantInvoiceNumber"": ""{merchantInvoiceNumber}""
                        }}";

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}
			try
			{
				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();

					dynamic output = JsonConvert.DeserializeObject(result);
					string paymentID = output.paymentID;
					if (output.statusCode != "0000")
					{
						PostData.IPGTrxIDs("Bkash", merchantInvoiceNumber, paymentID.Replace("{}", ""), "N/A", "Error");
						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						return output.statusCode;
					}

					PostData.IPGTrxIDs("Bkash", merchantInvoiceNumber, paymentID.Replace("{}", ""), "N/A", "Success");
					PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
					ReturnResponce = output.bkashURL;
				}
			}

			catch
			{
				CreatePayment(payerReference, amount, merchantInvoiceNumber);
			}
			return ReturnResponce;


		}
		public static string CreateApplicationPayment(string payerReference, string amount, string merchantInvoiceNumber)
		{
			var url = BkashBase_Url + "checkout/create";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
			httpRequest.Method = "POST";

			httpRequest.Accept = ".s";
			httpRequest.ContentType = "application/json";
			httpRequest.Headers["Authorization"] = getGrandToken();
			httpRequest.Headers["X-App-Key"] = BkashAppkey;



			string data = $@"
                        {{
                                 ""mode"": ""0011"",
                                 ""payerReference"": ""{payerReference}"",
                                 ""callbackURL"": ""{ServerResponceBaseURL + "payments/ApplicationBkashExecutePayment"}"",
                                 ""amount"": ""{amount}"",
                                 ""currency"": ""BDT"",
                                 ""intent"": ""sale"",
                                 ""merchantInvoiceNumber"": ""{merchantInvoiceNumber}""
                        }}";

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}
			try
			{
				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();

					dynamic output = JsonConvert.DeserializeObject(result);
					string paymentID = output.paymentID;
					if (output.statusCode != "0000")
					{
						PostData.IPGTrxIDs("Bkash", merchantInvoiceNumber, paymentID.Replace("{}", ""), "N/A", "Error");
						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						return output.statusCode;
					}

					PostData.IPGTrxIDs("Bkash", merchantInvoiceNumber, paymentID.Replace("{}", ""), "N/A", "Success");
					PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
					ReturnResponce = output.bkashURL;
				}
			}

			catch
			{
				CreatePayment(payerReference, amount, merchantInvoiceNumber);
			}
			return ReturnResponce;


		}





		public static string Execute(string paymentID)
		{
			dynamic output;
			var url = BkashBase_Url + "checkout/execute";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
			httpRequest.Method = "POST";

			httpRequest.Accept = ".s";
			httpRequest.ContentType = "application/json";
			httpRequest.Headers["Authorization"] = getGrandToken();
			httpRequest.Headers["X-App-Key"] = BkashAppkey;



			string data = $@"
                        {{
                                
                                 ""paymentID"": ""{paymentID}""
                        }}";

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}

			try
			{

				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					output = JsonConvert.DeserializeObject(result);

					string merchantInvoiceNumber = output.merchantInvoiceNumber;
					string trxID = output.trxID;
					if (output.statusCode != "0000")
					{

						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						return output.statusCode;
					}
					if (output.statusCode == "2029")
					{

						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						return output.statusCode;
					}
					PostData.IPGTrxIDs("Bkash", merchantInvoiceNumber.Replace("{}", ""), paymentID, trxID.Replace("{}", ""), "Success");
					PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
					return ReturnResponce = output.trxID;

				}

			}

			catch
			{
				string res = PaymentStatus(paymentID);
				if (res == "Initiated")
				{
					Execute(paymentID);
				}
				if (res == "Completed")
				{
					return PaymentTRXID(paymentID);
				}

			}
			return null;

		}


		public static string PaymentStatus(string paymentID)
		{
			var url = BkashBase_Url + "payment/status";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
			httpRequest.Method = "POST";

			httpRequest.Accept = ".s";
			httpRequest.ContentType = "application/json";
			httpRequest.Headers["Authorization"] = getGrandToken();
			httpRequest.Headers["X-App-Key"] = BkashAppkey;



			string data = $@"
                        {{
                                
                                 ""paymentID"": ""{paymentID}""
                        }}";

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}

			try
			{
				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					dynamic output = JsonConvert.DeserializeObject(result);
					if (output.statusCode != "0000")
					{
						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						return output.statusCode;
					}
					PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
					return output.transactionStatus;
				}
			}
			catch
			{
				PaymentStatus(paymentID);
			}
			return null;
		}
		public static string PaymentTRXID(string paymentID)
		{
			var url = BkashBase_Url + "payment/status";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
			httpRequest.Method = "POST";

			httpRequest.Accept = ".s";
			httpRequest.ContentType = "application/json";
			httpRequest.Headers["Authorization"] = getGrandToken();
			httpRequest.Headers["X-App-Key"] = BkashAppkey;



			string data = $@"
                        {{
                                
                                 ""paymentID"": ""{paymentID}""
                        }}";

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}

			try
			{
				var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					dynamic output = JsonConvert.DeserializeObject(result);
					if (output.statusCode != "0000")
					{
						PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
						return output.statusCode;
					}
					PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
					return output.trxID;
				}
			}
			catch
			{
				PaymentTRXID(paymentID);
			}
			return null;
		}

		public static string SearchTransaction(string trxID)
		{
			var url = BkashBase_Url + "payment/status";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Timeout = (System.Int32)TimeSpan.FromSeconds(30).TotalMilliseconds;
			httpRequest.Method = "POST";

			httpRequest.Accept = ".s";
			httpRequest.ContentType = "application/json";
			httpRequest.Headers["Authorization"] = getGrandToken();
			httpRequest.Headers["X-App-Key"] = BkashAppkey;



			string data = $@"
                        {{
                                
                                 ""trxID"": ""{trxID}""
                        }}";

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}

			var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
				dynamic output = JsonConvert.DeserializeObject(result);

				if (output.statusCode != "0000")
				{
					PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
					return output.statusCode;
				}
				PostData.InsertApiResponder("Bkash", data.Replace(" ", ""), result);
				return output.transactionStatus;
			}
		}


	}






	public class CityBankIPG
	{
		// THE City Bank
		public static string CityBankMerchant = "9107629784";
		public static string CityBankproxy = "";
		public static string CityBankproxyauth = "";
		public static string CityBankUserName = "cropshaat";
		public static string CityBankPassword = "123456Aa";
		public static string CityBankBaseUrl = "https://ecomm-webservice.thecitybank.com:7788/";
		// public static string CityBankBaseUrl = "https://sandbox.thecitybank.com:7788/";
		public static string CityBankServiceUrltoken = CityBankBaseUrl + "transaction/token";
		public static string CityBankServiceUrlEcom = CityBankBaseUrl + "transaction/createorder";
		public static string CityBankGetOrderDetailsUrl = CityBankBaseUrl + "transaction/getorderdetailsapi";
		// public static string CityBankCertPath = "D:/DEVELOPMENT/Git/cropshaatecom/E-Commerce/wwwroot/";
		public static string CityBankCertPath = "~/";
		// public static string CallbackServer = "https://localhost:5001/";
		public static string CallbackServer = "https://www.cropshaat.com/";

		public static string CityBankorderID;
		public static string CityBanksessionID;

		public static string CityBankGetToken()
		{

			string postDataToken =
				"{\"userName\":\"" + CityBankUserName + "\"," +
				"\"password\":\"" + CityBankPassword + "\"}";

			string resToken = Functions.ProcessRequest(CityBankCertPath, postDataToken, CityBankServiceUrltoken, CityBankproxy, CityBankproxyauth);
			PostData.InsertApiResponder("CityBank", postDataToken.Replace(" ", ""), resToken.Replace(" ", ""));
			dynamic resTokenList = JsonConvert.DeserializeObject(resToken);
			return resTokenList.transactionId;
		}

		public static string CityBankCreateOrder(double PurchaseAmount, string InvoiceID)
		{



			string postDataEcom = "{\"merchantId\":\"" + CityBankMerchant + "\","
				+ "\"amount\":\"" + PurchaseAmount * 100 + "\","
				+ "\"currency\":\"050\","
				+ "\"description\":\"" + InvoiceID + "\","
				+ "\"approveUrl\":\"" + CallbackServer + "payments/CitybankPayment?Invoice=" + InvoiceID + "\","
				+ "\"cancelUrl\":\"" + CallbackServer + "payments/CitybankPayment?Invoice=" + InvoiceID + "\","
				+ "\"declineUrl\":\"" + CallbackServer + "payments/CitybankPayment?Invoice=" + InvoiceID + "\","
				+ "\"userName\":\"" + CityBankUserName + "\","
				+ "\"passWord\":\"" + CityBankPassword + "\","
				+ "\"secureToken\":\"" + CityBankGetToken() + "\"}";

			string resEcom = Functions.ProcessRequest(CityBankCertPath, postDataEcom, CityBankServiceUrlEcom, CityBankproxy, CityBankproxyauth);
			dynamic resEcomList = JsonConvert.DeserializeObject(resEcom);
			PostData.InsertApiResponder("CityBank", postDataEcom.Replace(" ", ""), resEcom.Replace(" ", ""));
			string URL = resEcomList.items.url;
			string OrderID = resEcomList.items.orderId;
			string SessionID = resEcomList.items.sessionId;

			CityBankorderID = OrderID;
			CityBanksessionID = SessionID;
			PostData.IPGTrxIDs("CityBank", InvoiceID, OrderID.Replace("{}", ""), SessionID.Replace("{}", ""), "Success");

			return URL + "?ORDERID=" + OrderID + "&SESSIONID=" + SessionID;
		}

		public static string CityBankGetOrderDetails(string orderID, string sessionID)
		{

			string postDataEcom = "{\"orderID\":\"" + orderID + "\","
				+ "\"sessionID\":\"" + sessionID + "\","
				+ "\"merchantID\":\"" + CityBankMerchant + "\","
				+ "\"userName\":\"" + CityBankUserName + "\","
				+ "\"passWord\":\"" + CityBankPassword + "\","
				+ "\"secureToken\":\"" + CityBankGetToken() + "\"}";

			string resEcom = Functions.ProcessRequest(CityBankCertPath, postDataEcom, CityBankGetOrderDetailsUrl, CityBankproxy, CityBankproxyauth);

			//dynamic resEcomList = JsonConvert.DeserializeObject(resEcom);
			PostData.InsertApiResponder("CityBank", postDataEcom.Replace(" ", ""), resEcom.Replace(" ", ""));
			return resEcom;
		}



	}
}
