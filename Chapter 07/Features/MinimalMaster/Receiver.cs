using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using System.Diagnostics;

namespace MinimalMaster
{
    class Receiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {

                SPWeb site = (SPWeb)properties.Feature.Parent;
                logMessage(site.ServerRelativeUrl + "_catalogs/masterpage/minimal.master", EventLogEntryType.Information);

                site.MasterUrl = site.ServerRelativeUrl + "_catalogs/masterpage/minimal.master";
                site.CustomMasterUrl = site.ServerRelativeUrl + "_catalogs/masterpage/minimal.master";
                site.Update();
            }
            catch (SPException x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
            catch (Exception x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            try
            {
                SPWeb site = (SPWeb)properties.Feature.Parent;
                logMessage(site.ServerRelativeUrl + "_catalogs/masterpage/minimal.master", EventLogEntryType.Information);

                site.MasterUrl = site.ServerRelativeUrl +
                "_catalogs/masterpage/default.master";
                site.CustomMasterUrl = site.ServerRelativeUrl +
                "_catalogs/masterpage/default.master";
                site.Update();
            }
            catch (SPException x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
            catch (Exception x)
            {
                logMessage(x.Message, EventLogEntryType.Error);
            }
        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
            logMessage("MinimalMaster Feature installed", EventLogEntryType.SuccessAudit);
        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
            logMessage("MinimalMaster Feature uninstalling", EventLogEntryType.Information);
        }

        private void logMessage(string message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists("SharePoint Features"))
                EventLog.CreateEventSource("SharePoint Features", "Application");
            EventLog.WriteEntry("SharePoint Features", message, type);
        }
    }
}

