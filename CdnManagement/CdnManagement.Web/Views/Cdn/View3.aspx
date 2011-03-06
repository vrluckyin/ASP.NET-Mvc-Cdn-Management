<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CdnManagement.Web.Models.Employee>" %>
<%@ Import Namespace="CdnManagement" %>
<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>View3</title>
    <%: Html.LoadResources()  %>
    
    <script type="text/javascript">
        $(document).ready(function () {
            alert('view 3 loaded');
            $(".firstName").html("jquery is Loaded.");
        });
    </script>
</head>
<body>
    <div class="firstName"><%: Model.FirstName %>  </div>
    <div class="lastName"> <%: Model.LastName %> </div>
</body>
</html>