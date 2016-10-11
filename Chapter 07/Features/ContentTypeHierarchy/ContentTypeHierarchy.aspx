<%@ Page Language="C#" MasterPageFile="~/_layouts/application.master" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c"%> 
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="~/_controltemplates/ToolBarButton.ascx" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
Content Types Hierarchy
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="PlaceHolderPageDescription">
This page shows all Site Content Types and their hierarchical relationships
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="PlaceHolderMain" >
<TABLE border="0" width="100%" cellspacing="0" cellpadding="0">
  <TR>
    <TD ID="mngfieldToobar">
      <wssuc:ToolBar id="onetidMngFieldTB" runat="server">
      <Template_Buttons>
          <wssuc:ToolBarButton runat="server" Text="<%$Resources:wss,multipages_createbutton_text%>" id="idAddField" ToolTip="<%$Resources:wss,mngctype_create_alt%>" NavigateUrl="ctypenew.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="C" />
        </Template_Buttons>
      </wssuc:ToolBar>
    </TD>
  </TR>
  </TABLE>

<%
    
    //System Content Type is the root
    SPSite site = SPControl.GetContextSite(Context);
    SPContentTypeCollection types = site.OpenWeb().ContentTypes;
    SPContentTypeId id = types[0].Id;
    Response.Write("<table style='font-size:10pt' border='0'" + " cellpadding='2' width='50%'><tr><td>");
    Response.Write("<li><a class='ms-topnav'" + " href=\"/_layouts/ManageContentType.aspx?ctype=" +
    types[0].Id.ToString() + "\">" + types[0].Name + "</a><span class='ms-webpartpagedescription'>" + types[0].Description + "</span></li>");
    ShowChildren(id);
    Response.Write("</ol></td></tr></table>");
    
%>

</asp:Content>

<script runat="server">
    
    public void ShowChildren(SPContentTypeId id)
    {
        SPSite site = SPControl.GetContextSite(Context);
        SPContentTypeCollection types = site.RootWeb.ContentTypes;

        Response.Write("<ol>");

        foreach (SPContentType type in types)
        {
            if (type.Parent.Id == id && type.Parent.Id != type.Id)
            {
                Response.Write("<li><a class='ms-topnav'" 
                + " href=\"/_layouts/ManageContentType.aspx?ctype=" +
               type.Id.ToString() + "\">" + type.Name +
               "</a><span class='ms-webpartpagedescription'>" +
               type.Description + "</span></li>");
                ShowChildren(type.Id);
            }
        }

        Response.Write("</ol>");

    }
</script>