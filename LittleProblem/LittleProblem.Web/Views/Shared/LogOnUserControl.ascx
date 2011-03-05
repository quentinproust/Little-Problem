<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink("Log Out", "LogOut", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Log In", "LogIn", "Account") %> ]
<%
    }
%>
