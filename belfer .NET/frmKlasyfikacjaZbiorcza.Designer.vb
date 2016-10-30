<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKlasyfikacjaZbiorcza
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
    Me.tlpKlasyfikacja = New System.Windows.Forms.TableLayoutPanel()
    Me.hStatus = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.hNKL = New System.Windows.Forms.Label()
    Me.hKlasa = New System.Windows.Forms.Label()
    Me.hStudentNumber = New System.Windows.Forms.Label()
    Me.hNoNdst = New System.Windows.Forms.Label()
    Me.h12Ndst = New System.Windows.Forms.Label()
    Me.h3AndMoreNdst = New System.Windows.Forms.Label()
    Me.hKL = New System.Windows.Forms.Label()
    Me.tlpZachowanie = New System.Windows.Forms.TableLayoutPanel()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.hStanKlasy = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.cmdRefresh = New System.Windows.Forms.Button()
    Me.tlpKlasyfikacjaRoczna = New System.Windows.Forms.TableLayoutPanel()
    Me.Klasa = New System.Windows.Forms.Label()
    Me.Label32 = New System.Windows.Forms.Label()
    Me.lblNoPromocjaEtykieta = New System.Windows.Forms.Label()
    Me.lblPromocjaEtykieta = New System.Windows.Forms.Label()
    Me.Label26 = New System.Windows.Forms.Label()
    Me.Label19 = New System.Windows.Forms.Label()
    Me.Label20 = New System.Windows.Forms.Label()
    Me.Label21 = New System.Windows.Forms.Label()
    Me.Label22 = New System.Windows.Forms.Label()
    Me.Label23 = New System.Windows.Forms.Label()
    Me.Label24 = New System.Windows.Forms.Label()
    Me.lblStatus = New System.Windows.Forms.Label()
    Me.lvKlasyfikacja = New System.Windows.Forms.ListView()
    Me.cmdReject = New System.Windows.Forms.Button()
    Me.cmdAccept = New System.Windows.Forms.Button()
    Me.gbTyp = New System.Windows.Forms.GroupBox()
    Me.rbPrzedmiot = New System.Windows.Forms.RadioButton()
    Me.rbZachowanie = New System.Windows.Forms.RadioButton()
    Me.gbStatus = New System.Windows.Forms.GroupBox()
    Me.rbMissing = New System.Windows.Forms.RadioButton()
    Me.rbAll = New System.Windows.Forms.RadioButton()
    Me.rbSubmitted = New System.Windows.Forms.RadioButton()
    Me.rbRejected = New System.Windows.Forms.RadioButton()
    Me.rbAccepted = New System.Windows.Forms.RadioButton()
    Me.gbZakres = New System.Windows.Forms.GroupBox()
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblRecord = New System.Windows.Forms.Label()
    Me.tlpKlasyfikacja.SuspendLayout()
    Me.tlpZachowanie.SuspendLayout()
    Me.tlpKlasyfikacjaRoczna.SuspendLayout()
    Me.gbTyp.SuspendLayout()
    Me.gbStatus.SuspendLayout()
    Me.gbZakres.SuspendLayout()
    Me.SuspendLayout()
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Enabled = False
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(901, 130)
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
    Me.cmdClose.Location = New System.Drawing.Point(901, 570)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 124
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'tlpKlasyfikacja
    '
    Me.tlpKlasyfikacja.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpKlasyfikacja.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    Me.tlpKlasyfikacja.ColumnCount = 8
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
    Me.tlpKlasyfikacja.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
    Me.tlpKlasyfikacja.Controls.Add(Me.hStatus, 7, 0)
    Me.tlpKlasyfikacja.Controls.Add(Me.Label2, 1, 0)
    Me.tlpKlasyfikacja.Controls.Add(Me.hNKL, 3, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.hKlasa, 0, 0)
    Me.tlpKlasyfikacja.Controls.Add(Me.hStudentNumber, 2, 0)
    Me.tlpKlasyfikacja.Controls.Add(Me.hNoNdst, 4, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.h12Ndst, 5, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.h3AndMoreNdst, 6, 1)
    Me.tlpKlasyfikacja.Controls.Add(Me.hKL, 2, 1)
    Me.tlpKlasyfikacja.Location = New System.Drawing.Point(9, 429)
    Me.tlpKlasyfikacja.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpKlasyfikacja.Name = "tlpKlasyfikacja"
    Me.tlpKlasyfikacja.RowCount = 3
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
    Me.tlpKlasyfikacja.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
    Me.tlpKlasyfikacja.Size = New System.Drawing.Size(886, 68)
    Me.tlpKlasyfikacja.TabIndex = 211
    Me.tlpKlasyfikacja.Visible = False
    '
    'hStatus
    '
    Me.hStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.hStatus.BackColor = System.Drawing.SystemColors.Window
    Me.hStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hStatus.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hStatus.Location = New System.Drawing.Point(822, 1)
    Me.hStatus.Margin = New System.Windows.Forms.Padding(0)
    Me.hStatus.Name = "hStatus"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hStatus, 3)
    Me.hStatus.Size = New System.Drawing.Size(66, 66)
    Me.hStatus.TabIndex = 252
    Me.hStatus.Text = "Status"
    Me.hStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label2
    '
    Me.Label2.BackColor = System.Drawing.SystemColors.Window
    Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label2.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label2.Location = New System.Drawing.Point(54, 1)
    Me.Label2.Margin = New System.Windows.Forms.Padding(0)
    Me.Label2.Name = "Label2"
    Me.tlpKlasyfikacja.SetRowSpan(Me.Label2, 3)
    Me.Label2.Size = New System.Drawing.Size(52, 66)
    Me.Label2.TabIndex = 252
    Me.Label2.Text = "Stan klasy"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hNKL
    '
    Me.hNKL.BackColor = System.Drawing.SystemColors.Window
    Me.hNKL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hNKL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hNKL.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hNKL.Location = New System.Drawing.Point(250, 23)
    Me.hNKL.Margin = New System.Windows.Forms.Padding(0)
    Me.hNKL.Name = "hNKL"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hNKL, 2)
    Me.hNKL.Size = New System.Drawing.Size(142, 44)
    Me.hNKL.TabIndex = 224
    Me.hNKL.Text = "nkl ze wszystkich przedmiotów"
    Me.hNKL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hKlasa
    '
    Me.hKlasa.BackColor = System.Drawing.SystemColors.Window
    Me.hKlasa.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hKlasa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hKlasa.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hKlasa.Location = New System.Drawing.Point(1, 1)
    Me.hKlasa.Margin = New System.Windows.Forms.Padding(0)
    Me.hKlasa.Name = "hKlasa"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hKlasa, 3)
    Me.hKlasa.Size = New System.Drawing.Size(52, 66)
    Me.hKlasa.TabIndex = 213
    Me.hKlasa.Text = "Klasa"
    Me.hKlasa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hStudentNumber
    '
    Me.hStudentNumber.BackColor = System.Drawing.SystemColors.Window
    Me.tlpKlasyfikacja.SetColumnSpan(Me.hStudentNumber, 5)
    Me.hStudentNumber.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hStudentNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hStudentNumber.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hStudentNumber.Location = New System.Drawing.Point(107, 1)
    Me.hStudentNumber.Margin = New System.Windows.Forms.Padding(0)
    Me.hStudentNumber.Name = "hStudentNumber"
    Me.hStudentNumber.Size = New System.Drawing.Size(714, 21)
    Me.hStudentNumber.TabIndex = 214
    Me.hStudentNumber.Text = "Liczba uczniów"
    Me.hStudentNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hNoNdst
    '
    Me.hNoNdst.BackColor = System.Drawing.SystemColors.Window
    Me.hNoNdst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hNoNdst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hNoNdst.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hNoNdst.Location = New System.Drawing.Point(393, 23)
    Me.hNoNdst.Margin = New System.Windows.Forms.Padding(0)
    Me.hNoNdst.Name = "hNoNdst"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hNoNdst, 2)
    Me.hNoNdst.Size = New System.Drawing.Size(142, 44)
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
    Me.h12Ndst.Location = New System.Drawing.Point(536, 23)
    Me.h12Ndst.Margin = New System.Windows.Forms.Padding(0)
    Me.h12Ndst.Name = "h12Ndst"
    Me.tlpKlasyfikacja.SetRowSpan(Me.h12Ndst, 2)
    Me.h12Ndst.Size = New System.Drawing.Size(142, 44)
    Me.h12Ndst.TabIndex = 216
    Me.h12Ndst.Text = "z jedną lub dwiema ocenami ndst"
    Me.h12Ndst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'h3AndMoreNdst
    '
    Me.h3AndMoreNdst.BackColor = System.Drawing.SystemColors.Window
    Me.h3AndMoreNdst.Dock = System.Windows.Forms.DockStyle.Fill
    Me.h3AndMoreNdst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.h3AndMoreNdst.ForeColor = System.Drawing.SystemColors.Desktop
    Me.h3AndMoreNdst.Location = New System.Drawing.Point(679, 23)
    Me.h3AndMoreNdst.Margin = New System.Windows.Forms.Padding(0)
    Me.h3AndMoreNdst.Name = "h3AndMoreNdst"
    Me.tlpKlasyfikacja.SetRowSpan(Me.h3AndMoreNdst, 2)
    Me.h3AndMoreNdst.Size = New System.Drawing.Size(142, 44)
    Me.h3AndMoreNdst.TabIndex = 217
    Me.h3AndMoreNdst.Text = "z trzema i więcej ocenami ndst"
    Me.h3AndMoreNdst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'hKL
    '
    Me.hKL.BackColor = System.Drawing.SystemColors.Window
    Me.hKL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.hKL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.hKL.ForeColor = System.Drawing.SystemColors.Desktop
    Me.hKL.Location = New System.Drawing.Point(107, 23)
    Me.hKL.Margin = New System.Windows.Forms.Padding(0)
    Me.hKL.Name = "hKL"
    Me.tlpKlasyfikacja.SetRowSpan(Me.hKL, 2)
    Me.hKL.Size = New System.Drawing.Size(142, 44)
    Me.hKL.TabIndex = 218
    Me.hKL.Text = "klasyfikowanych"
    Me.hKL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'tlpZachowanie
    '
    Me.tlpZachowanie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpZachowanie.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    Me.tlpZachowanie.ColumnCount = 10
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpZachowanie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59.0!))
    Me.tlpZachowanie.Controls.Add(Me.Label12, 2, 0)
    Me.tlpZachowanie.Controls.Add(Me.Label4, 1, 0)
    Me.tlpZachowanie.Controls.Add(Me.hStanKlasy, 0, 0)
    Me.tlpZachowanie.Controls.Add(Me.Label3, 3, 0)
    Me.tlpZachowanie.Controls.Add(Me.Label5, 3, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label6, 4, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label7, 5, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label8, 6, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label9, 7, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label10, 8, 1)
    Me.tlpZachowanie.Controls.Add(Me.Label11, 9, 0)
    Me.tlpZachowanie.Location = New System.Drawing.Point(9, 533)
    Me.tlpZachowanie.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpZachowanie.Name = "tlpZachowanie"
    Me.tlpZachowanie.RowCount = 2
    Me.tlpZachowanie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
    Me.tlpZachowanie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.0!))
    Me.tlpZachowanie.Size = New System.Drawing.Size(885, 68)
    Me.tlpZachowanie.TabIndex = 213
    Me.tlpZachowanie.Visible = False
    '
    'Label12
    '
    Me.Label12.BackColor = System.Drawing.SystemColors.Window
    Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label12.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label12.Location = New System.Drawing.Point(107, 1)
    Me.Label12.Margin = New System.Windows.Forms.Padding(0)
    Me.Label12.Name = "Label12"
    Me.tlpZachowanie.SetRowSpan(Me.Label12, 2)
    Me.Label12.Size = New System.Drawing.Size(102, 66)
    Me.Label12.TabIndex = 252
    Me.Label12.Text = "klasyfikowanych"
    Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label4
    '
    Me.Label4.BackColor = System.Drawing.SystemColors.Window
    Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label4.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label4.Location = New System.Drawing.Point(54, 1)
    Me.Label4.Margin = New System.Windows.Forms.Padding(0)
    Me.Label4.Name = "Label4"
    Me.tlpZachowanie.SetRowSpan(Me.Label4, 2)
    Me.Label4.Size = New System.Drawing.Size(52, 66)
    Me.Label4.TabIndex = 252
    Me.Label4.Text = "Stan klasy"
    Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
    Me.tlpZachowanie.SetRowSpan(Me.hStanKlasy, 2)
    Me.hStanKlasy.Size = New System.Drawing.Size(52, 66)
    Me.hStanKlasy.TabIndex = 252
    Me.hStanKlasy.Text = "Klasa"
    Me.hStanKlasy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label3
    '
    Me.Label3.BackColor = System.Drawing.SystemColors.Window
    Me.tlpZachowanie.SetColumnSpan(Me.Label3, 6)
    Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label3.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label3.Location = New System.Drawing.Point(210, 1)
    Me.Label3.Margin = New System.Windows.Forms.Padding(0)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(617, 22)
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
    Me.Label5.Location = New System.Drawing.Point(210, 24)
    Me.Label5.Margin = New System.Windows.Forms.Padding(0)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(102, 43)
    Me.Label5.TabIndex = 1
    Me.Label5.Tag = "6"
    Me.Label5.Text = "wzorowych"
    Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label6
    '
    Me.Label6.BackColor = System.Drawing.SystemColors.Window
    Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label6.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label6.Location = New System.Drawing.Point(313, 24)
    Me.Label6.Margin = New System.Windows.Forms.Padding(0)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(102, 43)
    Me.Label6.TabIndex = 2
    Me.Label6.Tag = "5"
    Me.Label6.Text = "bardzo dobrych"
    Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label7
    '
    Me.Label7.BackColor = System.Drawing.SystemColors.Window
    Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label7.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label7.Location = New System.Drawing.Point(416, 24)
    Me.Label7.Margin = New System.Windows.Forms.Padding(0)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(102, 43)
    Me.Label7.TabIndex = 3
    Me.Label7.Tag = "4"
    Me.Label7.Text = "dobrych"
    Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label8
    '
    Me.Label8.BackColor = System.Drawing.SystemColors.Window
    Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label8.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label8.Location = New System.Drawing.Point(519, 24)
    Me.Label8.Margin = New System.Windows.Forms.Padding(0)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(102, 43)
    Me.Label8.TabIndex = 4
    Me.Label8.Tag = "3"
    Me.Label8.Text = "poprawnych"
    Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label9
    '
    Me.Label9.BackColor = System.Drawing.SystemColors.Window
    Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label9.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label9.Location = New System.Drawing.Point(622, 24)
    Me.Label9.Margin = New System.Windows.Forms.Padding(0)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(102, 43)
    Me.Label9.TabIndex = 5
    Me.Label9.Tag = "2"
    Me.Label9.Text = "nieodpowiednich"
    Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label10
    '
    Me.Label10.BackColor = System.Drawing.SystemColors.Window
    Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label10.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label10.Location = New System.Drawing.Point(725, 24)
    Me.Label10.Margin = New System.Windows.Forms.Padding(0)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(102, 43)
    Me.Label10.TabIndex = 6
    Me.Label10.Tag = "1"
    Me.Label10.Text = "nagannych"
    Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.BackColor = System.Drawing.SystemColors.Window
    Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label11.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label11.Location = New System.Drawing.Point(828, 1)
    Me.Label11.Margin = New System.Windows.Forms.Padding(0)
    Me.Label11.Name = "Label11"
    Me.tlpZachowanie.SetRowSpan(Me.Label11, 2)
    Me.Label11.Size = New System.Drawing.Size(59, 66)
    Me.Label11.TabIndex = 253
    Me.Label11.Text = "Status"
    Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRefresh.Image = Global.belfer.NET.My.Resources.Resources.refresh_24
    Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdRefresh.Location = New System.Drawing.Point(901, 168)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(117, 36)
    Me.cmdRefresh.TabIndex = 231
    Me.cmdRefresh.Text = "Odśwież"
    Me.cmdRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdRefresh.UseVisualStyleBackColor = True
    '
    'tlpKlasyfikacjaRoczna
    '
    Me.tlpKlasyfikacjaRoczna.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tlpKlasyfikacjaRoczna.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    Me.tlpKlasyfikacjaRoczna.ColumnCount = 10
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
    Me.tlpKlasyfikacjaRoczna.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Klasa, 0, 0)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label32, 5, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblNoPromocjaEtykieta, 8, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblPromocjaEtykieta, 7, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label26, 2, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label19, 1, 0)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label20, 2, 0)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label21, 4, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label22, 5, 2)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label23, 6, 2)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.Label24, 3, 1)
    Me.tlpKlasyfikacjaRoczna.Controls.Add(Me.lblStatus, 9, 0)
    Me.tlpKlasyfikacjaRoczna.Location = New System.Drawing.Point(9, 45)
    Me.tlpKlasyfikacjaRoczna.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpKlasyfikacjaRoczna.Name = "tlpKlasyfikacjaRoczna"
    Me.tlpKlasyfikacjaRoczna.RowCount = 3
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
    Me.tlpKlasyfikacjaRoczna.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
    Me.tlpKlasyfikacjaRoczna.Size = New System.Drawing.Size(886, 68)
    Me.tlpKlasyfikacjaRoczna.TabIndex = 243
    '
    'Klasa
    '
    Me.Klasa.BackColor = System.Drawing.SystemColors.Window
    Me.Klasa.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Klasa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Klasa.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Klasa.Location = New System.Drawing.Point(1, 1)
    Me.Klasa.Margin = New System.Windows.Forms.Padding(0)
    Me.Klasa.Name = "Klasa"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Klasa, 3)
    Me.Klasa.Size = New System.Drawing.Size(52, 66)
    Me.Klasa.TabIndex = 252
    Me.Klasa.Text = "Klasa"
    Me.Klasa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label32
    '
    Me.Label32.BackColor = System.Drawing.SystemColors.Window
    Me.tlpKlasyfikacjaRoczna.SetColumnSpan(Me.Label32, 2)
    Me.Label32.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label32.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label32.Location = New System.Drawing.Point(416, 23)
    Me.Label32.Margin = New System.Windows.Forms.Padding(0)
    Me.Label32.Name = "Label32"
    Me.Label32.Size = New System.Drawing.Size(205, 21)
    Me.Label32.TabIndex = 230
    Me.Label32.Text = "którzy przystąpią do egz. popraw."
    Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblNoPromocjaEtykieta
    '
    Me.lblNoPromocjaEtykieta.BackColor = System.Drawing.SystemColors.Window
    Me.lblNoPromocjaEtykieta.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblNoPromocjaEtykieta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblNoPromocjaEtykieta.ForeColor = System.Drawing.SystemColors.Desktop
    Me.lblNoPromocjaEtykieta.Location = New System.Drawing.Point(725, 23)
    Me.lblNoPromocjaEtykieta.Margin = New System.Windows.Forms.Padding(0)
    Me.lblNoPromocjaEtykieta.Name = "lblNoPromocjaEtykieta"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.lblNoPromocjaEtykieta, 2)
    Me.lblNoPromocjaEtykieta.Size = New System.Drawing.Size(102, 44)
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
    Me.lblPromocjaEtykieta.Location = New System.Drawing.Point(622, 23)
    Me.lblPromocjaEtykieta.Margin = New System.Windows.Forms.Padding(0)
    Me.lblPromocjaEtykieta.Name = "lblPromocjaEtykieta"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.lblPromocjaEtykieta, 2)
    Me.lblPromocjaEtykieta.Size = New System.Drawing.Size(102, 44)
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
    Me.Label26.Location = New System.Drawing.Point(107, 23)
    Me.Label26.Margin = New System.Windows.Forms.Padding(0)
    Me.Label26.Name = "Label26"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label26, 2)
    Me.Label26.Size = New System.Drawing.Size(102, 44)
    Me.Label26.TabIndex = 224
    Me.Label26.Text = "klasyfikowanych"
    Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label19
    '
    Me.Label19.BackColor = System.Drawing.SystemColors.Window
    Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label19.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label19.Location = New System.Drawing.Point(54, 1)
    Me.Label19.Margin = New System.Windows.Forms.Padding(0)
    Me.Label19.Name = "Label19"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label19, 3)
    Me.Label19.Size = New System.Drawing.Size(52, 66)
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
    Me.Label20.Location = New System.Drawing.Point(107, 1)
    Me.Label20.Margin = New System.Windows.Forms.Padding(0)
    Me.Label20.Name = "Label20"
    Me.Label20.Size = New System.Drawing.Size(720, 21)
    Me.Label20.TabIndex = 214
    Me.Label20.Text = "Liczba uczniów"
    Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label21
    '
    Me.Label21.BackColor = System.Drawing.SystemColors.Window
    Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label21.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label21.Location = New System.Drawing.Point(313, 23)
    Me.Label21.Margin = New System.Windows.Forms.Padding(0)
    Me.Label21.Name = "Label21"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label21, 2)
    Me.Label21.Size = New System.Drawing.Size(102, 44)
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
    Me.Label22.Location = New System.Drawing.Point(416, 45)
    Me.Label22.Margin = New System.Windows.Forms.Padding(0)
    Me.Label22.Name = "Label22"
    Me.Label22.Size = New System.Drawing.Size(102, 22)
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
    Me.Label23.Location = New System.Drawing.Point(519, 45)
    Me.Label23.Margin = New System.Windows.Forms.Padding(0)
    Me.Label23.Name = "Label23"
    Me.Label23.Size = New System.Drawing.Size(102, 22)
    Me.Label23.TabIndex = 217
    Me.Label23.Text = "z 2 ocen. ndst"
    Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label24
    '
    Me.Label24.BackColor = System.Drawing.SystemColors.Window
    Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label24.ForeColor = System.Drawing.SystemColors.Desktop
    Me.Label24.Location = New System.Drawing.Point(210, 23)
    Me.Label24.Margin = New System.Windows.Forms.Padding(0)
    Me.Label24.Name = "Label24"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.Label24, 2)
    Me.Label24.Size = New System.Drawing.Size(102, 44)
    Me.Label24.TabIndex = 218
    Me.Label24.Text = "nkl ze wszystkich przedmiotów"
    Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblStatus
    '
    Me.lblStatus.AutoSize = True
    Me.lblStatus.BackColor = System.Drawing.SystemColors.Window
    Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblStatus.ForeColor = System.Drawing.SystemColors.Desktop
    Me.lblStatus.Location = New System.Drawing.Point(828, 1)
    Me.lblStatus.Margin = New System.Windows.Forms.Padding(0)
    Me.lblStatus.Name = "lblStatus"
    Me.tlpKlasyfikacjaRoczna.SetRowSpan(Me.lblStatus, 3)
    Me.lblStatus.Size = New System.Drawing.Size(60, 66)
    Me.lblStatus.TabIndex = 231
    Me.lblStatus.Text = "Status"
    Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lvKlasyfikacja
    '
    Me.lvKlasyfikacja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lvKlasyfikacja.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvKlasyfikacja.Location = New System.Drawing.Point(9, 113)
    Me.lvKlasyfikacja.Name = "lvKlasyfikacja"
    Me.lvKlasyfikacja.Size = New System.Drawing.Size(886, 288)
    Me.lvKlasyfikacja.TabIndex = 244
    Me.lvKlasyfikacja.UseCompatibleStateImageBehavior = False
    '
    'cmdReject
    '
    Me.cmdReject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdReject.Enabled = False
    Me.cmdReject.Image = Global.belfer.NET.My.Resources.Resources.reject_24
    Me.cmdReject.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdReject.Location = New System.Drawing.Point(901, 88)
    Me.cmdReject.Name = "cmdReject"
    Me.cmdReject.Size = New System.Drawing.Size(117, 36)
    Me.cmdReject.TabIndex = 245
    Me.cmdReject.Tag = "2"
    Me.cmdReject.Text = "Odrzuć"
    Me.cmdReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdReject.UseVisualStyleBackColor = True
    '
    'cmdAccept
    '
    Me.cmdAccept.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAccept.Enabled = False
    Me.cmdAccept.Image = Global.belfer.NET.My.Resources.Resources.accept_24
    Me.cmdAccept.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdAccept.Location = New System.Drawing.Point(901, 46)
    Me.cmdAccept.Name = "cmdAccept"
    Me.cmdAccept.Size = New System.Drawing.Size(117, 36)
    Me.cmdAccept.TabIndex = 246
    Me.cmdAccept.Tag = "3"
    Me.cmdAccept.Text = "Zatwierdź"
    Me.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdAccept.UseVisualStyleBackColor = True
    '
    'gbTyp
    '
    Me.gbTyp.Controls.Add(Me.rbPrzedmiot)
    Me.gbTyp.Controls.Add(Me.rbZachowanie)
    Me.gbTyp.Location = New System.Drawing.Point(12, 2)
    Me.gbTyp.Name = "gbTyp"
    Me.gbTyp.Size = New System.Drawing.Size(175, 40)
    Me.gbTyp.TabIndex = 247
    Me.gbTyp.TabStop = False
    Me.gbTyp.Text = "Typ"
    '
    'rbPrzedmiot
    '
    Me.rbPrzedmiot.AutoSize = True
    Me.rbPrzedmiot.Location = New System.Drawing.Point(6, 17)
    Me.rbPrzedmiot.Name = "rbPrzedmiot"
    Me.rbPrzedmiot.Size = New System.Drawing.Size(71, 17)
    Me.rbPrzedmiot.TabIndex = 201
    Me.rbPrzedmiot.TabStop = True
    Me.rbPrzedmiot.Tag = "P"
    Me.rbPrzedmiot.Text = "Przedmiot"
    Me.rbPrzedmiot.UseVisualStyleBackColor = True
    '
    'rbZachowanie
    '
    Me.rbZachowanie.AutoSize = True
    Me.rbZachowanie.Location = New System.Drawing.Point(83, 17)
    Me.rbZachowanie.Name = "rbZachowanie"
    Me.rbZachowanie.Size = New System.Drawing.Size(84, 17)
    Me.rbZachowanie.TabIndex = 202
    Me.rbZachowanie.TabStop = True
    Me.rbZachowanie.Tag = "Z"
    Me.rbZachowanie.Text = "Zachowanie"
    Me.rbZachowanie.UseVisualStyleBackColor = True
    '
    'gbStatus
    '
    Me.gbStatus.Controls.Add(Me.rbMissing)
    Me.gbStatus.Controls.Add(Me.rbAll)
    Me.gbStatus.Controls.Add(Me.rbSubmitted)
    Me.gbStatus.Controls.Add(Me.rbRejected)
    Me.gbStatus.Controls.Add(Me.rbAccepted)
    Me.gbStatus.Location = New System.Drawing.Point(222, 2)
    Me.gbStatus.Name = "gbStatus"
    Me.gbStatus.Size = New System.Drawing.Size(429, 40)
    Me.gbStatus.TabIndex = 248
    Me.gbStatus.TabStop = False
    Me.gbStatus.Text = "Status"
    '
    'rbMissing
    '
    Me.rbMissing.AutoSize = True
    Me.rbMissing.Location = New System.Drawing.Point(350, 17)
    Me.rbMissing.Name = "rbMissing"
    Me.rbMissing.Size = New System.Drawing.Size(73, 17)
    Me.rbMissing.TabIndex = 210
    Me.rbMissing.TabStop = True
    Me.rbMissing.Tag = "0"
    Me.rbMissing.Text = "Brakujące"
    Me.rbMissing.UseVisualStyleBackColor = True
    '
    'rbAll
    '
    Me.rbAll.AutoSize = True
    Me.rbAll.Location = New System.Drawing.Point(6, 17)
    Me.rbAll.Name = "rbAll"
    Me.rbAll.Size = New System.Drawing.Size(73, 17)
    Me.rbAll.TabIndex = 209
    Me.rbAll.TabStop = True
    Me.rbAll.Tag = "Status IN (1,2,3) "
    Me.rbAll.Text = "Wszystkie"
    Me.rbAll.UseVisualStyleBackColor = True
    '
    'rbSubmitted
    '
    Me.rbSubmitted.AutoSize = True
    Me.rbSubmitted.Location = New System.Drawing.Point(85, 17)
    Me.rbSubmitted.Name = "rbSubmitted"
    Me.rbSubmitted.Size = New System.Drawing.Size(81, 17)
    Me.rbSubmitted.TabIndex = 205
    Me.rbSubmitted.TabStop = True
    Me.rbSubmitted.Tag = "Status IN (1) "
    Me.rbSubmitted.Text = "Przekazane"
    Me.rbSubmitted.UseVisualStyleBackColor = True
    '
    'rbRejected
    '
    Me.rbRejected.AutoSize = True
    Me.rbRejected.Location = New System.Drawing.Point(267, 17)
    Me.rbRejected.Name = "rbRejected"
    Me.rbRejected.Size = New System.Drawing.Size(77, 17)
    Me.rbRejected.TabIndex = 207
    Me.rbRejected.TabStop = True
    Me.rbRejected.Tag = "Status IN (2) "
    Me.rbRejected.Text = "Odrzucone"
    Me.rbRejected.UseVisualStyleBackColor = True
    '
    'rbAccepted
    '
    Me.rbAccepted.AutoSize = True
    Me.rbAccepted.Location = New System.Drawing.Point(172, 17)
    Me.rbAccepted.Name = "rbAccepted"
    Me.rbAccepted.Size = New System.Drawing.Size(89, 17)
    Me.rbAccepted.TabIndex = 206
    Me.rbAccepted.TabStop = True
    Me.rbAccepted.Tag = "Status IN (3) "
    Me.rbAccepted.Text = "Zatwierdzone"
    Me.rbAccepted.UseVisualStyleBackColor = True
    '
    'gbZakres
    '
    Me.gbZakres.Controls.Add(Me.rbSemestr)
    Me.gbZakres.Controls.Add(Me.rbRokSzkolny)
    Me.gbZakres.Location = New System.Drawing.Point(712, 2)
    Me.gbZakres.Name = "gbZakres"
    Me.gbZakres.Size = New System.Drawing.Size(169, 40)
    Me.gbZakres.TabIndex = 249
    Me.gbZakres.TabStop = False
    Me.gbZakres.Text = "Zakres"
    '
    'rbSemestr
    '
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Location = New System.Drawing.Point(6, 17)
    Me.rbSemestr.Name = "rbSemestr"
    Me.rbSemestr.Size = New System.Drawing.Size(69, 17)
    Me.rbSemestr.TabIndex = 201
    Me.rbSemestr.TabStop = True
    Me.rbSemestr.Tag = "S"
    Me.rbSemestr.Text = "Semestr I"
    Me.rbSemestr.UseVisualStyleBackColor = True
    '
    'rbRokSzkolny
    '
    Me.rbRokSzkolny.AutoSize = True
    Me.rbRokSzkolny.Location = New System.Drawing.Point(81, 17)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(82, 17)
    Me.rbRokSzkolny.TabIndex = 202
    Me.rbRokSzkolny.TabStop = True
    Me.rbRokSzkolny.Tag = "R"
    Me.rbRokSzkolny.Text = "RokSzkolny"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(6, 609)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(45, 13)
    Me.Label1.TabIndex = 250
    Me.Label1.Text = "Rekord:"
    '
    'lblRecord
    '
    Me.lblRecord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblRecord.AutoSize = True
    Me.lblRecord.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblRecord.ForeColor = System.Drawing.Color.Red
    Me.lblRecord.Location = New System.Drawing.Point(57, 609)
    Me.lblRecord.Name = "lblRecord"
    Me.lblRecord.Size = New System.Drawing.Size(61, 13)
    Me.lblRecord.TabIndex = 251
    Me.lblRecord.Text = "lblRecord"
    '
    'frmKlasyfikacjaZbiorcza
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1026, 638)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lblRecord)
    Me.Controls.Add(Me.gbZakres)
    Me.Controls.Add(Me.gbStatus)
    Me.Controls.Add(Me.gbTyp)
    Me.Controls.Add(Me.cmdReject)
    Me.Controls.Add(Me.cmdAccept)
    Me.Controls.Add(Me.lvKlasyfikacja)
    Me.Controls.Add(Me.tlpKlasyfikacjaRoczna)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.tlpZachowanie)
    Me.Controls.Add(Me.tlpKlasyfikacja)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Name = "frmKlasyfikacjaZbiorcza"
    Me.Text = "Zestawienie wyników klasyfikacji"
    Me.tlpKlasyfikacja.ResumeLayout(False)
    Me.tlpZachowanie.ResumeLayout(False)
    Me.tlpZachowanie.PerformLayout()
    Me.tlpKlasyfikacjaRoczna.ResumeLayout(False)
    Me.tlpKlasyfikacjaRoczna.PerformLayout()
    Me.gbTyp.ResumeLayout(False)
    Me.gbTyp.PerformLayout()
    Me.gbStatus.ResumeLayout(False)
    Me.gbStatus.PerformLayout()
    Me.gbZakres.ResumeLayout(False)
    Me.gbZakres.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents tlpKlasyfikacja As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents hKlasa As System.Windows.Forms.Label
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
  Friend WithEvents hNKL As System.Windows.Forms.Label
  Friend WithEvents tlpKlasyfikacjaRoczna As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents Label32 As System.Windows.Forms.Label
  Friend WithEvents lblNoPromocjaEtykieta As System.Windows.Forms.Label
  Friend WithEvents lblPromocjaEtykieta As System.Windows.Forms.Label
  Friend WithEvents Label26 As System.Windows.Forms.Label
  Friend WithEvents Label19 As System.Windows.Forms.Label
  Friend WithEvents Label20 As System.Windows.Forms.Label
  Friend WithEvents Label21 As System.Windows.Forms.Label
  Friend WithEvents Label22 As System.Windows.Forms.Label
  Friend WithEvents Label23 As System.Windows.Forms.Label
  Friend WithEvents Label24 As System.Windows.Forms.Label
  Friend WithEvents lvKlasyfikacja As System.Windows.Forms.ListView
  Friend WithEvents cmdReject As System.Windows.Forms.Button
  Friend WithEvents cmdAccept As System.Windows.Forms.Button
  Friend WithEvents gbTyp As System.Windows.Forms.GroupBox
  Friend WithEvents rbPrzedmiot As System.Windows.Forms.RadioButton
  Friend WithEvents rbZachowanie As System.Windows.Forms.RadioButton
  Friend WithEvents gbStatus As System.Windows.Forms.GroupBox
  Friend WithEvents rbAll As System.Windows.Forms.RadioButton
  Friend WithEvents rbSubmitted As System.Windows.Forms.RadioButton
  Friend WithEvents rbRejected As System.Windows.Forms.RadioButton
  Friend WithEvents rbAccepted As System.Windows.Forms.RadioButton
  Friend WithEvents gbZakres As System.Windows.Forms.GroupBox
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lblRecord As System.Windows.Forms.Label
  Friend WithEvents lblStatus As System.Windows.Forms.Label
  Friend WithEvents Klasa As System.Windows.Forms.Label
  Friend WithEvents rbMissing As System.Windows.Forms.RadioButton
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents hStatus As System.Windows.Forms.Label
  Friend WithEvents Label11 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents hStanKlasy As System.Windows.Forms.Label
  Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
