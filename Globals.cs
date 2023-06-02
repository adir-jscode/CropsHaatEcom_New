
using CropsHaatEcom_New.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;


namespace CropsHaatEcom_New
{
    public class dbUpdater
    {
        public static void OrderStatusUpdater(string OrderID,string OrderStatus)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Update [dbo].[OrderMasterTbl] set [Status]='" + OrderStatus + "' Where [OrderID]='" + OrderID + "'", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        
        public static void ApplicationStatusUpdater(string OApplicationID, string ApplicationStatus)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Update [dbo].[DelegatesInfo] set [Status]='" + ApplicationStatus + "' Where [ApplicationID]='" + OApplicationID + "'", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void PaymentMethodUpdater(string OrderID,string PaymentMode)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Update [dbo].[OrderMasterTbl] set [PaymentMode]='" + PaymentMode + "' Where [OrderID]='" + OrderID + "'", con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        public static void UpdateCertData(byte[] certdata)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Insert Into [dbo].[ApiKey] ([APIAuthName],[ApiKey],[Secretkey],[Data]) Values(1,2,3,@BinData)", con))
                {
                    con.Open();
                    SqlParameter sqlParam = cmd.Parameters.AddWithValue("@BinData", certdata);
                    sqlParam.DbType = DbType.Binary;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
    public class Globals
    {
        public static string ConnectionString()
        {
            //  return "Data Source=DESKTOP-NQ6ENV1\\SQLEXPRESS; Initial Catalog=Cropshaat; User Id=Superadmin;Password=12345;";
           return "Data Source=cropshaatdb.cnozd4z8hksk.ap-southeast-1.rds.amazonaws.com,1433;Initial Catalog=xylidic;User Id=CropsHaatAdmin;password=cROPShAAT2022;TrustServerCertificate=True";
           // return "Data Source=103.108.140.160,1434;Initial Catalog=xylidic;User Id=charmless;password=p9zb53A@6";

        }


        public static string stringSpliter(string RawString,int ArrayNo)
        {
            var result = RawString.Split(")");

            if (ArrayNo == 0)
            {
                return result[ArrayNo] + ")";
            }
            else
            {
                byte[] array = new byte[result[1].Length+1];
                return result[1];
            }
            
        }


        public static string encryptString = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");
        public static List<Cart> GetCartData() {


            if (Cart.carts != null)
            {
                return Cart.carts;
            }
            else
            {
                return new List<Cart>();
            }
            


        }
       [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
       
        public static string UserID()
        {


            return DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF"); 

        }

        public static List<Cart> cart { get; set; }

        public static void Addincart(Cart InputCart)
        {
            
            if (Cart.carts == null)
            {


                Cart.carts = new List<Cart>();
                Cart.carts.Add(InputCart);


            }
            else
            {
                Cart.carts.Add(InputCart);
               
            }


    


        }
      
      

        // public static List<Cart> cart = new List<Cart>();




        public static string GetWeightinWord(int gm)
        {
           
            string SolidWight;
           
            int div = 1000;
            decimal temp1;
            temp1 = (Decimal)gm / div;
            int KG_in_gm = (int)temp1;
            int gm_out_gm = (int)(((Decimal)temp1 - (int)KG_in_gm) * 1000);

            if (KG_in_gm > 0 && KG_in_gm < 1000)
            {
                SolidWight = KG_in_gm + " KG";
                return  SolidWight + " " + gm_out_gm + " GM ";

            }
           

            else if (KG_in_gm > 1000)
            {

                decimal temp2;
                temp2 = (Decimal)KG_in_gm / 1000;
                int Ton_in_KG = (int)temp2;
                int KG_out_KG = (int)(((Decimal)temp2 - (int)Ton_in_KG) * 1000);

                SolidWight = Ton_in_KG + " Ton " + KG_out_KG + " KG";
                return SolidWight + " " + gm_out_gm + " GM ";
               
            }

            else if (KG_in_gm == 0)
            {
                SolidWight = "";
                return SolidWight + " " + gm_out_gm + " GM ";

            }
            else
            {
                return "";
            }




        }
    }

    public static class PostData
    {
        public static void InsertDeligatesApplication(
            string ApplicationID
            ,string FullName
            ,string Address
            ,string District
            ,string Area
            ,string ZipCode
            ,string PhoneNumber
            ,string Email
            ,string NIDNumber


            )
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insert_DelegatesInfo", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                                       
                     cmd.Parameters.AddWithValue("@DateTime", DateTime.UtcNow.ToString());
                     cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                     cmd.Parameters.AddWithValue("@FullName", FullName);
                     cmd.Parameters.AddWithValue("@Address", Address);
                     cmd.Parameters.AddWithValue("@District", District);
                     cmd.Parameters.AddWithValue("@Area", Area);
                     cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
                     cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                     cmd.Parameters.AddWithValue("@Email", Email);
                     cmd.Parameters.AddWithValue("@NIDNumber", NIDNumber);
                     cmd.Parameters.AddWithValue("@Status", "Payment Pending");
                     cmd.Parameters.AddWithValue("@TrXID", "N/A");





                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();


                   
                    connec.Close();

                }

            }

        }
        public static void InsertSMSDetails(
            string csms_id,
            string sms,
            string msisdn,
            string sid,
            string Response,
            string System
            )
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insertSMSDetails", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                                       

                    cmd.Parameters.AddWithValue("@csms_id", csms_id);
                     cmd.Parameters.AddWithValue("@DateTime", DateTime.UtcNow.ToString());
                     cmd.Parameters.AddWithValue("@sms", sms);
                     cmd.Parameters.AddWithValue("@msisdn", msisdn);
                     cmd.Parameters.AddWithValue("@sid", sid);
                     cmd.Parameters.AddWithValue("@Response", Response);
                     cmd.Parameters.AddWithValue("@System", System);





                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();


                   
                    connec.Close();

                }

            }

        }


        public static void IPGTrxIDs(
            string GateWay,
            string InvoiceID,
            string OrderID_PaymentID,
            string SessionID_TrXID,
            string Status
            )
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insert_IPGTrxIDs", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                                       

                    cmd.Parameters.AddWithValue("@DateTime",DateTime.UtcNow.ToString() );
                     cmd.Parameters.AddWithValue("@GateWay", GateWay);
                     cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                     cmd.Parameters.AddWithValue("@OrderID_PaymentID", OrderID_PaymentID);
                     cmd.Parameters.AddWithValue("@SessionID_TrXID", SessionID_TrXID);
                     cmd.Parameters.AddWithValue("@Status", Status);






                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();


                   
                    connec.Close();

                }

            }

        }


         public static void InsertApiResponder(
            string APIResponder,
            string ApiRequestBody,
            string ApiResponce
            )
         {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("InsertAPIResponder", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                                       

                     cmd.Parameters.AddWithValue("@APIResponder", APIResponder);
                     cmd.Parameters.AddWithValue("@ApiRequestBody", ApiRequestBody);
                     cmd.Parameters.AddWithValue("@ApiResponce", ApiResponce);
                     cmd.Parameters.AddWithValue("@dateTime", DateTime.Now.AddHours(6));
                    
                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();


                   
                    connec.Close();

                }

            }

        }




        public static void InsertStockRequest(
           string ProductID,
           string Qty,
           string userID

           )
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("InsertNewStockRequest", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@Qty", Qty);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@datetime", DateTime.UtcNow.ToString());
                    cmd.Parameters.AddWithValue("@validated", "0");
                  



                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();



                    connec.Close();

                }

            }

        }



        public static void InsertOrderMasterTbl(
            string OrderID,
            string OrderDateTime,
            string CustomerID,
            string ShippingAddrName,
            string ShippingAddrPhnNum,
            string ShippingAddrAddress,
            string ShippingAddrDis,
            string ShippingAddrArea,
            string PaymentMode,
            string ShippingMethod,
            string Subtotal,
            string TotalWeight,
            string ShippingCharge,
            string GrandTotal,
            string PaidAmoint,
            string Due,
            string Status)
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insertOrderMasterTbl", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;


                     cmd.Parameters.AddWithValue("@OrderID", OrderID);
                     cmd.Parameters.AddWithValue("@OrderDateTime",OrderDateTime);
                     cmd.Parameters.AddWithValue("@CustomerID",CustomerID);
                     cmd.Parameters.AddWithValue("@ShippingAddrName",ShippingAddrName);
                     cmd.Parameters.AddWithValue("@ShippingAddrPhnNum",ShippingAddrPhnNum);
                     cmd.Parameters.AddWithValue("@ShippingAddrAddress",ShippingAddrAddress);
                     cmd.Parameters.AddWithValue("@ShippingAddrDis",ShippingAddrDis);
                     cmd.Parameters.AddWithValue("@ShippingAddrArea",ShippingAddrArea);
                     cmd.Parameters.AddWithValue("@PaymentMode",PaymentMode);
                     cmd.Parameters.AddWithValue("@ShippingMethod",ShippingMethod);
                     cmd.Parameters.AddWithValue("@Subtotal",Subtotal);
                     cmd.Parameters.AddWithValue("@TotalWeight",TotalWeight);
                     cmd.Parameters.AddWithValue("@ShippingCharge",ShippingCharge);
                     cmd.Parameters.AddWithValue("@GrandTotal",GrandTotal);
                     cmd.Parameters.AddWithValue("@PaidAmoint",PaidAmoint);
                     cmd.Parameters.AddWithValue("@Due",Due);
                     cmd.Parameters.AddWithValue("@Status",Status);




                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();



                    connec.Close();

                }

            }

        }






        public static void InsertOrderDetailsTable(
            string OrderID,
            string ProductID,
            string Rate,
            string Qty,
            string ProductSubTotal )
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insertOrderDetailsTable", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@Rate", Rate);
                    cmd.Parameters.AddWithValue("@Qty", Qty);
                    cmd.Parameters.AddWithValue("@SubTotal", ProductSubTotal);
                   




                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();



                    connec.Close();

                }

            }

        }







        public static void InsertOrderTrackingMaster(
           string OrderID,
           string OrderStatus,
           string StatusDiscriptions
           )
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insertOrderTrackingMaster", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@OrderID", OrderID);
                    cmd.Parameters.AddWithValue("@OrderStatus", OrderStatus);
                    cmd.Parameters.AddWithValue("@StatusDiscriptions", StatusDiscriptions);
                    cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString());
                   





                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();



                    connec.Close();

                }

            }

        }


    }


  

    public static class SMS
    {

       public static string sid = "CROPSHAATOTP";

       public static string api_token = "joj7wxwu-ms9xqu4n-equ2egn7-q0m7ytda-tkjf4vi2";

       public static string sms_url = "https://smsplus.sslwireless.com/api/v3/send-sms?";
        public static void SendQuickSms(string msisdn, string sms)
        {
            string csms_id = DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF");
            string myParameters = "api_token=" + api_token +
           "&msisdn=" + msisdn + "&sms=" + System.Web.HttpUtility.UrlEncode(sms) +
           "&csms_id=" + csms_id + "&sid=" + sid;
             
              HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sms_url +
             myParameters);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
               var resp = reader.ReadToEnd();
                PostData.InsertSMSDetails(csms_id,sms, msisdn, sid, resp, "Web");
                
            }
           
        }

        public static void SendQuickSmsAdmin(string sms)
        {
            SendQuickSms("01869666755", sms);
            SendQuickSms("01709816108", sms);
        }
    }
    public class Account
    {
        public static void UpdateUserDetails(
            string id,
            string FullName, string FullAddress,
            string PhoneNumber, string Password,
            string District, string Area)
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("uPDATE_UserDetails", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;




                    cmd.Parameters.AddWithValue("@ID       ", id);

                    cmd.Parameters.AddWithValue("@FullName   ", FullName);
                    cmd.Parameters.AddWithValue("@FullAddress   ", FullAddress);

                    cmd.Parameters.AddWithValue("@PhoneNumber          ", PhoneNumber);

                    cmd.Parameters.AddWithValue("@Password      ", Password);
                    cmd.Parameters.AddWithValue("@District      ", District);
                    cmd.Parameters.AddWithValue("@Area      ", Area);





                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();


                   
                    connec.Close();

                }

            }
        }
        public static void AddNewUser(
           string FullName, string FullAddress,
           string PhoneNumber, string Password,
           string District, string Area)
        {
            using (SqlConnection connec = new SqlConnection(Globals.ConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("insert_UserDetails", connec))
                {

                    connec.Open();

                    cmd.CommandType = CommandType.StoredProcedure;




                    cmd.Parameters.AddWithValue("@ID       ", DateTime.UtcNow.ToString("ddMMyyyyHHmmssFFFFF"));

                    cmd.Parameters.AddWithValue("@FullName   ", FullName);
                    cmd.Parameters.AddWithValue("@FullAddress   ", FullAddress);

                    cmd.Parameters.AddWithValue("@PhoneNumber          ", PhoneNumber);

                    cmd.Parameters.AddWithValue("@Password      ", Password);
                    cmd.Parameters.AddWithValue("@District      ", District);
                    cmd.Parameters.AddWithValue("@Area      ", Area);





                    cmd.ExecuteNonQuery();
                    // ID = cmd.ExecuteScalar().ToString();


                    string msgString = "Welcome " + FullName + ",\nYour account is Successfully Created in Crops Haat.";
                    SMS.SendQuickSms(PhoneNumber, msgString);
                    connec.Close();

                }

            }
        }

        public static void ForgetPassword(string PhoneNumber)
        {
            string count = "";
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT[Password] FROM [xylidic].[dbo].[UserDetails] where [PhoneNumber]='"+ PhoneNumber + "'", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        count = sdr.GetValue(0).ToString();

                        string msgString = "Your Password is: "+ count +"\nRegards,\nCrops Haat";
                        SMS.SendQuickSms(PhoneNumber, msgString);
                    }
                    else
                    {
                        count = "";
                    }

                }
            }
           
        }



        public static string GetPassword(string PhoneNumber)
        {
            string count = "";
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT[Password] FROM [xylidic].[dbo].[UserDetails] where [PhoneNumber]='" + PhoneNumber + "'", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return count = sdr.GetValue(0).ToString();

                      
                    }
                    else
                    {
                        return count = "";
                    }

                }
            }

        }
        public static bool PhoneNumberAvaliable(string PhoneNumber)
        {
            string count = "";
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT[Password] FROM [xylidic].[dbo].[UserDetails] where [PhoneNumber]='" + PhoneNumber + "'", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                       return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

        }

        public static List<UserModel> LoginValidate(string PhoneNumber, string Password)
        {
            string Responce = "";
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [xylidic].[dbo].[UserDetails] where [PhoneNumber]='" + PhoneNumber + "' and [Password]='"+Password+"'", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                  
                    if (sdr.Read())
                    {
                        con.Close();
                        con.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "DataTable1");
                        con.Close();

                        DataTable dt = ds.Tables["DataTable1"];

                        
                        List<UserModel> User = new List<UserModel>();

                        return User = (from DataRow dr in dt.Rows
                                           select new UserModel()
                                           {
                                               ID = dr["ID"].ToString(),
                                               FullName = dr["FullName"].ToString(),
                                               FullAddress = dr["FullAddress"].ToString(),
                                               PhoneNumber = dr["PhoneNumber"].ToString(),
                                               District = dr["District"].ToString(),
                                               Area = dr["Area"].ToString()                                            

                                           }).ToList();
                    }
                    else
                    {
                        return new List<UserModel>();
                    }

                }
            }

        }
    }
    public class GetData
    {
        public static int allProductCount(string SearchKey)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT Count(*)  FROM [dbo].[ProductDetailsCatagory] where [IsAvailableStockl]!='2' and [ProductName] like  '%" + SearchKey + "%' OR [MainCategory] like  '%" + SearchKey + "%'  OR [Catagory] like  '%" + SearchKey + "%'  OR [Catagory] like  '%" + SearchKey + "%'", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        count =Convert.ToInt32(sdr.GetValue(0).ToString());
                    }
                    else
                    {
                        count = 0;
                    }

                }
            }
            return count;
        }
        public static byte[] getCrtData()
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT [Data] FROM [xylidic].[dbo].[ApiKey] where ID='2'", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                         
                       return (byte[])sdr[0];
                    }
                    else
                    {
                        return new byte[0];
                    }

                }
            }
           
        }

        public static int TotalOderINaday()
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT Count(*)  FROM [dbo].[OrderMasterTbl]", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        count = Convert.ToInt32(sdr.GetValue(0).ToString());
                    }
                    else
                    {
                        count = 0;
                    }

                }
            }
            return count;
        }
        public static string GetInvoiceIDfromIPG(string OrderID_PaymentID)
        {
           
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT InvoiceID  FROM [dbo].[IPGTrxIDs] where OrderID_PaymentID='"+ OrderID_PaymentID + "' ", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return sdr.GetValue(0).ToString();
                    }
                    else
                    {
                        return "";
                    }

                }
            }
           
        }
        public static string GetOrderID_PaymentIDfromIPG(string InvID)
        {
           
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  TOP (1) OrderID_PaymentID  FROM [dbo].[IPGTrxIDs] where InvoiceID='" + InvID + "' order by DateTime desc ", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return sdr.GetValue(0).ToString();
                    }
                    else
                    {
                        return "";
                    }

                }
            }
           
        }
        public static string GetSessionID_TrXIDfromIPG(string InvID)
        {
           
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  TOP (1) SessionID_TrXID  FROM [dbo].[IPGTrxIDs] where InvoiceID='" + InvID + "' order by DateTime desc ", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return sdr.GetValue(0).ToString();
                    }
                    else
                    {
                        return "";
                    }

                }
            }
           
        }
        
        public static string GetNameOfDelegatesApplication(string ApplicationID)
        {
           
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT FullName  FROM [dbo].[DelegatesInfo] where ApplicationID='" + ApplicationID + "' ", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return sdr.GetValue(0).ToString();
                    }
                    else
                    {
                        return "";
                    }

                }
            }
           
        }
         public static string GetPhoneNumberOfDelegatesApplication(string ApplicationID)
        {
           
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT PhoneNumber  FROM [dbo].[DelegatesInfo] where ApplicationID='" + ApplicationID + "' ", con))
                {
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return sdr.GetValue(0).ToString();
                    }
                    else
                    {
                        return "";
                    }

                }
            }
           
        }

        public static List<ProductDetailsModel> GridViewProducts(string SearchKey, int Skip, int NextLoad)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[ProductDetailsCatagory]  where [IsAvailableStockl]!='2' order by MarketPrice OFFSET (" + Skip + ") ROWS FETCH NEXT (" + NextLoad + ") ROWS ONLY", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];


                    List<ProductDetailsModel> productdetails = new List<ProductDetailsModel>();

                    return productdetails = (from DataRow dr in dt.Rows
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




                }
            }




        } 
        
        
      
        public static List<ProductDetailsModel> GridViewSearchProducts(string SearchKey, int Skip, int NextLoad)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[ProductDetailsCatagory]  where [IsAvailableStockl]!='2' and [ProductName] like  '%" + SearchKey + "%' OR [MainCategory] like  '%" + SearchKey + "%'  OR [Catagory] like  '%" + SearchKey + "%'  OR [Catagory] like  '%" + SearchKey + "%'order by ProductName OFFSET (" + Skip + ") ROWS FETCH NEXT (" + NextLoad + ") ROWS ONLY", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];


                    List<ProductDetailsModel> productdetails = new List<ProductDetailsModel>();

                    return productdetails = (from DataRow dr in dt.Rows
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




                }
            }




        }



        public static List<OrderssModel> GetAllFromOrderMaster(string CustomerID)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[OrderMasterTbl]  where [CustomerID]='"+ CustomerID + "' order by OrderDateTime desc", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<OrderssModel> Orderss = new List<OrderssModel>();

                    return Orderss = (from DataRow dr in dt.Rows
                                       select new OrderssModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           OrderID = dr["OrderID"].ToString(),
                                           OrderDateTime = dr["OrderDateTime"].ToString(),
                                           CustomerID = dr["CustomerID"].ToString(),
                                           ShippingAddrName = dr["ShippingAddrName"].ToString(),
                                           ShippingAddrPhnNum = dr["ShippingAddrPhnNum"].ToString(),
                                           ShippingAddrAddress = dr["ShippingAddrAddress"].ToString(),
                                           ShippingAddrDis = dr["ShippingAddrDis"].ToString(),
                                           ShippingAddrArea = dr["ShippingAddrArea"].ToString(),
                                           PaymentMode = dr["PaymentMode"].ToString(),
                                           ShippingMethod = dr["ShippingMethod"].ToString(),
                                           Subtotal = dr["Subtotal"].ToString(),
                                           TotalWeight = dr["TotalWeight"].ToString(),
                                           ShippingCharge = dr["ShippingCharge"].ToString(),
                                           GrandTotal = dr["GrandTotal"].ToString(),
                                           PaidAmoint = dr["PaidAmoint"].ToString(),
                                           Due = dr["Due"].ToString(),
                                           Status = dr["Status"].ToString()


                                       }).ToList();




                }
            }




        }


        public static List<Asset> GetAsset(string AssetType)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[AssetTable] where [AssetTable]='" + AssetType + "'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<Asset> Assets = new List<Asset>();

                    return Assets = (from DataRow dr in dt.Rows
                                      select new Asset()
                                      {

                                          //StudentName = dr["StudentName"].ToString(),
                                          //Address = dr["Address"].ToString(),
                                          //MobileNo = dr["MobileNo"].ToString()

                                          ID = dr["ID"].ToString(),
                                          AssetTable = dr["AssetTable"].ToString(),
                                          AsserName = dr["AsserName"].ToString()

                                      }).ToList();




                }
            }




        }
        public static List<OrderssModel> GetAllFromOrderMasterByOrderID(string OrderID)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[OrderMasterTbl]  where [OrderID]='" + OrderID + "' order by OrderDateTime desc", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<OrderssModel> Orderss = new List<OrderssModel>();

                    return Orderss = (from DataRow dr in dt.Rows
                                      select new OrderssModel()
                                      {

                                          //StudentName = dr["StudentName"].ToString(),
                                          //Address = dr["Address"].ToString(),
                                          //MobileNo = dr["MobileNo"].ToString()

                                          OrderID = dr["OrderID"].ToString(),
                                          OrderDateTime = dr["OrderDateTime"].ToString(),
                                          CustomerID = dr["CustomerID"].ToString(),
                                          ShippingAddrName = dr["ShippingAddrName"].ToString(),
                                          ShippingAddrPhnNum = dr["ShippingAddrPhnNum"].ToString(),
                                          ShippingAddrAddress = dr["ShippingAddrAddress"].ToString(),
                                          ShippingAddrDis = dr["ShippingAddrDis"].ToString(),
                                          ShippingAddrArea = dr["ShippingAddrArea"].ToString(),
                                          PaymentMode = dr["PaymentMode"].ToString(),
                                          ShippingMethod = dr["ShippingMethod"].ToString(),
                                          Subtotal = dr["Subtotal"].ToString(),
                                          TotalWeight = dr["TotalWeight"].ToString(),
                                          ShippingCharge = dr["ShippingCharge"].ToString(),
                                          GrandTotal = dr["GrandTotal"].ToString(),
                                          PaidAmoint = dr["PaidAmoint"].ToString(),
                                          Due = dr["Due"].ToString(),
                                          Status = dr["Status"].ToString()


                                      }).ToList();




                }
            }




        }

        public static List<ShortOrderDetails> GetAllfromorderDetailsByOrderID(string OrderID)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[OrderDetailswithProductName]  where [OrderID]='" + OrderID + "'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ShortOrderDetails> ShortOrderDetails = new List<ShortOrderDetails>();

                    return ShortOrderDetails = (from DataRow dr in dt.Rows
                                      select new ShortOrderDetails()
                                      {

                                          //StudentName = dr["StudentName"].ToString(),
                                          //Address = dr["Address"].ToString(),
                                          //MobileNo = dr["MobileNo"].ToString()

                                          OrderDetailsID = dr["OrderDetailsID"].ToString(),
                                          OrderID = dr["OrderID"].ToString(),
                                          ProductID = dr["ProductID"].ToString(),
                                          ProductName = dr["ProductName"].ToString(),
                                          Rate = dr["Rate"].ToString(),
                                          Qty = dr["Qty"].ToString(),
                                          SubTotal = dr["SubTotal"].ToString()



                                      }).ToList();




                }
            }




        }
        public static List<ProductsModel> SimillerProducts(string subName, string name)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT  *  FROM [dbo].[ProductTable]  where   [IsAvailableStockl]!='2' and [ProductName] Like '%" + subName + "%' and [ProductName]!='"+name+"'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductsModel> products = new List<ProductsModel>();

                    return products = (from DataRow dr in dt.Rows
                                       select new ProductsModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           ProductId = dr["ProductId"].ToString(),
                                           ProductName =Globals.stringSpliter(dr["ProductName"].ToString(),2),
                                           Catagory = dr["Catagory"].ToString(),
                                           SKUColor = dr["SKUColor"].ToString(),
                                           SKUSize = dr["SKUSize"].ToString(),
                                           SKUWeight = dr["SKUWeight"].ToString(),
                                           Rating = dr["Rating"].ToString(),

                                           ShortDetails = dr["ShortDetails"].ToString(),
                                           Description = dr["Description"].ToString(),


                                           MarketPrice = Convert.ToDecimal(dr["MarketPrice"].ToString()),
                                           DiscountRatio = Convert.ToDecimal(dr["DiscountRatio"].ToString()),
                                           HaatPrice = Convert.ToDecimal(dr["HaatPrice"].ToString()),
                                           Supplier = dr["Supplier"].ToString(),

                                           FeatureProduct = dr["FeatureProduct"].ToString(),
                                           TopSellingProduct = dr["TopSellingProduct"].ToString(),
                                           IsAvailableStockl = dr["IsAvailableStockl"].ToString(),
                                           Origin = dr["Origin"].ToString()


                                       }).ToList();




                }
            }




        }
        public static List<ProductsModel> GetTopSellingProducts()
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT TOP (8)  *  FROM [dbo].[ProductTable]  where  [IsAvailableStockl]!='2' and [TopSellingProduct]='1'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductsModel> products = new List<ProductsModel>();

                    return products = (from DataRow dr in dt.Rows
                                       select new ProductsModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           ProductId = dr["ProductId"].ToString(),
                                           ProductName = dr["ProductName"].ToString(),
                                           Catagory = dr["Catagory"].ToString(),
                                           SKUColor = dr["SKUColor"].ToString(),
                                           SKUSize = dr["SKUSize"].ToString(),
                                           SKUWeight = dr["SKUWeight"].ToString(),
                                           Rating = dr["Rating"].ToString(),

                                           ShortDetails = dr["ShortDetails"].ToString(),
                                           Description = dr["Description"].ToString(),

                                          
                                           MarketPrice = Convert.ToDecimal(dr["MarketPrice"].ToString()),
                                           DiscountRatio = Convert.ToDecimal(dr["DiscountRatio"].ToString()),
                                           HaatPrice = Convert.ToDecimal(dr["HaatPrice"].ToString()),
                                           Supplier = dr["Supplier"].ToString(),

                                           FeatureProduct = dr["FeatureProduct"].ToString(),
                                           TopSellingProduct = dr["TopSellingProduct"].ToString(),
                                           IsAvailableStockl = dr["IsAvailableStockl"].ToString(),
                                           Origin = dr["Origin"].ToString()


                                       }).ToList();

                    


                }
            }




        }
         public static List<ProductsModel> GetMixProducts()
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM [dbo].[ProductTable]  where  [IsAvailableStockl]!='2' and ProductName like '%PURAN DHAKA%' and SKUWeight='0.10'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductsModel> products = new List<ProductsModel>();

                    return products = (from DataRow dr in dt.Rows
                                       select new ProductsModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           ProductId = dr["ProductId"].ToString(),
                                           ProductName = dr["ProductName"].ToString(),
                                           Catagory = dr["Catagory"].ToString(),
                                           SKUColor = dr["SKUColor"].ToString(),
                                           SKUSize = dr["SKUSize"].ToString(),
                                           SKUWeight = dr["SKUWeight"].ToString(),
                                           Rating = dr["Rating"].ToString(),

                                           ShortDetails = dr["ShortDetails"].ToString(),
                                           Description = dr["Description"].ToString(),

                                          
                                           MarketPrice = Convert.ToDecimal(dr["MarketPrice"].ToString()),
                                           DiscountRatio = Convert.ToDecimal(dr["DiscountRatio"].ToString()),
                                           HaatPrice = Convert.ToDecimal(dr["HaatPrice"].ToString()),
                                           Supplier = dr["Supplier"].ToString(),

                                           FeatureProduct = dr["FeatureProduct"].ToString(),
                                           TopSellingProduct = dr["TopSellingProduct"].ToString(),
                                           IsAvailableStockl = dr["IsAvailableStockl"].ToString(),
                                           Origin = dr["Origin"].ToString()


                                       }).ToList();

                    


                }
            }




        }

        public static List<ProductsModel> GetRecomendedProducts(string category)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT TOP (8) *  FROM [dbo].[ProductTable]  where  [IsAvailableStockl]!='2' and [Catagory]='" + category+"'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductsModel> products = new List<ProductsModel>();

                    return products = (from DataRow dr in dt.Rows
                                       select new ProductsModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           ProductId = dr["ProductId"].ToString(),
                                           ProductName = dr["ProductName"].ToString(),
                                           Catagory = dr["Catagory"].ToString(),
                                           SKUColor = dr["SKUColor"].ToString(),
                                           SKUSize = dr["SKUSize"].ToString(),
                                           SKUWeight = dr["SKUWeight"].ToString(),
                                           Rating = dr["Rating"].ToString(),

                                           ShortDetails = dr["ShortDetails"].ToString(),
                                           Description = dr["Description"].ToString(),


                                           MarketPrice = Convert.ToDecimal(dr["MarketPrice"].ToString()),
                                           DiscountRatio = Convert.ToDecimal(dr["DiscountRatio"].ToString()),
                                           HaatPrice = Convert.ToDecimal(dr["HaatPrice"].ToString()),
                                           Supplier = dr["Supplier"].ToString(),

                                           FeatureProduct = dr["FeatureProduct"].ToString(),
                                           TopSellingProduct = dr["TopSellingProduct"].ToString(),
                                           IsAvailableStockl = dr["IsAvailableStockl"].ToString(),
                                           Origin = dr["Origin"].ToString()


                                       }).ToList();




                }
            }




        }


        public static List<OrderTrackingModel> GetOrderTrackingDetails(string OrderID)
        {
            
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM [xylidic].[dbo].[OrderTrackingMaster] where [OrderID]='"+ OrderID + "' order by [DateTime]", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<OrderTrackingModel> OrderTrackingModel = new List<OrderTrackingModel>();

                    return OrderTrackingModel = (from DataRow dr in dt.Rows
                                       select new OrderTrackingModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           ID = dr["ID"].ToString(),
                                           OrderID = dr["OrderID"].ToString(),
                                           OrderStatus = dr["OrderStatus"].ToString(),
                                           StatusDiscriptions = dr["StatusDiscriptions"].ToString(),
                                           DateTime = dr["DateTime"].ToString()
                                           


                                       }).ToList();




                }
            }




        }






        List<ProductSKUSize> sKUSizes = new List<ProductSKUSize>();
        public static List<ProductSKUSize> GetQtyDropData()
        {
            List<ProductSKUSize> sKUSizes = new List<ProductSKUSize>();
            
            for (int i = 500; i <= 5000000;)
            {
                sKUSizes.Add(new ProductSKUSize()
                {
                    Text = Globals.GetWeightinWord(i),
                    Value = i

                }) ;


                i = i + 500;
            }
            return sKUSizes;
        }
        List<Cart> cart = new List<Cart>();
      
        public static int CartData()
        {
           

            return 0;
        }


        public static List<ProductsModel> GetFeaturedProducts()
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT TOP (8) *  FROM [dbo].[ProductTable]  where  [IsAvailableStockl]!='2' and [FeatureProduct]='1'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductsModel> products = new List<ProductsModel>();

                    return products = (from DataRow dr in dt.Rows
                                       select new ProductsModel()
                                       {

                                           //StudentName = dr["StudentName"].ToString(),
                                           //Address = dr["Address"].ToString(),
                                           //MobileNo = dr["MobileNo"].ToString()

                                           ProductId = dr["ProductId"].ToString(),
                                           ProductName = dr["ProductName"].ToString(),
                                           Catagory = dr["Catagory"].ToString(),
                                           SKUColor = dr["SKUColor"].ToString(),
                                           SKUSize = dr["SKUSize"].ToString(),
                                           SKUWeight = dr["SKUWeight"].ToString(),
                                           Rating= dr["Rating"].ToString(),

                                           ShortDetails = dr["ShortDetails"].ToString(),
                                           Description = dr["Description"].ToString(),


                                           MarketPrice = Convert.ToDecimal(dr["MarketPrice"].ToString()),
                                           DiscountRatio = Convert.ToDecimal(dr["DiscountRatio"].ToString()),
                                           HaatPrice = Convert.ToDecimal(dr["HaatPrice"].ToString()),
                                           Supplier = dr["Supplier"].ToString(),

                                           FeatureProduct = dr["FeatureProduct"].ToString(),
                                           TopSellingProduct = dr["TopSellingProduct"].ToString(),
                                           IsAvailableStockl = dr["IsAvailableStockl"].ToString(),
                                           Origin = dr["Origin"].ToString()


                                       }).ToList();




                }
            }
        }

        public static List<ProductDetailsModel> GetProductdetails(string ProductID)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT TOP (1) *  FROM [dbo].[ProductDetailsCatagory]  where  [IsAvailableStockl]!='2' and [ProductID]='" + ProductID + "'", con))
                {
                    con.Open();

                    SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2, "DataTable2");
                    con.Close();

                    DataTable dt = ds2.Tables["DataTable2"];



                    List<ProductDetailsModel> productdetails = new List<ProductDetailsModel>();

                    return productdetails = (from DataRow dr in dt.Rows
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




                }
            }

        }
        public static List<ProductCetegory> GetProductSubCategory(string MainCategory)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM [dbo].[ProductcetagoryTable] where [MainCategory]='" + MainCategory + "'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductCetegory> ProductCetegory = new List<ProductCetegory>();

                    return ProductCetegory = (from DataRow dr in dt.Rows
                                               select new ProductCetegory()
                                               {

                                                   //StudentName = dr["StudentName"].ToString(),
                                                   //Address = dr["Address"].ToString(),
                                                   //MobileNo = dr["MobileNo"].ToString()

                                                   ID = Convert.ToInt32(dr["ID"].ToString()),
                                                   MainCategory = dr["MainCategory"].ToString(),
                                                   SubCategory = dr["SubCategory"].ToString(),
                                          


                                               }).ToList();




                }
            }
        }


        public static List<ProductReviewModel> GetProductReviwes(string ID)
        {
            using (SqlConnection con = new SqlConnection(Globals.ConnectionString()))
            {
                // MessageBox.Show("SELECT ProductName FROM Products WHERE ProductName like  '" + SearchString + "'");
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM [dbo].[ReviewTable] where [ProductID]='" + ID + "'", con))
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataTable1");
                    con.Close();

                    DataTable dt = ds.Tables["DataTable1"];



                    List<ProductReviewModel> ProductCetegory = new List<ProductReviewModel>();

                    return ProductCetegory = (from DataRow dr in dt.Rows
                                              select new ProductReviewModel()
                                              {

                                                  //StudentName = dr["StudentName"].ToString(),
                                                  //Address = dr["Address"].ToString(),
                                                  //MobileNo = dr["MobileNo"].ToString()

                                                  ID = Convert.ToInt32(dr["ID"].ToString()),
                                                  ProductID = dr["ProductID"].ToString(),
                                                  OrderNumber = dr["OrderNumber"].ToString(),
                                                  Rating = dr["Rating"].ToString(),
                                                  Time = dr["Time"].ToString(),
                                                  Title = dr["Title"].ToString(),
                                                  Body = dr["Body"].ToString()



                                              }).ToList();


                   

                }
            }
        }
    }



   
}
