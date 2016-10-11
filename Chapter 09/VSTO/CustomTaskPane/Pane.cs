using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;


namespace CustomTaskPane
{
    public partial class NamePane : UserControl
    {
        public NamePane()
        {
            InitializeComponent();
        }

        private void insert_Click(object sender, EventArgs e)
        {
            Word.Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
            currentRange.Text = list.SelectedItem.ToString();

        }

        private void NamePane_Load(object sender, EventArgs e)
        {
            list.Items.Add("Microsoft SharePoint");
            list.Items.Add("Microsoft Exchange");
            list.Items.Add("Microsoft Windows");
            list.Items.Add("Microsoft Vista");
        }


    }
}
