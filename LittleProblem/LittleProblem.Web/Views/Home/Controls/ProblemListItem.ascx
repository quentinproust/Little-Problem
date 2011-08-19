<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LittleProblem.Data.Model.Problem>" %>

<div class="problem <%= Model.IsClosed() ? "closed" : "not-closed" %>">
    <div class="answers"><%= Model.Responses.Count %></div>
    <div class="description"><%= Html.ActionLink(Model.Title, "Details", "Problem", new { id = Model.Id.ToString() }, null)%></div>
</div>