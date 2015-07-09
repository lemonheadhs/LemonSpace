<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TinyLibrary.WebApp.TinyLibraryService.BookData>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>All Books</h2>
    <table>
        <tr>
            <th></th>
            <th>
                Title
            </th>
            <th>
                Publisher
            </th>
            <th>
                ISBN
            </th>
            <th>
                Publish Date
            </th>
            <th>
                Status
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
            <%-- 
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
            --%>
                <%: Html.ActionLink("Details", "BookDetails", new {  id=item.Id  })%> 
               <%-- 
                |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
                --%> 
            </td>
            <td>
                <%: item.Title %>
            </td>
            <td>
                <%: item.Publisher %>
            </td>
            <td>
                <%: item.ISBN %>
            </td>
            <td>
                <%: item.PubDate %>
            </td>
            <td>
               <%-- <%: item.Lent ? Html.Encode("Lent") : Html.Encode("Available")%> --%>
               <%
                   if (item.Lent)
                   {
                       Response.Write(Html.Encode(string.Format("Lent to {0}", item.LendTo)));
                   }
                   else
                   {
                       Response.Write(Html.ActionLink("Borrow", "BookBorrow", new { id = item.Id }));
                   }
                %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Add New Book", "AddBook") %>
    </p>
</asp:Content>
