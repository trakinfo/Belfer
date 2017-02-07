namespace BelferCS
{
    partial class frmZbiorczaAnalizaOcen
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPrzedmiot = new System.Windows.Forms.RadioButton();
            this.rbNauczyciel = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbRokSzkolny = new System.Windows.Forms.RadioButton();
            this.rbSemestr = new System.Windows.Forms.RadioButton();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.pbrProgress = new System.Windows.Forms.ProgressBar();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.txtSeek = new System.Windows.Forms.TextBox();
            this.tlvAnaliza = new BrightIdeasSoftware.TreeListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLiczbowoProcentowo = new System.Windows.Forms.RadioButton();
            this.rbProcentowo = new System.Windows.Forms.RadioButton();
            this.rbLiczbowo = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAnaliza)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbPrzedmiot);
            this.panel1.Controls.Add(this.rbNauczyciel);
            this.panel1.Location = new System.Drawing.Point(12, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 38);
            this.panel1.TabIndex = 0;
            // 
            // rbPrzedmiot
            // 
            this.rbPrzedmiot.AutoSize = true;
            this.rbPrzedmiot.Location = new System.Drawing.Point(109, 10);
            this.rbPrzedmiot.Name = "rbPrzedmiot";
            this.rbPrzedmiot.Size = new System.Drawing.Size(101, 17);
            this.rbPrzedmiot.TabIndex = 1;
            this.rbPrzedmiot.TabStop = true;
            this.rbPrzedmiot.Text = "wg przedmiotów";
            this.rbPrzedmiot.UseVisualStyleBackColor = true;
            this.rbPrzedmiot.CheckedChanged += new System.EventHandler(this.rbNauczyciel_CheckedChanged);
            // 
            // rbNauczyciel
            // 
            this.rbNauczyciel.AutoSize = true;
            this.rbNauczyciel.Location = new System.Drawing.Point(9, 10);
            this.rbNauczyciel.Name = "rbNauczyciel";
            this.rbNauczyciel.Size = new System.Drawing.Size(94, 17);
            this.rbNauczyciel.TabIndex = 0;
            this.rbNauczyciel.TabStop = true;
            this.rbNauczyciel.Text = "wg nauczycieli";
            this.rbNauczyciel.UseVisualStyleBackColor = true;
            this.rbNauczyciel.CheckedChanged += new System.EventHandler(this.rbNauczyciel_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbRokSzkolny);
            this.panel2.Controls.Add(this.rbSemestr);
            this.panel2.Location = new System.Drawing.Point(746, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(181, 38);
            this.panel2.TabIndex = 0;
            // 
            // rbRokSzkolny
            // 
            this.rbRokSzkolny.AutoSize = true;
            this.rbRokSzkolny.Location = new System.Drawing.Point(89, 10);
            this.rbRokSzkolny.Name = "rbRokSzkolny";
            this.rbRokSzkolny.Size = new System.Drawing.Size(83, 17);
            this.rbRokSzkolny.TabIndex = 1;
            this.rbRokSzkolny.TabStop = true;
            this.rbRokSzkolny.Tag = "R";
            this.rbRokSzkolny.Text = "Rok szkolny";
            this.rbRokSzkolny.UseVisualStyleBackColor = true;
            this.rbRokSzkolny.CheckedChanged += new System.EventHandler(this.rbSemestr_CheckedChanged);
            // 
            // rbSemestr
            // 
            this.rbSemestr.AutoSize = true;
            this.rbSemestr.Location = new System.Drawing.Point(14, 10);
            this.rbSemestr.Name = "rbSemestr";
            this.rbSemestr.Size = new System.Drawing.Size(69, 17);
            this.rbSemestr.TabIndex = 0;
            this.rbSemestr.TabStop = true;
            this.rbSemestr.Tag = "S";
            this.rbSemestr.Text = "Semestr I";
            this.rbSemestr.UseVisualStyleBackColor = true;
            this.rbSemestr.CheckedChanged += new System.EventHandler(this.rbSemestr_CheckedChanged);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Image = global::BelferCS.Properties.Resources.refresh_24;
            this.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdRefresh.Location = new System.Drawing.Point(933, 46);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(93, 36);
            this.cmdRefresh.TabIndex = 246;
            this.cmdRefresh.Text = "Odśwież";
            this.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Image = global::BelferCS.Properties.Resources.close;
            this.cmdClose.Location = new System.Drawing.Point(933, 391);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(93, 35);
            this.cmdClose.TabIndex = 191;
            this.cmdClose.Text = "&Zamknij";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Image = global::BelferCS.Properties.Resources.print_24;
            this.cmdPrint.Location = new System.Drawing.Point(933, 88);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(93, 35);
            this.cmdPrint.TabIndex = 190;
            this.cmdPrint.Text = "&Drukuj ...";
            this.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // pbrProgress
            // 
            this.pbrProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbrProgress.Location = new System.Drawing.Point(156, 432);
            this.pbrProgress.Maximum = 422;
            this.pbrProgress.Name = "pbrProgress";
            this.pbrProgress.Size = new System.Drawing.Size(188, 23);
            this.pbrProgress.Step = 1;
            this.pbrProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbrProgress.TabIndex = 252;
            this.pbrProgress.Visible = false;
            // 
            // Label5
            // 
            this.Label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(660, 435);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(31, 13);
            this.Label5.TabIndex = 250;
            this.Label5.Text = "Filtruj";
            // 
            // Label8
            // 
            this.Label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(13, 435);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(45, 13);
            this.Label8.TabIndex = 248;
            this.Label8.Text = "Rekord:";
            // 
            // lblRecord
            // 
            this.lblRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblRecord.ForeColor = System.Drawing.Color.Red;
            this.lblRecord.Location = new System.Drawing.Point(64, 435);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(61, 13);
            this.lblRecord.TabIndex = 249;
            this.lblRecord.Text = "lblRecord";
            // 
            // txtSeek
            // 
            this.txtSeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSeek.Location = new System.Drawing.Point(697, 432);
            this.txtSeek.Name = "txtSeek";
            this.txtSeek.Size = new System.Drawing.Size(230, 20);
            this.txtSeek.TabIndex = 247;
            this.txtSeek.TextChanged += new System.EventHandler(this.txtSeek_TextChanged);
            // 
            // tlvAnaliza
            // 
            this.tlvAnaliza.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlvAnaliza.CellEditUseWholeCell = false;
            this.tlvAnaliza.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tlvAnaliza.Location = new System.Drawing.Point(12, 46);
            this.tlvAnaliza.Name = "tlvAnaliza";
            this.tlvAnaliza.ShowGroups = false;
            this.tlvAnaliza.Size = new System.Drawing.Size(915, 380);
            this.tlvAnaliza.TabIndex = 253;
            this.tlvAnaliza.UseCompatibleStateImageBehavior = false;
            this.tlvAnaliza.UseHotItem = true;
            this.tlvAnaliza.UseTranslucentHotItem = true;
            this.tlvAnaliza.View = System.Windows.Forms.View.Details;
            this.tlvAnaliza.VirtualMode = true;
            this.tlvAnaliza.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.tlvAnaliza_CellToolTipShowing);
            this.tlvAnaliza.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.tlvAnaliza_ItemSelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbLiczbowoProcentowo);
            this.groupBox1.Controls.Add(this.rbProcentowo);
            this.groupBox1.Controls.Add(this.rbLiczbowo);
            this.groupBox1.Location = new System.Drawing.Point(933, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 115);
            this.groupBox1.TabIndex = 254;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Analizuj";
            // 
            // rbLiczbowoProcentowo
            // 
            this.rbLiczbowoProcentowo.Location = new System.Drawing.Point(6, 65);
            this.rbLiczbowoProcentowo.Name = "rbLiczbowoProcentowo";
            this.rbLiczbowoProcentowo.Size = new System.Drawing.Size(81, 44);
            this.rbLiczbowoProcentowo.TabIndex = 2;
            this.rbLiczbowoProcentowo.Tag = "2";
            this.rbLiczbowoProcentowo.Text = "liczbowo i procentowo";
            this.rbLiczbowoProcentowo.UseVisualStyleBackColor = true;
            this.rbLiczbowoProcentowo.CheckedChanged += new System.EventHandler(this.rbLiczbowo_CheckedChanged);
            // 
            // rbProcentowo
            // 
            this.rbProcentowo.AutoSize = true;
            this.rbProcentowo.Location = new System.Drawing.Point(6, 46);
            this.rbProcentowo.Name = "rbProcentowo";
            this.rbProcentowo.Size = new System.Drawing.Size(81, 17);
            this.rbProcentowo.TabIndex = 1;
            this.rbProcentowo.Tag = "1";
            this.rbProcentowo.Text = "procentowo";
            this.rbProcentowo.UseVisualStyleBackColor = true;
            this.rbProcentowo.CheckedChanged += new System.EventHandler(this.rbLiczbowo_CheckedChanged);
            // 
            // rbLiczbowo
            // 
            this.rbLiczbowo.AutoSize = true;
            this.rbLiczbowo.Checked = true;
            this.rbLiczbowo.Location = new System.Drawing.Point(6, 19);
            this.rbLiczbowo.Name = "rbLiczbowo";
            this.rbLiczbowo.Size = new System.Drawing.Size(66, 17);
            this.rbLiczbowo.TabIndex = 0;
            this.rbLiczbowo.TabStop = true;
            this.rbLiczbowo.Tag = "0";
            this.rbLiczbowo.Text = "liczbowo";
            this.rbLiczbowo.UseVisualStyleBackColor = true;
            this.rbLiczbowo.CheckedChanged += new System.EventHandler(this.rbLiczbowo_CheckedChanged);
            // 
            // frmZbiorczaAnalizaOcen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 467);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tlvAnaliza);
            this.Controls.Add(this.pbrProgress);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.txtSeek);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmZbiorczaAnalizaOcen";
            this.Text = "Zbiorcza analiza ocen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZbiorczaAnalizaOcen_FormClosed);
            this.Load += new System.EventHandler(this.frmZbiorczaAnalizaOcen_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAnaliza)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPrzedmiot;
        private System.Windows.Forms.RadioButton rbNauczyciel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbRokSzkolny;
        private System.Windows.Forms.RadioButton rbSemestr;
        internal System.Windows.Forms.Button cmdPrint;
        internal System.Windows.Forms.Button cmdClose;
        internal System.Windows.Forms.Button cmdRefresh;
        internal System.Windows.Forms.ProgressBar pbrProgress;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label lblRecord;
        internal System.Windows.Forms.TextBox txtSeek;
        private BrightIdeasSoftware.TreeListView tlvAnaliza;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLiczbowoProcentowo;
        private System.Windows.Forms.RadioButton rbProcentowo;
        private System.Windows.Forms.RadioButton rbLiczbowo;
    }
}