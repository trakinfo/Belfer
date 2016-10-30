<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKlasyfikacja
    Inherits System.Windows.Forms.Form

    'Formularz zastępuje metodę dispose, aby wyczyścić listę składników.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wymagane przez Projektanta formularzy systemu Windows
    Private components As System.ComponentModel.IContainer

    'UWAGA: Następująca procedura jest wymagana przez Projektanta formularzy systemu Windows
    'Można to modyfikować, używając Projektanta formularzy systemu Windows.  
    'Nie należy modyfikować za pomocą edytora kodu.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.lblObsadaFilter = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.cmdAddNew = New System.Windows.Forms.Button()
    Me.cmdSend = New System.Windows.Forms.Button()
    Me.cmdDelete = New System.Windows.Forms.Button()
    Me.tlpKlasyfikacja = New System.Windows.Forms.TableLayoutPanel()
    Me.lblKL = New System.Windows.Forms.Label()
    Me.hNKL = New System.Windows.Forms.Label()
    Me.lblNKL = New System.Windows.Forms.Label()
    Me.lbl3AndMoreNDST = New System.Windows.Forms.Label()
    Me.lbl12NDST = New System.Windows.Forms.Label()
    Me.lblNoNDST = New System.Windows.Forms.Label()
    Me.hStanKlasy = New System.Windows.Forms.Label()
    Me.hStudentNumber = New System.Windows.Forms.Label()
    Me.hNoNdst = New System.Windows.Forms.Label()
    Me.h12Ndst = New System.Windows.Forms.Label()
    Me.h3AndMoreNdst = New System.Windows.Forms.Label()
    Me.hKL = New System.Windows.Forms.Label()
    Me.lblStanKlasy = New System.Windows.Forms.Label()
    Me.lblWychowawca = New System.Windows.Forms.Label()
    Me.tlpZachowanie = New System.Windows.Forms.TableLayoutPanel()
    Me.lblDb = New System.Windows.Forms.Label()
    Me.lblBdb = New System.Windows.Forms.Label()
    Me.lblWz = New System.Windows.Forms.Label()
    Me.lblNg = New System.Windows.Forms.Label()
    Me.lblNdp = New System.Windows.Forms.Label()
    Me.lblPop = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.lblRP = New System.Windows.Forms.Label()
    Me.labelAvg = New System.Windows.Forms.Label()
    Me.lblDataRP = New System.Windows.Forms.Label()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.lblAvg = New System.Windows.Forms.Label()
    Me.tp1 = New System.Windows.Forms.TabPage()
    Me.lvNdstByPrzedmiot = New System.Windows.Forms.ListView()
    Me.lvZachowanie = New System.Windows.Forms.ListView()
    Me.lvNdst = New System.Windows.Forms.ListView()
    Me.lvNkl = New System.Windows.Forms.ListView()
    Me.tcWykaz = New System.Windows.Forms.TabControl()
    Me.tlpKlasyfikacjaRoczna = New System.Windows.Forms.TableLayoutPanel()
    Me.Label32 = New System.Windows.Forms.Label()
    Me.lblNoPromocja = New System.Windows.Forms.Label()
    Me.lblPromocja = New System.Windows.Forms.Label()
    Me.lblEgzPopraw2Ndst = New System.Windows.Forms.Label()
    Me.lblNoPromocjaEtykieta = New System.Windows.Forms.Label()
    Me.lblPromocjaEtykieta = New System.Windows.Forms.Label()
    Me.Label26 = New System.Windows.Forms.Label()
    Me.lblEgzPopraw1Ndst = New System.Windows.Forms.Label()
    Me.lblNoNDSTRS = New System.Windows.Forms.Label()
    Me.lblNKLRS = New System.Windows.Forms.Label()
    Me.lblKLRS = New System.Windows.Forms.Label()
    Me.Label19 = New System.Windows.Forms.Label()
    Me.Label20 = New System.Windows.Forms.Label()
    Me.Label21 = New System.Windows.Forms.Label()
    Me.Label22 = New System.Windows.Forms.Label()
    Me.Label23 = New System.Windows.Forms.Label()
    Me.Label24 = New System.Windows.Forms.Label()
    Me.lblStanKlasyRS = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblStatus = New System.Windows.Forms.Label()
    Me.tlpKlasyfikacja.SuspendLayout()
    Me.tlpZachowanie.SuspendLayout()
    Me.tp1.SuspendLayout()
    Me.tcWykaz.SuspendLayout()
    Me.tlpKlasyfikacjaRoczna.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(898, 245)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 35)
    Me.cmdPrint.TabIndex = 125
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(898, 587)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 124
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'rbRokSzkolny
    '
    Me.rbRokSzkolny.AutoSize = True
    Me.rbRokSzkolny.Location = New System.Drawing.Point(932, 13)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(82, 17)
    Me.rbRokSzkolny.TabIndex = 206
    Me.rbRokSzkolny.Tag = "R"
    Me.rbRokSzkolny.Text = "RokSzkolny"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'rbSemestr
    '
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Location = New System.Drawing.Point(857, 13)
    Me.rbSemestr.Name = "rbSemestr"
    Me.rbSemestr.Size = New System.Drawing.Size(69, 17)
    Me.rbSemestr.TabIndex = 205
    Me.rbSemestr.Tag = "S"
    Me.rbSemestr.Text = "Semestr I"
    Me.rbSemestr.UseVisualStyleBackColor = True
    '
    'lblObsadaFilter
    '
    Me.lblObsadaFilter.AutoSize = True
    Me.lblObsadaFilter.Location = New System.Drawing.Point(12, 15)
    Me.lblObsadaFilter.Name = "lblObsadaFilter"
    Me.lblObsadaFilter.Size = New System.Drawing.Size(33, 13)
    Me.lblObsadaFilter.TabIndex = 204
    Me.lblObsadaFilter.Text = "Klasa"
    Me.lblObsadaFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(238, 21)
    Me.cbKlasa.TabIndex = 203
    '
    'cmdAddNew
    '
    Me.cmdAddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAddNew.Enabled = False
    Me.cmdAddNew.Image = Global.belfer.NET.My.Resources.Resources.add_24
    Me.cmdAddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAddNew.Location = New System.Drawing.Point(898, 60)
    Me.cmdAddNew.Name = "cmdAddNew"
    Me.cmdAddNew.Size = New System.Drawing.Size(117, 36)
    Me.cmdAddNew.TabIndex = 207
    Me.cmdAddNew.Text = "Dodaj"
    Me.cmdAddNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAddNew.UseVisualStyleBackColor = True
    '
    'cmdSend
    '
    Me.cmdSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSend.Enabled = False
    Me.cmdSend.Image = Global.belfer.NET.My.Resources.Resources.send_24
    Me.cmdSend.Location = New System.Drawing.Point(898, 102)
    Me.cmdSend.Name = "cmdSend"
    Me.cmdSend.Size = New System.Drawing.Size(117, 36)
    Me.cmdSend.TabIndex = 208
    Me.cmdSend.Text = "Przekaż"
    Me.cmdSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdSend.UseVisualStyleBackColor = True
    '
    'cmdDelete
    '
    Me.cmdDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelete.Enabled = False
    Me.cmdDelete.Image = Global.belfer.NET.My.Resources.Resources.del_24
    Me.cmdDelete.Location = New System.Drawing.Point(898, 144)
    Me.cmdDelete.Name = "cmdDelete"
    Me.cmdDelete.Size = New System.Drawing.Size(117, 36)
    Me.cmdDelete.TabIndex = 209
    Me.cmdDelete.Text = "&Usuń"
    Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdDelete.UseVisualStyleBackColor = True
    '
    'tlpKlasyfikacja
    '
    Me.tlpKlasyfikacja.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpKlasyfikacja.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    Me.tlpKlasyfikacja.ColumnCount = 6
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.8!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.8!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.8!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.8!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.8!))
    Me.tlpKlasyfikacja.Controls.Add(Me.lblKL, 0, 3)
    Me.tlpKlasyfikacja.Controls.Add(Me.hNKL, 2, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.lblNKL, 2, 3)
    Me.tlpKlasyfikacja.Controls.Add(Me.lbl3AndMoreNDST, 5, 3)
    Me.tlpKlasyfikacja.Controls.Add(Me.lbl12NDST, 4, 3)
    Me.tlpKlasyfikacja.Controls.Add(Me.lblNoNDST, 3, 3)
    Me.tlpKlasyfikacja.Controls.Add(Me.hStanKlasy, 0, 0)
    Me.tlpKlasyfikacja.Controls.Add(Me.hStudentNumber, 1, 0)
    Me.tlpKlasyfikacja.Controls.Add(Me.hNoNdst, 3, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.h12Ndst, 4, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.h3AndMoreNdst, 5, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.hKL, 1, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.lblStanKlasy, 0, 3)
    Me.tlpKlasyfikacja.Location = New System.Drawing.Point(15, 59)
    Me.tlpKlasyfikacja.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpKlasyfikacja.Name = "tlpKlasyfikacja"
    Me.tlpKlasyfikacja.RowCount = 4
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    Me.tlpKlasyfikacja.Size = New System.Drawing.Size(873, 90)
    Me.tlpKlasyfikacja.TabIndex = 211
    Me.tlpKlasyfikacja.Visible = False
    '
    'lblKL
    '
    Me.lblKL.AutoSize = True
    Me.lblKL.BackColor = System.Drawing.SystemColors.Window
    Me.lblKL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblKL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblKL.Location = New System.Drawing.Point(53, 67)
    Me.lblKL.Margin = New System.Windows.Forms.Padding(0)
    Me.lblKL.Name = "lblKL"
    Me.lblKL.Size = New System.Drawing.Size(162, 22)
    Me.lblKL.TabIndex = 225
    Me.lblKL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hNKL
    '
    Me.hNKL.BackColor = System.Drawing.SystemColors.Window
    Me.hNKL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hNKL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hNKL.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hNKL.Location = New System.Drawing.Point(216, 23)
    Me.hNKL.Margin = New System.Windows.Forms.Padding(0)
    Me.hNKL.Name = "hNKL"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hNKL, 2)
    Me.hNKL.Size = New System.Drawing.Size(162, 43)
    Me.hNKL.TabIndex = 224
    Me.hNKL.Text = "nkl ze wszystkich przedmiotów"
    Me.hNKL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNKL
    '
    Me.lblNKL.AutoSize = True
    Me.lblNKL.BackColor = System.Drawing.SystemColors.Window
    Me.lblNKL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNKL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNKL.Location = New System.Drawing.Point(216, 67)
    Me.lblNKL.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNKL.Name = "lblNKL"
    Me.lblNKL.Size = New System.Drawing.Size(162, 22)
    Me.lblNKL.TabIndex = 223
    Me.lblNKL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lbl3AndMoreNDST
    '
    Me.lbl3AndMoreNDST.AutoSize = True
    Me.lbl3AndMoreNDST.BackColor = System.Drawing.SystemColors.Window
    Me.lbl3AndMoreNDST.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lbl3AndMoreNDST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lbl3AndMoreNDST.Location = New System.Drawing.Point(705, 67)
    Me.lbl3AndMoreNDST.Margin = New System.Windows.Forms.Padding(0)
    Me.lbl3AndMoreNDST.Name = "lbl3AndMoreNDST"
    Me.lbl3AndMoreNDST.Size = New System.Drawing.Size(167, 22)
    Me.lbl3AndMoreNDST.TabIndex = 222
    Me.lbl3AndMoreNDST.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lbl12NDST
    '
    Me.lbl12NDST.AutoSize = True
    Me.lbl12NDST.BackColor = System.Drawing.SystemColors.Window
    Me.lbl12NDST.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lbl12NDST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lbl12NDST.Location = New System.Drawing.Point(542, 67)
    Me.lbl12NDST.Margin = New System.Windows.Forms.Padding(0)
    Me.lbl12NDST.Name = "lbl12NDST"
    Me.lbl12NDST.Size = New System.Drawing.Size(162, 22)
    Me.lbl12NDST.TabIndex = 221
    Me.lbl12NDST.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNoNDST
    '
    Me.lblNoNDST.AutoSize = True
    Me.lblNoNDST.BackColor = System.Drawing.SystemColors.Window
    Me.lblNoNDST.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNoNDST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNoNDST.Location = New System.Drawing.Point(379, 67)
    Me.lblNoNDST.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNoNDST.Name = "lblNoNDST"
    Me.lblNoNDST.Size = New System.Drawing.Size(162, 22)
    Me.lblNoNDST.TabIndex = 220
    Me.lblNoNDST.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hStanKlasy
    '
    Me.hStanKlasy.BackColor = System.Drawing.SystemColors.Window
    Me.hStanKlasy.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hStanKlasy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hStanKlasy.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hStanKlasy.Location = New System.Drawing.Point(1, 1)
    Me.hStanKlasy.Margin = New System.Windows.Forms.Padding(0)
    Me.hStanKlasy.Name = "hStanKlasy"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hStanKlasy, 3)
    Me.hStanKlasy.Size = New System.Drawing.Size(51, 65)
    Me.hStanKlasy.TabIndex = 213
    Me.hStanKlasy.Text = "Stan klasy"
    Me.hStanKlasy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hStudentNumber
    '
    Me.hStudentNumber.BackColor = System.Drawing.SystemColors.Window
    Me.tlpKlasyfikacja.SetColumnSpan(Me.hStudentNumber, 5)
    Me.hStudentNumber.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hStudentNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hStudentNumber.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hStudentNumber.Location = New System.Drawing.Point(53, 1)
    Me.hStudentNumber.Margin = New System.Windows.Forms.Padding(0)
    Me.hStudentNumber.Name = "hStudentNumber"
    Me.hStudentNumber.Size = New System.Drawing.Size(819, 21)
    Me.hStudentNumber.TabIndex = 214
    Me.hStudentNumber.Text = "Liczba uczniów z ocenami niedostatecznymi"
    Me.hStudentNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hNoNdst
    '
    Me.hNoNdst.BackColor = System.Drawing.SystemColors.Window
    Me.hNoNdst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hNoNdst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hNoNdst.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hNoNdst.Location = New System.Drawing.Point(379, 23)
    Me.hNoNdst.Margin = New System.Windows.Forms.Padding(0)
    Me.hNoNdst.Name = "hNoNdst"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hNoNdst, 2)
    Me.hNoNdst.Size = New System.Drawing.Size(162, 43)
    Me.hNoNdst.TabIndex = 215
    Me.hNoNdst.Text = "bez ocen ndst"
    Me.hNoNdst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'h12Ndst
    '
    Me.h12Ndst.BackColor = System.Drawing.SystemColors.Window
    Me.h12Ndst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.h12Ndst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.h12Ndst.ForeColor = System.Drawing.SystemColors.Desktop
    Me.h12Ndst.Location = New System.Drawing.Point(542, 23)
    Me.h12Ndst.Margin = New System.Windows.Forms.Padding(0)
    Me.h12Ndst.Name = "h12Ndst"
    Me.tlpKlasyfikacja.SetRowSpan(Me.h12Ndst, 2)
    Me.h12Ndst.Size = New System.Drawing.Size(162, 43)
    Me.h12Ndst.TabIndex = 216
    Me.h12Ndst.Text = "z 1-2 ocenami ndst"
    Me.h12Ndst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'h3AndMoreNdst
    '
    Me.h3AndMoreNdst.BackColor = System.Drawing.SystemColors.Window
    Me.h3AndMoreNdst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.h3AndMoreNdst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.h3AndMoreNdst.ForeColor = System.Drawing.SystemColors.Desktop
    Me.h3AndMoreNdst.Location = New System.Drawing.Point(705, 23)
    Me.h3AndMoreNdst.Margin = New System.Windows.Forms.Padding(0)
    Me.h3AndMoreNdst.Name = "h3AndMoreNdst"
    Me.tlpKlasyfikacja.SetRowSpan(Me.h3AndMoreNdst, 2)
    Me.h3AndMoreNdst.Size = New System.Drawing.Size(167, 43)
    Me.h3AndMoreNdst.TabIndex = 217
    Me.h3AndMoreNdst.Text = "z 3 i więcej ocenami ndst"
    Me.h3AndMoreNdst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hKL
    '
    Me.hKL.BackColor = System.Drawing.SystemColors.Window
    Me.hKL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hKL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hKL.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hKL.Location = New System.Drawing.Point(53, 23)
    Me.hKL.Margin = New System.Windows.Forms.Padding(0)
    Me.hKL.Name = "hKL"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hKL, 2)
    Me.hKL.Size = New System.Drawing.Size(162, 43)
    Me.hKL.TabIndex = 218
    Me.hKL.Text = "klasyfikowanych"
    Me.hKL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblStanKlasy
    '
    Me.lblStanKlasy.AutoSize = True
    Me.lblStanKlasy.BackColor = System.Drawing.SystemColors.Window
    Me.lblStanKlasy.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblStanKlasy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblStanKlasy.Location = New System.Drawing.Point(1, 67)
    Me.lblStanKlasy.Margin = New System.Windows.Forms.Padding(0)
    Me.lblStanKlasy.Name = "lblStanKlasy"
    Me.lblStanKlasy.Size = New System.Drawing.Size(51, 22)
    Me.lblStanKlasy.TabIndex = 219
    Me.lblStanKlasy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblWychowawca
    '
    Me.lblWychowawca.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblWychowawca.ForeColor = System.Drawing.Color.Black
    Me.lblWychowawca.Location = New System.Drawing.Point(379, 15)
    Me.lblWychowawca.Name = "lblWychowawca"
    Me.lblWychowawca.Size = New System.Drawing.Size(262, 13)
    Me.lblWychowawca.TabIndex = 212
    Me.lblWychowawca.Text = "lblWychowawca"
    Me.lblWychowawca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'tlpZachowanie
    '
    Me.tlpZachowanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpZachowanie.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    Me.tlpZachowanie.ColumnCount = 6
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
    Me.tlpZachowanie.Controls.Add(Me.lblDb, 0, 2)
    Me.tlpZachowanie.Controls.Add(Me.lblBdb, 0, 2)
    Me.tlpZachowanie.Controls.Add(Me.lblWz, 0, 2)
    Me.tlpZachowanie.Controls.Add(Me.lblNg, 0, 2)
    Me.tlpZachowanie.Controls.Add(Me.lblNdp, 0, 2)
    Me.tlpZachowanie.Controls.Add(Me.lblPop, 0, 2)
    Me.tlpZachowanie.Controls.Add(Me.Label3, 0, 0)
    Me.tlpZachowanie.Controls.Add(Me.Label5, 0, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label6, 1, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label7, 2, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label8, 3, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label9, 4, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label10, 5, 1)
    Me.tlpZachowanie.Location = New System.Drawing.Point(15, 152)
    Me.tlpZachowanie.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpZachowanie.Name = "tlpZachowanie"
    Me.tlpZachowanie.RowCount = 3
    Me.tlpZachowanie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
    Me.tlpZachowanie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
    Me.tlpZachowanie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
    Me.tlpZachowanie.Size = New System.Drawing.Size(873, 67)
    Me.tlpZachowanie.TabIndex = 213
    Me.tlpZachowanie.Visible = False
    '
    'lblDb
    '
    Me.lblDb.AutoSize = True
    Me.lblDb.BackColor = System.Drawing.SystemColors.Window
    Me.lblDb.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblDb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblDb.Location = New System.Drawing.Point(291, 43)
    Me.lblDb.Margin = New System.Windows.Forms.Padding(0)
    Me.lblDb.Name = "lblDb"
    Me.lblDb.Size = New System.Drawing.Size(144, 23)
    Me.lblDb.TabIndex = 225
    Me.lblDb.Tag = "4"
    Me.lblDb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblBdb
    '
    Me.lblBdb.AutoSize = True
    Me.lblBdb.BackColor = System.Drawing.SystemColors.Window
    Me.lblBdb.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblBdb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblBdb.Location = New System.Drawing.Point(146, 43)
    Me.lblBdb.Margin = New System.Windows.Forms.Padding(0)
    Me.lblBdb.Name = "lblBdb"
    Me.lblBdb.Size = New System.Drawing.Size(144, 23)
    Me.lblBdb.TabIndex = 224
    Me.lblBdb.Tag = "5"
    Me.lblBdb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblWz
    '
    Me.lblWz.AutoSize = True
    Me.lblWz.BackColor = System.Drawing.SystemColors.Window
    Me.lblWz.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblWz.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblWz.Location = New System.Drawing.Point(1, 43)
    Me.lblWz.Margin = New System.Windows.Forms.Padding(0)
    Me.lblWz.Name = "lblWz"
    Me.lblWz.Size = New System.Drawing.Size(144, 23)
    Me.lblWz.TabIndex = 223
    Me.lblWz.Tag = "6"
    Me.lblWz.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNg
    '
    Me.lblNg.AutoSize = True
    Me.lblNg.BackColor = System.Drawing.SystemColors.Window
    Me.lblNg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNg.Location = New System.Drawing.Point(726, 43)
    Me.lblNg.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNg.Name = "lblNg"
    Me.lblNg.Size = New System.Drawing.Size(146, 23)
    Me.lblNg.TabIndex = 222
    Me.lblNg.Tag = "1"
    Me.lblNg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNdp
    '
    Me.lblNdp.AutoSize = True
    Me.lblNdp.BackColor = System.Drawing.SystemColors.Window
    Me.lblNdp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNdp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNdp.Location = New System.Drawing.Point(581, 43)
    Me.lblNdp.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNdp.Name = "lblNdp"
    Me.lblNdp.Size = New System.Drawing.Size(144, 23)
    Me.lblNdp.TabIndex = 221
    Me.lblNdp.Tag = "2"
    Me.lblNdp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblPop
    '
    Me.lblPop.AutoSize = True
    Me.lblPop.BackColor = System.Drawing.SystemColors.Window
    Me.lblPop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblPop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblPop.Location = New System.Drawing.Point(436, 43)
    Me.lblPop.Margin = New System.Windows.Forms.Padding(0)
    Me.lblPop.Name = "lblPop"
    Me.lblPop.Size = New System.Drawing.Size(144, 23)
    Me.lblPop.TabIndex = 220
    Me.lblPop.Tag = "3"
    Me.lblPop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label3
    '
    Me.Label3.BackColor = System.Drawing.SystemColors.Window
    Me.tlpZachowanie.SetColumnSpan(Me.Label3, 6)
    Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label3.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label3.Location = New System.Drawing.Point(1, 1)
    Me.Label3.Margin = New System.Windows.Forms.Padding(0)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(871, 20)
    Me.Label3.TabIndex = 0
    Me.Label3.Text = "Liczba ocen zachowania"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label5
    '
    Me.Label5.BackColor = System.Drawing.SystemColors.Window
    Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label5.Location = New System.Drawing.Point(1, 22)
    Me.Label5.Margin = New System.Windows.Forms.Padding(0)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(144, 20)
    Me.Label5.TabIndex = 1
    Me.Label5.Text = "wzorowych"
    Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label6
    '
    Me.Label6.BackColor = System.Drawing.SystemColors.Window
    Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label6.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label6.Location = New System.Drawing.Point(146, 22)
    Me.Label6.Margin = New System.Windows.Forms.Padding(0)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(144, 20)
    Me.Label6.TabIndex = 2
    Me.Label6.Text = "bardzo dobrych"
    Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label7
    '
    Me.Label7.BackColor = System.Drawing.SystemColors.Window
    Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label7.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label7.Location = New System.Drawing.Point(291, 22)
    Me.Label7.Margin = New System.Windows.Forms.Padding(0)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(144, 20)
    Me.Label7.TabIndex = 3
    Me.Label7.Text = "dobrych"
    Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label8
    '
    Me.Label8.BackColor = System.Drawing.SystemColors.Window
    Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label8.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label8.Location = New System.Drawing.Point(436, 22)
    Me.Label8.Margin = New System.Windows.Forms.Padding(0)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(144, 20)
    Me.Label8.TabIndex = 4
    Me.Label8.Text = "poprawnych"
    Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label9
    '
    Me.Label9.BackColor = System.Drawing.SystemColors.Window
    Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label9.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label9.Location = New System.Drawing.Point(581, 22)
    Me.Label9.Margin = New System.Windows.Forms.Padding(0)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(144, 20)
    Me.Label9.TabIndex = 5
    Me.Label9.Text = "nieodpowiednich"
    Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label10
    '
    Me.Label10.BackColor = System.Drawing.SystemColors.Window
    Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label10.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label10.Location = New System.Drawing.Point(726, 22)
    Me.Label10.Margin = New System.Windows.Forms.Padding(0)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(146, 20)
    Me.Label10.TabIndex = 6
    Me.Label10.Text = "nagannych"
    Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.Enabled = False
    Me.cmdRefresh.Image = Global.belfer.NET.My.Resources.Resources.refresh_24
    Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdRefresh.Location = New System.Drawing.Point(898, 286)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(117, 36)
    Me.cmdRefresh.TabIndex = 231
    Me.cmdRefresh.Text = "Odśwież"
    Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'lblRP
    '
    Me.lblRP.AutoSize = True
    Me.lblRP.Location = New System.Drawing.Point(12, 41)
    Me.lblRP.Name = "lblRP"
    Me.lblRP.Size = New System.Drawing.Size(191, 13)
    Me.lblRP.TabIndex = 233
    Me.lblRP.Text = "Data klasyfikacyjnego posiedzenia RP:"
    Me.lblRP.Visible = False
    '
    'labelAvg
    '
    Me.labelAvg.AutoSize = True
    Me.labelAvg.Location = New System.Drawing.Point(752, 41)
    Me.labelAvg.Name = "labelAvg"
    Me.labelAvg.Size = New System.Drawing.Size(73, 13)
    Me.labelAvg.TabIndex = 234
    Me.labelAvg.Text = "Średnia ocen:"
    Me.labelAvg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.labelAvg.Visible = False
    '
    'lblDataRP
    '
    Me.lblDataRP.AutoSize = True
    Me.lblDataRP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblDataRP.Location = New System.Drawing.Point(209, 41)
    Me.lblDataRP.Name = "lblDataRP"
    Me.lblDataRP.Size = New System.Drawing.Size(51, 13)
    Me.lblDataRP.TabIndex = 235
    Me.lblDataRP.Text = "DataRP"
    Me.lblDataRP.Visible = False
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Location = New System.Drawing.Point(295, 15)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(78, 13)
    Me.Label14.TabIndex = 236
    Me.Label14.Text = "Wychowawca:"
    '
    'lblAvg
    '
    Me.lblAvg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblAvg.Location = New System.Drawing.Point(831, 41)
    Me.lblAvg.Name = "lblAvg"
    Me.lblAvg.Size = New System.Drawing.Size(57, 13)
    Me.lblAvg.TabIndex = 237
    Me.lblAvg.Text = "lblAvg"
    Me.lblAvg.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lblAvg.Visible = False
    '
    'tp1
    '
    Me.tp1.Controls.Add(Me.lvNdstByPrzedmiot)
    Me.tp1.Controls.Add(Me.lvZachowanie)
    Me.tp1.Controls.Add(Me.lvNdst)
    Me.tp1.Controls.Add(Me.lvNkl)
    Me.tp1.Location = New System.Drawing.Point(4, 22)
    Me.tp1.Name = "tp1"
    Me.tp1.Padding = New System.Windows.Forms.Padding(3)
    Me.tp1.Size = New System.Drawing.Size(872, 378)
    Me.tp1.TabIndex = 0
    Me.tp1.Text = "Wykaz uczniów nkl., z ocenami ndst. oraz z obniżonymi ocenami zachowania"
    Me.tp1.UseVisualStyleBackColor = True
    '
    'lvNdstByPrzedmiot
    '
    Me.lvNdstByPrzedmiot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvNdstByPrzedmiot.Location = New System.Drawing.Point(3, 210)
    Me.lvNdstByPrzedmiot.Name = "lvNdstByPrzedmiot"
    Me.lvNdstByPrzedmiot.Size = New System.Drawing.Size(867, 165)
    Me.lvNdstByPrzedmiot.TabIndex = 226
    Me.lvNdstByPrzedmiot.Tag = "Liczba uczniów z ocenami niedostatecznymi i nieklasyfikowanych z poszczególnych p" & _
    "rzedmiotów"
    Me.lvNdstByPrzedmiot.UseCompatibleStateImageBehavior = False
    '
    'lvZachowanie
    '
    Me.lvZachowanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvZachowanie.BackColor = System.Drawing.SystemColors.Window
    Me.lvZachowanie.Location = New System.Drawing.Point(585, 3)
    Me.lvZachowanie.Name = "lvZachowanie"
    Me.lvZachowanie.Size = New System.Drawing.Size(285, 201)
    Me.lvZachowanie.TabIndex = 225
    Me.lvZachowanie.Tag = "Wykaz uczniów z nieodpowiednią lub naganną oceną zachowania"
    Me.lvZachowanie.UseCompatibleStateImageBehavior = False
    '
    'lvNdst
    '
    Me.lvNdst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvNdst.Location = New System.Drawing.Point(294, 3)
    Me.lvNdst.Name = "lvNdst"
    Me.lvNdst.Size = New System.Drawing.Size(285, 201)
    Me.lvNdst.TabIndex = 224
    Me.lvNdst.Tag = "Wykaz uczniów z ocenami niedostatecznymi i nieklasyfikowanych z jednego lub kilku" & _
    " przedmiotów"
    Me.lvNdst.UseCompatibleStateImageBehavior = False
    '
    'lvNkl
    '
    Me.lvNkl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lvNkl.Location = New System.Drawing.Point(3, 3)
    Me.lvNkl.Name = "lvNkl"
    Me.lvNkl.Size = New System.Drawing.Size(285, 201)
    Me.lvNkl.TabIndex = 223
    Me.lvNkl.Tag = "Wykaz uczniów nieklasyfikowanych ze wszystkich przedmiotów"
    Me.lvNkl.UseCompatibleStateImageBehavior = False
    '
    'tcWykaz
    '
    Me.tcWykaz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tcWykaz.Controls.Add(Me.tp1)
    Me.tcWykaz.Location = New System.Drawing.Point(12, 222)
    Me.tcWykaz.Name = "tcWykaz"
    Me.tcWykaz.SelectedIndex = 0
    Me.tcWykaz.Size = New System.Drawing.Size(880, 404)
    Me.tcWykaz.TabIndex = 232
    Me.tcWykaz.Visible = False
    '
    'tlpKlasyfikacjaRoczna
    '
    Me.tlpKlasyfikacjaRoczna.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpKlasyfikacjaRoczna.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    Me.tlpKlasyfikacjaRoczna.ColumnCount = 8
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.003603!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.42806!))
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label32, 4, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblNoPromocja, 7, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblPromocja, 6, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblEgzPopraw2Ndst, 5, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblNoPromocjaEtykieta, 7, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblPromocjaEtykieta, 6, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label26, 1, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblEgzPopraw1Ndst, 4, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblNoNDSTRS, 3, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblNKLRS, 2, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblKLRS, 1, 3)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label19, 0, 0)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label20, 1, 0)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label21, 3, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label22, 4, 2)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label23, 5, 2)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label24, 2, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblStanKlasyRS, 0, 3)
    Me.tlpKlasyfikacjaRoczna.Location = New System.Drawing.Point(77, 336)
    Me.tlpKlasyfikacjaRoczna.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpKlasyfikacjaRoczna.Name = "tlpKlasyfikacjaRoczna"
    Me.tlpKlasyfikacjaRoczna.RowCount = 4
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    Me.tlpKlasyfikacjaRoczna.Size = New System.Drawing.Size(873, 90)
    Me.tlpKlasyfikacjaRoczna.TabIndex = 243
    Me.tlpKlasyfikacjaRoczna.Visible = False
    '
    'Label32
    '
    Me.Label32.BackColor = System.Drawing.SystemColors.Window
    Me.tlpKlasyfikacjaRoczna.SetColumnSpan(Me.Label32, 2)
    Me.Label32.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label32.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label32.Location = New System.Drawing.Point(404, 23)
    Me.Label32.Margin = New System.Windows.Forms.Padding(0)
    Me.Label32.Name = "Label32"
    Me.Label32.Size = New System.Drawing.Size(233, 21)
    Me.Label32.TabIndex = 230
    Me.Label32.Text = "którzy przystąpią do egzam. popraw."
    Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNoPromocja
    '
    Me.lblNoPromocja.AutoSize = True
    Me.lblNoPromocja.BackColor = System.Drawing.SystemColors.Window
    Me.lblNoPromocja.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNoPromocja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNoPromocja.Location = New System.Drawing.Point(755, 67)
    Me.lblNoPromocja.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNoPromocja.Name = "lblNoPromocja"
    Me.lblNoPromocja.Size = New System.Drawing.Size(117, 22)
    Me.lblNoPromocja.TabIndex = 229
    Me.lblNoPromocja.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblPromocja
    '
    Me.lblPromocja.AutoSize = True
    Me.lblPromocja.BackColor = System.Drawing.SystemColors.Window
    Me.lblPromocja.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblPromocja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblPromocja.Location = New System.Drawing.Point(638, 67)
    Me.lblPromocja.Margin = New System.Windows.Forms.Padding(0)
    Me.lblPromocja.Name = "lblPromocja"
    Me.lblPromocja.Size = New System.Drawing.Size(116, 22)
    Me.lblPromocja.TabIndex = 228
    Me.lblPromocja.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblEgzPopraw2Ndst
    '
    Me.lblEgzPopraw2Ndst.AutoSize = True
    Me.lblEgzPopraw2Ndst.BackColor = System.Drawing.SystemColors.Window
    Me.lblEgzPopraw2Ndst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblEgzPopraw2Ndst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblEgzPopraw2Ndst.Location = New System.Drawing.Point(521, 67)
    Me.lblEgzPopraw2Ndst.Margin = New System.Windows.Forms.Padding(0)
    Me.lblEgzPopraw2Ndst.Name = "lblEgzPopraw2Ndst"
    Me.lblEgzPopraw2Ndst.Size = New System.Drawing.Size(116, 22)
    Me.lblEgzPopraw2Ndst.TabIndex = 227
    Me.lblEgzPopraw2Ndst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNoPromocjaEtykieta
    '
    Me.lblNoPromocjaEtykieta.BackColor = System.Drawing.SystemColors.Window
    Me.lblNoPromocjaEtykieta.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNoPromocjaEtykieta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNoPromocjaEtykieta.ForeColor = System.Drawing.SystemColors.Desktop
    Me.lblNoPromocjaEtykieta.Location = New System.Drawing.Point(755, 23)
    Me.lblNoPromocjaEtykieta.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNoPromocjaEtykieta.Name = "lblNoPromocjaEtykieta"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.lblNoPromocjaEtykieta, 2)
    Me.lblNoPromocjaEtykieta.Size = New System.Drawing.Size(117, 43)
    Me.lblNoPromocjaEtykieta.TabIndex = 226
    Me.lblNoPromocjaEtykieta.Text = "niepromowanych"
    Me.lblNoPromocjaEtykieta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblPromocjaEtykieta
    '
    Me.lblPromocjaEtykieta.BackColor = System.Drawing.SystemColors.Window
    Me.lblPromocjaEtykieta.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblPromocjaEtykieta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblPromocjaEtykieta.ForeColor = System.Drawing.SystemColors.Desktop
    Me.lblPromocjaEtykieta.Location = New System.Drawing.Point(638, 23)
    Me.lblPromocjaEtykieta.Margin = New System.Windows.Forms.Padding(0)
    Me.lblPromocjaEtykieta.Name = "lblPromocjaEtykieta"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.lblPromocjaEtykieta, 2)
    Me.lblPromocjaEtykieta.Size = New System.Drawing.Size(116, 43)
    Me.lblPromocjaEtykieta.TabIndex = 225
    Me.lblPromocjaEtykieta.Text = "promowanych"
    Me.lblPromocjaEtykieta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label26
    '
    Me.Label26.BackColor = System.Drawing.SystemColors.Window
    Me.Label26.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label26.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label26.Location = New System.Drawing.Point(53, 23)
    Me.Label26.Margin = New System.Windows.Forms.Padding(0)
    Me.Label26.Name = "Label26"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label26, 2)
    Me.Label26.Size = New System.Drawing.Size(116, 43)
    Me.Label26.TabIndex = 224
    Me.Label26.Text = "klasyfikowanych"
    Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblEgzPopraw1Ndst
    '
    Me.lblEgzPopraw1Ndst.AutoSize = True
    Me.lblEgzPopraw1Ndst.BackColor = System.Drawing.SystemColors.Window
    Me.lblEgzPopraw1Ndst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblEgzPopraw1Ndst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblEgzPopraw1Ndst.Location = New System.Drawing.Point(404, 67)
    Me.lblEgzPopraw1Ndst.Margin = New System.Windows.Forms.Padding(0)
    Me.lblEgzPopraw1Ndst.Name = "lblEgzPopraw1Ndst"
    Me.lblEgzPopraw1Ndst.Size = New System.Drawing.Size(116, 22)
    Me.lblEgzPopraw1Ndst.TabIndex = 223
    Me.lblEgzPopraw1Ndst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNoNDSTRS
    '
    Me.lblNoNDSTRS.AutoSize = True
    Me.lblNoNDSTRS.BackColor = System.Drawing.SystemColors.Window
    Me.lblNoNDSTRS.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNoNDSTRS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNoNDSTRS.Location = New System.Drawing.Point(287, 67)
    Me.lblNoNDSTRS.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNoNDSTRS.Name = "lblNoNDSTRS"
    Me.lblNoNDSTRS.Size = New System.Drawing.Size(116, 22)
    Me.lblNoNDSTRS.TabIndex = 222
    Me.lblNoNDSTRS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNKLRS
    '
    Me.lblNKLRS.AutoSize = True
    Me.lblNKLRS.BackColor = System.Drawing.SystemColors.Window
    Me.lblNKLRS.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNKLRS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNKLRS.Location = New System.Drawing.Point(170, 67)
    Me.lblNKLRS.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNKLRS.Name = "lblNKLRS"
    Me.lblNKLRS.Size = New System.Drawing.Size(116, 22)
    Me.lblNKLRS.TabIndex = 221
    Me.lblNKLRS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblKLRS
    '
    Me.lblKLRS.AutoSize = True
    Me.lblKLRS.BackColor = System.Drawing.SystemColors.Window
    Me.lblKLRS.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblKLRS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblKLRS.Location = New System.Drawing.Point(53, 67)
    Me.lblKLRS.Margin = New System.Windows.Forms.Padding(0)
    Me.lblKLRS.Name = "lblKLRS"
    Me.lblKLRS.Size = New System.Drawing.Size(116, 22)
    Me.lblKLRS.TabIndex = 220
    Me.lblKLRS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label19
    '
    Me.Label19.BackColor = System.Drawing.SystemColors.Window
    Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label19.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label19.Location = New System.Drawing.Point(1, 1)
    Me.Label19.Margin = New System.Windows.Forms.Padding(0)
    Me.Label19.Name = "Label19"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label19, 3)
    Me.Label19.Size = New System.Drawing.Size(51, 65)
    Me.Label19.TabIndex = 213
    Me.Label19.Text = "Stan klasy"
    Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label20
    '
    Me.Label20.BackColor = System.Drawing.SystemColors.Window
    Me.tlpKlasyfikacjaRoczna.SetColumnSpan(Me.Label20, 7)
    Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label20.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label20.Location = New System.Drawing.Point(53, 1)
    Me.Label20.Margin = New System.Windows.Forms.Padding(0)
    Me.Label20.Name = "Label20"
    Me.Label20.Size = New System.Drawing.Size(819, 21)
    Me.Label20.TabIndex = 214
    Me.Label20.Text = "Liczba uczniów z ocenami niedostatecznymi"
    Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label21
    '
    Me.Label21.BackColor = System.Drawing.SystemColors.Window
    Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label21.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label21.Location = New System.Drawing.Point(287, 23)
    Me.Label21.Margin = New System.Windows.Forms.Padding(0)
    Me.Label21.Name = "Label21"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label21, 2)
    Me.Label21.Size = New System.Drawing.Size(116, 43)
    Me.Label21.TabIndex = 215
    Me.Label21.Text = "bez ocen ndst"
    Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label22
    '
    Me.Label22.BackColor = System.Drawing.SystemColors.Window
    Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label22.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label22.Location = New System.Drawing.Point(404, 45)
    Me.Label22.Margin = New System.Windows.Forms.Padding(0)
    Me.Label22.Name = "Label22"
    Me.Label22.Size = New System.Drawing.Size(116, 21)
    Me.Label22.TabIndex = 216
    Me.Label22.Text = "z 1 oceną ndst"
    Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label23
    '
    Me.Label23.BackColor = System.Drawing.SystemColors.Window
    Me.Label23.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label23.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label23.Location = New System.Drawing.Point(521, 45)
    Me.Label23.Margin = New System.Windows.Forms.Padding(0)
    Me.Label23.Name = "Label23"
    Me.Label23.Size = New System.Drawing.Size(116, 21)
    Me.Label23.TabIndex = 217
    Me.Label23.Text = "z 2 ocenami ndst"
    Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label24
    '
    Me.Label24.BackColor = System.Drawing.SystemColors.Window
    Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label24.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label24.Location = New System.Drawing.Point(170, 23)
    Me.Label24.Margin = New System.Windows.Forms.Padding(0)
    Me.Label24.Name = "Label24"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label24, 2)
    Me.Label24.Size = New System.Drawing.Size(116, 43)
    Me.Label24.TabIndex = 218
    Me.Label24.Text = "nkl ze wszystkich przedmiotów"
    Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblStanKlasyRS
    '
    Me.lblStanKlasyRS.AutoSize = True
    Me.lblStanKlasyRS.BackColor = System.Drawing.SystemColors.Window
    Me.lblStanKlasyRS.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblStanKlasyRS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblStanKlasyRS.Location = New System.Drawing.Point(1, 67)
    Me.lblStanKlasyRS.Margin = New System.Windows.Forms.Padding(0)
    Me.lblStanKlasyRS.Name = "lblStanKlasyRS"
    Me.lblStanKlasyRS.Size = New System.Drawing.Size(51, 22)
    Me.lblStanKlasyRS.TabIndex = 219
    Me.lblStanKlasyRS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(647, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(40, 13)
    Me.Label1.TabIndex = 236
    Me.Label1.Text = "Status:"
    '
    'lblStatus
    '
    Me.lblStatus.AutoSize = True
    Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblStatus.Location = New System.Drawing.Point(693, 15)
    Me.lblStatus.Name = "lblStatus"
    Me.lblStatus.Size = New System.Drawing.Size(43, 13)
    Me.lblStatus.TabIndex = 244
    Me.lblStatus.Text = "Status"
    '
    'frmKlasyfikacja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1026, 638)
    Me.Controls.Add(Me.lblStatus)
    Me.Controls.Add(Me.tlpKlasyfikacjaRoczna)
    Me.Controls.Add(Me.lblAvg)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Label14)
    Me.Controls.Add(Me.lblDataRP)
    Me.Controls.Add(Me.labelAvg)
    Me.Controls.Add(Me.lblRP)
    Me.Controls.Add(Me.tcWykaz)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.tlpZachowanie)
    Me.Controls.Add(Me.lblWychowawca)
    Me.Controls.Add(Me.tlpKlasyfikacja)
    Me.Controls.Add(Me.cmdDelete)
    Me.Controls.Add(Me.cmdSend)
    Me.Controls.Add(Me.cmdAddNew)
    Me.Controls.Add(Me.rbRokSzkolny)
    Me.Controls.Add(Me.rbSemestr)
    Me.Controls.Add(Me.lblObsadaFilter)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Name = "frmKlasyfikacja"
    Me.Text = "Wyniki klasyfikacji"
    Me.tlpKlasyfikacja.ResumeLayout(False)
    Me.tlpKlasyfikacja.PerformLayout()
    Me.tlpZachowanie.ResumeLayout(False)
    Me.tlpZachowanie.PerformLayout()
    Me.tp1.ResumeLayout(False)
    Me.tcWykaz.ResumeLayout(False)
    Me.tlpKlasyfikacjaRoczna.ResumeLayout(False)
    Me.tlpKlasyfikacjaRoczna.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents lblObsadaFilter As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents cmdAddNew As System.Windows.Forms.Button
  Friend WithEvents cmdSend As System.Windows.Forms.Button
  Friend WithEvents cmdDelete As System.Windows.Forms.Button
  Friend WithEvents tlpKlasyfikacja As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents lblWychowawca As System.Windows.Forms.Label
  Friend WithEvents hStanKlasy As System.Windows.Forms.Label
  Friend WithEvents hStudentNumber As System.Windows.Forms.Label
  Friend WithEvents hNoNdst As System.Windows.Forms.Label
  Friend WithEvents tlpZachowanie As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents h12Ndst As System.Windows.Forms.Label
  Friend WithEvents h3AndMoreNdst As System.Windows.Forms.Label
  Friend WithEvents hKL As System.Windows.Forms.Label
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents Label7 As System.Windows.Forms.Label
  Friend WithEvents Label8 As System.Windows.Forms.Label
  Friend WithEvents Label9 As System.Windows.Forms.Label
  Friend WithEvents Label10 As System.Windows.Forms.Label
  Friend WithEvents cmdRefresh As System.Windows.Forms.Button
  Friend WithEvents lblNKL As System.Windows.Forms.Label
  Friend WithEvents lbl3AndMoreNDST As System.Windows.Forms.Label
  Friend WithEvents lbl12NDST As System.Windows.Forms.Label
  Friend WithEvents lblNoNDST As System.Windows.Forms.Label
  Friend WithEvents lblStanKlasy As System.Windows.Forms.Label
  Friend WithEvents lblDb As System.Windows.Forms.Label
  Friend WithEvents lblBdb As System.Windows.Forms.Label
  Friend WithEvents lblWz As System.Windows.Forms.Label
  Friend WithEvents lblNg As System.Windows.Forms.Label
  Friend WithEvents lblNdp As System.Windows.Forms.Label
  Friend WithEvents lblPop As System.Windows.Forms.Label
  Friend WithEvents lblRP As System.Windows.Forms.Label
  Friend WithEvents labelAvg As System.Windows.Forms.Label
  Friend WithEvents lblDataRP As System.Windows.Forms.Label
  Friend WithEvents Label14 As System.Windows.Forms.Label
  Friend WithEvents lblAvg As System.Windows.Forms.Label
  Friend WithEvents lblKL As System.Windows.Forms.Label
  Friend WithEvents hNKL As System.Windows.Forms.Label
  Friend WithEvents tp1 As System.Windows.Forms.TabPage
  Friend WithEvents lvNdstByPrzedmiot As System.Windows.Forms.ListView
  Friend WithEvents lvZachowanie As System.Windows.Forms.ListView
  Friend WithEvents lvNdst As System.Windows.Forms.ListView
  Friend WithEvents lvNkl As System.Windows.Forms.ListView
  Friend WithEvents tcWykaz As System.Windows.Forms.TabControl
  Friend WithEvents tlpKlasyfikacjaRoczna As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents Label32 As System.Windows.Forms.Label
  Friend WithEvents lblNoPromocja As System.Windows.Forms.Label
  Friend WithEvents lblPromocja As System.Windows.Forms.Label
  Friend WithEvents lblEgzPopraw2Ndst As System.Windows.Forms.Label
  Friend WithEvents lblNoPromocjaEtykieta As System.Windows.Forms.Label
  Friend WithEvents lblPromocjaEtykieta As System.Windows.Forms.Label
  Friend WithEvents Label26 As System.Windows.Forms.Label
  Friend WithEvents lblEgzPopraw1Ndst As System.Windows.Forms.Label
  Friend WithEvents lblNoNDSTRS As System.Windows.Forms.Label
  Friend WithEvents lblNKLRS As System.Windows.Forms.Label
  Friend WithEvents lblKLRS As System.Windows.Forms.Label
  Friend WithEvents Label19 As System.Windows.Forms.Label
  Friend WithEvents Label20 As System.Windows.Forms.Label
  Friend WithEvents Label21 As System.Windows.Forms.Label
  Friend WithEvents Label22 As System.Windows.Forms.Label
  Friend WithEvents Label23 As System.Windows.Forms.Label
  Friend WithEvents Label24 As System.Windows.Forms.Label
  Friend WithEvents lblStanKlasyRS As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
