<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TinyLibrary.WebApp.TinyLibraryService.RegistrationData>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Books
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>My Books</h2>
    <p>Followings are my books registered.</p>
    <table>
        <tr>
            <th>
                ISBN
            </th>
            <th>
                Title
            </th>
            <th>
                Registered Date
            </th>
            <th>
                Return Due Date
            </th>
            <th>
                ReturnDate
            </th>
            <th>
                Status
            </th>
            <th>
                Operation
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.BookISBN %>
            </td>
            <td>
                <%: Html.ActionLink(item.BookTitle, "BookDetails", new{id=item.BookGuid}) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Date) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.DueDate) %>
            </td>
            <td>
                <%
                    if (item.ReturnDate.Year.Equals(DateTime.MaxValue.Year) &&
                        item.ReturnDate.Month.Equals(DateTime.MaxValue.Month) &&
                        item.ReturnDate.Day.Equals(DateTime.MaxValue.Day))
                    {
                        Response.Write(Html.Encode("-"));
                    }
                    else
                    {
                        Response.Write(String.Format("{0:g}", item.ReturnDate));
                    }
                 %>
            </td>
            <td>
                <%: item.StatusText %>
            </td>
            <td>
                <%
                    if (item.Status == -1 || item.Status == 0)
                    {
                        Response.Write(Html.ActionLink("Return", "BookReturn", new { id = item.BookGuid }));
                    }
                    else
                    {
                        Response.Write(Html.Encode(""));
                    }
               %>
            </td>
        </tr>
    
    <% } %>

    </table>


</asp:Content>

