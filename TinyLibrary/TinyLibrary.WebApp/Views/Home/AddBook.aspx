<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TinyLibrary.WebApp.TinyLibraryService.BookData>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Title) %>
                <%: Html.ValidationMessageFor(model => model.Title) %>
            </div>
                        
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ISBN) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ISBN) %>
                <%: Html.ValidationMessageFor(model => model.ISBN) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Pages) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Pages) %>
                <%: Html.ValidationMessageFor(model => model.Pages) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.PubDate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.PubDate) %>
                <%: Html.ValidationMessageFor(model => model.PubDate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Publisher) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Publisher) %>
                <%: Html.ValidationMessageFor(model => model.Publisher) %>
            </div>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

