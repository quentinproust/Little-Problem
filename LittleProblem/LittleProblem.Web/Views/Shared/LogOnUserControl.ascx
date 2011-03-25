<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Session["openid"] != null) {
%>
        Welcome <b><%= Html.Encode(Session["username"]) %></b> !
        [ <%= Html.ActionLink("Profile", "Profile", "Account") %> ]
        [ <%= Html.ActionLink("Log Out", "LogOut", "Account") %> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Log In", "LogIn", "Account") %> ]
<%
    }
%>
