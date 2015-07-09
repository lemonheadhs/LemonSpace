<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BookReturn
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Returned</h2>
    <p>You have successfully returned the book to the library.</p>
    <p>
    Please click <%: Html.ActionLink("here", "MyBooks") %> to view your registered books, or click <%: Html.ActionLink("here", "Index") %> to return to the home page.
    </p>
</asp:Content>
