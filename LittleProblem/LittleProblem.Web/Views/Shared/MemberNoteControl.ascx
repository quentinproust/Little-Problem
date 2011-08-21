<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LittleProblem.Web.Models.NoteModel>" %>

<div class="note">
    <span class="gold points"><%= Model.Gold %></span>
    <span class="silver points"><%= Model.Silver %></span>
    <span class="bronze points"><%= Model.Bronze %></span>
</div>