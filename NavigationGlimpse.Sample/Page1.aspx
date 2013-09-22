<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Page1.aspx.cs" Inherits="NavigationGlimpse.Sample.Page1" MasterPageFile="~/Master1.Master" Theme="Theme" %>
<%@ Register assembly="Navigation" namespace="Navigation" tagprefix="nav" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Content">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<asp:HyperLink runat="server" NavigateUrl="{RefreshLink &dateOfBirth?datetime=1980/10/4}" Text="Refresh" />
			<asp:HyperLink runat="server" NavigateUrl="{NavigationLink* Next,name=Bob}" Text="Next" />
			<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample,age=19}" Text="Sample" />
			<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample2,name=Brenda}" Text="Sample2" />
			<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample3}" Text="Sample3" />
			<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample4}" Text="Sample4" />
			<asp:HyperLink runat="server" NavigateUrl="{NavigationLink Sample5}" Text="Sample5" />
			<asp:Button runat="server" Text="Submit" OnClick="Button_Click" />
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
