//This web part generates a script block that looks like this
/*
<script type="text/javascript">
 <!--
  code
 // -->
</script>
 */

//The following are included in the project automatically
using System;
using System.Collections.Generic;
using System.Text;

//The following were added manually after setting a reference to System.Web
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace SPScriptBlock
{
    public class Block : WebPart
    {
        string m_scriptBlock = "";
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
        WebDisplayName("Script"),
        WebDescription("The JavaScript to insert in the page.")]
        public string Script
        {
            get { return m_scriptBlock; }
            set { m_scriptBlock = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (m_scriptBlock != "" &&
               !Page.ClientScript.IsClientScriptBlockRegistered(m_scriptKey))
                Page.ClientScript.RegisterClientScriptBlock(
                  typeof(string), m_scriptKey, m_scriptBlock, true);

        }

    }
}