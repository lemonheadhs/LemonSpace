<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BookBorrow
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Borrowed</h2>
    <p>You have successfully borrowed the book.</p>
    <p>
    Please click <%: Html.ActionLink("here", "MyBooks") %> to view your borrowed books, or click <%: Html.ActionLink("here", "Index") %> to return to the home page.
    </p>

</asp:Content>
