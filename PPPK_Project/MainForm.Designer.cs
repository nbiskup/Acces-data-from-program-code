namespace PPPK_Project
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbQuer = new System.Windows.Forms.TabControl();
            this.tpSql = new System.Windows.Forms.TabPage();
            this.lblError = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbProcedure = new System.Windows.Forms.TextBox();
            this.lbProcedureParameters = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbProcedures = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbViewColumns = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbViews = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbTableColumns = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpQuery = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tlpResults = new System.Windows.Forms.TableLayoutPanel();
            this.tbMessages = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.cbUseDatabase = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbQuer.SuspendLayout();
            this.tpSql.SuspendLayout();
            this.tpQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbQuer
            // 
            this.tbQuer.Controls.Add(this.tpSql);
            this.tbQuer.Controls.Add(this.tpQuery);
            this.tbQuer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQuer.Location = new System.Drawing.Point(0, 0);
            this.tbQuer.Name = "tbQuer";
            this.tbQuer.SelectedIndex = 0;
            this.tbQuer.Size = new System.Drawing.Size(1234, 654);
            this.tbQuer.TabIndex = 0;
            // 
            // tpSql
            // 
            this.tpSql.Controls.Add(this.lblError);
            this.tpSql.Controls.Add(this.label8);
            this.tpSql.Controls.Add(this.tbProcedure);
            this.tpSql.Controls.Add(this.lbProcedureParameters);
            this.tpSql.Controls.Add(this.label7);
            this.tpSql.Controls.Add(this.lbProcedures);
            this.tpSql.Controls.Add(this.label6);
            this.tpSql.Controls.Add(this.lbViewColumns);
            this.tpSql.Controls.Add(this.label5);
            this.tpSql.Controls.Add(this.lbViews);
            this.tpSql.Controls.Add(this.label4);
            this.tpSql.Controls.Add(this.lbTableColumns);
            this.tpSql.Controls.Add(this.label3);
            this.tpSql.Controls.Add(this.lbTables);
            this.tpSql.Controls.Add(this.label2);
            this.tpSql.Controls.Add(this.cbDatabases);
            this.tpSql.Controls.Add(this.label1);
            this.tpSql.Location = new System.Drawing.Point(4, 22);
            this.tpSql.Name = "tpSql";
            this.tpSql.Padding = new System.Windows.Forms.Padding(3);
            this.tpSql.Size = new System.Drawing.Size(1226, 628);
            this.tpSql.TabIndex = 0;
            this.tpSql.Text = "Sql manager";
            this.tpSql.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(532, 35);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(324, 371);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Procedure definition:";
            // 
            // tbProcedure
            // 
            this.tbProcedure.Location = new System.Drawing.Point(434, 371);
            this.tbProcedure.Multiline = true;
            this.tbProcedure.Name = "tbProcedure";
            this.tbProcedure.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProcedure.Size = new System.Drawing.Size(470, 238);
            this.tbProcedure.TabIndex = 14;
            // 
            // lbProcedureParameters
            // 
            this.lbProcedureParameters.FormattingEnabled = true;
            this.lbProcedureParameters.Location = new System.Drawing.Point(1043, 382);
            this.lbProcedureParameters.Name = "lbProcedureParameters";
            this.lbProcedureParameters.Size = new System.Drawing.Size(159, 238);
            this.lbProcedureParameters.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(925, 382);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Procedure parameters:";
            // 
            // lbProcedures
            // 
            this.lbProcedures.FormattingEnabled = true;
            this.lbProcedures.Location = new System.Drawing.Point(84, 371);
            this.lbProcedures.Name = "lbProcedures";
            this.lbProcedures.Size = new System.Drawing.Size(233, 238);
            this.lbProcedures.TabIndex = 11;
            this.lbProcedures.SelectedIndexChanged += new System.EventHandler(this.lbProcedures_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 371);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Procedures:";
            // 
            // lbViewColumns
            // 
            this.lbViewColumns.FormattingEnabled = true;
            this.lbViewColumns.Location = new System.Drawing.Point(1043, 90);
            this.lbViewColumns.Name = "lbViewColumns";
            this.lbViewColumns.Size = new System.Drawing.Size(159, 238);
            this.lbViewColumns.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(957, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "View Columns:";
            // 
            // lbViews
            // 
            this.lbViews.FormattingEnabled = true;
            this.lbViews.Location = new System.Drawing.Point(671, 90);
            this.lbViews.Name = "lbViews";
            this.lbViews.Size = new System.Drawing.Size(233, 238);
            this.lbViews.TabIndex = 7;
            this.lbViews.SelectedIndexChanged += new System.EventHandler(this.lbViews_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(614, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Views:";
            // 
            // lbTableColumns
            // 
            this.lbTableColumns.FormattingEnabled = true;
            this.lbTableColumns.Location = new System.Drawing.Point(434, 90);
            this.lbTableColumns.Name = "lbTableColumns";
            this.lbTableColumns.Size = new System.Drawing.Size(159, 238);
            this.lbTableColumns.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(348, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Table Columns:";
            // 
            // lbTables
            // 
            this.lbTables.FormattingEnabled = true;
            this.lbTables.Location = new System.Drawing.Point(84, 90);
            this.lbTables.Name = "lbTables";
            this.lbTables.Size = new System.Drawing.Size(233, 238);
            this.lbTables.TabIndex = 3;
            this.lbTables.SelectedIndexChanged += new System.EventHandler(this.lbTables_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tables:";
            // 
            // cbDatabases
            // 
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(84, 27);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(164, 21);
            this.cbDatabases.TabIndex = 1;
            this.cbDatabases.SelectedIndexChanged += new System.EventHandler(this.cbDatabases_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Databases:";
            // 
            // tpQuery
            // 
            this.tpQuery.Controls.Add(this.label11);
            this.tpQuery.Controls.Add(this.label10);
            this.tpQuery.Controls.Add(this.tlpResults);
            this.tpQuery.Controls.Add(this.tbMessages);
            this.tpQuery.Controls.Add(this.btnExecute);
            this.tpQuery.Controls.Add(this.tbQuery);
            this.tpQuery.Controls.Add(this.cbUseDatabase);
            this.tpQuery.Controls.Add(this.label9);
            this.tpQuery.Location = new System.Drawing.Point(4, 22);
            this.tpQuery.Name = "tpQuery";
            this.tpQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tpQuery.Size = new System.Drawing.Size(1226, 628);
            this.tpQuery.TabIndex = 1;
            this.tpQuery.Text = "Query";
            this.tpQuery.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(642, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Messages:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(642, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Results:";
            // 
            // tlpResults
            // 
            this.tlpResults.ColumnCount = 1;
            this.tlpResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.Location = new System.Drawing.Point(645, 304);
            this.tlpResults.Name = "tlpResults";
            this.tlpResults.RowCount = 1;
            this.tlpResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpResults.Size = new System.Drawing.Size(547, 277);
            this.tlpResults.TabIndex = 18;
            // 
            // tbMessages
            // 
            this.tbMessages.Location = new System.Drawing.Point(645, 112);
            this.tbMessages.Multiline = true;
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessages.Size = new System.Drawing.Size(547, 112);
            this.tbMessages.TabIndex = 17;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(120, 424);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(158, 58);
            this.btnExecute.TabIndex = 16;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(24, 112);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbQuery.Size = new System.Drawing.Size(399, 273);
            this.tbQuery.TabIndex = 15;
            // 
            // cbUseDatabase
            // 
            this.cbUseDatabase.FormattingEnabled = true;
            this.cbUseDatabase.Location = new System.Drawing.Point(88, 33);
            this.cbUseDatabase.Name = "cbUseDatabase";
            this.cbUseDatabase.Size = new System.Drawing.Size(164, 21);
            this.cbUseDatabase.TabIndex = 3;
            this.cbUseDatabase.SelectedIndexChanged += new System.EventHandler(this.cbUseDatabase_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Databases:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 654);
            this.Controls.Add(this.tbQuer);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.tbQuer.ResumeLayout(false);
            this.tpSql.ResumeLayout(false);
            this.tpSql.PerformLayout();
            this.tpQuery.ResumeLayout(false);
            this.tpQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbQuer;
        private System.Windows.Forms.TabPage tpSql;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbProcedure;
        private System.Windows.Forms.ListBox lbProcedureParameters;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lbProcedures;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbViewColumns;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lbViews;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbTableColumns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDatabases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpQuery;
        private System.Windows.Forms.TableLayoutPanel tlpResults;
        private System.Windows.Forms.TextBox tbMessages;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.ComboBox cbUseDatabase;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblError;
    }
}