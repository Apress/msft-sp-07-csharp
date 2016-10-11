using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.Office.Workflow.Utility;
using System.Diagnostics;

namespace ReviewFlow
{
    public sealed partial class Workflow1 : SharePointSequentialWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties workflowProperties = new Microsoft.SharePoint.Workflow.SPWorkflowActivationProperties();
        public string managerUsername = default(string);
        public string managerFullname = default(string);
        public string reviewType = default(string);
        public string reviewComments = default(string);

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                //Save the identifier for the workflow
                workflowId = workflowProperties.WorkflowId;

                // InitiationData comes from the initialization form
                XmlDocument document = new XmlDocument();
                document.LoadXml(workflowProperties.InitiationData);

                XmlNamespaceManager ns = new XmlNamespaceManager(document.NameTable);
                ns.AddNamespace("my", "http://schemas.microsoft.com/office/infopath/2003/myXSD/2006-12-05T20:04:17");
                managerUsername = document.SelectSingleNode("/my:flowFields/my:managerUsername", ns).InnerText;
                managerFullname = document.SelectSingleNode("/my:flowFields/my:managerFullname", ns).InnerText;
                reviewType = document.SelectSingleNode("/my:flowFields/my:reviewType", ns).InnerText;
                reviewComments = document.SelectSingleNode("/my:flowFields/my:reviewComments", ns).InnerText;
            }
            catch (Exception x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
        }

        public Guid taskId = default(System.Guid);
        public SPWorkflowTaskProperties taskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void createTask1_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                // Create unique task ID
                taskId = Guid.NewGuid();

                //Set task properties
                taskProperties.AssignedTo = managerUsername;
                taskProperties.Description = reviewComments + "/n";
                taskProperties.Title = "Perform employee review [" + reviewType + "]";

                //Populate the action form using the secondary data source
                taskProperties.ExtendedProperties["reviewComments"] = reviewComments;
            }
            catch (Exception x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }

        }

        //Looping
        private bool complete = false;
        private void notComplete(object sender, ConditionalEventArgs e)
        {
            e.Result = !complete;
        }


        public SPWorkflowTaskProperties afterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties beforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void onTaskChanged1_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                complete = bool.Parse(afterProperties.ExtendedProperties["reviewCompleted"].ToString());
            }
            catch (Exception x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
        }

        private void completeTask1_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                afterProperties.Description += afterProperties.ExtendedProperties["reviewNotes"].ToString();
                afterProperties.PercentComplete = 100;
            }
            catch (Exception x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
        }

        private void logMessage(string message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists("SharePoint Workflow"))
                EventLog.CreateEventSource("SharePoint Workflow", "Application");
            EventLog.WriteEntry("SharePoint Workflow", message, type);
        }
    }

}


