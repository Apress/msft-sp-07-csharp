using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;

namespace EditorPartDemo
{
    public class PhoneEditor : EditorPart
    {
        TextBox property = null;
        Label messages = null;

        protected override void CreateChildControls()
        {
            property = new TextBox();
            Controls.Add(property);

            messages = new Label();
            Controls.Add(messages);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            property.RenderControl(writer);
            writer.Write("<br/>");
            messages.RenderControl(writer);
        }

        public override bool ApplyChanges()
        {
            try
            {
                Regex expression = new Regex(@"\(\d\d\d\)\s\d\d\d-\d\d\d\d");
                Match match = expression.Match(property.Text);
                if (match.Success == true)
                {
                    ((PhoneLabel)WebPartToEdit).Phone = property.Text;
                    messages.Text = "";
                }
                else
                {
                    ((PhoneLabel)WebPartToEdit).Phone = "Invalid phone number";
                }
            }
            catch (Exception x)
            {
                messages.Text += x.Message;
            }
            return true;
        }

        public override void SyncChanges()
        {
            try
            {
                property.Text = ((PhoneLabel)WebPartToEdit).Phone;
            }
            catch { }
        }

    }
    public class PhoneLabel : WebPart
    {
        protected string m_phone;

        [Personalizable(PersonalizationScope.Shared), WebBrowsable(false),
        WebDisplayName("Phone"),
        WebDescription("Phone number")]
        public string Phone
        {
            get { return m_phone; }
            set { m_phone = value; }
        }

        public override EditorPartCollection CreateEditorParts()
        {
            ArrayList partsArray = new ArrayList();

            PhoneEditor phonePart = new PhoneEditor();
            phonePart.ID = this.ID + "_editorPart1";
            phonePart.Title = "Phone Number";
            phonePart.GroupingText = "(xxx) xxx-xxxx";
            partsArray.Add(phonePart);

            EditorPartCollection parts = new EditorPartCollection(partsArray);
            return parts;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("<p>" + m_phone + "</p>");
        }
    }

}
