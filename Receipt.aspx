<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="Receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
	<script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
	<%--<link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">--%>
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <style>
@media (min-width: 1200px)
{
.container {
    width: 500px !important;
}
}
.invoice-title h2, .invoice-title h3 {
    display: inline-block;
}

.table > tbody > tr > .no-line {
    border-top: none;
}

.table > thead > tr > .no-line {
    border-bottom: none;
}

.table > tbody > tr > .thick-line {
    border-top: 2px solid;
}
div.solid {border-style: solid;}

</style>
</head>
<body>
    <%--<div class="container  solid">
    <div class="row">
        <div class="col-xs-12">
			<div class="invoice-title">
    			<h2>Invoice</h2><h3 class="pull-right">Order # </h3>
    		</div>
    		<hr>
			<div class="invoice-title text-center">
			<h2>Transaction is </h2><br>
    		</div>   		
    	</div>
    </div>
    
    <div class="row">
    	<div class="col-md-12">
    		<div class="panel panel-default">
    			<div class="panel-heading">
    				<h3 class="panel-title"><strong>Order summary</strong></h3>
    			</div>
    			<div class="panel-body">
    				<div class="table-responsive">
    					<table class="table table-condensed">
    						
    						<tbody>
    							<tr>
    								<td class="no-line"></td>
    								<td class="no-line"></td>
    								<td class="no-line text-center"><strong>Order Date</strong></td>
    								<td class="no-line text-center"></td>
    							</tr>
                                <tr>
    								<td class="no-line"></td>
    								<td class="no-line"></td>
    								<td class="no-line text-center"><strong>Product Amount</strong></td>
    								<td class="no-line text-center"></td>
    							</tr>
                                <tr>
    								<td class="no-line"></td>
    								<td class="no-line"></td>
    								<td class="no-line text-center"><strong>Response Code Description</strong></td>
    								<td class="no-line text-center">></td>
    							</tr>
                               
    						</tbody>
    					</table>
    				</div>
    			</div>
    		</div>
    	</div>
    </div>
</div>--%>


       <%--<div class='col-lg-6 col-md-6 col-sm-6 col-xs-12 col-md-push-3 col-sm-push-3'>
            <div class='panel panel-primary'>
                <div class='panel-heading'>
                    Order Status<div class='pull-right'>Transaction ID <small></small></div>                   
                </div>
                <div class='form-group'>
                    <div class='col-sm-6 col-xs-6'>
                        <div class='col-xs-12'>Order Date :</div>
                    </div>
                    <div class='col-sm-6 col-xs-6'>
                        <div class='col-xs-12'>20-05-2025</div>
                    </div>
                </div>
                <div class='form-group'>
                    <div class='col-sm-6 col-xs-6'>
                        <div class='col-xs-12'>Product Amount :</div>
                    </div>
                    <div class='col-sm-6 col-xs-6'>
                        <div class='col-xs-12'>20-05-2025</div>
                    </div>
                </div>
                <div class='form-group'>
                    <div class='col-sm-6 col-xs-6'>
                        <div class='col-xs-12'>Response Code Description :</div>
                    </div>
                    <div class='col-sm-6 col-xs-6'>
                        <div class='col-xs-12'>20-05-2025</div>
                    </div>
                </div>                
            </div>
        </div>--%>


    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 col-md-push-3 col-sm-push-3 mt-05" style="margin-top: 100px;">
    <div class="panel panel-primary">
        <div class="panel-heading">
            Order Status
            <div class="pull-right">Transaction ID : <asp:Label ID="OrderID" runat="server" Text=""></asp:Label></div>
        </div>
        <div style="margin-bottom: 100px">
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12">Order Date :</div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12"><asp:Label ID="Orderdate" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12">Product Amount :</div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12"><asp:Label ID="Amount" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12">Response Code Description :</div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12"><asp:Label ID="ResponseCodeDescription" runat="server" Text=""></asp:Label></div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12">Inquiry Payment Status : </div>
            </div>
            <div class="col-sm-6 col-xs-6">
                <div class="col-xs-12"><asp:Label ID="result" runat="server" Text=""></asp:Label></div>
            </div>

            <div class="col-xs-12"><asp:Label ID="Textdemo" runat="server" Text=""></asp:Label></div>
        </div>

    </div>
    </div>

    



</body>
</html>
