<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LittleProblem.Web.Models.ProblemModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Problem Submission
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Enter your problem</h2>
    <% using (Html.BeginForm()) { %>
		<%= Html.ValidationSummary(true, "") %>
		<div>
			<fieldset>
                <p class="clear">
				    <%= Html.TextBoxFor(p => p.Title)%>
                    <%= Html.TextAreaFor(p => p.Text) %>
                    <input type="submit" value="Submit problem" />
			    </p>
			</fieldset>
		</div>
	<% } %>

</asp:Content>
