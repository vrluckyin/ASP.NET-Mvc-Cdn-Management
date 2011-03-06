<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CdnManagement.Web.Models.Employee>" %>
<%@ Import Namespace="CdnManagement" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>View1</title>
    <%: Html.Style("Site.css")  %>
    <%: Html.Script("Cdn.Script.jquery") %>
    
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
