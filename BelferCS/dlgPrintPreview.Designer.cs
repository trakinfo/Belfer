namespace BelferCS
{
    partial class dlgPrintPreview
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
            this.pvWydruk = new System.Windows.Forms.PrintPreviewControl();
            this.gbZoom = new System.Windows.Forms.GroupBox();
            this.nudZoom = new System.Windows.Forms.NumericUpDown();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.gbFont = new System.Windows.Forms.GroupBox();
            this.txtHStyle = new System.Windows.Forms.TextBox();
            this.txtSHStyle = new System.Windows.Forms.TextBox();
            this.txtStyle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmdFontSetup = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gbPage = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudTopMargin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudLeftMargin = new System.Windows.Forms.NumericUpDown();
            this.rbHorizontal = new System.Windows.Forms.RadioButton();
            this.rbVertical = new System.Windows.Forms.RadioButton();
            this.gbZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.TableLayoutPanel1.SuspendLayout();
            this.gbFont.SuspendLayout();
            this.gbPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).BeginInit();
            this.SuspendLayout();
            // 
            // pvWydruk
            // 
            this.pvWydruk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pvWydruk.Location = new System.Drawing.Point(0, 0);
            this.pvWydruk.Name = "pvWydruk";
            this.pvWydruk.Size = new System.Drawing.Size(824, 636);
            this.pvWydruk.TabIndex = 1;
            this.pvWydruk.UseAntiAlias = true;
            this.pvWydruk.Click += new System.EventHandler(this.pvWydruk_Click);
            // 
            // gbZoom
            // 
            this.gbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbZoom.Controls.Add(this.nudZoom);
            this.gbZoom.Controls.Add(this.tbZoom);
            this.gbZoom.Location = new System.Drawing.Point(830, 12);
            this.gbZoom.Name = "gbZoom";
            this.gbZoom.Size = new System.Drawing.Size(196, 53);
            this.gbZoom.TabIndex = 24;
            this.gbZoom.TabStop = false;
            this.gbZoom.Text = "Powiększenie";
            // 
            // nudZoom
            // 
            this.nudZoom.Location = new System.Drawing.Point(6, 19);
            this.nudZoom.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudZoom.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudZoom.Name = "nudZoom";
            this.nudZoom.Size = new System.Drawing.Size(53, 20);
            this.nudZoom.TabIndex = 158;
            this.nudZoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudZoom.ValueChanged += new System.EventHandler(this.nudZoom_ValueChanged);
            // 
            // tbZoom
            // 
            this.tbZoom.AutoSize = false;
            this.tbZoom.LargeChange = 10;
            this.tbZoom.Location = new System.Drawing.Point(73, 15);
            this.tbZoom.Maximum = 400;
            this.tbZoom.Minimum = 50;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(117, 34);
            this.tbZoom.TabIndex = 6;
            this.tbZoom.TickFrequency = 10;
            this.tbZoom.Value = 100;
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 2;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(830, 595);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(196, 29);
            this.TableLayoutPanel1.TabIndex = 25;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(15, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 0;
            this.OK_Button.Text = "&Drukuj";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(113, 3);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(67, 23);
            this.Cancel_Button.TabIndex = 1;
            this.Cancel_Button.Text = "Zamknij";
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // gbFont
            // 
            this.gbFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFont.Controls.Add(this.txtHStyle);
            this.gbFont.Controls.Add(this.txtSHStyle);
            this.gbFont.Controls.Add(this.txtStyle);
            this.gbFont.Controls.Add(this.label9);
            this.gbFont.Controls.Add(this.label10);
            this.gbFont.Controls.Add(this.cmdFontSetup);
            this.gbFont.Controls.Add(this.label3);
            this.gbFont.Location = new System.Drawing.Point(830, 270);
            this.gbFont.Name = "gbFont";
            this.gbFont.Size = new System.Drawing.Size(196, 319);
            this.gbFont.TabIndex = 26;
            this.gbFont.TabStop = false;
            this.gbFont.Text = "Ustawienia czcionki";
            // 
            // txtHStyle
            // 
            this.txtHStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHStyle.Location = new System.Drawing.Point(4, 235);
            this.txtHStyle.Multiline = true;
            this.txtHStyle.Name = "txtHStyle";
            this.txtHStyle.ReadOnly = true;
            this.txtHStyle.Size = new System.Drawing.Size(189, 82);
            this.txtHStyle.TabIndex = 195;
            // 
            // txtSHStyle
            // 
            this.txtSHStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSHStyle.Location = new System.Drawing.Point(4, 134);
            this.txtSHStyle.Multiline = true;
            this.txtSHStyle.Name = "txtSHStyle";
            this.txtSHStyle.ReadOnly = true;
            this.txtSHStyle.Size = new System.Drawing.Size(189, 82);
            this.txtSHStyle.TabIndex = 194;
            // 
            // txtStyle
            // 
            this.txtStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStyle.Location = new System.Drawing.Point(4, 33);
            this.txtStyle.Multiline = true;
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(189, 82);
            this.txtStyle.TabIndex = 193;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(9, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 188;
            this.label9.Text = "Nagłówek strony";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(6, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 174;
            this.label10.Text = "Nagłówek sekcji";
            // 
            // cmdFontSetup
            // 
            this.cmdFontSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFontSetup.Location = new System.Drawing.Point(162, 9);
            this.cmdFontSetup.Name = "cmdFontSetup";
            this.cmdFontSetup.Size = new System.Drawing.Size(28, 23);
            this.cmdFontSetup.TabIndex = 170;
            this.cmdFontSetup.Text = "...";
            this.cmdFontSetup.UseVisualStyleBackColor = true;
            this.cmdFontSetup.Click += new System.EventHandler(this.cmdFontSetup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Czcionka bazowa";
            // 
            // gbPage
            // 
            this.gbPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPage.Controls.Add(this.label5);
            this.gbPage.Controls.Add(this.label4);
            this.gbPage.Controls.Add(this.nudTopMargin);
            this.gbPage.Controls.Add(this.label2);
            this.gbPage.Controls.Add(this.label1);
            this.gbPage.Controls.Add(this.nudLeftMargin);
            this.gbPage.Controls.Add(this.rbHorizontal);
            this.gbPage.Controls.Add(this.rbVertical);
            this.gbPage.Location = new System.Drawing.Point(830, 90);
            this.gbPage.Name = "gbPage";
            this.gbPage.Size = new System.Drawing.Size(196, 156);
            this.gbPage.TabIndex = 27;
            this.gbPage.TabStop = false;
            this.gbPage.Text = "Ustawienia strony";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "górny i dolny";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "lewy i prawy";
            // 
            // nudTopMargin
            // 
            this.nudTopMargin.Location = new System.Drawing.Point(90, 126);
            this.nudTopMargin.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTopMargin.Name = "nudTopMargin";
            this.nudTopMargin.Size = new System.Drawing.Size(47, 20);
            this.nudTopMargin.TabIndex = 5;
            this.nudTopMargin.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTopMargin.ValueChanged += new System.EventHandler(this.nudTopMargin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Orientacja";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Marginesy (mm)";
            // 
            // nudLeftMargin
            // 
            this.nudLeftMargin.Location = new System.Drawing.Point(90, 100);
            this.nudLeftMargin.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLeftMargin.Name = "nudLeftMargin";
            this.nudLeftMargin.Size = new System.Drawing.Size(47, 20);
            this.nudLeftMargin.TabIndex = 2;
            this.nudLeftMargin.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLeftMargin.ValueChanged += new System.EventHandler(this.nudLeftMargin_ValueChanged);
            // 
            // rbHorizontal
            // 
            this.rbHorizontal.AutoSize = true;
            this.rbHorizontal.Location = new System.Drawing.Point(23, 55);
            this.rbHorizontal.Name = "rbHorizontal";
            this.rbHorizontal.Size = new System.Drawing.Size(64, 17);
            this.rbHorizontal.TabIndex = 1;
            this.rbHorizontal.TabStop = true;
            this.rbHorizontal.Text = "pozioma";
            this.rbHorizontal.UseVisualStyleBackColor = true;
            this.rbHorizontal.CheckedChanged += new System.EventHandler(this.rbVertical_CheckedChanged);
            // 
            // rbVertical
            // 
            this.rbVertical.AutoSize = true;
            this.rbVertical.Location = new System.Drawing.Point(23, 32);
            this.rbVertical.Name = "rbVertical";
            this.rbVertical.Size = new System.Drawing.Size(65, 17);
            this.rbVertical.TabIndex = 0;
            this.rbVertical.TabStop = true;
            this.rbVertical.Text = "pionowa";
            this.rbVertical.UseVisualStyleBackColor = true;
            this.rbVertical.CheckedChanged += new System.EventHandler(this.rbVertical_CheckedChanged);
            // 
            // dlgPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 636);
            this.Controls.Add(this.gbPage);
            this.Controls.Add(this.gbFont);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.gbZoom);
            this.Controls.Add(this.pvWydruk);
            this.MaximizeBox = false;
            this.Name = "dlgPrintPreview";
            this.Text = "Podgląd wydruku";
            this.gbZoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.gbFont.ResumeLayout(false);
            this.gbFont.PerformLayout();
            this.gbPage.ResumeLayout(false);
            this.gbPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PrintPreviewControl pvWydruk;
        internal System.Windows.Forms.GroupBox gbZoom;
        internal System.Windows.Forms.NumericUpDown nudZoom;
        internal System.Windows.Forms.TrackBar tbZoom;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.GroupBox gbFont;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudTopMargin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLeftMargin;
        private System.Windows.Forms.RadioButton rbVertical;
        internal System.Windows.Forms.Button cmdFontSetup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStyle;
        private System.Windows.Forms.TextBox txtHStyle;
        private System.Windows.Forms.TextBox txtSHStyle;
        internal System.Windows.Forms.RadioButton rbHorizontal;
    }
}