<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LittleProblem.Web.Models.LogInModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>

    <% using (Html.BeginForm()) { %>
        <%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.OpenId) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.OpenId) %>
                    <%= Html.ValidationMessageFor(m => m.OpenId) %>
                </div>
                
                <p>
                    <input type="submit" value="Log In" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
