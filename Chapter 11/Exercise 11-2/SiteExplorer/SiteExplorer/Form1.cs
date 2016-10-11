using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Xml;

namespace SiteExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conn =
            "Integrated Security=SSPI;" +
            "Initial Catalog=WSS_Content" +
            ";Data Source=VSSQL";

            string sql = "SELECT Title, FullUrl " +
            "FROM dbo.Webs " +
            "WHERE (ParentWebId IS NULL) AND (FullUrl <> '') " +
            "AND (FullUrl IS NOT NULL) " +
            "ORDER BY Title";

            try
            {
                //Return the sites
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    DataSet dataSet = new DataSet("root");
                    adapter.SelectCommand = new SqlCommand(sql, connection);
                    adapter.Fill(dataSet, "Sites");

                    //Put top-level sites in tree
                    DataRowCollection siteRows = dataSet.Tables["Sites"].Rows;

                    foreach (DataRow siteRow in siteRows)
                    {
                        TreeNode treeNode = new TreeNode();
                        treeNode.Text = siteRow["Title"].ToString();
                        treeNode.Tag = "http://VSMOSS/" +
                        siteRow["FullUrl"].ToString();
                        treeView1.Nodes.Add(treeNode);
                        fillTree(treeNode);
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }
        private void fillTree(TreeNode parent)
        {
            //Redirect web service
            MOSSService.Webs service = new MOSSService.Webs();
            service.Url = parent.Tag.ToString() + "/_vti_bin/Webs.asmx";
            service.Credentials = CredentialCache.DefaultCredentials;

            //Get child webs
            XmlNode nodes = service.GetWebCollection();

            //Add child webs to tree
            foreach (XmlNode node in nodes)
            {
                TreeNode child = new TreeNode();
                child.Text = node.Attributes["Title"].Value;
                child.Tag = node.Attributes["Url"].Value;
                parent.Nodes.Add(child);
                fillTree(child);

            }
        }
    }
}