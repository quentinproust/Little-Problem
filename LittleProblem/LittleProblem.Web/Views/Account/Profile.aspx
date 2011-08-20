<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LittleProblem.Web.Models.ProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Profile</h2>

	<% if (ViewData["Error"] != null)
	   {%>

		<div class="error">
			<%= ViewData["Error"] %>
		</div>
	<%
	   }
	   else
	   {
		   using (Html.BeginForm())
		   {%>
		<%= Html.ValidationSummary(true)%>
		
		<fieldset>
			<legend>Informations</legend>
			
			<div class="editor-label">
				<%= Html.LabelFor(model => model.UserName)%>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.UserName)%>
				<%= Html.ValidationMessageFor(model => model.UserName)%>
			</div>
			<p>
				If you want us to contact you if you have a problem,
				please give us your email. 
				It will not be transmitted to other websites.
			</p>
			<div class="editor-label">
				<%= Html.LabelFor(model => model.Email)%>
			</div>
			<div class="editor-field">
				<%= Html.TextBoxFor(model => model.Email)%>
				<%= Html.ValidationMessageFor(model => model.Email)%>
			</div>
			

			<p>
				<input type="submit" value="Save" />
			</p>
		</fieldset>

        <fieldset>
            <legend>LittleProblem Note</legend>

            <p>
                This is the note user gave you when they vote on your responses.
            </p>
            <div class="note">
                <div class="gold-points"><%= Model.Note.Gold %></div>
                <div class="silver-points"><%= Model.Note.Silver %></div>
                <div class="bronze-points"><%= Model.Note.Bronze %></div>
            </div>
        </fieldset>

	<% }
	   } %>

</asp:Content>

