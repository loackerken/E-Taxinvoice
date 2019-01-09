using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_TaxInvoice
{
    public partial class ShowReport : Form
    {
        public string FileName;
        public DataSet DataSource;
        public bool Autosave;
        public ShowReport()  
        {
            InitializeComponent();
        }

        private void ShowReport_Load(object sender, EventArgs e)
        {
            if (Autosave)
                this.Hide();
            CrystalDecisions.CrystalReports.Engine.ReportDocument RptFile = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            RptFile.Load(Application.StartupPath + "\\Report\\INVReport.rpt");
            RptFile.SetDataSource(DataSource);

            if (Autosave)
            {
                DataSource.WriteXml(string.Format("{0}\\Export\\{1}.xml", Application.StartupPath, FileName));
                RptFile.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,
                    string.Format("{0}\\Export\\{1}.pdf", Application.StartupPath, FileName));
                this.Close();
            }
            else
            {
                crystalReportViewer1.ReportSource = RptFile;
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

