namespace BelferCS
{
    partial class frmZbiorczyWykazEgzaminPoprawkowy
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
            this.olvStudent = new BrightIdeasSoftware.ObjectListView();
            this.cbSeek = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.txtSeek = new System.Windows.Forms.TextBox();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbOneSubject = new System.Windows.Forms.RadioButton();
            this.rbTwoSubject = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.olvStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // olvStudent
            // 
            this.olvStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvStudent.CellEditUseWholeCell = false;
            this.olvStudent.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvStudent.Location = new System.Drawing.Point(12, 35);
            this.olvStudent.Name = "olvStudent";
            this.olvStudent.Size = new System.Drawing.Size(787, 391);
            this.olvStudent.TabIndex = 2;
            this.olvStudent.UseCompatibleStateImageBehavior = false;
            this.olvStudent.View = System.Windows.Forms.View.Details;
            // 
            // cbSeek
            // 
            this.cbSeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSeek.DropDownWidth = 200;
            this.cbSeek.FormattingEnabled = true;
            this.cbSeek.Location = new System.Drawing.Point(441, 432);
            this.cbSeek.Name = "cbSeek";
            this.cbSeek.Size = new System.Drawing.Size(122, 21);
            this.cbSeek.TabIndex = 247;
            this.cbSeek.SelectedIndexChanged += new System.EventHandler(this.cbSeek_SelectedIndexChanged);
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(387, 435);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(48, 13);
            this.Label5.TabIndex = 246;
            this.Label5.Text = "Filtruj wg";
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(12, 435);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(45, 13);
            this.Label8.TabIndex = 244;
            this.Label8.Text = "Rekord:";
            // 
            // lblRecord
            // 
            this.lblRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRecord.ForeColor = System.Drawing.Color.Red;
            this.lblRecord.Location = new System.Drawing.Point(63, 435);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(61, 13);
            this.lblRecord.TabIndex = 245;
            this.lblRecord.Text = "lblRecord";
            // 
            // txtSeek
            // 
            this.txtSeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeek.Location = new System.Drawing.Point(569, 432);
            this.txtSeek.Name = "txtSeek";
            this.txtSeek.Size = new System.Drawing.Size(230, 20);
            this.txtSeek.TabIndex = 243;
            this.txtSeek.TextChanged += new System.EventHandler(this.txtSeek_TextChanged);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Image = global::BelferCS.Properties.Resources.refresh_24;
            this.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRefresh.Location = new System.Drawing.Point(805, 35);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(93, 36);
            this.cmdRefresh.TabIndex = 250;
            this.cmdRefresh.Text = "Odśwież";
            this.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Image = global::BelferCS.Properties.Resources.print_24;
            this.cmdPrint.Location = new System.Drawing.Point(805, 77);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(93, 35);
            this.cmdPrint.TabIndex = 249;
            this.cmdPrint.Text = "&Drukuj ...";
            this.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Image = global::BelferCS.Properties.Resources.close;
            this.cmdClose.Location = new System.Drawing.Point(805, 391);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(93, 35);
            this.cmdClose.TabIndex = 251;
            this.cmdClose.Text = "&Zamknij";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(12, 12);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(67, 17);
            this.rbAll.TabIndex = 252;
            this.rbAll.TabStop = true;
            this.rbAll.Tag = "0";
            this.rbAll.Text = "Wszyscy";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbOneSubject
            // 
            this.rbOneSubject.AutoSize = true;
            this.rbOneSubject.Location = new System.Drawing.Point(85, 12);
            this.rbOneSubject.Name = "rbOneSubject";
            this.rbOneSubject.Size = new System.Drawing.Size(127, 17);
            this.rbOneSubject.TabIndex = 253;
            this.rbOneSubject.Tag = "1";
            this.rbOneSubject.Text = "Z jednego przedmiotu";
            this.rbOneSubject.UseVisualStyleBackColor = true;
            this.rbOneSubject.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbTwoSubject
            // 
            this.rbTwoSubject.AutoSize = true;
            this.rbTwoSubject.Location = new System.Drawing.Point(218, 12);
            this.rbTwoSubject.Name = "rbTwoSubject";
            this.rbTwoSubject.Size = new System.Drawing.Size(129, 17);
            this.rbTwoSubject.TabIndex = 254;
            this.rbTwoSubject.Tag = "2";
            this.rbTwoSubject.Text = "Z dwóch przedmiotów";
            this.rbTwoSubject.UseVisualStyleBackColor = true;
            this.rbTwoSubject.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // frmZbiorczyWykazEgzaminPoprawkowy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 467);
            this.Controls.Add(this.rbTwoSubject);
            this.Controls.Add(this.rbOneSubject);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cbSeek);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.txtSeek);
            this.Controls.Add(this.olvStudent);
            this.Name = "frmZbiorczyWykazEgzaminPoprawkowy";
            this.Text = "Wykaz uczniów dopuszczonych do egzaminu poprawkowego";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZbiorczyWykazEgzaminPoprawkowy_FormClosed);
            this.Load += new System.EventHandler(this.frmZbiorczyWykazEgzaminPoprawkowy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.olvStudent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private BrightIdeasSoftware.ObjectListView olvStudent;
        internal System.Windows.Forms.ComboBox cbSeek;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label lblRecord;
        internal System.Windows.Forms.TextBox txtSeek;
        internal System.Windows.Forms.Button cmdRefresh;
        internal System.Windows.Forms.Button cmdPrint;
        internal System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbOneSubject;
        private System.Windows.Forms.RadioButton rbTwoSubject;
    }
}