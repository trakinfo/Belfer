using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using BelferCS.Properties;
namespace BelferCS
{
    public partial class dlgPrintPreview : Form
    {
        public dlgPrintPreview()
        {
            InitializeComponent();
            pvWydruk.Document = Doc;
            pvWydruk.Zoom = (double)nudZoom.Value * 0.01;
            GetPageSettings();
            GetFontSettings();
            SetMargins();
        }
        public PrintDocument Doc = new PrintDocument();
        public delegate void PreviewModeDelegate(bool IsPreview);
        public event PreviewModeDelegate PreviewModeChanged;

        private void OK_Button_Click(object sender, EventArgs e)
        {
            var dlgPrint = new PrintDialog();
            dlgPrint.AllowSomePages = false;
            dlgPrint.PrinterSettings.FromPage = 1;
            dlgPrint.PrinterSettings.ToPage = 1;
            if (dlgPrint.ShowDialog() == DialogResult.OK)
            {
                PreviewModeChanged?.Invoke(false);
                pvWydruk.Document.DefaultPageSettings.PrinterSettings.Copies = dlgPrint.PrinterSettings.Copies;
                pvWydruk.Document.DefaultPageSettings.PrinterSettings.FromPage = dlgPrint.PrinterSettings.FromPage;
                pvWydruk.Document.DefaultPageSettings.PrinterSettings.ToPage = dlgPrint.PrinterSettings.ToPage;
                pvWydruk.Document.DefaultPageSettings.PrinterSettings.PrinterName = dlgPrint.PrinterSettings.PrinterName;
                pvWydruk.Document.Print();
                PreviewModeChanged?.Invoke(true);                
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void GetPageSettings()
        {
            if (Settings.Default.Landscape) { rbHorizontal.Checked = true; } else { rbVertical.Checked = true; }
            var M = new CalcHelper.Math();
            nudLeftMargin.Value = (decimal)Math.Round(M.INtoMM(Settings.Default.LeftMargin));
            nudTopMargin.Value = (decimal)Math.Round(M.INtoMM(Settings.Default.TopMargin));
        }

        private void GetFontSettings()
        {
            txtStyle.Font = Settings.Default.TextFont;
            txtStyle.Text = Settings.Default.TextFont.FontFamily.Name+"; ";
            txtStyle.Text += Settings.Default.TextFont.Size+"pt; ";
            if (Settings.Default.TextFont.Bold) { txtStyle.Text += "pogrubienie,"; }
            if (Settings.Default.TextFont.Italic) { txtStyle.Text += "kursywa"; }

            txtSHStyle.Font = Settings.Default.SubHeaderFont;
            txtSHStyle.Text = Settings.Default.SubHeaderFont.FontFamily.Name+"; ";
            txtSHStyle.Text += Settings.Default.SubHeaderFont.Size+"pt; ";
            if (Settings.Default.SubHeaderFont.Bold) { txtSHStyle.Text += "pogrubienie,"; }
            if (Settings.Default.SubHeaderFont.Italic) { txtSHStyle.Text += "kursywa"; }

            txtHStyle.Font = Settings.Default.HeaderFont;
            txtHStyle.Text = Settings.Default.HeaderFont.FontFamily.Name+"; ";
            txtHStyle.Text += Settings.Default.HeaderFont.Size+"pt; ";
            if (Settings.Default.HeaderFont.Bold) { txtHStyle.Text += "pogrubienie,"; }
            if (Settings.Default.HeaderFont.Italic) { txtHStyle.Text += "kursywa"; }
        }

        private void nudZoom_ValueChanged(object sender, EventArgs e)
        {
            tbZoom.Value = (int)nudZoom.Value;
            pvWydruk.Zoom = (double)nudZoom.Value * 0.01;
        }

        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            nudZoom.Value = tbZoom.Value;
            pvWydruk.Zoom = tbZoom.Value * 0.01;
        }
        public void NewRow()
        {
            pvWydruk.Rows += 1;
        }

        private void pvWydruk_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift || ModifierKeys == Keys.Control)
            {
                if ((double)nudZoom.Minimum <= pvWydruk.Zoom * 100 / 2)
                {
                    pvWydruk.Zoom = pvWydruk.Zoom / 2.0;
                    nudZoom.Value = (decimal)pvWydruk.Zoom * 100;
                }                
            }
            else
            {
                if ((double)nudZoom.Maximum >= pvWydruk.Zoom * 100 * 2)
                {
                    pvWydruk.Zoom = pvWydruk.Zoom * 2.0;
                    nudZoom.Value = (decimal)pvWydruk.Zoom * 100;
                }
            }
        }

        private void cmdFontSetup_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.FontMustExist = true;
            fd.Font = Settings.Default.TextFont;
            fd.MinSize = 8;
            fd.MaxSize = 16;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.TextFont = fd.Font;
                Settings.Default.SubHeaderFont = new Font(Settings.Default.SubHeaderFont.FontFamily, Settings.Default.TextFont.Size + 1, FontStyle.Bold);
                Settings.Default.HeaderFont = new Font(Settings.Default.HeaderFont.FontFamily, Settings.Default.TextFont.Size + 2, FontStyle.Bold);
                Settings.Default.Save();
                GetFontSettings();
                pvWydruk.Rows = 1;
                pvWydruk.InvalidatePreview();
            }
        }

        private void rbVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked) return;
            Settings.Default.Landscape = ((RadioButton)sender).Name == rbHorizontal.Name ? ((RadioButton)sender).Checked : !((RadioButton)sender).Checked;
            Settings.Default.Save();
            pvWydruk.Document.DefaultPageSettings.Landscape = Settings.Default.Landscape;
            pvWydruk.InvalidatePreview();
        }

        private void nudLeftMargin_ValueChanged(object sender, EventArgs e)
        {
            var CHM = new CalcHelper.Math();
            Settings.Default.LeftMargin = (int)CHM.MMtoIN(((float)((NumericUpDown)sender).Value));
            Settings.Default.Save();
            SetHorizontalMargins();
            pvWydruk.InvalidatePreview();
        }

        private void nudTopMargin_ValueChanged(object sender, EventArgs e)
        {
            var CHM = new CalcHelper.Math();
            Settings.Default.TopMargin = (int)CHM.MMtoIN(((float)((NumericUpDown)sender).Value));
            Settings.Default.Save();
            SetVerticalMargins();
            pvWydruk.InvalidatePreview();
        }
        private void SetMargins()
        {
            SetHorizontalMargins();
            SetHorizontalMargins();
        }
        private void SetHorizontalMargins()
        {
            Doc.DefaultPageSettings.Margins.Left = Settings.Default.LeftMargin;
            Doc.DefaultPageSettings.Margins.Right = Settings.Default.LeftMargin;
        }
        private void SetVerticalMargins()
        {
            Doc.DefaultPageSettings.Margins.Top = Settings.Default.TopMargin;
            Doc.DefaultPageSettings.Margins.Bottom = Settings.Default.TopMargin;
        }
    }
}
