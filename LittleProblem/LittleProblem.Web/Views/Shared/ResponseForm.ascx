<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LittleProblem.Web.Models.ResponseModel>" %>

<% using (Html.BeginForm("Answer", "Problem")) {%>
    <%= Html.ValidationSummary(true) %>

    <div>
        <legend>Try submitting your solution</legend>
        <div class="editor-field">
            <%= Html.TextAreaFor(model => model.Text) %>
            <%= Html.ValidationMessageFor(model => model.Text) %>
        </div>
            
        <%= Html.TextBoxFor(model => model.ProblemId, new {type = "hidden"}) %>
            
        <p>
            <input type="submit" value="Answer" />
        </p>
    </div>

<% } %>


