using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IdentityModel.Metadata;
using System.Xml;
using System.Security.Permissions;

public partial class Response : System.Web.UI.Page
{
    operation op = new operation();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnlpaymentsuccessinfo.Visible = false;
            pnlpaymentunsuccessinfo.Visible = false;
            string strpipeSeperatedString = null;
            string strPaymentId = string.Empty;
            string strTranId = string.Empty;
            string strECI = string.Empty;
            string strResult = string.Empty;
            string strTrackId = string.Empty;
            string strResponseCode = string.Empty;
            string strAuthCode = string.Empty;
            string strRRN = string.Empty;
            string stramount = string.Empty;
            string strresponseHash = string.Empty;

            if (Request.QueryString["PaymentId"] != null)
            {
                strPaymentId = Request.QueryString["PaymentId"];
            }

            if (Request.QueryString["TranId"] != null)
            {
                strTranId = Request.QueryString["TranId"];
            }

            if (Request.QueryString["ECI"] != null)
            {
                strECI = Request.QueryString["ECI"];
            }

            if (Request.QueryString["Result"] != null)
            {
                strResult = Request.QueryString["Result"];
            }

            if (Request.QueryString["TrackId"] != null)
            {
                strTrackId = Request.QueryString["TrackId"];
            }

            if (Request.QueryString["ResponseCode"] != null)
            {
                strResponseCode = Request.QueryString["ResponseCode"];
            }

            if (Request.QueryString["AuthCode"] != null)
            {
                strAuthCode = Request.QueryString["AuthCode"];
            }

            if (Request.QueryString["RRN"] != null)
            {
                strRRN = Request.QueryString["RRN"];
            }

            if (Request.QueryString["amount"] != null)
            {
                stramount = Request.QueryString["amount"];
            }

            if (Request.QueryString["responseHash"] != null)
            {
                strresponseHash = Request.QueryString["responseHash"];
            }

            XmlDocument XmlDocObj = new XmlDocument();
            XmlDocObj.Load(Server.MapPath("~/Key.xml"));
            String secret = XmlDocObj.SelectSingleNode("Application/log/secret").InnerText;
            strpipeSeperatedString = strTranId + "|" + secret + "|" + strResponseCode + "|" + stramount; //+ "|" + secret + "|" + txtAmount.Text + "|" + txtCurrency.Text;

            string strHash = op.sha256_hash(strpipeSeperatedString);
            if (strresponseHash == strHash)
            {
                getresponse();
            }
            else
            {
                pnlpaymentsuccessinfo.Visible = false;
                pnlpaymentunsuccessinfo.Visible = false;

                Label1.Visible = true;
                //Label1.Text = "Something went Wrong or Data Tampered";
                Label1.Text = "Hash Mismatch !!!!";
            }
        }
        catch (Exception Ex)
        {
            op.WriteErrorToFile("Page_Load: " + Ex.Message);
        }
    } 

    public void getresponse()
    {
        //get configuration data
        XmlDocument XmlDocObj = new XmlDocument();
        XmlDocObj.Load(Server.MapPath("~/Key.xml"));

        String Hosted = (XmlDocObj.SelectSingleNode("Application/log/Hosted").InnerText);
        String password = XmlDocObj.SelectSingleNode("Application/log/password").InnerText;
        String secret = XmlDocObj.SelectSingleNode("Application/log/secret").InnerText;
        String trackid = XmlDocObj.SelectSingleNode("Application/log/trackid").InnerText;
        String currency = XmlDocObj.SelectSingleNode("Application/log/currency").InnerText;
         
          string strpipeSeperatedString = null;
          string strTranId = string.Empty;
          string stramount = string.Empty;
          string strTrackId = string.Empty;
          string strPaymentId = string.Empty;
          if (Request.QueryString["TranId"] != null)
          {
              strTranId = Request.QueryString["TranId"];
          }
          if (Request.QueryString["amount"] != null)
          {
              stramount = Request.QueryString["amount"];
          }
          if (Request.QueryString["PaymentId"] != null)
          {
              strPaymentId = Request.QueryString["PaymentId"];
          }
          if (Request.QueryString["TrackId"] != null)
          {
              strTrackId = Request.QueryString["TrackId"];
          }
        
           //strpipeSeperatedString = strTrackId + "|" + Hosted + "|" + password + "|" + secret + "|" + stramount + "|" + "INR";
          strpipeSeperatedString = strTrackId + "|" + Hosted + "|" + password + "|" + secret + "|" + stramount + "|" + currency;
        
          string strHash = op.sha256_hash(strpipeSeperatedString);
          JObject generatedJson = op.generateJsonResponse(strTrackId, Hosted, "10", "10.10.10.101", password, currency, strTranId, Request.QueryString["amount"], "", "", "", "", "", strHash);
          string parsedContent = generatedJson.ToString();
          var baseAddress = (XmlDocObj.SelectSingleNode("Application/log/url").InnerText);
          //get final URL and payID
          string sfinalUrl = op.GetTargetResult(baseAddress, parsedContent);
       
          string strResult = string.Empty;
          string strresponseCode = string.Empty;
       
          //get final Result and ResponseCode
          string strTargetUrl = string.Empty;
          string strpayid = string.Empty;
          string[] sfinalUrlBreak = sfinalUrl.Split(new string[] { "?response=" }, StringSplitOptions.None);
          strResult = sfinalUrlBreak[0];
          if (strResult == "Successful")
          {
              strResult = "SUCCESS";
          }
          //strResult = "SUCCESS"; ///aSk Sagar  result ==successful
          strresponseCode = sfinalUrlBreak[1];
        if (Request.QueryString["Result"] == strResult && Request.QueryString["ResponseCode"] == strresponseCode)
        {
            txtPaymentId.Text = strPaymentId;
            txtTrackId.Text = strTrackId;
            txtResult.Text = strResult;
            txtamount.Text = stramount;
            
            pnlpaymentsuccessinfo.Visible = true;
            dvpayment.InnerText = op.readFile(strresponseCode);
            //dvpayment.InnerText = "Transaction is " + " " + strResult;
        }
        else
        {
            pnlpaymentsuccessinfo.Visible = false;
            pnlpaymentunsuccessinfo.Visible = false;

            Label1.Visible = true;
            Label1.Text = "Data Tampar !!!!";
        } 
    } 
}