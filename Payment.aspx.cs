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
using Microsoft.Win32;



public partial class Payment : System.Web.UI.Page
{
    operation op = new operation();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Application["HasExecuted"] == null || (bool)Application["HasExecuted"] == false)
            {
                // Run the method only once globally
                string frameworkVersion = GetDotNetFrameworkVersion();
                op.WriteErrorToFile("Expected DotNet Framework Version 4.5 | Found Dot Net Version " + frameworkVersion);

                // Set the application variable to indicate the method has been executed
                Application["HasExecuted"] = true;
            }
            if (!IsPostBack)
            {
                txtSmslanguageSendLinkMode.Visible = false;
                txtTranid.Text = op.GenerateRandom();
                XmlDocument XmlDocObj = new XmlDocument();
                XmlDocObj.Load(Server.MapPath("~/Key.xml"));
                txtCurrency.Text = (XmlDocObj.SelectSingleNode("Application/log/currency").InnerText);
                txtmetadataone.Text = (XmlDocObj.SelectSingleNode("Application/log/metadata").InnerText);
                Session["Count"] = 1;
                Session["finalUrl"] = null;                
            }
            if (txtTranid.Text == null)
            {
                txtTranid.Text = op.GenerateRandom();
                //Session["Count"] = 1;
            }            
        }
        catch (Exception Ex)
        {
            op.WriteErrorToFile("Page_Load: " + Ex.Message);
        }
    }

    #region btnsubmit_Click
    protected void btnsubmit_Click(object sender, EventArgs e)   
    {
        ddlSmslanguage.Enabled = true;
        //RegSmslanguage.Enabled = true;
        string strpipeSeperatedString = null;
        string sfinalUrl = string.Empty;
        //int? SadadAction = null;
        //bool SadadChecked = CheckboxSadad.Checked;
        //if (SadadChecked == true)
        //{
        //    SadadAction = 1;
        //}
        //else
        //{
        //    SadadAction = 0;
        //}
        //op.readFile("101");
        //string sResponsevalue = op.readFile();
        int iCount = 0;
        //get merchant server Ip address
        //Session["Count"] = 1;
        try
        {            
            string striframe = txtUDF5.Text.ToUpper().Trim();
            if (Session["Count"] != null)
            {
                iCount = Convert.ToInt16(Session["Count"]);
            }
            if (iCount == 1)
            {                
                //formSubmit();
                string merchantIp = op.GetLocalIPAddress();//GetIPAddress();
                //try
                //{
                //get configuration parameter 
                XmlDocument XmlDocObj = new XmlDocument();
                XmlDocObj.Load(Server.MapPath("~/Key.xml"));

                String Hosted = (XmlDocObj.SelectSingleNode("Application/log/Hosted").InnerText);
                String password = (XmlDocObj.SelectSingleNode("Application/log/password").InnerText);
                String secret = (XmlDocObj.SelectSingleNode("Application/log/secret").InnerText);
                String trackid = (XmlDocObj.SelectSingleNode("Application/log/trackid").InnerText);

                strpipeSeperatedString = txtTranid.Text + "|" + Hosted + "|" + password + "|" + secret + "|" + txtAmount.Text + "|" + txtCurrency.Text;
                //generate hash value using sha256_hash 
                string strHash = op.sha256_hash(strpipeSeperatedString);
                string sAction = ddlAction.SelectedItem.Value;
                string STCPayAction = string.Empty;
                if (sAction == "13")
                {
                    string OriginalValue = "13";
                    int STCPay = 1;
                    STCPayAction = OriginalValue.Replace(sAction.ToString(), STCPay.ToString());
                }
                string Token = ddltoken.SelectedItem.Value;
                string sTransId = txtTransId.Text;
                string smsLanguage = ddlSmslanguage.SelectedItem.Text;
                JObject generatedJson = null;
                //if (iCount == 1)
                //{
                //create json object
                if (sAction == "17")
                {
                    generatedJson = op.generateJsonSadad(trackid, smsLanguage, txtSendlinkmode.Text, txtCountry.Text, txtfirst_name.Text, txtlast_name.Text, txtaddress.Text, txtcity.Text, txtstate.Text, txtzip.Text, txtPhoneno.Text, txtcustomerEmail.Text, txtTranid.Text, Hosted, password, secret, txtAmount.Text, txtCurrency.Text, sAction, strHash, merchantIp, txtTranid.Text,
                        txtSadadBillNumber.Text, txtCustomerFullName.Text, txtCustomerIdNumber.Text, txtCustomerIdType.Text, txtCustomerEmailAddress.Text, txtCustomerMobileNumber.Text, txtCustomerPreviousBalance.Text, txtCustomerTaxNumber.Text, txtExpireDate.Text, txtServiceName.Text, txtBuildingNumber.Text,
                        txtCityCode.Text, txtDistrictCode.Text, txtPostalCode.Text, txtStreetName.Text, txtname.Text, txtQuantity.Text, txtDiscount.Text, txtDiscountType.Text, txtVat.Text, txtUnitPrice.Text, txtIsPartialAllowed.Text, txtMiniPartialAmount.Text, txtmetadataone.Text);
                }
                else if (sAction == "1" || sAction == "4")
                {
                    generatedJson = op.generateJsonPurchasePreAuthorization(sTransId, Token, txtcardtoken.Text, txtCountry.Text, txtfirst_name.Text, txtlast_name.Text, txtaddress.Text, txtcity.Text, txtstate.Text, txtzip.Text, txtPhoneno.Text, txtcustomerEmail.Text, txtTranid.Text, Hosted, password, secret, txtAmount.Text, txtCurrency.Text, sAction, strHash, merchantIp, txtTranid.Text, striframe, txtmetadataone.Text);
                }
                else if (sAction == "3")
                {
                    generatedJson = op.generateJsonVoidPurchase(sTransId, txtCountry.Text, txtcustomerEmail.Text, Hosted, password, txtAmount.Text, txtCurrency.Text, txtTranid.Text, sAction, strHash, merchantIp, sTransId, striframe, txtmetadataone.Text);
                }
                else if (sAction == "6")
                {
                    generatedJson = op.generateJsonVoidRefund(sTransId, txtCountry.Text, txtcustomerEmail.Text, Hosted, password, txtAmount.Text, txtCurrency.Text, txtTranid.Text, sAction, strHash, merchantIp, sTransId, striframe, txtmetadataone.Text);
                }
                else if (sAction == "7")
                {
                    generatedJson = op.generateJsonVoidCapture(sTransId, txtCountry.Text, txtcustomerEmail.Text, Hosted, password, txtAmount.Text, txtCurrency.Text, txtTranid.Text, sAction, strHash, merchantIp, sTransId, striframe, txtmetadataone.Text);
                }
                else if (sAction == "10")
                {
                    generatedJson = op.generateJsonTransactionEnquiry(sTransId, txtCountry.Text, txtcustomerEmail.Text, Hosted, password, txtAmount.Text, txtCurrency.Text, txtTranid.Text, sAction, strHash, merchantIp, sTransId, striframe, txtmetadataone.Text);
                }
                else if (sAction == "12")
                {
                    generatedJson = op.generateJsonTokenization(txtTranid.Text, txtcardtoken.Text, txtzip.Text, txtaddress.Text, txtcity.Text, txtstate.Text, txtcustomerEmail.Text, txtCustomerMobileNumber.Text, Token, txtCountry.Text, Hosted, password, txtAmount.Text, txtCurrency.Text, txtTranid.Text, sAction, strHash, merchantIp, sTransId, striframe, txtmetadataone.Text);
                }
                else if (STCPayAction == "1")
                {
                    generatedJson = op.generateJsonSTCPay(txtTranid.Text, sTransId, txtfirst_name.Text, txtlast_name.Text, txtzip.Text, txtcity.Text, txtstate.Text, txtaddress.Text, txtCustomerMobileNumber.Text, txtCountry.Text, txtcustomerEmail.Text, Hosted, password, txtAmount.Text, txtCurrency.Text, txtTranid.Text, STCPayAction, strHash, merchantIp, sTransId, striframe, txtmetadataone.Text);
                }
                else if (sAction == "5" || sAction == "9" || sAction == "2")
                {
                    generatedJson = op.generateJsonCaptureRefundVoid(sTransId, Token, txtcardtoken.Text, txtCountry.Text, txtfirst_name.Text, txtlast_name.Text, txtaddress.Text, txtcity.Text, txtstate.Text, txtzip.Text, txtPhoneno.Text, txtcustomerEmail.Text, txtTranid.Text, Hosted, password, secret, txtAmount.Text, txtCurrency.Text, sAction, strHash, merchantIp, txtTranid.Text, sTransId, txtmetadataone.Text);
                }
                //JObject generatedJson = op.generateJson(txtCountry.Text, txtfirst_name.Text, txtlast_name.Text, txtaddress.Text, txtcity.Text, txtstate.Text, txtzip.Text, txtPhoneno.Text, txtcustomerEmail.Text, txtTranid.Text, Hosted, password, secret, txtAmount.Text, txtCurrency.Text, "1", strHash, merchantIp, txtTranid.Text);
                string parsedContent = generatedJson.ToString();
                var tetes = JsonConvert.SerializeObject(JObject.Parse(parsedContent));
                op.WriteErrorToFile("Request : " + tetes);



                //var index = parsedContent.IndexOf('{', parsedContent.IndexOf('{') + 1);
                //var index2 = parsedContent.IndexOf('}', parsedContent.IndexOf('}') + 1);
                //if (sAction == "17")
                //{
                //    string ouput = string.Concat(parsedContent.Substring(0, index), "[{", parsedContent.Substring(index + 1));
                //    string ouput2 = string.Concat(ouput.Substring(0, index2), "}]", ouput.Substring(index2 + 2));

                //    parsedContent = ouput2;
                //    op.WriteErrorToFile("Sadad Request : " + parsedContent);
                //}
                Session["Count"] = 0;

                var baseAddress = (XmlDocObj.SelectSingleNode("Application/log/url").InnerText);

                //get final URL and payID
                sfinalUrl = op.GetTargetUrl(baseAddress, parsedContent);
                Session["finalUrl"] = sfinalUrl;

            }
            if (iCount == 0)
            {
                sfinalUrl = Session["finalUrl"] as string;
            }
            //string strcontentlength = sfinalUrl.Substring(0, 1);
            //sfinalUrl = sfinalUrl.Substring(1, sfinalUrl.Length - 1);
            //if (strcontentlength == "1")
            //{
            //    Label1.Text = "Wrong value entered";
            //}
           

            //get final URL and payID
            string strTargetUrl = string.Empty;
            string strpayid = string.Empty;
            //string[] sfinalUrlBreak = sfinalUrl.Split(new string[] { "?paymentid=" }, StringSplitOptions.None);
            string[] sfinalUrlBreak = sfinalUrl.Split(new string[] { "" }, StringSplitOptions.None);
            strTargetUrl = sfinalUrlBreak[0];
            //strpayid = sfinalUrlBreak[1];

            //string sResponseCode = Session["ResponseCode"] as string;
            //if (sResponseCode != null)
            //{
            //    //Response.Redirect("Receipt.aspx");
            //    Label1.Text = op.readFile(sResponseCode);
            //    //Label1.Text = "The operation has timed out";
            //}
            //if (Convert.ToInt16(SadadAction) == 1)
            //{
            //    var content = Session["returnContent"];
            //    dynamic dvresponse = JsonConvert.DeserializeObject(content.ToString());
            //    Label1.Text = "Result : " + Convert.ToString(dvresponse["result"].Value) + "; Response Code :" + Convert.ToString(dvresponse["responseCode"].Value)
            //        + "; Sadad Number :" + Convert.ToString(dvresponse["sadadNumber"].Value) + "; Bill Number :" + Convert.ToString(dvresponse["billNumber"].Value) + ";";
               
            //}

            if (sfinalUrl != null && sfinalUrl != "")
            {
                if (strTargetUrl != null && strTargetUrl != "")
                {
                    StringBuilder sb = new StringBuilder();
                    if (striframe != "IFRAME")
                    {
                        sb.Append("<html>");
                        sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                        sb.AppendFormat(@"<form name='form' id='payment-form' action='{0}' method='post'>", strTargetUrl);
                        //sb.AppendFormat(@"<input type='hidden' name='paymentid' value='{0}'>", strpayid);
                        sb.AppendFormat(@"<input type='hidden' name='paymentid'>");
                        sb.Append("</form>");
                        sb.Append("</body>");
                        sb.Append("</html>");
                        Response.Write(sb.ToString());

                        StringBuilder strScript = new StringBuilder();
                        strScript.Append("<script language='javascript'>");
                        strScript.Append("var ID= document.getElementById('payment-form');");
                        strScript.Append("ID.submit();");
                        strScript.Append("</script>");
                    }
                    else
                    {
                        sb.Append("<html>");
                        sb.AppendFormat(@"<body>");
                        sb.AppendFormat(@"<center><iframe id='concerto_checkout_iframe'  >");
                        sb.AppendFormat(@"<input type='hidden' name='paymentId' value='{0}'>", strpayid);
                        sb.Append("</iframe></center>");
                        sb.Append("</body>");
                        sb.Append("</html>");
                        sb.AppendFormat("<script>");
                        sb.AppendFormat("var frame = document.getElementById('concerto_checkout_iframe');");
                        sb.AppendFormat("frame.setAttribute('src', '" + sfinalUrl + "');");
                        sb.AppendFormat("frame.setAttribute('scrolling', 'no');");
                        sb.AppendFormat("frame.setAttribute('title', 'Payment Gateway');");
                        sb.AppendFormat("frame.setAttribute('width', '500');");
                        sb.AppendFormat("frame.setAttribute('height', '360');");
                        sb.AppendFormat("frame.setAttribute('style', 'border:1px solid #0000;background:white;  margin-top:2%;');");
                        sb.Append("</script>");
                        Response.Write(sb.ToString());
                    }
                    Response.End();
                }
            }

            string sResponseCode = Session["ResponseCode"] as string;
            if (sResponseCode != null)
            {
                Response.Redirect("Receipt.aspx");
                //Label1.Text = op.readFile(sResponseCode);
                //Label1.Text = "The operation has timed out";
            }
            //if (sAction == "17")
            //{
            //    var content = Session["returnContent"];
            //    dynamic dvresponse = JsonConvert.DeserializeObject(content.ToString());
            //    Label1.Text = "Result : " + Convert.ToString(dvresponse["result"].Value) + "; Response Code :" + Convert.ToString(dvresponse["responseCode"].Value)
            //        + "; Sadad Number :" + Convert.ToString(dvresponse["sadadNumber"].Value) + "; Bill Number :" + Convert.ToString(dvresponse["billNumber"].Value) + ";";

            //}
            ////}
            //string sResponseCode = Session["ResponseCode"] as string;
            //if (sResponseCode !=null)
            //{
            //    Label1.Text = op.readFile(sResponseCode);
            //    //Label1.Text = "The operation has timed out";
            //}

            //}
        }
        catch (Exception Ex)
        {
            Label1.Text = "The operation has timed out";
            op.WriteErrorToFile("btnsubmit_Click: " + Ex.Message);
        }
    }

    protected void ddlTransaction_Change(object sender, EventArgs e)
    {
        //stuff that never gets hit
        if (ddlAction.SelectedItem.Value == "17")
        {
            divSadadPaymentDetails.Visible = true;
            ddlSmslanguage.Enabled = true;
            RqfvSendlinkmode.Enabled = true;
            Smslan.Style.Add("margin", "0px 0px 0px 0px");
            Sendlink.Style.Add("margin", "0px 0px 0px 0px");
            txtSmslanguageSendLinkMode.Visible = true;
            //ddlAction.Enabled = false;
        }
        else
        {
            //ddlAction.Enabled = true;
            divSadadPaymentDetails.Visible = false;
            Smslan.Style.Add("margin", "20px 0px 20px 0px");
            Sendlink.Style.Add("margin", "20px 0px 20px 0px");
            txtSmslanguageSendLinkMode.Visible = false;
        }
    }

    #endregion
    //protected void chkBox_CheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox chk = sender as CheckBox;
    //    if (chk.Checked)
    //    {
    //        //div1.Style.Add(HtmlTextWriterStyle.Display, "block");
    //        divSadadPaymentDetails.Visible = true;
    //        ddlSmslanguage.Enabled = true;
    //        RqfvSendlinkmode.Enabled = true;
    //        Smslan.Style.Add("margin", "0px 0px 0px 0px");
    //        Sendlink.Style.Add("margin", "0px 0px 0px 0px");
    //        txtSmslanguageSendLinkMode.Visible = true;
    //        ddlAction.Enabled = false;
    //    }
    //    else
    //    {
    //        ddlAction.Enabled = true;
    //        divSadadPaymentDetails.Visible = false;
    //        Smslan.Style.Add("margin", "20px 0px 20px 0px");
    //        Sendlink.Style.Add("margin", "20px 0px 20px 0px");
    //        txtSmslanguageSendLinkMode.Visible = false;
    //    }
    //}

    public static string GetDotNetFrameworkVersion()
    {
        string version = string.Empty;
        string registryKey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";

        try
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    object versionValue = key.GetValue("Version");
                    if (versionValue != null)
                    {
                        version = versionValue.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error reading registry: " + ex.Message);            
        }

        return version;
    }
}

