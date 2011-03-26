<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LittleProblem.Data.Aggregate.ProblemAggregate>" %>
<%@ Import Namespace="LittleProblem.Web" %>
<%@ Import Namespace="LittleProblem.Web.Helpers" %>
<%@ Import Namespace="LittleProblem.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Problem : <%= Html.Encode(Model.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Problem <em><%= Html.Encode(Model.Title) %></em></h2>
	<div>
		Submitted on <%= Html.Encode(String.Format("{0:g}", Model.OpenedDate)) %> 
		by <em><%= Html.Encode(Model.Submitter.UserName) %></em>
	</div>
	<h3>Description</h3>
	<p>
		<%= Html.Encode(Model.Text) %>
	</p>

	<h3>Responses to this problem :</h3>
	<div id="responses">
	<% foreach (var response in Model.Responses) { %>
		<div class="response">
			<div class="message">
				<strong><%= Html.Encode(response.Text) %></strong>
			</div>
			<div class="user">
				by <em><%= response.Submitter.UserName %></em>
			</div>
			<div class="note">
                <%= Html.ActionLink("Up", "Up", "Problem", new { id = Model.Id, responseId = response.Id}, null)%> | 
                <%= Html.ActionLink("Down", "Down", "Problem", new { id = Model.Id, responseId = response.Id}, null)%>
            </div>
			<% if(Session.IsCurrentMember(Model.Submitter)) { %>
			    <div class="accept"><%= Html.ActionLink("Accept as a solution", "Close", "Problem",new { id = Model.Id }, null) %></div>
			<% } %>
		</div>
	<%    } %>
	</div>

	<% if(!Model.IsClosed) Html.RenderPartial("ResponseForm", new ResponseModel { ProblemId = Model.Id }); %>
</asp:Content>

