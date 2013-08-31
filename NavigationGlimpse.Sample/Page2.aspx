<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page2.aspx.cs" Inherits="NavigationGlimpse.Sample.Page2" %>
<%@ Register assembly="Navigation" namespace="Navigation" tagprefix="nav" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:HyperLink runat="server" NavigateUrl="{NavigationBackLink 1}" Text="Back" />    
		<asp:Button runat="server" Text="Submit" />
    </div>
    </form>
</body>
</html>
