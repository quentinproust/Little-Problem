<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<LittleProblem.Data.Model.Problem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <div>
        <h2>Last problems submitted by members</h2>

        <div id="problems">
        <% if (Model.Count == 0) { %>
            No problems has yet to be submitted.
        <% } else { foreach (var problem in Model) {%>
            <div class="problem <%= problem.IsClosed() ? "closed" : "not-closed" %>">
                <div class="answers"><%= problem.Responses.Count %></div>
                <div class="description"><%= Html.ActionLink(problem.Title, "Details", "Problem", new { id = problem.Id.ToString() }, null)%></div>
            </div>
        <% } } %>
        </div>
    </div>
</asp:Content>
