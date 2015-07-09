<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TinyLibrary.WebApp.TinyLibraryService.BookData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BookDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Book Details: <%: Model.Title %></h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Title</div>
        <div class="display-field"><%: Model.Title %></div>
        <br />

        <div class="display-label">ISBN</div>
        <div class="display-field"><%: Model.ISBN %></div>
        <br />

        <div class="display-label">Pages</div>
        <div class="display-field"><%: Model.Pages %></div>
        <br />

        <div class="display-label">PubDate</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.PubDate) %></div>
        <br />

        <div class="display-label">Publisher</div>
        <div class="display-field"><%: Model.Publisher %></div>
        
    </fieldset>
    <p>
        <%: Html.ActionLink("Back to List", "Index") %> |
        <%
            if (Model.Lent)
            {
                 Response.Write(MvcHtmlString.Create(string.Format("This book was lent to {0} @ {1}.", Model.LendTo, Model.LendDate)));
            }
            else
            {
                Response.Write(Html.ActionLink("Borrow This Book", "BookBorrow", new { id = Model.Id }));
            }
             %>
             <%-- 
        <%: Model.Lent ? MvcHtmlString.Create("Unavailable") : Html.ActionLink("Borrow", "BookBorrow", new { id = Model.Id })%>
        --%>
    </p>

</asp:Content>

