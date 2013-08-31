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
		<asp:HyperLink runat="server" NavigateUrl="{RefreshLink}" Text="Refresh" />
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Next}" Text="Next" />
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample}" Text="Sample" />
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample2}" Text="Sample2" />
		<asp:Button runat="server" Text="Submit" />
    </div>
    </form>
</body>
</html>
