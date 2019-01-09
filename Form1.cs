using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_TaxInvoice
{
    public partial class Form1 : Form
    {
       public string FileName;
        DataSet InvData = new DataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter DA = new SqlDataAdapter(string.Format("SELECT * FROM OINV WHERE DocEntry = {0}", textBox1.Text),
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString);
            SqlDataAdapter DB = new SqlDataAdapter(string.Format("SELECT * FROM INV1 WHERE DocEntry = {0}", textBox1.Text),
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString);
            SqlDataAdapter DC = new SqlDataAdapter(string.Format("SELECT * FROM OAIM WHERE DocEntry = {0}", textBox1.Text),
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString);
            DA.Fill(InvData, "OINV");
            DB.Fill(InvData, "INV1");
            DC.Fill(InvData, "OADM");
            // dataGridView1.DataSource = InvData.Tables["OINV"];
            ShowReport frm1 = new ShowReport();
            frm1.DataSource = InvData;
            FileName = DateTime.Now.ToString("hhmmss");
            if (checkBox1.Checked)
            {
                frm1.FileName = FileName;
                frm1.Autosave = true;
                frm1.Show();
                MessageBox.Show("Auto Save Success");
            }
            else
            {
                frm1.Autosave = false;
                frm1.Show();
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PDFA3Invoice pdf = new PDFA3Invoice();

            string base_folder = System.AppDomain.CurrentDomain.BaseDirectory;
            string pdfFilePath = base_folder + "in/pdfA3.pdf";
         //   System.IO.File.WriteAllBytes(pdfFilePath, pdfByte);
            string xmlFilePath = base_folder + "in/ContentInformation.xml";
            string xmlFileName = "ETDA-invoice.xml";
            //System.IO.File.WriteAllBytes(xmlFileName, xmlByte);
          //  System.IO.File.WriteAllText(xmlFilePath, xmlString, System.Text.Encoding.UTF8);

            string xmlVersion = "1.0";
            string documentID = textBox1.Text;//this.invoiceID;
            string documentOID = "";
            /*  Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
              dlg.FileName = this.invoiceID + ".pdf";
              dlg.DefaultExt = ".pdf";
              dlg.Filter = "Pdf Files|*.pdf";
          */
            bool result = true; //dlg.ShowDialog().Value;
            pdfFilePath = string.Format("{0}\\Export\\{1}.pdf", Application.StartupPath, FileName);
            xmlFileName = string.Format("{0}\\Export\\{1}.xml", Application.StartupPath, FileName);
            if (result == true)
            {
                string outputPath = string.Format("{0}\\Merge\\{1}.pdf", Application.StartupPath, FileName);
                pdf.CreatePDFA3Invoice(pdfFilePath, xmlFilePath, xmlFileName, xmlVersion, documentID, documentOID, outputPath, "Tax Invoice");
                //updateRunningNumber();
                this.Close();
            }
            else
            {
              //  createBtn.IsEnabled = true;
            }
        }
    }
}
