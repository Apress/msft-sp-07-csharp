using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MileageCalculator.ExcelWebServices;
using System.Web.Services.Protocols;

namespace Mileage_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            ExcelService calcSheet = new ExcelService();
            Status[] outStatus;
            RangeCoordinates rangeCoordinates = new RangeCoordinates();
            string sheetName = "Sheet1";
            string targetWorkbookPath = "http://vsmoss/sites/intranet/Docs/Expense%20Calculators/Mileage%20Calculator.xlsx";
            calcSheet.Credentials = System.Net.CredentialCache.DefaultCredentials;

            try
            {
                string id = calcSheet.OpenWorkbook(
                  targetWorkbookPath, "en-US", "en-US", out outStatus);

                object rateCell = calcSheet.GetCell(
                  id, sheetName, 1, 1, true, out outStatus);
                calcSheet.SetCell(id, sheetName, 1, 0, mileage.Value);
                calcSheet.Calculate(id, sheetName, rangeCoordinates);
                object reimbursementCell = calcSheet.GetCell(
                  id, sheetName, 1, 2, true, out outStatus);

                rate.Text = rateCell.ToString();
                reimbursement.Text = reimbursementCell.ToString();

                calcSheet.CloseWorkbook(id);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

        }

    }
}