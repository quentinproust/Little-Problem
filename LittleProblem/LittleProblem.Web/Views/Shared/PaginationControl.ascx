<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LittleProblem.Web.Models.PaginationModel>" %>

<div class="pagination">
    <% for (var page = 0; page < Model.NbPages; page++) { %>
        <%=Html.ActionLink(page.ToString(), Model.Action, Model.Controller, new {id = page}, 
        Model.CurrentPage == page ? new { @class="current" }: null)%>
    <%} %>
</div>
