<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.Mobile.aspx.cs" Inherits="NavigationGlimpse.Sample.Page1_Mobile" MasterPageFile="~/NestedMaster1.Mobile.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Content">
	Mobile
	<asp:HyperLink runat="server" NavigateUrl="{RefreshLink dateOfBirth?datetime=1980/10/4}" Text="Refresh" />
	<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Next,name=Bob}" Text="Next" />
	<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample,age=19}" Text="Sample" />
	<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample2,name=Brenda}" Text="Sample2" />
	<asp:Button runat="server" Text="Submit" />
</asp:Content>
