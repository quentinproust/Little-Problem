<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LittleProblem.Web.Models.LogInModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Login</h2>

	<% using (Html.BeginForm()) { %>
		<%= Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
		<div>
			<fieldset>
				<legend>Select you open provider !</legend>
				
				<div id="openid_input_area"></div>

				<div class="openid_choice">  
					<p>Please click your account provider:</p>  
					<div id="openid_btns"></div>  
				</div>                                 
				 
                <p class="clear">
				    <%= Html.TextBoxFor(m => m.OpenId)%>
                    <input type="submit" value="Log In" />
			    </p>

				<noscript>  
				    <p>OpenID is service that allows you to log-on to many different websites using a single indentity.  
				Find out <a href="http://openid.net/what/">more about OpenID</a> and <a href="http://openid.net/get/">how to get an OpenID enabled account</a>.</p>  
				</noscript>  

			</fieldset>
		</div>
	<% } %>

    <script type="text/javascript" src="../../Scripts/openid-jquery.js"></script>
    <script type="text/javascript" src="../../Scripts/openid-en.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			openid.init('openid_identifier');
		});  
	</script>  

</asp:Content>
