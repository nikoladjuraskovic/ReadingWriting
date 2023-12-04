<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReadTxtFile.aspx.cs" Inherits="ReadingWritingFiles.ReadTxtFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>

    <h1>Reading Text Files</h1>

    <br />

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

    <br />

    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

    <br />
    <br />

    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>

    

    <br />
    <br />
    <br />
    <br />

    <h2>Ispis podataka preko ReadAllLines</h2>

    <asp:GridView ID="GridView1" runat="server" CssClass="table" Width="30%"></asp:GridView>

    <br />

    <h2>Ispis podataka preko StreamReader</h2>

    <asp:GridView ID="GridView2" runat="server" CssClass="table" Width="30%"></asp:GridView>


</asp:Content>
