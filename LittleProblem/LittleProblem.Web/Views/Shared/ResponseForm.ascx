<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LittleProblem.Web.Models.ResponseModel>" %>

    <% using (Html.BeginForm("Answer", "Problem")) {%>
        <%= Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Text) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Text) %>
                <%= Html.ValidationMessageFor(model => model.Text) %>
            </div>
            
            <%= Html.TextBoxFor(model => model.ProblemId, new {type = "hidden"}) %>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>


