<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WriteTxtFile.aspx.cs" Inherits="ReadingWritingFiles.WriteTxtFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>

    <asp:Panel ID="Panel1" runat="server" CssClass="form-group">

        <asp:Label ID="Label1" runat="server" Text="Enter text to write to file:"></asp:Label>

        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>

    </asp:Panel>

    <br />

    <asp:Button ID="Button1" runat="server" Text="Write to text file" CssClass="btn btn-secondary" OnClick="Button1_Click" />


</asp:Content>
