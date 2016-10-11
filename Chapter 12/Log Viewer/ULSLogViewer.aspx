<%@ Page Language="C#" MasterPageFile="~/_admin/admin.master" %>

<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages.Administration, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.Administration" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Data" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
    Unified Logging Service (ULS) Logs
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="PlaceHolderPageDescription">
    This page allows you to browse the Unified Logging Service logs
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="PlaceHolderMain">
    <table border="0" cellspacing="0" cellpadding="0" class="ms-propertysheet" width="100%">
        <tr>
            <td>
                <!-- This section lists the log files that are on the server -->
                <wssuc:InputFormSection Title="Available log files" runat="server">
                    <template_description>
			            <asp:Literal Id="Literal1" runat="server" text="These are the available log files that you can view."/>
		            </template_description>
                    <template_inputformcontrols>
			            <wssuc:InputFormControl runat="server" LabelText="Select a log file to view">
				            <Template_control>
                                <asp:DropDownList ID="listFiles" runat="server" EnableViewState="true" Width="100%"/>
				            </Template_control>
			            </wssuc:InputFormControl>
		            </template_inputformcontrols>
                </wssuc:InputFormSection>
                <!-- This section allows filtering of the logs by category and severity -->
                <wssuc:InputFormSection Title="Log Filtering" runat="server">
                    <template_description>
			            <asp:Literal runat="server" text="Select the keywords to use when filtering the logs"/>
		            </template_description>
                    <template_inputformcontrols>
					            <TABLE border="0" cellspacing="0" cellpadding="0" width="100%">
						            <tr><td>
			                        <wssuc:InputFormControl runat="server" LabelText="This category..." LabelAssociatedControlId="listCategories">
				                        <Template_control>
							            <asp:DropDownList Id="listCategories" runat="server" EnableViewState="true">
	                                        <asp:ListItem value="empty" Text=""/>
	                                        <asp:ListItem value="Administration" Text="Administration"/>
	                                        <asp:ListItem value="Backup and Restore" Text="Backup and Restore"/>
	                                        <asp:ListItem value="Backward Compatible" Text="Backward Compatible Administration and Object Model"/>
	                                        <asp:ListItem value="Business Data" Text="Business Data Catalog"/>
	                                        <asp:ListItem value="Communication" Text="Communication"/>
	                                        <asp:ListItem value="Content Deployment" Text="Content Deployment"/>
	                                        <asp:ListItem value="Database" Text="Database"/>
	                                        <asp:ListItem value="Document Management" Text="Document Management"/>
	                                        <asp:ListItem value="E-Mail" Text="E-Mail" />
	                                        <asp:ListItem value="Excel" Text="Excel Services"/>
	                                        <asp:ListItem value="Feature Infrastructure" Text="Feature Infrastructure"/>
	                                        <asp:ListItem value="Fields" Text="Fields"/>
	                                        <asp:ListItem value="Forms Services" Text="Forms Services"/>
	                                        <asp:ListItem value="General" Text="General"/>
	                                        <asp:ListItem value="Information Policy Management" Text="Information Policy Management"/>
	                                        <asp:ListItem value="IRM" Text="Information Rights Management"/>
	                                        <asp:ListItem value="Knowledge Network Server" Text="Knowledge Network Server"/>
	                                        <asp:ListItem value="Launcher Service" Text="Launcher Service"/>
	                                        <asp:ListItem value="Load Balancer Service" Text="Load Balancer Service"/>
	                                        <asp:ListItem value="Long running operation infrastructure" Text="Long running operation infrastructure"/>
	                                        <asp:ListItem value="MCMS 2002 Migration" Text="MCMS 2002 Migration"/>
	                                        <asp:ListItem value="Office Server" Text="MOSS General"/>
	                                        <asp:ListItem value="Shared Services" Text="MOSS Shared Services"/>
	                                        <asp:ListItem value="MS Search" Text="MS Search"/>
	                                        <asp:ListItem value="Project Server" Text="Project Server"/>
	                                        <asp:ListItem value="Publishing" Text="Publishing Features"/>
	                                        <asp:ListItem value="Records Center" Text="Records Center"/>
	                                        <asp:ListItem value="Runtime" Text="Runtime"/>
	                                        <asp:ListItem value="Server Help" Text="Server Help"/>
	                                        <asp:ListItem value="Session State Service" Text="Session State Service"/>
	                                        <asp:ListItem value="Setup and Upgrade" Text="Setup and Upgrade"/>
	                                        <asp:ListItem value="SharePoint Services" Text="SharePoint Services"/>
	                                        <asp:ListItem value="Site Directory" Text="Site Directory"/>
	                                        <asp:ListItem value="Site Management" Text="Site Management"/>
	                                        <asp:ListItem value="SSO" Text="SSO"/>
	                                        <asp:ListItem value="Timer" Text="Timer"/>
	                                        <asp:ListItem value="Topology" Text="Topology"/>
	                                        <asp:ListItem value="Unified Logging Service" Text="Unified Logging Service"/>
	                                        <asp:ListItem value="Upgrade" Text="Upgrade"/>
	                                        <asp:ListItem value="User Profiles" Text="User Profiles"/>
	                                        <asp:ListItem value="Web Controls" Text="Web Controls"/>
	                                        <asp:ListItem value="Web Parts" Text="Web Parts"/>
	                                        <asp:ListItem value="WebParts" Text="WebParts"/>
	                                        <asp:ListItem value="Workflow" Text="Workflow"/>
	                                     </asp:DropDownList>
							            </Template_control>
						            </wssuc:InputFormControl>
						            </td></tr>
						            <tr><td>
						            <wssuc:InputFormControl runat="server" LabelText="AND this event severity..." LabelAssociatedControlId="listEvent" >
							            <Template_control>
								            <asp:DropDownList id="listEvent" runat="server" EnableViewState="true">
									            <asp:ListItem Value="empty" Text=""/>
									            <asp:ListItem Value="Error" Text="Error"/>
									            <asp:ListItem Value="Warning" Text="Warning"/>
									            <asp:ListItem Value="Failure" Text="Failure"/>
									            <asp:ListItem Value="Critical" Text="Critical"/>
									            <asp:ListItem Value="Success" Text="Success"/>
									            <asp:ListItem Value="Information" Text="Information"/>
								            </asp:DropDownList>
							            </Template_control>
						            </wssuc:InputFormControl>
						            </td></tr>
						            <tr><td>
						            <wssuc:InputFormControl runat="server" LabelText="OR this trace severity" LabelAssociatedControlId="listTrace" >
							            <Template_control>
								            <asp:DropDownList id="listTrace" runat="server" EnableViewState="true">
									            <asp:ListItem Value="empty" Text=""/>
									            <asp:ListItem Value="Unexpected" Text="Unexpected"/>
									            <asp:ListItem Value="Monitorable" Text="Monitorable"/>
									            <asp:ListItem Value="High" Text="High"/>
									            <asp:ListItem Value="Medium" Text="Medium"/>
									            <asp:ListItem Value="Verbose" Text="Verbose"/>
								            </asp:DropDownList>
							            </Template_control>
						            </wssuc:InputFormControl>
						            </td></tr>					            
						        </TABLE>
		            </template_inputformcontrols>
                </wssuc:InputFormSection>
                <!-- This section contains a button to show the logs -->
                <wssuc:InputFormSection Title="Show the log" runat="server">
                    <template_description>
			            <asp:Literal Id="Literal3" runat="server" text="Press the button to view the selected log."/><br />
			            <asp:Literal Id="Literal2" runat="server" text="Note: If the log file is large, it can take some time to process and display."/>
		            </template_description>
                    <template_inputformcontrols>
			            <wssuc:InputFormControl runat="server" LabelText="">
				            <Template_control>
                                <asp:Button Id="buttonGo" Text="Go" Width="60px" runat="server" onclick="FillGrid"/>
				            </Template_control>
			            </wssuc:InputFormControl>
		            </template_inputformcontrols>
                </wssuc:InputFormSection>
            </td>
        </tr>
    </table>
    <!-- This table shows the selected log entries -->
    <table border="0" cellspacing="0" cellpadding="0" class="ms-propertysheet" width="100%">
        <tr>
            <td>
                <asp:Label runat="server" ID="message" class="ms-error" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid runat="server" ID="gridLog" Width="100%" class="ms-descriptionText"
                    GridLines="Horizontal" />
            </td>
        </tr>
    </table>
</asp:Content>

<script runat="server">

    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            try
            {
                //Open the farm
                SPFarm farm = SPFarm.Open("Data Source=LITWARESERVER;Initial Catalog=SharePoint_Config;Integrated Security=SSPI");

                //Get reference to ULS
                SPDiagnosticsService diagnostics = new SPDiagnosticsService("log", farm);
                string[] files = Directory.GetFiles(diagnostics.LogLocation,"*.log");

                //Show list of logs
                DataTable lines = new DataTable("Lines");
                lines.Columns.Add("Name", typeof(string));
                lines.Columns.Add("Path", typeof(string));

                for (int i = files.Length - 1; i>-1; i--)
                {
                    if (files[i].IndexOf("Diagnostics") == -1)
                    {
                        string[] line = { files[i].Substring(files[i].LastIndexOf("\\") + 1), files[i] };
                        lines.Rows.Add(line);
                    }
                }
                
                listFiles.DataSource = lines;
                listFiles.DataTextField = "Name";
                listFiles.DataValueField = "Path";
                listFiles.DataBind();
                listFiles.Items.Insert(0, "Select a file...");


            }
            catch (Exception x)
            {
                message.Text = x.Message;
            }
        }
    }

    protected void FillGrid(object sender, EventArgs e)
    {

        try
        {
            message.Text = "";
            
            //Create table
            DataTable entries = new DataTable("Entries");
            entries.Columns.Add("Timestamp", typeof(string));
            entries.Columns.Add("Process", typeof(string));
            entries.Columns.Add("TID", typeof(string));
            entries.Columns.Add("Area", typeof(string));
            entries.Columns.Add("Category", typeof(string));
            entries.Columns.Add("EventID", typeof(string));
            entries.Columns.Add("Level", typeof(string));
            entries.Columns.Add("Message", typeof(string));
            entries.Columns.Add("Correlation", typeof(string));

            //Fill table
            StreamReader reader = new StreamReader(listFiles.SelectedValue);
            reader.ReadLine().Split(new char[] { '\t' });

            while (!reader.EndOfStream)
            {
                string[] fields = reader.ReadLine().Split(new char[] { '\t' });
                
               //Category selected
                if(listCategories.Text != "empty")
                {
                    //Category and Level selected
                    if(listEvent.Text != "empty" || listTrace.Text != "empty")
                    {
                        if(fields[4].IndexOf(listCategories.Text) >= 0
                           &&
                           (fields[6].IndexOf(listEvent.Text) >= 0 || fields[6].IndexOf(listTrace.Text) >= 0))
                        {
                            entries.Rows.Add(fields);
                        }

                    }
                    //Category, but no Level selected
                    else
                    {
                        if(fields[4].IndexOf(listCategories.Text) >= 0)
                        {
                            entries.Rows.Add(fields);
                        }
                    }
                }
                    
                //Category not selected
                else
                {
                    //Level selected
                    if(listEvent.Text != "empty" || listTrace.Text != "empty")
                    {
                        if(fields[6].IndexOf(listEvent.Text) >= 0 || fields[6].IndexOf(listTrace.Text) >= 0)
                        {
                            entries.Rows.Add(fields);
                        }
                    }
                    
                    //Nothing selected
                    else
                    {
                        entries.Rows.Add(fields);
                    }
                }
            }

            //Show in grid
            gridLog.DataSource = entries;
            gridLog.DataBind();
        }
            
        catch (Exception x)
        {
            message.Text = x.Message;
        }
    }
         
</script>

