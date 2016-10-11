using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace SPDataPart
{
    public class Adventure : WebPart
    {
        DataGrid grid;
        Label messages;

        //Member variables for properties
        protected string m_connection;

        //Connection Property
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(true),
        WebDisplayName("Connection String"),
        WebDescription("The connection string for the AdventureWorks database")]
        public string Connection
        {
            get { return m_connection; }
            set { m_connection = value; }
        }

        //Filter value
        string m_lastName = null;

        //Filter Property
        [Personalizable(PersonalizationScope.Shared), WebBrowsable(false),
        WebDisplayName("Last Name"),
        WebDescription("The last name to use as a filter")]
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        [ConnectionConsumer("Last Name")]
        public void GetConnectionInterface(IWebPartField providerPart)
        {
            FieldCallback callback = new FieldCallback(ReceiveField);
            providerPart.GetFieldValue(callback);
        }

        public void ReceiveField(object field)
        {
            if (field != null)
                LastName = field.ToString();
        }


        protected override void CreateChildControls()
        {
            //Add grid
            grid = new DataGrid();
            grid.AutoGenerateColumns = false;
            grid.Width = Unit.Percentage(100);
            grid.GridLines = GridLines.Horizontal;
            grid.HeaderStyle.CssClass = "ms-vh2";
            grid.CellPadding = 2;

            //Grid columns
            BoundColumn column = new BoundColumn();
            column.DataField = "FullName";
            column.HeaderText = "Associate";
            grid.Columns.Add(column);
            column = new BoundColumn();
            column.DataField = "Title";
            column.HeaderText = "Title";
            grid.Columns.Add(column);

            column = new BoundColumn();
            column.DataField = "SalesTerritory";
            column.HeaderText = "Territory";
            grid.Columns.Add(column);

            column = new BoundColumn();
            column.DataField = "2002";
            column.HeaderText = "2002";
            column.DataFormatString = "{0:C}";
            column.ItemStyle.BackColor = Color.Wheat;
            grid.Columns.Add(column);

            column = new BoundColumn();
            column.DataField = "2003";
            column.HeaderText = "2003";
            column.DataFormatString = "{0:C}";
            grid.Columns.Add(column);

            column = new BoundColumn();
            column.DataField = "2004";
            column.HeaderText = "2004";
            column.DataFormatString = "{0:C}";
            column.ItemStyle.BackColor = Color.Wheat;
            grid.Columns.Add(column);

            Controls.Add(grid);

            //Add label
            messages = new Label();
            Controls.Add(messages);

        }
        protected override void RenderContents(HtmlTextWriter writer)
        {
            DataSet dataSet = null;
            string sql = "SELECT     FullName, Title, SalesTerritory, " +
            "[2002], [2003], [2004] FROM Sales.vSalesPersonSalesByFiscalYears " +
            "WHERE FullName LIKE '%" + LastName + "'";

            //Get data
            using (SqlConnection conn = new SqlConnection(Connection))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    dataSet = new DataSet("root");
                    adapter.Fill(dataSet, "sales");

                }
                catch (SqlException x)
                {
                    messages.Text = x.Message;
                }
                catch (Exception x)
                {
                    messages.Text += x.Message;
                }

            }

            //Bind data
            try
            {
                grid.DataSource = dataSet;
                grid.DataMember = "sales";
                DataBind();
            }
            catch (Exception x)
            {
                messages.Text += x.Message;
            }

            //Display data
            writer.Write("<table border=\"0\" width=\"100%\">");
            writer.Write("<tr><td>");
            grid.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("<tr><td>");
            messages.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("</table>");

        }
    }
}
