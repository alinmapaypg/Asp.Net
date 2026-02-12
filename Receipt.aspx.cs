using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Xml;


public partial class Receipt : System.Web.UI.Page
{
    operation op = new operation();
    public class PayloadModel
    {
        public string Data { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        operation op = new operation();
        try
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod == "POST")
                {
                    XmlDocument XmlDocObj = new XmlDocument();
                    XmlDocObj.Load(Server.MapPath("~/Key.xml"));

                    String MerchantKey = (XmlDocObj.SelectSingleNode("Application/log/MerchantKey").InnerText);

                    if (Request.HttpMethod == "POST")
                    {
                        using (StreamReader reader = new StreamReader(Request.InputStream))
                        {
                            string rawPayload = reader.ReadToEnd();
                            ProcessPostRequest(rawPayload, MerchantKey);
                        }
                    }

                    // Read the raw JSON payload from the request body
                    //using (StreamReader reader = new StreamReader(Request.InputStream))
                    //{
                    //    string jsonPayload = reader.ReadToEnd();

                    //    // Deserialize the JSON payload if needed
                    //    var data = JsonConvert.DeserializeObject<PayloadModel>(jsonPayload);
                    //    //Response.Write("Received Data: " + );
                        
                    //}
                }

                var content = Session["returnContent"];
                dynamic dvresponse = JsonConvert.DeserializeObject(content.ToString());
                string sResponseCode = Session["ResponseCode"] as string;
                
                if (sResponseCode != null)
                {
                    result.Text = Convert.ToString(dvresponse["result"].Value);
                    Orderdate.Text = Convert.ToString(dvresponse["transactionDateTime"].Value);
                    OrderID.Text = Convert.ToString(dvresponse["transactionId"].Value);
                    Amount.Text = Convert.ToString(dvresponse["amountDetails"]["amount"].Value);
                    ResponseCodeDescription.Text = Convert.ToString(dvresponse["responseDescription"].Value);

                    // Assuming you have a method to read the response code description
                    //ResponseCodeDescription.Text = op.readFile(sResponseCode); // Adjust as needed


                    //result.Text = Convert.ToString(dvresponse["result"].Value);
                    //Orderdate.Text = Convert.ToString(dvresponse["orderId"].Value);
                    //Amount.Text = Convert.ToString(dvresponse["amount"].Value);
                    //OrderID.Text = Convert.ToString(dvresponse["transactionId"].Value);
                    //ResponseCodeDescription.Text = op.readFile(sResponseCode);//Convert.ToString(dvresponse["tranid"].Value);
                    //Label1.Text = op.readFile(sResponseCode);
                    //Label1.Text = op.readFile(sResponseCode);
                    //Label1.Text = "The operation has timed out";
                }
            }
        }
        catch (Exception Ex)
        {
            op.WriteErrorToFile("Page_Load: " + Ex.Message);
        }
              
    }


    public void ProcessPostRequest(string decodedData, string merKey)
    {
        try
        {
            // Preserve the + character by replacing it with a placeholder before parsing
            string preservedData = decodedData.Replace("%2F", "/");

            // Parse the query string
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(preservedData);

            // Access the data
            string data = queryParameters[0];

            // If needed, decode the data back to its original form
            data = data.Replace("%2B", "+");
            //string jsonData = data;
            string jsonData = data;

            var queryStringParams = HttpUtility.ParseQueryString(jsonData);

            string encryptedResponse = jsonData;

            // Decrypt the data
            string decryptedData;
            try
            {
                decryptedData = DecryptData(encryptedResponse, merKey);
                dynamic jsonObject = JsonConvert.DeserializeObject(decryptedData);
                //Textdemo.Text = jsonObject.transactionId;
                result.Text = jsonObject.result;
                Orderdate.Text = jsonObject.transactionDateTime;
                OrderID.Text = jsonObject.transactionId;
                Amount.Text = jsonObject.amountDetails.amount;
                ResponseCodeDescription.Text = jsonObject.responseDescription;
                op.WriteErrorToFile("Decrypted Response body " + decryptedData);
            }
            catch (Exception ex)
            {
                // Handle decryption failure
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error processing the request: " + ex.Message);
        }
    }

    public static string DecryptData(string encryptedResponse, string merKey)
    {
        byte[] key = HexStringToByteArray(merKey);
        string decodedAndDecrypted = DecodeAndDecrypt(encryptedResponse, key);

        return decodedAndDecrypted;
    }

    private static string DecodeAndDecrypt(string encodedText, byte[] secretKey)
    {
        // Decode the Base64 encoded string
        byte[] decodedBytes = Convert.FromBase64String(encodedText);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Mode = CipherMode.ECB; // ECB mode (ensure it's safe for your use case)
            aesAlg.Padding = PaddingMode.PKCS7; // PKCS7 padding
            aesAlg.Key = secretKey;

            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(decodedBytes, 0, decodedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }

    public static byte[] HexStringToByteArray(string hexString)
    {
        int len = hexString.Length;
        byte[] data = new byte[len / 2];
        for (int i = 0; i < len; i += 2)
        {
            data[i / 2] = (byte)((GetHexValue(hexString[i]) << 4) + GetHexValue(hexString[i + 1]));
        }
        return data;
    }

    private static int GetHexValue(char hexChar)
    {
        if (hexChar >= '0' && hexChar <= '9')
            return hexChar - '0';
        if (hexChar >= 'A' && hexChar <= 'F')
            return hexChar - 'A' + 10;
        if (hexChar >= 'a' && hexChar <= 'f')
            return hexChar - 'a' + 10;

        throw new ArgumentException("Invalid hex character");
    }

}
