<%@ Page Language="C#" Inherits="System.Web.UI.MobileControls.MobilePage" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0,Culture=neutral, PublicKeyToken=71e9bce111e9429c"%> 
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Import Namespace="System.Web.UI.MobileControls" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<script language="c#" runat="server">
    
    public void Page_Load()
    {
        SPSite site = SPControl.GetContextSite(Context);
        SPWebCollection webs = site.AllWebs;

        foreach (SPWeb web in webs)
        {
            Link webLink = new Link();
            webLink.NavigateUrl = web.Url +
              "/_layouts/mobile/default.aspx";
            webLink.Text = web.Title;
            welcomeForm.Controls.Add(webLink);

        }
    }
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <mobile:Form ID="welcomeForm" Runat="server">
        <mobile:Image ID="bannerImage" Runat="server" ImageUrl="../images/addtofavorites.gif">
        </mobile:Image>List of Sites<br />
    </mobile:Form>
</body>
</html>
