<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcCdnManagement.Models.Employee>" %>
<%@ Import Namespace="Mvc3.CdnManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View3
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.Resource(ResourceType.Style, "Cdn.Style.Site"); %>
<% Html.Resource(ResourceType.Script, "Cdn.Script.jquery"); %>
<h2>View3</h2>
<div class="firstName">
        <%: Model.FirstName %>
    </div>
    <div class="lastName">
        <%: Model.LastName %>
    </div>
</asp:Content>
