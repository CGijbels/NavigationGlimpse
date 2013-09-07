<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.Mobile.aspx.cs" Inherits="NavigationGlimpse.Sample.Page1_Mobile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Next,name=MobileBob}" Text="Next" />
		<asp:Button runat="server" Text="Submit" />
    </div>
    </form>
</body>
</html>
