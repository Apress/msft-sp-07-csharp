/* This web part writes the text from a property to a Text Box
 * when a button is pushed.
 */ 

using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Hello
{  
    public class SharePoint:WebPart
    {
        protected Button myButton;
        protected TextBox myTextBox;



        private string _content;

        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true),
        WebDisplayName("Content"),WebDescription("The content to show")]
        public string Content
        {
            get{return _content;}
            set { _content = value; }
        }

        protected override void CreateChildControls()
        {
            myTextBox = new TextBox();
            this.Controls.Add(myTextBox);

            myButton = new Button();
            myButton.Text = "Push Me!";
            myButton.Click += new EventHandler(myButton_Click);
            this.Controls.Add(myButton);
            
        }

        void myButton_Click(object sender, EventArgs e)
        {
            myTextBox.Text = Content;
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            myButton.RenderControl(writer);
            myTextBox.RenderControl(writer);
        }
    }
}
