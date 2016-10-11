//This part creates the include directive
//<script src="{js file}" type="text/javascript"></script>

//The following are included in the project automatically
using System;
using System.Collections.Generic;
using System.Text;

//The following were added manually after setting a reference to System.Web
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SPScriptInclude
{
    public class Loader : WebPart
    {
        string m_scriptFile = "";
        string m_scriptKey = "scriptKey";

        //Key Property
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true),
        WebDisplayName("Script Key"),
        WebDescription("A unique key for the script.")]
        public string ScriptKey
        {
            get { return m_scriptKey; }
            set { m_scriptKey = value; }
        }

        //Script Property
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true),
        WebDisplayName("Script File"),
        WebDescription("The name of the JavaScript file to insert in the page.")]
        public string ScriptFile
        {
            get { return m_scriptFile; }
            set { m_scriptFile = value; }
        }


        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);

            if (m_scriptFile != "" && !Page.ClientScript.IsClientScriptIncludeRegistered(m_scriptKey))
                Page.ClientScript.RegisterClientScriptInclude(m_scriptKey, Page.ResolveClientUrl(m_scriptFile));


        }

    }
}
