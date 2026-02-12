<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        .required-field {
            color: red;
        }
    </style>
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />

    <%--<script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js">--%>
    <%--</script>--%>
    <%--<script src="//code.jquery.com/jquery-1.11.1.min.js">--%>
    <%--    </script>--%>
</head>
<body>
    <form id="form1" runat="server" onsubmit='return sendRequest();' enctype="multipart/form-data" method="post">
        <%--<form id="form1" runat="server" method="post">--%>
        <div class="container wrapper">
            <div class="row cart-head">
                <div class="container">
                    <div class="row">
                        <p></p>
                    </div>

                    <div class="row">
                        <p></p>
                    </div>
                </div>
            </div>
            <div class="row cart-body">
                <%-- <asp:HiddenField ID="hfStatus" ClientIDMode="Static" runat="server"/>--%>
                <%-- <form class="form-horizontal" method="post" action="../php_plugin/payment.php">--%>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 col-md-push-6 col-sm-push-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">Order</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <h4>Order Details</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Order ID</strong></div>
                                <div class="col-md-12" style="margin-bottom: 10px;">
                                    <%-- <input type="text" class="form-control" name="tranid" value="<?php echo rand(); ?>" />--%>
                                    <asp:TextBox ID="txtTranid" runat="server" ReadOnly="false" class="form-control">   </asp:TextBox>
                                </div>
                                <%--<div class="span1"></div>--%>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Transaction ID(For refund/capture/void)</strong></div>
                                <div class="col-md-12">
                                    <%-- <input type="text" class="form-control" name="tranid" value="<?php echo rand(); ?>" />--%>
                                    <asp:TextBox ID="txtTransId" runat="server" class="form-control">   </asp:TextBox>
                                    <h id="transIdError" style="color: red;" class="errorElements"></h>
                                </div>
                                <%--<div class="span1"></div>--%>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <strong>Currency:</strong>
                                    <%--<input type="text" name="currency" class="form-control" value="INR" />--%>
                                    <asp:TextBox ID="txtCurrency" runat="server" class="form-control" value="INR"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rqfvCurrency"  ForeColor="Red" runat="server" ControlToValidate="txtCurrency" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="span1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <strong>Amount:</strong>
                                    <%-- <input type="text" name="amount" class="form-control" value="1.00" />--%>
                                    <asp:TextBox ID="txtAmount" runat="server" class="form-control" value="1.00"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqfvAmount" ForeColor="Red" runat="server" ControlToValidate="txtAmount" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span1"></div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <strong>Action:</strong>
                                    <asp:DropDownList ID="ddlAction" CssClass="form-control" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddlTransaction_Change">
                                        <asp:ListItem Value="1">Purchase</asp:ListItem>
                                        <asp:ListItem Value="4">Pre-Authorization</asp:ListItem>
                                        <asp:ListItem Value="5">Capture Transaction</asp:ListItem>
                                        <asp:ListItem Value="9">Void Authorization</asp:ListItem>
                                        <asp:ListItem Value="2">Refund Transaction</asp:ListItem>

                                        <asp:ListItem Value="3">Void Purchase</asp:ListItem>  
                                        <asp:ListItem Value="6">Void Refund</asp:ListItem>
                                        <asp:ListItem Value="7">Void Capture</asp:ListItem>
                                        <asp:ListItem Value="10">Transaction Enquiry</asp:ListItem>
                                        <asp:ListItem Value="12">Tokenization</asp:ListItem>                                                                                                                     
                                        <asp:ListItem Value="13">STC Pay</asp:ListItem>
                                        <asp:ListItem Value="17">Sadad</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <%--<div class="col-md-6 col-xs-12" style="margin-top: 25px">
                                    <asp:CheckBox ID="CheckboxSadad" value="1" runat="server" Text="Sadad" OnCheckedChanged="chkBox_CheckedChanged" AutoPostBack="true"/> 
                                </div>
                                <div class="span1"></div>--%>
                            </div>
                            <br />
                            <div class="form-group">                                
                                <div class="col-md-12"><br />
                                    <h4>Tokenization</h4>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <strong>Token Operation:</strong>
                                    <asp:DropDownList ID="ddltoken" CssClass="form-control" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="A">Add</asp:ListItem>
                                        <asp:ListItem Value="U">Update</asp:ListItem>
                                        <asp:ListItem Value="D">Delete</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="span1"></div>
                                <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <strong>Card Token:</strong>
                                    <asp:TextBox ID="txtcardtoken" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="span1"></div>
                            </div>
                            </div>

                            <div class="form-group" style="display:none">
                                <div class="col-md-6 col-xs-12" style="margin: 15px 0px 0px 1px">
                                    <strong>UDF5:</strong>
                                    <asp:TextBox ID="txtUDF5" runat="server" class="form-control"> </asp:TextBox>
                                </div>
                                <div class="span1"></div>
                            </div>                            
                            <div class="form-group">
                                <%--<div class="col-md-6 col-xs-12">
                                    <strong>Amount:</strong>
                                    <asp:TextBox ID="TextBox2" runat="server" class="form-control" value="1.00"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ControlToValidate="txtAmount" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span1"></div>--%>
                            </div>
                            <br />
                            <br />
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " style="margin-top: 10px;" visible="false" runat="server" id="divSadadPaymentDetails">
                                <div class="panel panel-info">
                                    <div class="panel-body">
                                        <div class="col-md-12">
                                            <h4>Sadad Payment Details</h4>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Bill Number:</strong>
                                                    <asp:TextBox ID="txtSadadBillNumber" runat="server" Type="number" class="form-control"> </asp:TextBox>
                                                </div>
                                                <%--<div class="col-md-6 col-xs-12">
                                                    <strong>Entity Activity Id:</strong>
                                                    <asp:TextBox ID="txtSadadEntityActivityId" runat="server" class="form-control"> </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSadadEntityActivityId" ForeColor="Red" runat="server" ControlToValidate="txtSadadEntityActivityId" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Full Name: <span class="required-field">*</span></strong>
                                                    <asp:TextBox ID="txtCustomerFullName" runat="server" class="form-control"> </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCustomerFullName" ForeColor="Red" runat="server" ControlToValidate="txtCustomerFullName" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Id Number:</strong>
                                                    <asp:TextBox ID="txtCustomerIdNumber" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Id Type:</strong>
                                                    <%--<asp:TextBox ID="txtCustomerIdType" runat="server" class="form-control"> </asp:TextBox>--%>
                                                    <asp:DropDownList AutoPostBack="true" CssClass="form-control"  ValidationGroup="g1" 
                                                        ID="txtCustomerIdType" runat="server">
                                                        <asp:ListItem Text="National Id" Value="NAT" />
                                                        <asp:ListItem Text="Passport" Value="PAS" />
                                                         <asp:ListItem Text="Iqama" Value="IQA" />
                                                        <asp:ListItem Text="Commercial Register" Value="CRR" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Email Address:<span class="required-field">*</span></strong>
                                                    <asp:TextBox ID="txtCustomerEmailAddress" runat="server" class="form-control"> </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegCustomerEmailAddress" runat="server" ControlToValidate="txtCustomerEmailAddress"
                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                                    <asp:RequiredFieldValidator ID="RfvCustomerEmailAddress" runat="server" ControlToValidate="txtCustomerEmailAddress"
                                                    ForeColor="Red" Display="Dynamic" ErrorMessage="Required" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Mobile Number: <span class="required-field">*</span></strong>
                                                    <asp:TextBox ID="txtCustomerMobileNumber" runat="server" class="form-control"> </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCustomerMobileNumber" ForeColor="Red" runat="server" ControlToValidate="txtCustomerMobileNumber" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Previous Balance:</strong>
                                                    <asp:TextBox ID="txtCustomerPreviousBalance" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" onkeyup="CalculateTotalAmt()"> </asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvCustomerPreviousBalance" ForeColor="Red" runat="server" ControlToValidate="txtCustomerPreviousBalance" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Customer Tax Number:</strong>
                                                    <asp:TextBox ID="txtCustomerTaxNumber" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Expire Date:<span class="required-field">*</span></strong>
                                                    <asp:TextBox ID="txtExpireDate" type="date" runat="server" class="form-control date-picker"> </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Service Name:</strong>
                                                    <asp:TextBox ID="txtServiceName" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Building Number:</strong>
                                                    <asp:TextBox ID="txtBuildingNumber" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>City Code:</strong>
                                                    <asp:TextBox ID="txtCityCode" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>District Code:</strong>
                                                    <asp:TextBox ID="txtDistrictCode" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Postal Code:</strong>
                                                    <asp:TextBox ID="txtPostalCode" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Street Name:</strong>
                                                    <asp:TextBox ID="txtStreetName" runat="server" class="form-control"> </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 " style="margin-top: 10px;">
                                                    <div class="panel panel-info">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <h4>Bill Item List</h4>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <strong>Name:<span class="required-field">*</span></strong>
                                                                        <asp:TextBox ID="txtname" runat="server" class="form-control"> </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvname" ForeColor="Red" runat="server" ControlToValidate="txtname" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <strong>Quantity:<span class="required-field">*</span></strong>
                                                                        <asp:TextBox ID="txtQuantity" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" onkeyup="CalculateTotalAmt()"> </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvQuantity" ForeColor="Red" runat="server" ControlToValidate="txtQuantity" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <strong>Discount:<span class="required-field">*</span></strong>
                                                                        <asp:TextBox ID="txtDiscount" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" onkeyup="CalculateTotalAmt()"> </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvDiscount" ForeColor="Red" runat="server" ControlToValidate="txtDiscount" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <strong>Discount Type:<span class="required-field">*</span></strong>
                                                                        <%--<asp:TextBox ID="txtDiscountType" runat="server" class="form-control"> </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvDiscountType" ForeColor="Red" runat="server" ControlToValidate="txtDiscountType" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>--%>
                                                                        <asp:DropDownList ID="txtDiscountType" CssClass="form-control" runat="server">
                                                                            <asp:ListItem Value="FIXED">FIXED</asp:ListItem>
                                                                            <asp:ListItem Value="PERCENT">PERCENT</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <strong>Vat:<span class="required-field">*</span></strong>
                                                                        <asp:TextBox ID="txtVat" runat="server" value="15" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" onkeyup="CalculateTotalAmt()"> </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvVat" ForeColor="Red" runat="server" ControlToValidate="txtVat" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-6 col-xs-12">
                                                                        <strong>Unit Price:<span class="required-field">*</span></strong>
                                                                        <asp:TextBox ID="txtUnitPrice" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" onkeyup="CalculateTotalAmt()"> </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvUnitPrice" ForeColor="Red" runat="server" ControlToValidate="txtUnitPrice" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6 col-xs-12">
                                                    <strong>Is Partial Allowed:</strong>
                                                    <%--<asp:TextBox ID="txtIsPartialAllowed" runat="server" class="form-control"> </asp:TextBox>--%>
                                                    <asp:CheckBox ID="txtIsPartialAllowed" runat="server" onclick="enablePartialAmtField()" />
                                                </div>
                                                <div class="col-md-6 col-xs-12" id="MiniPartialAmountHide">
                                                    <strong>Mini Partial Amount:</strong>
                                                    <asp:TextBox ID="txtMiniPartialAmount" runat="server" class="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" onchange="checkamt()"> </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="form-group">
                                <%--<div class="col-md-6 col-sm-6 col-xs-12">--%>
                                <div class="col-md-12 col-sm-12 col-xs-12" style="margin-top: 10px!important;">
                                    <%--  <button type="submit" class="btn btn-primary btn-submit-fix">Place Order</button>--%>
                                    <%--UseSubmitBehavior="false" OnClientClick="this.disabled='true';"--%>
                                    <asp:Button ID="btnsubmit" class="btn btn-primary btn-submit-fix" runat="server" Text="Place Order" OnClick="btnsubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="form-group">
                     <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                     </div>--%>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 col-md-pull-6 col-sm-pull-6">
                    <!--SHIPPING METHOD-->
                    <div class="panel panel-info">
                        <div class="panel-heading">Address</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <h4>Shipping Address</h4>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Country:<span class="required-field">*</span></strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtCountry" runat="server" class="form-control" value="IN"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqfCountry" runat="server" ForeColor="Red" ControlToValidate="txtCountry" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6 col-xs-12">
                                    <strong>First Name:</strong>
                                    <asp:TextBox ID="txtfirst_name" runat="server" class="form-control"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Rqfvfirst_name" ForeColor="Red" runat="server" ControlToValidate="txtfirst_name" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Regfirst_name" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtfirst_name" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="span1"></div>
                                <div class="col-md-6 col-xs-12">
                                    <strong>Last Name:</strong>
                                    <asp:TextBox ID="txtlast_name" runat="server" class="form-control" Style="margin-bottom: 20px;"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Rqfvlast_name" ForeColor="Red" runat="server" ControlToValidate="txtlast_name" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Reglast_name" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtlast_name" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div><br /><br /><br />
                            <div class="form-group">
                                <div class="col-md-12"><strong>Address:</strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtaddress" runat="server" class="form-control" Style="margin-bottom: 20px;"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Rqfvadddress" ForeColor="Red" runat="server" ControlToValidate="txtaddress" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>--%>
                                    <%--  <asp:RegularExpressionValidator ID="RegAddress" ForeColor="Red" Display = "Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtaddress" ValidationExpression="^[A-Za-z]*$" ></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>City:</strong></div>
                                <div class="col-md-12">

                                    <asp:TextBox ID="txtcity" runat="server" class="form-control" Style="margin-bottom: 20px;"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Rqfvcity" ForeColor="Red" runat="server" ControlToValidate="txtcity" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator ID="Regcity" ForeColor="Red" Display = "Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtcity" ValidationExpression="^[A-Za-z]*$" ></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>State:</strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtstate" runat="server" class="form-control" Style="margin-bottom: 20px;"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Rqfvstate" ForeColor="Red" runat="server" ControlToValidate="txtstate" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Regtxtstate" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtstate" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Zip / Postal Code:</strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtzip" runat="server" MaxLength="6" ControlToValidate="txtzip" class="form-control" ErrorMessage="Enter Valid zip" ValidationExpression="^[0-9]{10,12}$" Style="margin-bottom: 20px;">  </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Rqfvzip" runat="server" ForeColor="Red"  MaxLength="6" ControlToValidate="txtzip" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator ID="RequiredFieldValidatorZip" ForeColor="Red" ControlToValidate="txtZip" ValidationExpression="^(\d{6}|\d{6}\-\d{5})$" ErrorMessage="Zip code must be numeric." Display="dynamic" runat="server"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" ErrorMessage="Required!" ControlToValidate="txtZip"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Phone Number:</strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtPhoneno" runat="server" MaxLength="12" class="form-control" Style="margin-bottom: 20px;"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RfaPhoneno" ForeColor="Red" runat="server" ControlToValidate="txtPhoneno" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="Revphonenumber" ForeColor="Red" runat="server" ControlToValidate="txtPhoneno" CssClass="validator" ErrorMessage="Enter Valid Cell Number" ValidationExpression="^[0-9]{10,12}$"></asp:RegularExpressionValidator>--%>
                                </div>

                            </div>

                            <div class="form-group" id="txtSmslanguageSendLinkMode" runat="server">
                                <div id="Smslan" runat="server" class="col-md-6 col-xs-12" style="margin: 20px 0px 20px 0px">
                                    <strong>SmsLanguage:</strong>
                                    <%--<asp:TextBox ID="txtSmslanguage" runat="server" class="form-control"> </asp:TextBox>--%>
                                   <%-- <asp:DropDownList ID="txtSmslanguage" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="English">English</asp:ListItem>
                                        <asp:ListItem Value="Arabic">Arabic</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:DropDownList AutoPostBack="true" CssClass="form-control"  ValidationGroup="g1" 
                                        ID="ddlSmslanguage" runat="server">
                                        <asp:ListItem Text="en" Value="English" />
                                        <asp:ListItem Text="ar" Value="Arabic" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSmslanguage" 
                                        ErrorMessage="Required!" InitialValue="Select" ForeColor="Red" CssClass="validator" Enabled="False"></asp:RequiredFieldValidator>
                                    <%--<asp:RequiredFieldValidator ID="RqfvSmslanguage" ForeColor="Red" runat="server" ControlToValidate="txtSmslanguage" CssClass="validator" ErrorMessage="Required!" Enabled="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegSmslanguage" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtSmslanguage" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>--%>
                                    <p id="isSmsLanguage"></p>
                                </div>
                                <div class="span1"></div>
                                <div id="Sendlink" runat="server" class="col-md-6 col-xs-12" style="margin: 20px 0px 20px 0px">
                                    <strong>SendLinkMode: <span class="required-field">*</span></strong>
                                    <asp:TextBox ID="txtSendlinkmode" runat="server" class="form-control"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RqfvSendlinkmode" ForeColor="Red" runat="server" ControlToValidate="txtSendlinkmode" CssClass="validator" ErrorMessage="Required!" Enabled="False"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegSendlinkmode" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="only Number allowed" ControlToValidate="txtSendlinkmode" ValidationExpression="^[0-9]{0,10}$"></asp:RegularExpressionValidator>
                                    <p id="idSendLinkMode"></p>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Email Address:<span class="required-field">*</span></strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtcustomerEmail" runat="server" class="form-control"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RefcustomerEmail" runat="server" ControlToValidate="txtcustomerEmail" CssClass="validator" ErrorMessage="Required!" ></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="Rfvemail" runat="server" ControlToValidate="txtcustomerEmail" CssClass="validator" ErrorMessage="Invalid EmailID" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                    <asp:RegularExpressionValidator ID="RefcustomerEmail" runat="server" ControlToValidate="txtcustomerEmail"
                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                    <asp:RequiredFieldValidator ID="Rfvemail" runat="server" ControlToValidate="txtcustomerEmail"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Required" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12"><strong>Meta Data</strong></div>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtmetadataone" runat="server" class="form-control"> </asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ControlToValidate="txtfirst_name" CssClass="validator" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="only characters allowed" ControlToValidate="txtfirst_name" ValidationExpression="^[A-Za-z]*$"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>                            
                        </div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Style="color: red; font-weight: bold;" Text=""></asp:Label>
                </div>
            </div>
            <div class="row cart-footer">
            </div>
        </div>
    </form>
    <script>
        jQuery(document).ready(function ($) {
            if (window.history && window.history.pushState) {
                $(window).on('popstate', function () {
                    var hashLocation = location.hash;
                    var hashSplit = hashLocation.split("#!/");
                    var hashName = hashSplit[1];
                    if (hashName !== '') {
                        var hash = window.location.hash;
                        if (hash === '') {
                            alert('Do not press Back button was pressed......');
                        }
                    }
                });
            }
        });
    </script>

    <script type="text/javascript">
        function round(num, decimalPlaces) {
            num = Math.round(num + "e" + decimalPlaces);
            return Number(num + "e" + -decimalPlaces);
        }
        function CalculateTotalAmt() {
            var CusPreBal = document.getElementById("txtCustomerPreviousBalance").value;
            var Quantity = document.getElementById("txtQuantity").value;
            var discount = document.getElementById("txtDiscount").value;
            var discounttype = document.getElementById("txtDiscountType").value;
            var vat = document.getElementById("txtVat").value;
            var unitprice = document.getElementById("txtUnitPrice").value;
            var totalamt = document.getElementById("txtAmount").value;
            var totalamt = Quantity * unitprice;
            var e = document.getElementById("txtDiscountType").value;
            if (e == 'FIXED') {
                totalamt = (totalamt - discount);
            }
            else if (e == 'PERCENT') {
                totalamt = totalamt - ((discount / 100) * totalamt);
            }
            totalamt = totalamt + ((vat / 100) * totalamt);
            totalamt = round(totalamt, 2);
            var finalamt = Number(totalamt) + Number(CusPreBal);
            finalamt = round(finalamt, 2);
            finalamt
            document.getElementById("txtAmount").value = finalamt;

        }           
        function enablePartialAmtField() {
            if (($('#txtIsPartialAllowed').is(':checked'))) {
                $('#txtMiniPartialAmount').show();
                $('#txtIsPartialAllowed').val('Y');
                $mini_amt = document.getElementById('txtMiniPartialAmount').value;
                $total_amt = document.getElementById('txtAmount').value;
                $('#MiniPartialAmountHide').show();
                if (Number($mini_amt) < 10 && $total_amt < 10) {
                    alert('Partial Amount should be greater than 10');
                }
            } else {
                $('#MiniPartialAmountHide').hide();
                $('#txtIsPartialAllowed').val('N');
                $('#txtMiniPartialAmount').val(0);
            }
        }
        function checkamt() {
            $mini_amt = document.getElementById('txtMiniPartialAmount').value;
            $total_amt = document.getElementById('txtAmount').value;

            if (Number($mini_amt) < 10 || Number($mini_amt) == 0) {

                alert('Partial Amount should be greater than 10');
            } else
                if (Number($total_amt) < Number($mini_amt)) {

                    alert('Partial Amount should be less than Amount');
                }
        }
        function disableBackDate() {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();
            today = yyyy + '-' + mm + '-' + dd;
            var nexttoday = new Date().toISOString().split('T')[0];
            var datess = document.getElementsByName("txtExpireDate")[0].setAttribute('min', nexttoday);
            var nextWeekDate = new Date(new Date().getTime() + 14 * 24 * 60 * 60 * 1000).toISOString().split('T')[0]
            $('#txtExpireDate').attr('max', nextWeekDate);
            $('#txtExpireDate').attr('value', today);
            $('#MiniPartialAmountHide').hide();
        }
        disableBackDate()
    </script>

    <script type="text/javascript">
        function sendRequest() {
            debugger;
            var ErrorFlag = true;
            var transId = $("#txtTransId").val();
            var actionCode = $("#ddlAction").val();
			
            if (actionCode == "2" || actionCode == "9" || actionCode == "5" || actionCode == "10" || actionCode == "3" || actionCode == "6") {
                if (transId == '') {
                    document.getElementById("transIdError").style.visibility = "visible";
                    document.getElementById("transIdError").style.display = "block";
                    document.getElementById("transIdError").innerHTML = "Please Enter Transaction Id";
                    ErrorFlag = false;
                } else {
                    document.getElementById("transIdError").style.visibility = "hidden";
                }
            }        
            if (ErrorFlag == false) {
                //alert(ErrorFlag);
                return false;
            }
        }
	</script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</body>

</html>


