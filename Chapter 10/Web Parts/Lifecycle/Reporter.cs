//The following are included in the project automatically
using System;
using System.Collections.Generic;
using System.Text;

//The following were added manually after setting a reference to System.Web
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace SPLifecycle
{
    public class Reporter : WebPart
    {
        //variable for reporting events
        private string m_report = "";

        //variable for button and text
        private Button m_button;
        private TextBox m_text;

        protected override void OnInit(EventArgs e)
        {
            m_report += "OnInit<br/>";
            base.OnInit(e);
        }

        protected override void LoadViewState(object savedState)
        {
            m_report += "LoadViewState<br/>";

            object[] viewState = null;
            if (savedState != null)
            {
                viewState = (object[])savedState;
                base.LoadViewState(viewState[0]);
                m_report += (string)viewState[1] + "<br/>";
            }

        }

        protected override void CreateChildControls()
        {
            m_report += "CreateChildControls<br/>";

            m_button = new Button();
            m_button.Text = "Push Me!";
            m_button.Click += new EventHandler(m_button_Click);
            Controls.Add(m_button);

            m_text = new TextBox();
            Controls.Add(m_text);
        }

        protected override void OnLoad(EventArgs e)
        {
            m_report += "OnLoad<br/>";
            base.OnLoad(e);
        }

        void m_button_Click(object sender, EventArgs e)
        {
            m_report += "Button Click<br/>";
        }

        protected override void OnPreRender(EventArgs e)
        {
            m_report += "OnPreRender<br/>";
            base.OnPreRender(e);
        }

        protected override object SaveViewState()
        {
            m_report += "SaveViewState<br/>";

            object[] viewState = new object[2];
            viewState[0] = base.SaveViewState();
            viewState[1] = "myData";

            return viewState;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            m_report += "RenderContents<br/>";
            writer.Write(m_report);
            m_text.RenderControl(writer);
            m_button.RenderControl(writer);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }


    }
}

