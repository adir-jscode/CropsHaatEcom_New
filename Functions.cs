using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace CropsHaatEcom_New
{
	public class Functions
	{
		public static string ProcessRequest(string certPath, string post_data_token, string service_url, string proxy, string proxyauth)
		{
			var result = "";
			try
			{
				PostData.InsertApiResponder("CityBank", "debuger", "inside Citybank ProcessRequest");


				//Following command need to execute to create single PFX certificate by SignedCert and Merchant PrivateKey 
				//openssl pkcs12 -export -out createorder.pfx -in createorder.crt -inkey createorder.key -password pass:02468


				PostData.InsertApiResponder("CityBank", "debuger", "-1-");
				//var pfxPath = "~/createorder.pfx";



				//var pfxPath = "D:\\DEVELOPMENT\\GITHUB\\CropsHaatEcom_New\\wwwroot\\13.250.70.239.pfx";
				//var pfxPath = "~/13.250.70.239.pfx";
				var pfxPath = "https://cropshaat.com/13.250.70.239.pfx";



				PostData.InsertApiResponder("CityBank", "debuger pfxPath", pfxPath);

				//string certificateText = System.IO.File.ReadAllText(pfxPath);

				// PostData.InsertApiResponder("CityBank", "debuger certificateText", certificateText);



				X509Certificate2 cert = new X509Certificate2(pfxPath, "02468");

				var certdata = cert.RawData;
				//dbUpdater.UpdateCertData(certdata);


				//X509Certificate2 cert2 = new X509Certificate2(GetData.getCrtData());

				//PostData.InsertApiResponder("CityBank", "debuger", cert.ToString());

				var request = (HttpWebRequest)WebRequest.Create(service_url);
				request.ContentType = "application/json";
				request.Method = "POST";
				request.ServerCertificateValidationCallback = (e, r, c, n) => true;
				request.ClientCertificates.Add(cert);



				PostData.InsertApiResponder("CityBank", "debuger", "-3-");
				using (var streamWriter = new StreamWriter(request.GetRequestStream()))
				{
					PostData.InsertApiResponder("CityBank", "debuger", "-4-");
					streamWriter.Write(post_data_token);
					streamWriter.Flush();
					streamWriter.Close();
				}
				PostData.InsertApiResponder("CityBank", "debuger", "-5-");


				var httpResponse = (HttpWebResponse)request.GetResponse();

				PostData.InsertApiResponder("CityBank", "debuger", "-6-");

				using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
					result = streamReader.ReadToEnd();
				PostData.InsertApiResponder("CityBank", "debuger result", result);
			}
			catch (Exception e)
			{
				//string messages = GetExceptionMessages(e);
				PostData.InsertApiResponder("CityBank", "debuger Error", e.Message);

			}




			return result;
		}
	}
}
