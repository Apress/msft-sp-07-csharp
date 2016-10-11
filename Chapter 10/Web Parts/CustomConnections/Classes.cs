using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CustomConnections
{
    public interface IStringConnection
    {
        string ProvidedString { get;}
    }


    public class StringProvider : WebPart, IStringConnection
    {

        protected string m_string="Test Data";

        [ConnectionProvider("String Provider")]
        public IStringConnection ConnectionInterface()
        {
            return this;
        }

        //The passed value
        public string ProvidedString
        {
            get { return m_string; }
        }
    }

    public class StringConsumer : WebPart
    {

        IStringConnection m_providerPart = null;

        [ConnectionConsumer("String Consumer")]
        public void GetConnectionInterface(IStringConnection providerPart)
        {
            m_providerPart = providerPart;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                writer.Write(m_providerPart.ProvidedString);
            }
            catch
            {
                writer.Write("No Connection");
            }
        }
    }

}
