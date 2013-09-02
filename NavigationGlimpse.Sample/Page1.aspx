<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="NavigationGlimpse.Sample.Page1" %>
<%@ Register assembly="Navigation" namespace="Navigation" tagprefix="nav" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:HyperLink runat="server" NavigateUrl="{RefreshLink dateOfBirth?datetime=1980/10/4}" Text="Refresh" />
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Next,name=Bob}" Text="Next" />
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample,age=19}" Text="Sample" />
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample2,name=Brenda}" Text="Sample2" />
		<asp:Button runat="server" Text="Submit" />
    </div>
    </form>
</body>
</html>
