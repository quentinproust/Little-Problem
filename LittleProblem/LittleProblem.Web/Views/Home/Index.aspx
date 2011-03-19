<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<LittleProblem.Data.Model.Problem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <div>
        <h2>Last problems submitted by members</h2>

        <div>
        <% if (Model.Count == 0) { %>
            No problems has yet to be submitted.
        <% } else {
                  foreach (var problem in Model)
                  {%>
            <div>
                 <%= Html.ActionLink(problem.Title, "Details", "Problem", new { id = problem.Id.ToString() }, null)%>
                | <%= problem.Responses.Count %>
            </div>
        <%
                  }
} %>
        </div>
    </div>
</asp:Content>
