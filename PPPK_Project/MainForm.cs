using PPPK_Project.DAL;
using PPPK_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPPK_Project
{
    public partial class MainForm : Form
    {
        private StringBuilder sbQuery = new StringBuilder();

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init() => LoadDatabases();

        private void LoadDatabases()
        {
            cbDatabases.DataSource = new List<Database>(RepositoryFactory.GetRepository().GetDatabases());
            cbUseDatabase.DataSource = new List<Database>(RepositoryFactory.GetRepository().GetDatabases());
        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            lbTables.DataSource = (cbDatabases.SelectedItem as Database).Tables;
            lbViews.DataSource = (cbDatabases.SelectedItem as Database).Views;
            lbProcedures.DataSource = (cbDatabases.SelectedItem as Database).Procedures;
        }

        private void Clear()
        {
            lbTableColumns.DataSource = null;
            lbViewColumns.DataSource = null;
            tbProcedure.Text = string.Empty;
            lbProcedureParameters.DataSource = null;
        }

        private void lbTables_SelectedIndexChanged(object sender, EventArgs e) => lbTableColumns.DataSource = (lbTables.SelectedItem as DBEntity).Columns;

        private void lbViews_SelectedIndexChanged(object sender, EventArgs e) => lbViewColumns.DataSource = (lbViews.SelectedItem as DBEntity).Columns;

        private void lbProcedures_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbProcedure.Text = (lbProcedures.SelectedItem as Procedure).Definition;
            lbProcedureParameters.DataSource = (lbProcedures.SelectedItem as Procedure).Parameters;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

        private void cbUseDatabase_SelectedIndexChanged(object sender, EventArgs e) => ClearQuery();

        private void btnExecute_Click(object sender, EventArgs e)
        {
            sbQuery.AppendLine($"use {(cbUseDatabase.SelectedItem as Database).Name}\n");
            sbQuery.AppendLine(tbQuery.Text.Trim().ToString());
            ClearQuery();

            try
            {
                SelectData(RepositoryFactory.GetRepository().CreateResults(sbQuery.ToString()));
                sbQuery.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SelectData(SqlData sqlData)
        {
            foreach(var table  in sqlData.Data.Tables)
            {
                DataGridView dgv = new DataGridView();

                tlpResults.Controls.Add(dgv);
                dgv.Height = tlpResults.Height;
                dgv.Width = tlpResults.Width;
                dgv.DataSource = table;
            }
            tbMessages.Text = sqlData.Message;
        }

        private void ClearQuery()
        {
            tbMessages.Clear();
            tbQuery.Clear();
            tlpResults.Controls.Clear();
        }

       
    }
}
