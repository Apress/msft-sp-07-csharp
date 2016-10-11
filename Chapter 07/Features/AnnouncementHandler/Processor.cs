using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;

namespace AnnouncementHandler
{
    public class Processor : SPItemEventReceiver
    {
        public override void ItemAdding(SPItemEventProperties properties)
        {
            properties.AfterProperties["Body"] += "\n **For internal use only **\n";
        }

        public override void ItemDeleting(SPItemEventProperties properties)
        {
            properties.Cancel = true;
            properties.ErrorMessage = "Items cannot be deleted from this list.";
        }
    }
}
