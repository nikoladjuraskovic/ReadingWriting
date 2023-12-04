<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ReadingWritingFiles.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>


    <h1>Search Students</h1>

    <br />

    <asp:Panel ID="Panel1" runat="server" CssClass="form-group">

        <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>

        <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>

    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server" CssClass="form-group">

        <asp:Label ID="Label2" runat="server" Text="Last Name:"></asp:Label>

        <asp:TextBox ID="TextBoxLastName" runat="server" CssClass="form-control"></asp:TextBox>

    </asp:Panel>

    <asp:Panel ID="Panel3" runat="server" CssClass="form-group">

        <asp:Label ID="Label3" runat="server" Text="Year:"></asp:Label>

        <asp:DropDownList ID="DropDownListYear" runat="server" CssClass="form-control"></asp:DropDownList>

    </asp:Panel>

    <br />

    <asp:Panel ID="Panel4" runat="server" CssClass="form-group">

        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn  btn-primary" OnClick="Button1_Click" />

    </asp:Panel>

    <br />

    <%--Empty property-ji su u slucaju da se nista ne prikazuje u GridView.
        EmptyDataText - tekst za ispis
        EmptyDataRowStyle- boja teksta
        EmptyDataRowStyle-Font-Bold - boldovani font
        --%>
    <asp:GridView ID="GridView1" runat="server" CssClass="table" Width="30%"
        EmptyDataText="No data matches selection" EmptyDataRowStyle-ForeColor="Red"
        EmptyDataRowStyle-Font-Bold="true"></asp:GridView>


</asp:Content>
