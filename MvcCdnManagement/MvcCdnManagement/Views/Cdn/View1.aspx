﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcCdnManagement.Models.Employee>" %>
<%@ Import Namespace="Mvc3.CdnManagement" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>View1</title>
    <% Html.StyleLink("Styles/Site.css");  %>
    <% Html.ScriptLink("Scripts/jquery-1.4.1.min.js");  %>
    <script type="text/javascript">
        $(document).ready(function () {
            alert('loaded');
            $(".firstName").html("jquery is Loaded.");
        });
    </script>
</head>
<body>
    <div class="firstName"><%: Model.FirstName %>  </div>
    <div class="lastName"> <%: Model.LastName %> </div>
</body>
</html>
