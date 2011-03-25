<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LittleProblem.Data.Aggregate.ProblemAggregate>" %>
<%@ Import Namespace="LittleProblem.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Problem <em><%= Html.Encode(Model.Title) %></em></h2>
	<div>Submitted on <%= Html.Encode(String.Format("{0:g}", Model.OpenedDate)) %></div>
	<h3>Description</h3>
	<p>
		<%= Html.Encode(Model.Text) %>
	</p>

	<h3>Responses to this problem :</h3>
	<div>
	<% foreach (var response in Model.Responses) { %>
		<p>
			<strong><%= Html.Encode(response.Text) %></strong> by <em><%= response.Submitter.UserName %></em>
		</p>
	<%    } %>
	</div>

	<% Html.RenderPartial("ResponseForm", new ResponseModel { ProblemId = Model.Id.ToString() }); %>
</asp:Content>

