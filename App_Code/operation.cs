using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Management;
using System;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for operation
/// </summary>
public class operation
{


    public operation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region generate random no
    public string GenerateRandom()
    {
        //try
        //{
        Random rand = new Random((int)DateTime.Now.Ticks);
        int RandomNumber;
        RandomNumber = rand.Next(100000, 999999);
        return Convert.ToString(RandomNumber);
        //}
        //catch (Exception ex)
        //{
        //    WriteErrorToFile("GenerateRandom : " + ex.Message);
        //}

    }
    #endregion

    #region Get IP Address of Server
    public string GetIPAddress()
    {
        string sLocalComputerName = Dns.GetHostName();
        string sLocalIP = Dns.GetHostEntry(sLocalComputerName).AddressList[1].ToString();
        return sLocalIP;

    }

    public string GetLocalIPAddress()
    {
        string localIP = string.Empty;

        // Get the host name of the local machine
        string hostName = Dns.GetHostName();

        // Get the IP addresses associated with the host
        IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

        // Iterate through the addresses and find the first IPv4 address
        foreach (var ip in hostEntry.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork) // Check for IPv4
            {
                localIP = ip.ToString();
                break; // Exit the loop after finding the first IPv4 address
            }
        }

        return localIP;
    }

    #endregion

    #region Sha256hsh
    //public static String sha256_hash(string value)
    public String sha256_hash(string value)
    {
        StringBuilder Sb = new StringBuilder();
        try
        {
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
        }
        catch (Exception ex)
        {
        }
        return Sb.ToString();
    }
    #endregion

    #region generateJson
    public JObject generateJsonPurchasePreAuthorization(String sTransId, String Token, String txtCardtoken, String txtCountry, String txtfirst_name, String txtlast_name, String txtaddress, String txtcity, String txtstate, String txtzip, String txtPhoneno, String txtcustomerEmail, String transid, String Hosted, String password, String secret, String amount, String Currency, String Action, String strHash, String merchantIp, String Tranid, String IframeFlag, String txtmetadataone)
    {
        JObject JsonPurchasePreAuthorization = new JObject();

        try
        {
            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonPurchasePreAuthorization["terminalId"] = Hosted;
            JsonPurchasePreAuthorization["password"] = password;
            JsonPurchasePreAuthorization["paymentType"] = Action;
            JObject customer = new JObject();

            customer["cardHolderName"] = txtfirst_name + " " + txtlast_name;
            customer["customerEmail"] = txtcustomerEmail;
            customer["billingAddressStreet"] = txtaddress;
            customer["billingAddressCity"] = txtcity;
            customer["billingAddressState"] = txtstate;
            customer["billingAddressPostalCode"] = txtzip;
            customer["billingAddressCountry"] = txtCountry;

            JsonPurchasePreAuthorization["customer"] = customer;
            JsonPurchasePreAuthorization["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = transid;
            JsonPurchasePreAuthorization["order"] = order;
            JsonPurchasePreAuthorization["referenceId"] = sTransId;
            JsonPurchasePreAuthorization["signature"] = strHash;
            JObject tokenization = new JObject();
            tokenization["operation"] = Token;
            tokenization["cardToken"] = txtCardtoken;
            JsonPurchasePreAuthorization["tokenization"] = tokenization;
            JsonPurchasePreAuthorization["country"] = txtCountry;
            JsonPurchasePreAuthorization["amount"] = amount;
            JsonPurchasePreAuthorization["customerIp"] = merchantIp;
            JsonPurchasePreAuthorization["merchantIp"] = merchantIp;
            JsonPurchasePreAuthorization.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonPurchasePreAuthorization["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonPurchasePreAuthorization;
    }


    public JObject generateJsonSadad(String txtTranid, String txtSmslanguage, String txtSendlinkmode, String txtCountry, String txtfirst_name, String txtlast_name, String txtaddress, String txtcity, String txtstate, String txtzip, String txtPhoneno,
                                    String txtcustomerEmail, String transid, String Hosted, String password, String secret, String amount, String Currency, String Action, String strHash,
                                    String merchantIp, String Tranid,
                                    String SadadBillNumber, String SadadCustomerFullName, String SadadCustomerIdNumber, String SadadCustomerIdType,
                                        String SadadCustomerEmailAddress,
                                    String SadadCustomerMobileNumber, String SadadCustomerPreviousBalance, String SadadCustomerTaxNumber, String SadadExpireDate,
                                    String SadadServiceName, String SadadBuildingNumber, String SadadCityCode, String SadadDistrictCode, String SadadPostalCode,
                                    String SadadStreetName, String SadadName, String SadadQuantity, String SadadDiscount, String SadadDiscountType, String SadadVat,
                                    String SadadUnitPrice, String SadadIsPartialAllowed, String SadadMiniPartialAmount, String txtmetadataone)
    {
        JObject testJson = new JObject();

        try
        {
            testJson["terminalId"] = Hosted;
            testJson["password"] = password;
            testJson["signature"] = strHash;
            testJson["paymentType"] = 1;
            testJson["amount"] = amount;
            testJson["currency"] = Currency;

            JObject payment = new JObject();
            payment["paymentMethod"] = "SADAD";
            payment["channelName"] = "SADAD";
            testJson["paymentInstrument"] = payment;

            JObject customer = new JObject();
            customer["cardHolderName"] = txtfirst_name + " " + txtlast_name;
            customer["customerEmail"] = txtcustomerEmail;
            customer["billingAddressStreet"] = txtaddress;
            customer["billingAddressCity"] = txtcity;
            customer["billingAddressState"] = txtstate;
            customer["billingAddressPostalCode"] = txtzip;
            customer["billingAddressCountry"] = txtCountry;
            testJson["customer"] = customer;

            JObject testJsonSadad = new JObject();
            testJsonSadad["customerFullName"] = SadadCustomerFullName;
            testJsonSadad["districtCode"] = SadadDistrictCode;
            testJsonSadad["customerEmailAddress"] = SadadCustomerEmailAddress;
            testJsonSadad["cityCode"] = SadadCityCode;
            testJsonSadad["postalCode"] = SadadPostalCode;
            testJsonSadad["miniPartialAmount"] = SadadMiniPartialAmount;
            testJsonSadad["customerPreviousBalance"] = SadadCustomerPreviousBalance;
            testJsonSadad["serviceName"] = SadadServiceName;
            testJsonSadad["customerIdNumber"] = SadadCustomerIdNumber;
            testJsonSadad["streetName"] = SadadStreetName;
            JObject testJsonSadadBillItemList = new JObject();
            testJsonSadadBillItemList["name"] = SadadName;
            testJsonSadadBillItemList["quantity"] = SadadQuantity;
            testJsonSadadBillItemList["discount"] = SadadDiscount;
            testJsonSadadBillItemList["discountType"] = SadadDiscountType;
            testJsonSadadBillItemList["vat"] = SadadVat;
            testJsonSadadBillItemList["unitPrice"] = SadadUnitPrice;
            testJsonSadad.Add("billItemList", JsonConvert.SerializeObject(testJsonSadadBillItemList));            
            testJsonSadad["entityActivityId"] = "";//////////////Ack what should be add
            testJsonSadad["buildingNumber"] = SadadBuildingNumber;
            testJsonSadad["isPartialAllowed"] = SadadIsPartialAllowed;
            testJsonSadad["customerMobileNumber"] = SadadCustomerMobileNumber;
            testJsonSadad["expireDate"] = SadadExpireDate;
            testJsonSadad["customerIdType"] = SadadCustomerIdType;
            testJsonSadad["billNumber"] = SadadBillNumber;
            testJsonSadad["sendLinkMode"] = txtSendlinkmode;
            testJsonSadad["smsLanguage"] = txtSmslanguage;
            testJsonSadad["smsTemplate"] = "";//////////////Ack what should be add
            testJson["sadadPaymentDetails"] = testJsonSadad;

            JObject order = new JObject();
            order["orderId"] = transid;
            order["description"] = "Purchase of product XYZ";//////////////Ack what should be add
            testJson["order"] = order;

            JObject details = new JObject();
            details["userData"] = Regex.Replace(Convert.ToString(txtmetadataone), "(\\w+)", "\"$1\"");
            testJson["additionalDetails"] = details;

            JObject request = new JObject();
            request["rqstOrigin"] = "Web";//////////////Ack what should be add
            request["rqstProcessingType"] = "PaymentLink";//////////////Ack what should be add
            testJson["internalProcessing"] = request;
            testJson["merchantIP"] = merchantIp;//////////////Check MerchantIP should be small like merchantIp

            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();//GetDetails();// GetDestopOSInfo(); //getOSInfos();// os + "" + vs;//getOSInfos();//System.Environment.OSVersion.Platform.ToString(); //;
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;
            testJson.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
        }
        catch (Exception ex)
        {
        }
        return testJson;
    }

    public JObject generateJsonCaptureRefundVoid(String sTransId, String Token, String txtCardtoken, String txtCountry, String txtfirst_name, String txtlast_name, String txtaddress, String txtcity, String txtstate, String txtzip, String txtPhoneno, String txtcustomerEmail, String transid, String Hosted, String password, String secret, String amount, String Currency, String Action, String strHash, String merchantIp, String Tranid, String TransId, String txtmetadataone)
    {
        JObject JsonCaptureRefundVoid = new JObject();

        try
        {

            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonCaptureRefundVoid["terminalId"] = Hosted;
            JsonCaptureRefundVoid["password"] = password;
            JsonCaptureRefundVoid["paymentType"] = Action;
            JObject customer = new JObject();

            customer["cardHolderName"] = txtfirst_name + " " + txtlast_name;
            customer["customerEmail"] = txtcustomerEmail;
            customer["billingAddressStreet"] = txtaddress;
            customer["billingAddressCity"] = txtcity;
            customer["billingAddressState"] = txtstate;
            customer["billingAddressPostalCode"] = txtzip;
            customer["billingAddressCountry"] = txtCountry;

            JsonCaptureRefundVoid["customer"] = customer;
            JsonCaptureRefundVoid["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = transid;
            JsonCaptureRefundVoid["order"] = order;
            JsonCaptureRefundVoid["referenceId"] = sTransId;
            JsonCaptureRefundVoid["signature"] = strHash;
            JObject tokenization = new JObject();
            tokenization["operation"] = Token;
            tokenization["cardToken"] = txtCardtoken;
            JsonCaptureRefundVoid["tokenization"] = tokenization;
            JsonCaptureRefundVoid["country"] = txtCountry;
            JsonCaptureRefundVoid["amount"] = amount;
            JsonCaptureRefundVoid["customerIp"] = merchantIp;
            JsonCaptureRefundVoid["merchantIp"] = merchantIp;
            JsonCaptureRefundVoid.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonCaptureRefundVoid["additionalDetails"] = additionalDetails;
        }
        catch (Exception ex)
        {
        }
        return JsonCaptureRefundVoid;
    }

    public JObject generateJsonVoidPurchase(String sTransId, String txtCountry, String txtcustomerEmail, String Hosted, String password, String amount, String Currency, String Tranid, String Action, String strHash, String merchantIp, String TransId, String IframeFlag, String txtmetadataone)
    {
        JObject JsonVoidPurchase = new JObject();
        try
        {
            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonVoidPurchase["referenceId"] = sTransId;
            JsonVoidPurchase["terminalId"] = Hosted;
            JsonVoidPurchase["password"] = password;
            JsonVoidPurchase["signature"] = strHash;
            JsonVoidPurchase["paymentType"] = Action;
            JsonVoidPurchase["amount"] = amount;
            JsonVoidPurchase["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = Tranid;
            JsonVoidPurchase["order"] = order;
            JObject customer = new JObject();
            customer["billingAddressCountry"] = txtCountry;
            JsonVoidPurchase["customer"] = customer;
            JObject payment = new JObject();
            payment["paymentMethod"] = "CCI";
            JsonVoidPurchase["paymentInstrument"] = payment;          
            JsonVoidPurchase.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonVoidPurchase["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonVoidPurchase;
    }

    public JObject generateJsonVoidRefund(String sTransId, String txtCountry, String txtcustomerEmail, String Hosted, String password, String amount, String Currency, String Tranid, String Action, String strHash, String merchantIp, String TransId, String IframeFlag, String txtmetadataone)
    {
        JObject JsonVoidRefund = new JObject();

        try
        {
            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonVoidRefund["referenceId"] = sTransId;
            JsonVoidRefund["terminalId"] = Hosted;
            JsonVoidRefund["password"] = password;
            JsonVoidRefund["signature"] = strHash;
            JsonVoidRefund["paymentType"] = Action;
            JsonVoidRefund["amount"] = amount;
            JsonVoidRefund["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = Tranid;
            JsonVoidRefund["order"] = order;
            JObject customer = new JObject();
            customer["billingAddressCountry"] = txtCountry;
            JsonVoidRefund["customer"] = customer;
            JObject payment = new JObject();
            payment["paymentMethod"] = "CCI";
            JsonVoidRefund["paymentInstrument"] = payment;
            JsonVoidRefund.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonVoidRefund["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonVoidRefund;
    }

    public JObject generateJsonVoidCapture(String sTransId, String txtCountry, String txtcustomerEmail, String Hosted, String password, String amount, String Currency, String Tranid, String Action, String strHash, String merchantIp, String TransId, String IframeFlag, String txtmetadataone)
    {
        JObject JsonVoidCapture = new JObject();

        try
        {
            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonVoidCapture["referenceId"] = sTransId;
            JsonVoidCapture["terminalId"] = Hosted;
            JsonVoidCapture["password"] = password;
            JsonVoidCapture["signature"] = strHash;
            JsonVoidCapture["paymentType"] = Action;
            JsonVoidCapture["amount"] = amount;
            JsonVoidCapture["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = Tranid;
            JsonVoidCapture["order"] = order;
            JObject customer = new JObject();
            customer["billingAddressCountry"] = txtCountry;
            JsonVoidCapture["customer"] = customer;
            JObject payment = new JObject();
            payment["paymentMethod"] = "CCI";
            JsonVoidCapture["paymentInstrument"] = payment;
            JsonVoidCapture.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonVoidCapture["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonVoidCapture;
    }

    public JObject generateJsonTransactionEnquiry(String sTransId, String txtCountry, String txtcustomerEmail, String Hosted, String password, String amount, String Currency, String Tranid, String Action, String strHash, String merchantIp, String TransId, String IframeFlag, String txtmetadataone)
    {
        JObject JsonTransactionEnquiry = new JObject();

        try
        {
            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonTransactionEnquiry["referenceId"] = sTransId;
            JsonTransactionEnquiry["terminalId"] = Hosted;
            JsonTransactionEnquiry["password"] = password;
            JsonTransactionEnquiry["signature"] = strHash;
            JsonTransactionEnquiry["paymentType"] = Action;
            JsonTransactionEnquiry["amount"] = amount;
            JsonTransactionEnquiry["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = Tranid;
            JsonTransactionEnquiry["order"] = order;
            JObject customer = new JObject();
            customer["billingAddressCountry"] = txtCountry;
            JsonTransactionEnquiry["customer"] = customer;
            JObject payment = new JObject();
            JsonTransactionEnquiry["paymentInstrument"] = payment;
            JsonTransactionEnquiry.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonTransactionEnquiry["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonTransactionEnquiry;
    }

    public JObject generateJsonTokenization(String transid, String txtCardtoken, String txtaddress, String txtcity, String txtstate, String txtzip, String txtPhoneno, String txtcustomerEmail, String Token, String txtCountry, String Hosted, String password, String amount, String Currency, String Tranid, String Action, String strHash, String merchantIp, String TransId, String IframeFlag, String txtmetadataone)
    {
        JObject JsonTokenization = new JObject();

        try
        {
            //testJson["action"] = Action;
            //testJson["trackid"] = Tranid;            
            //testJson["terminalId"] = Hosted;
            //testJson["password"] = password;
            //testJson["amount"] = amount;
            //testJson["currency"] = Currency;
            //testJson["zipCode"] = txtzip;
            //testJson["country"] = txtCountry;
            //testJson["city"] = txtcity;
            //testJson["state"] = txtstate;
            //testJson["address"] = txtaddress;
            //testJson["customerEmail"] = txtcustomerEmail;
            //testJson["contactNumber"] = txtPhoneno;
            //testJson["requestHash"] = strHash;
            //testJson["tokenOperation"] = Token;
            //testJson["cardToken"] = txtCardtoken;            
            //testJson["metaData"] = txtmetadataone;
            //JObject DeviceInfo = new JObject();
            //string pluginName = "DotNet_Hosted";
            //double pluginVersion = 3.0;
            //string pluginPlatform = MobileOrDesktop();
            //string deviceModel = string.Empty;
            //if (pluginPlatform == "Desktop")
            //{
            //    deviceModel = GetDestopOSInfo();
            //}
            //else
            //{
            //    deviceModel = Dns.GetHostName();//GetDetails();// GetDestopOSInfo(); //getOSInfos();// os + "" + vs;//getOSInfos();//System.Environment.OSVersion.Platform.ToString(); //;
            //}
            //string devicePlatform = getOSInfo();
            //string deviceOSVersion = GetWebBrowserName();
            //DeviceInfo["pluginName"] = pluginName;
            //DeviceInfo["pluginVersion"] = pluginVersion;
            //DeviceInfo["deviceType"] = pluginPlatform;
            //DeviceInfo["deviceModel"] = deviceModel;
            //DeviceInfo["deviceOSVersion"] = devicePlatform;
            //DeviceInfo["clientPlatform"] = deviceOSVersion;
            //testJson.Add("deviceInfo", JsonConvert.SerializeObject(DeviceInfo));








            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonTokenization["terminalId"] = Hosted;
            JsonTokenization["password"] = password;
            JsonTokenization["signature"] = strHash;
            JsonTokenization["paymentType"] = Action;
            JsonTokenization["amount"] = amount;
            JsonTokenization["currency"] = Currency;

            JObject order = new JObject();
            order["orderId"] = transid;
            order["description"] = "Purchase of product XYZ";
            JsonTokenization["order"] = order;

            JObject tokenization = new JObject();
            tokenization["operation"] = Token;
            tokenization["cardToken"] = txtCardtoken;
            JsonTokenization["tokenization"] = tokenization;

            JObject customer = new JObject();
            customer["customerEmail"] = txtcustomerEmail;
            customer["billingAddressCity"] = txtcity;
            customer["billingAddressPostalCode"] = txtzip;
            customer["billingAddressCountry"] = txtCountry;
            JsonTokenization["customer"] = customer;     
                  
            JsonTokenization.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonTokenization["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonTokenization;
    }

    public JObject generateJsonSTCPay(String transid, String sTransId, String txtfirst_name, String txtlast_name, String txtzip, String txtcity, String txtstate, String txtaddress, String txtPhoneno, String txtCountry, String txtcustomerEmail, String Hosted, String password, String amount, String Currency, String Tranid, String STCPayAction, String strHash, String merchantIp, String TransId, String IframeFlag, String txtmetadataone)
    {
        JObject JsonSTCPay = new JObject();

        try
        {
            JObject testJsonSadadDeviceInfo = new JObject();
            string pluginName = "DotNet_Hosted";
            double pluginVersion = 3.0;
            string pluginPlatform = MobileOrDesktop();
            string deviceModel = string.Empty;
            if (pluginPlatform == "Desktop")
            {
                deviceModel = GetDestopOSInfo();
            }
            else
            {
                deviceModel = Dns.GetHostName();
            }
            string devicePlatform = getOSInfo();
            string deviceOSVersion = GetWebBrowserName();
            testJsonSadadDeviceInfo["pluginName"] = pluginName;
            testJsonSadadDeviceInfo["pluginVersion"] = pluginVersion;
            testJsonSadadDeviceInfo["deviceType"] = pluginPlatform;
            testJsonSadadDeviceInfo["deviceModel"] = deviceModel;
            testJsonSadadDeviceInfo["deviceOSVersion"] = devicePlatform;
            testJsonSadadDeviceInfo["clientPlatform"] = deviceOSVersion;

            JsonSTCPay["terminalId"] = Hosted;
            JsonSTCPay["password"] = password;
            JsonSTCPay["paymentType"] = STCPayAction;
            JObject customer = new JObject();

            customer["cardHolderName"] = txtfirst_name + " " + txtlast_name;
            customer["customerEmail"] = txtcustomerEmail;
            customer["billingAddressStreet"] = txtaddress;
            customer["billingAddressCity"] = txtcity;
            customer["billingAddressState"] = txtstate;
            customer["billingAddressPostalCode"] = txtzip;
            customer["billingAddressCountry"] = txtCountry;

            JsonSTCPay["customer"] = customer;
            JsonSTCPay["currency"] = Currency;
            JObject order = new JObject();
            order["orderId"] = transid;
            order["description"] = "Purchase of product XYZ";
            JsonSTCPay["order"] = order;
            JsonSTCPay["referenceId"] = sTransId;
            JsonSTCPay["signature"] = strHash;
            JObject payment = new JObject();
            payment["paymentMethod"] = "STCPAY";
            payment["channelName"] = "STCPAY";
            JsonSTCPay["paymentInstrument"] = payment; 
            JsonSTCPay["country"] = txtCountry;
            JsonSTCPay["amount"] = amount;
            JsonSTCPay["customerIp"] = merchantIp;
            JsonSTCPay["merchantIp"] = merchantIp;
            JsonSTCPay.Add("deviceInfo", JsonConvert.SerializeObject(testJsonSadadDeviceInfo));
            // Handle dynamic additionalDetails
            JObject additionalDetails = new JObject();
            if (!string.IsNullOrEmpty(txtmetadataone))
            {
                if (txtmetadataone.Trim().StartsWith("{") && txtmetadataone.Trim().EndsWith("}"))
                {
                    // Parse txtmetadataone if it's JSON
                    //additionalDetails = JObject.Parse(txtmetadataone);
                    additionalDetails["userData"] = txtmetadataone;
                }
                else
                {
                    // Treat as a plain string
                    additionalDetails["userData"] = txtmetadataone;
                }
            }
            JsonSTCPay["additionalDetails"] = additionalDetails;

        }
        catch (Exception ex)
        {
        }
        return JsonSTCPay;
    }

    #endregion

    #region target url
    //public static String sha256_hash(string value)
    public string GetTargetUrl(string sBaseUrl, string sJson)
    {
        ////string strcontentlength = string.Empty;
        //var response="";
        ////StringBuilder Sb = new StringBuilder();
        //try
        //{
        string finalUrl = string.Empty;

        var http = (HttpWebRequest)WebRequest.Create(new Uri(sBaseUrl));
        http.Accept = "application/json";
        http.ContentType = "application/json";
        http.Method = "POST";
        string parsedContent = sJson.ToString();
        ASCIIEncoding encoding = new ASCIIEncoding();
        Byte[] bytes = encoding.GetBytes(parsedContent);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        Stream newStream = http.GetRequestStream();
        newStream.Write(bytes, 0, bytes.Length);
        newStream.Close();

        var response = http.GetResponse();
        string strcontentlength = Convert.ToString(response);
        if (strcontentlength == null)
        {
            finalUrl = "1";
            //Label1.Text = "Wrong value entered";
        }
        else
        {
            finalUrl = "0";
        }
        var stream = response.GetResponseStream();
        var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();
        WriteErrorToFile("Response Body :  " + content);
        HttpContext.Current.Session["returnContent"] = content;
        //   var contentJson = await SendRequest(request);
        dynamic dvresponse = JsonConvert.DeserializeObject(content);
        
        string strTargetUrl = string.Empty;
        string strpayid = string.Empty;

        HttpContext.Current.Session["ResponseCode"] = dvresponse["responseCode"].Value;

        string url = string.Empty;
        if (dvresponse.ContainsKey("transactionId"))
        {
            if (dvresponse["responseDescription"].Value == "Transaction Approved")
            {
                string linkUrl = dvresponse["paymentLink"]["linkUrl"].ToString();

                if (linkUrl.Contains("?"))
                {
                    url = linkUrl + dvresponse["transactionId"].ToString();
                }
            }
            else
            {
                                
            }           
        }


        //if (dvresponse["targetUrl"].Value != null)
        //{
        //    strTargetUrl = dvresponse["targetUrl"].Value;
        //}

        //if (dvresponse["payid"].Value != null && dvresponse["payid"].Value != "")
        //{
        //    strpayid = dvresponse["payid"].Value;
        //}

        //finalUrl = finalUrl + strTargetUrl + "?paymentid=" + strpayid;

        //finalUrl = finalUrl + strTargetUrl + strpayid;
        finalUrl = url;

        //finalUrl = finalUrl + strTargetUrl + strpayid;
        //}
        //catch (Exception ex)
        //{
        //}
        return finalUrl;
        //return Sb.ToString();
    }
    #endregion

    #region Write Error Log
    public bool WriteErrorToFile(string sText)
    {
        try
        {
            string sFileName = "Error_" + DateTime.Now.Date.ToString("yyyyMMdd") + ".txt";
            //string sMonth = "";
            string executingPath = AppDomain.CurrentDomain.BaseDirectory;
            string sFolder = executingPath + "Logs\\";//"C:\\PG_Log\\";
            string sHeaderMessage = "PGLog " + DateTime.Now.ToString() + Environment.NewLine;

            if (System.IO.Directory.Exists(sFolder) == false)
            {
                System.IO.Directory.CreateDirectory(sFolder);
            }
            if (!System.IO.File.Exists(sFolder + sFileName))
            {
                sText = Environment.NewLine + sHeaderMessage + sText + Environment.NewLine;
            }
            else
            {
                sText = Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + sText + Environment.NewLine;
            }

            StreamWriter str = new StreamWriter(sFolder + sFileName, true);
            str.Write(sText);
            str.Flush();
            str.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    #region generateJsonResponse
    public JObject generateJsonResponse(String trackid, String Hosted, String Action, String merchantIp, String password, String Currency, String transid, String amount, String udf5, String udf3, String udf4, String udf1, String udf2, String strHash)
    {
        JObject testJson = new JObject();
        try
        {
            testJson["trackid"] = trackid;
            testJson["terminalId"] = Hosted;
            testJson["action"] = Action;
            testJson["merchantIp"] = merchantIp;
            testJson["password"] = password;
            testJson["currency"] = Currency;
            testJson["transid"] = transid;
            testJson["amount"] = amount;
            testJson["udf5"] = udf5;
            testJson["udf3"] = udf3;
            testJson["udf4"] = udf4;
            testJson["udf1"] = udf1;
            testJson["udf2"] = udf2;
            testJson["requestHash"] = strHash;
        }

        catch (Exception ex)
        {
        }
        return testJson;
    }
    #endregion

    #region target result and response
    //public static String sha256_hash(string value)
    public string GetTargetResult(string sBaseUrl, string sJson)
    {
        ////string strcontentlength = string.Empty;
        //var response="";
        ////StringBuilder Sb = new StringBuilder();
        //try
        //{
        string finalUrl = string.Empty;

        var http = (HttpWebRequest)WebRequest.Create(new Uri(sBaseUrl));
        http.Accept = "application/json";
        http.ContentType = "application/json";
        http.Method = "POST";
        string parsedContent = sJson.ToString();
        ASCIIEncoding encoding = new ASCIIEncoding();
        Byte[] bytes = encoding.GetBytes(parsedContent);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        Stream newStream = http.GetRequestStream();
        newStream.Write(bytes, 0, bytes.Length);
        newStream.Close();

        var response = http.GetResponse();
        string strcontentlength = Convert.ToString(response);
        if (strcontentlength == null)
        {
            //finalUrl = "1";
            //Label1.Text = "Wrong value entered";
            //}
            //else
            //{
            //    finalUrl = "0";
        }
        var stream = response.GetResponseStream();
        var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();

        WriteErrorToFile("Response Body :  " + content);

        //   var contentJson = await SendRequest(request);
        dynamic dvresponse = JsonConvert.DeserializeObject(content);
        string strTargetUrl = string.Empty;
        string strpayid = string.Empty;

        //if (dvresponse["targetUrl"].Value != null)
        //{
        //    strTargetUrl = dvresponse["targetUrl"].Value;
        //}

        //if (dvresponse["payid"].Value != null && dvresponse["payid"].Value != "")
        //{
        //    strpayid = dvresponse["payid"].Value;
        //}
        string strResult = string.Empty;
        string strresponseCode = string.Empty;
        if (dvresponse["result"].Value != null)
        {
            strResult = dvresponse["result"].Value;
        }

        if (dvresponse["responseCode"].Value != null)
        {
            strresponseCode = dvresponse["responseCode"].Value;
        }
        finalUrl = strResult + "?response=" + strresponseCode;
        //}
        //catch (Exception ex)
        //{
        //}
        return finalUrl;
        //return Sb.ToString();
    }
    #endregion

    #region readFile
    public string readFile(string sResponse)
    {
        string finalUrl = string.Empty;
        var data = new Dictionary<string, string>();
        string sFolder = System.Web.HttpContext.Current.Server.MapPath(@"~/responseCodeMapping.properties");
        string sResponsevalue = string.Empty;
        //string sPath = "~/responseCodeMapping.properties";
        //D:\Projects\PaymentGateway\PaymentGateway\responseCodeMapping.properties
        foreach (var row in File.ReadAllLines(sFolder))
             data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
        //Console.WriteLine(data["ServerName"]);
        //finalUrl = Convert.ToString(data[0]);
        //return finalUrl;
        foreach (var item in data)
        {
            if (item.Key == sResponse)
            {
                sResponsevalue = item.Value;
                return sResponsevalue;
            }
        }
        return "0";
        ////if (data["ServerName"])
        //{

        //}
    }
    #endregion

    public String getOSInfo()
    {
        //System.OperatingSystem os = System.Environment.OSVersion;
        var ua = HttpContext.Current.Request.UserAgent;
        //var ua = os.UserAgent;

        if (ua.Contains("Android"))
            return string.Format("Android {0}", GetMobileVersion(ua, "Android"));

        if (ua.Contains("iPad"))
            return string.Format("iPad OS {0}", GetMobileVersion(ua, "OS"));

        if (ua.Contains("iPhone"))
            return string.Format("iPhone OS {0}", GetMobileVersion(ua, "OS"));

        if (ua.Contains("Linux") && ua.Contains("KFAPWI"))
            return "Kindle Fire";

        if (ua.Contains("RIM Tablet") || (ua.Contains("BB") && ua.Contains("Mobile")))
            return "Black Berry";

        if (ua.Contains("Windows Phone"))
            return string.Format("Windows Phone {0}", GetMobileVersion(ua, "Windows Phone"));

        if (ua.Contains("Mac OS"))
            return "Mac OS";

        if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
            return "Windows XP";

        if (ua.Contains("Windows NT 6.0"))
            return "Windows Vista";

        if (ua.Contains("Windows NT 6.1"))
            return "Windows 7";

        if (ua.Contains("Windows NT 6.2"))
            return "Windows 8";

        if (ua.Contains("Windows NT 6.3"))
            return "Windows 8.1";

        if (ua.Contains("Windows NT 10"))
            return "Windows 10";

        //fallback to basic platform:
        return (ua.Contains("Mobile") ? " Mobile " : "");
    }
    public String GetMobileVersion(string userAgent, string device)
    {
        var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
        var version = string.Empty;

        foreach (var character in temp)
        {
            var validCharacter = false;
            int test = 0;

            if (Int32.TryParse(character.ToString(), out test))
            {
                version += character;
                validCharacter = true;
            }

            if (character == '.' || character == '_')
            {
                version += '.';
                validCharacter = true;
            }

            if (validCharacter == false)
                break;
        }

        return version;
    }
    #region Get WebBrowser Name
    public string GetWebBrowserName()
    {
        string WebBrowserName = string.Empty;
        try
        {
            WebBrowserName = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
        return WebBrowserName;
    }
    #endregion

    #region MobileOrDesktop
    public bool isMobileBrowser()
    {
        //GETS THE CURRENT USER CONTEXT    
        HttpContext context = HttpContext.Current;

        //FIRST TRY BUILT IN ASP.NT CHECK    
        if (context.Request.Browser.IsMobileDevice)
        {
            return true;
        }
        //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER    
        if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
        {
            return true;
        }
        //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP    
        if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
            context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
        {
            return true;
        }
        //AND FINALLY CHECK THE HTTP_USER_AGENT     
        //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING    
        if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
        {
            //Create a list of all mobile types    
            string[] mobiles =
                new[]    
        {    
            "midp", "j2me", "avant", "docomo",     
            "novarra", "palmos", "palmsource",     
            "240x320", "opwv", "chtml",    
            "pda", "windows ce", "mmp/",     
            "blackberry", "mib/", "symbian",     
            "wireless", "nokia", "hand", "mobi",    
            "phone", "cdm", "up.b", "audio",     
            "SIE-", "SEC-", "samsung", "HTC",     
            "mot-", "mitsu", "sagem", "sony"    
            , "alcatel", "lg", "eric", "vx",     
            "NEC", "philips", "mmm", "xx",     
            "panasonic", "sharp", "wap", "sch",    
            "rover", "pocket", "benq", "java",     
            "pt", "pg", "vox", "amoi",     
            "bird", "compal", "kg", "voda",    
            "sany", "kdd", "dbt", "sendo",     
            "sgh", "gradi", "jb", "dddi",     
            "moto", "iphone"    
        };

            //Loop through each item in the list created above     
            //and check if the header contains that text    
            foreach (string s in mobiles)
            {
                if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                    ToLower().Contains(s.ToLower()))
                {
                    return true;
                }
             }
        }

        return false;
    }

    public string MobileOrDesktop()
    {
        string WebBrowserName = string.Empty;
        if (isMobileBrowser() == true)
        {
            //code for mobile browser   
            return "Mobile";
        }
        else
        {
            //code for desktop browser   
            return "Desktop";
        }
        return WebBrowserName;
    }
    #endregion

    public string getOSInfos()
    {
        //Get Operating system information.
        OperatingSystem os = Environment.OSVersion;
        //Get version information about the os.
        Version vs = os.Version;

        //Variable to hold our return value
        string operatingSystem = "";

        if (os.Platform == PlatformID.Win32Windows)
        {
            //This is a pre-NT version of Windows
            switch (vs.Minor)
            {
                case 0:
                    operatingSystem = "95";
                    break;
                case 10:
                    if (vs.Revision.ToString() == "2222A")
                        operatingSystem = "98SE";
                    else
                        operatingSystem = "98";
                    break;
                case 90:
                    operatingSystem = "Me";
                    break;
                default:
                    break;
            }
        }
        else if (os.Platform == PlatformID.Win32NT)
        {
            switch (vs.Major)
            {
                case 3:
                    operatingSystem = "NT 3.51";
                    break;
                case 4:
                    operatingSystem = "NT 4.0";
                    break;
                case 5:
                    if (vs.Minor == 0)
                        operatingSystem = "2000";
                    else
                        operatingSystem = "XP";
                    break;
                case 6:
                    if (vs.Minor == 0)
                        operatingSystem = "Vista";
                    else if (vs.Minor == 1)
                        operatingSystem = "7";
                    else if (vs.Minor == 2)
                        operatingSystem = "8";
                    else
                        operatingSystem = "8.1";
                    break;
                case 10:
                    operatingSystem = "10";
                    break;
                default:
                    break;
            }
        }
        //Make sure we actually got something in our OS check
        //We don't want to just return " Service Pack 2" or " 32-bit"
        //That information is useless without the OS version.
        //if (operatingSystem != "")
        //{
        //    //Got something.  Let's prepend "Windows" and get more info.
        //    operatingSystem = "Windows " + operatingSystem;
        //    //See if there's a service pack installed.
        //    if (os.ServicePack != "")
        //    {
        //        //Append it to the OS name.  i.e. "Windows XP Service Pack 3"
        //        operatingSystem += " " + os.ServicePack;
        //    }
        //    //Append the OS architecture.  i.e. "Windows XP Service Pack 3 32-bit"
        //    //operatingSystem += " " + getOSArchitecture().ToString() + "-bit";
        //}
        //Return the information we've gathered.
        return operatingSystem;
    }

    public static string GetDestopOSInfo()
    {
        // Get the operating system information
        OperatingSystem os = Environment.OSVersion;

        // Get the version information
        Version version = os.Version;

        // Get the service pack version (if available)
        string servicePack = os.ServicePack;
     
        // Get the operating system platform
        PlatformID platform = os.Platform;

        // Build the OS information string
        string osInfo = os.VersionString ;

        if (!string.IsNullOrEmpty(servicePack))
        {
            osInfo += servicePack;
        }

        //osInfo += Environment.NewLine;

        osInfo += platform;

        return osInfo;
    }

    private string GetDetails()
    {
        string userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        //string userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
        userAgent = "Mozilla/5.0 (Linux; Android 6.0; Lenovo A7010a48 Build/MRA58K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.109 Mobile Safari/537.36";
        System.Text.RegularExpressions.Regex OS = new System.Text.RegularExpressions.Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
        System.Text.RegularExpressions.Regex device = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
        string device_info = string.Empty;
        string os = string.Empty;
        string version = string.Empty;
        string modelName = string.Empty; ;
        string modelNo = string.Empty; ;
        if (OS.IsMatch(userAgent))
        {
            //device_info = OS.Match(userAgent).Groups[0].Value;
            device_info = OS.Match(userAgent).Groups[0].Value + "$$" + OS.Match(userAgent).Groups[1].Value;
            os = device_info.Split(';')[0].Trim().Split(' ')[0];
            version = device_info.Split(';')[0];
            modelName = device_info.Split(';')[1].Trim().Split(' ')[0];
            modelNo = device_info.Split(';')[1].Trim().Split(' ')[1];
        }
        if (device.IsMatch(userAgent.Substring(0, 4)))
        {
            //device_info += device.Match(userAgent).Groups[0].Value;
            device_info = device.Match(userAgent).Groups[0].ToString()
                        + "$$" + device.Match(userAgent).Groups[1].ToString()
                        + "$$" + device.Match(userAgent).Groups[2].ToString()
                        + "$$" + device.Match(userAgent).Groups[3].ToString()
                        + "$$" + device.Match(userAgent).Groups[4].ToString();
        }
        if (!string.IsNullOrEmpty(device_info))
        {
            var test = os;
            var te22st = version;
            var tesqqt = modelName;
            var testaaq = modelNo;
        }
        //return device_info;
        return modelName;
    }
}


