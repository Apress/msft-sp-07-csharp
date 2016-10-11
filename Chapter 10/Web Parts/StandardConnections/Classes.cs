using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.ComponentModel;

namespace StandardConnections
{
    public class TextProvider : WebPart, IWebPartField
    {
        //Member variables
        protected Button button = null;
        protected TextBox text = null;
        string m_data = null;

        //Text Property
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(false),
        WebDisplayName("Text"),
        WebDescription("The text to send")]
        public string Text
        {
            get { return m_data; }
            set { m_data = value; }
        }

        //Child controls
        protected override void CreateChildControls()
        {
            button = new Button();
            button.Text = "Send Data";
            button.Click += new EventHandler(button_Click);
            Controls.Add(button);

            text = new TextBox();
            Controls.Add(text);
        }

        //Show UI
        protected override void RenderContents(HtmlTextWriter writer)
        {
            button.RenderControl(writer);
            text.RenderControl(writer);
        }

        //The connection description
        [ConnectionProvider("Text")]
        public IWebPartField ConnectionInterface()
        {
            return this;
        }

        //Callback object
        public void GetFieldValue(FieldCallback callback)
        {
            //Send data to consumer
            callback.Invoke(text.Text);
        }

        //Publish schema
        public PropertyDescriptor Schema
        {
            get
            { return TypeDescriptor.GetProperties(this)["Text"]; }
        }

        void button_Click(object sender, EventArgs e)
        {
            m_data = text.Text;
        }

    }

    public class TextConsumer : WebPart
    {
        string data;

        [ConnectionConsumer("Text")]
        public void GetConnectionInterface(IWebPartField providerPart)
        {
            FieldCallback callback = new FieldCallback(ReceiveField);
            providerPart.GetFieldValue(callback);
        }

        public void ReceiveField(object field)
        {
            data = (string)field;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                writer.Write(data);
            }
            catch
            {
                writer.Write("No connection.");
            }
        }
    }

}
