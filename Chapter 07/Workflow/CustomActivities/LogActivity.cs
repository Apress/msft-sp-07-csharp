using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;

namespace CustomActivities
{
    [Designer(typeof(ActivityDesigner), typeof(IDesigner)),
    ToolboxItem(typeof(ActivityToolboxItem)),
    Description("Logging Activity"),
    ActivityValidator(typeof(LogActivityValidator))]
    public sealed class LogActivity : Activity
    {
        //Name of the Log
        public static DependencyProperty LogNameProperty =
        DependencyProperty.Register("LogName", typeof(string), typeof(LogActivity));

        public string LogName
        {
            get { return ((string)(base.GetValue(LogActivity.LogNameProperty))); }
            set { base.SetValue(LogActivity.LogNameProperty, value); }
        }

        //Message Property
        public static DependencyProperty MessageProperty =
        DependencyProperty.Register("Message", typeof(string), typeof(LogActivity));

        public string Message
        {
            get { return ((string)(base.GetValue(LogActivity.MessageProperty))); }
            set { base.SetValue(LogActivity.MessageProperty, value); }
        }

        //Entry Type
        public static DependencyProperty EntryTypeProperty =
       DependencyProperty.Register("EntryType", typeof(string), typeof(LogActivity));

        public string EntryType
        {
            get { return ((string)(base.GetValue(LogActivity.EntryTypeProperty))); }
            set { base.SetValue(LogActivity.EntryTypeProperty, value); }
        }

        protected override ActivityExecutionStatus
          Execute(ActivityExecutionContext executionContext)
        {
            if (!EventLog.SourceExists(LogName))
                EventLog.CreateEventSource(LogName, "Application");

            switch (EntryType)
            {
                case "Error":
                    EventLog.WriteEntry(LogName, Message, EventLogEntryType.Error);
                    break;
                case "Failure":
                    EventLog.WriteEntry(LogName, Message, EventLogEntryType.FailureAudit);
                    break;
                case "Information":
                    EventLog.WriteEntry(LogName, Message, EventLogEntryType.Information);
                    break;
                case "Success":
                    EventLog.WriteEntry(LogName, Message, EventLogEntryType.SuccessAudit);
                    break;
                case "Warning":
                    EventLog.WriteEntry(LogName, Message, EventLogEntryType.Warning);
                    break;
            }

            return ActivityExecutionStatus.Closed;
        }
    }

    public class LogActivityValidator : ActivityValidator
    {
        public override ValidationErrorCollection
          ValidateProperties(ValidationManager manager, object obj)
        {
            ValidationErrorCollection errors = new ValidationErrorCollection();
            LogActivity activity = obj as LogActivity;

            if (activity == null)
                errors.Add(new ValidationError("Not a valid activity.", 1));
            else
            {
                if (activity.LogName == null)
                    errors.Add(new ValidationError("Not a valid log name.", 2));
                if (activity.Message == null)
                    errors.Add(new ValidationError("Not a valid message.", 3));
                if (activity.EntryType == null)
                    errors.Add(new ValidationError("Not a valid entry type.", 4));
            }
            return errors;
        }
    }
}
