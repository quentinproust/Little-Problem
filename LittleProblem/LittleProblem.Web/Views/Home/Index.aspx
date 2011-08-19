<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProblemListModel>" %>
<%@ Import Namespace="LittleProblem.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Last problems submitted by members</h2>

        <div id="problems">
        <% if (!Model.HasProblems) { %>
            No problems has yet to be submitted.
        <% } else { foreach (var problem in Model.Problems) {%>
            <% Html.RenderPartial("Controls/ProblemListItem", problem); %>
        <% } } %>
        </div>

        <%
           Html.RenderPartial("PaginationControl", new PaginationModel
                                                       {
                                                           Controller = "Home",
                                                           Action = "Index",
                                                           NbElements = Model.NbProblemsTotal,
                                                           CurrentPage = Model.CurrentPage
                                                       });%>
    </div>
</asp:Content>
