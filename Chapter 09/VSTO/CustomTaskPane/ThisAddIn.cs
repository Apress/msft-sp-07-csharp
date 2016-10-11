using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;

namespace CustomTaskPane
{
    public partial class ThisAddIn
    {
        private NamePane namePane;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            namePane = new NamePane();
            Microsoft.Office.Tools.CustomTaskPane newTaskPane =
              this.CustomTaskPanes.Add(namePane, "Names");
            newTaskPane.Visible = true;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
