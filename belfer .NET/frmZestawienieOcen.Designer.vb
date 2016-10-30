<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmZestawienieOcen
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
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dgvZestawienieOcen = New System.Windows.Forms.DataGridView()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.rbSchoolYear = New System.Windows.Forms.RadioButton()
    Me.rbAll = New System.Windows.Forms.RadioButton()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.lblData = New System.Windows.Forms.Label()
    Me.lblIP = New System.Windows.Forms.Label()
    Me.lblUser = New System.Windows.Forms.Label()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.chkAggregate = New System.Windows.Forms.CheckBox()
    Me.gbFunkcja = New System.Windows.Forms.GroupBox()
    Me.chkMin = New System.Windows.Forms.CheckBox()
    Me.chkMax = New System.Windows.Forms.CheckBox()
    Me.chkMediana = New System.Windows.Forms.CheckBox()
    Me.chkAvg = New System.Windows.Forms.CheckBox()
    Me.chkFunkcja = New System.Windows.Forms.CheckBox()
    CType(Me.dgvZestawienieOcen, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.Panel1.SuspendLayout()
    Me.gbFunkcja.SuspendLayout()
    Me.SuspendLayout()
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(53, 9)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(240, 21)
    Me.cbKlasa.TabIndex = 204
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(14, 12)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 203
    Me.Label2.Text = "Klasa"
    '
    'dgvZestawienieOcen
    '
    Me.dgvZestawienieOcen.AllowUserToAddRows = False
    Me.dgvZestawienieOcen.AllowUserToDeleteRows = False
    Me.dgvZestawienieOcen.AllowUserToResizeColumns = False
    Me.dgvZestawienieOcen.AllowUserToResizeRows = False
    Me.dgvZestawienieOcen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvZestawienieOcen.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
    DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    Me.dgvZestawienieOcen.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
    Me.dgvZestawienieOcen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvZestawienieOcen.Enabled = False
    Me.dgvZestawienieOcen.EnableHeadersVisualStyles = False
    Me.dgvZestawienieOcen.Location = New System.Drawing.Point(16, 36)
    Me.dgvZestawienieOcen.Name = "dgvZestawienieOcen"
    Me.dgvZestawienieOcen.RowHeadersVisible = False
    Me.dgvZestawienieOcen.RowTemplate.Height = 20
    Me.dgvZestawienieOcen.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvZestawienieOcen.Size = New System.Drawing.Size(987, 447)
    Me.dgvZestawienieOcen.TabIndex = 201
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(761, 489)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(117, 35)
    Me.cmdPrint.TabIndex = 219
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(884, 489)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 202
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'rbSemestr
    '
    Me.rbSemestr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Enabled = False
    Me.rbSemestr.Location = New System.Drawing.Point(712, 10)
    Me.rbSemestr.Name = "rbSemestr"
    Me.rbSemestr.Size = New System.Drawing.Size(69, 17)
    Me.rbSemestr.TabIndex = 0
    Me.rbSemestr.TabStop = True
    Me.rbSemestr.Text = "Semestr I"
    Me.rbSemestr.UseVisualStyleBackColor = True
    '
    'rbSchoolYear
    '
    Me.rbSchoolYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rbSchoolYear.AutoSize = True
    Me.rbSchoolYear.Enabled = False
    Me.rbSchoolYear.Location = New System.Drawing.Point(787, 10)
    Me.rbSchoolYear.Name = "rbSchoolYear"
    Me.rbSchoolYear.Size = New System.Drawing.Size(83, 17)
    Me.rbSchoolYear.TabIndex = 1
    Me.rbSchoolYear.TabStop = True
    Me.rbSchoolYear.Text = "Rok szkolny"
    Me.rbSchoolYear.UseVisualStyleBackColor = True
    '
    'rbAll
    '
    Me.rbAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.rbAll.AutoSize = True
    Me.rbAll.Enabled = False
    Me.rbAll.Location = New System.Drawing.Point(876, 10)
    Me.rbAll.Name = "rbAll"
    Me.rbAll.Size = New System.Drawing.Size(127, 17)
    Me.rbAll.TabIndex = 2
    Me.rbAll.TabStop = True
    Me.rbAll.Text = "Cały cykl kształcenia"
    Me.rbAll.UseVisualStyleBackColor = True
    '
    'Panel1
    '
    Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.Label16)
    Me.Panel1.Controls.Add(Me.Label17)
    Me.Panel1.Controls.Add(Me.lblData)
    Me.Panel1.Controls.Add(Me.lblIP)
    Me.Panel1.Controls.Add(Me.lblUser)
    Me.Panel1.Controls.Add(Me.Label18)
    Me.Panel1.Location = New System.Drawing.Point(2, 525)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(1006, 35)
    Me.Panel1.TabIndex = 220
    '
    'Label16
    '
    Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label16.AutoSize = True
    Me.Label16.Enabled = False
    Me.Label16.Location = New System.Drawing.Point(789, 13)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(85, 13)
    Me.Label16.TabIndex = 221
    Me.Label16.Text = "Data modyfikacji"
    '
    'Label17
    '
    Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label17.AutoSize = True
    Me.Label17.Enabled = False
    Me.Label17.Location = New System.Drawing.Point(646, 13)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(31, 13)
    Me.Label17.TabIndex = 220
    Me.Label17.Text = "Nr IP"
    '
    'lblData
    '
    Me.lblData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblData.Enabled = False
    Me.lblData.Location = New System.Drawing.Point(880, 8)
    Me.lblData.Name = "lblData"
    Me.lblData.Size = New System.Drawing.Size(120, 23)
    Me.lblData.TabIndex = 219
    Me.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIP
    '
    Me.lblIP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblIP.Enabled = False
    Me.lblIP.Location = New System.Drawing.Point(683, 8)
    Me.lblIP.Name = "lblIP"
    Me.lblIP.Size = New System.Drawing.Size(100, 23)
    Me.lblIP.TabIndex = 217
    Me.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUser
    '
    Me.lblUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lblUser.Enabled = False
    Me.lblUser.Location = New System.Drawing.Point(86, 8)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(554, 23)
    Me.lblUser.TabIndex = 218
    Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label18
    '
    Me.Label18.AutoSize = True
    Me.Label18.Enabled = False
    Me.Label18.Location = New System.Drawing.Point(6, 13)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(74, 13)
    Me.Label18.TabIndex = 216
    Me.Label18.Text = "Zmodyfikował"
    '
    'chkAggregate
    '
    Me.chkAggregate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkAggregate.AutoSize = True
    Me.chkAggregate.Checked = True
    Me.chkAggregate.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkAggregate.Location = New System.Drawing.Point(16, 499)
    Me.chkAggregate.Name = "chkAggregate"
    Me.chkAggregate.Size = New System.Drawing.Size(130, 17)
    Me.chkAggregate.TabIndex = 221
    Me.chkAggregate.Text = "Pokaż podsumowanie"
    Me.chkAggregate.UseVisualStyleBackColor = True
    '
    'gbFunkcja
    '
    Me.gbFunkcja.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.gbFunkcja.Controls.Add(Me.chkMin)
    Me.gbFunkcja.Controls.Add(Me.chkMax)
    Me.gbFunkcja.Controls.Add(Me.chkMediana)
    Me.gbFunkcja.Controls.Add(Me.chkAvg)
    Me.gbFunkcja.Location = New System.Drawing.Point(363, 484)
    Me.gbFunkcja.Name = "gbFunkcja"
    Me.gbFunkcja.Size = New System.Drawing.Size(305, 35)
    Me.gbFunkcja.TabIndex = 222
    Me.gbFunkcja.TabStop = False
    '
    'chkMin
    '
    Me.chkMin.AutoSize = True
    Me.chkMin.Checked = True
    Me.chkMin.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkMin.Location = New System.Drawing.Point(229, 15)
    Me.chkMin.Name = "chkMin"
    Me.chkMin.Size = New System.Drawing.Size(67, 17)
    Me.chkMin.TabIndex = 3
    Me.chkMin.Tag = "3"
    Me.chkMin.Text = "Minimum"
    Me.chkMin.UseVisualStyleBackColor = True
    '
    'chkMax
    '
    Me.chkMax.AutoSize = True
    Me.chkMax.Checked = True
    Me.chkMax.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkMax.Location = New System.Drawing.Point(147, 15)
    Me.chkMax.Name = "chkMax"
    Me.chkMax.Size = New System.Drawing.Size(76, 17)
    Me.chkMax.TabIndex = 2
    Me.chkMax.Tag = "2"
    Me.chkMax.Text = "Maksimum"
    Me.chkMax.UseVisualStyleBackColor = True
    '
    'chkMediana
    '
    Me.chkMediana.AutoSize = True
    Me.chkMediana.Checked = True
    Me.chkMediana.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkMediana.Location = New System.Drawing.Point(74, 15)
    Me.chkMediana.Name = "chkMediana"
    Me.chkMediana.Size = New System.Drawing.Size(67, 17)
    Me.chkMediana.TabIndex = 1
    Me.chkMediana.Tag = "1"
    Me.chkMediana.Text = "Mediana"
    Me.chkMediana.UseVisualStyleBackColor = True
    '
    'chkAvg
    '
    Me.chkAvg.AutoSize = True
    Me.chkAvg.Checked = True
    Me.chkAvg.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkAvg.Location = New System.Drawing.Point(6, 15)
    Me.chkAvg.Name = "chkAvg"
    Me.chkAvg.Size = New System.Drawing.Size(62, 17)
    Me.chkAvg.TabIndex = 0
    Me.chkAvg.Tag = "0"
    Me.chkAvg.Text = "Średnia"
    Me.chkAvg.UseVisualStyleBackColor = True
    '
    'chkFunkcja
    '
    Me.chkFunkcja.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.chkFunkcja.AutoSize = True
    Me.chkFunkcja.Checked = True
    Me.chkFunkcja.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkFunkcja.Location = New System.Drawing.Point(202, 499)
    Me.chkFunkcja.Name = "chkFunkcja"
    Me.chkFunkcja.Size = New System.Drawing.Size(155, 17)
    Me.chkFunkcja.TabIndex = 223
    Me.chkFunkcja.Text = "Pokaż funkcje statystyczne"
    Me.chkFunkcja.UseVisualStyleBackColor = True
    '
    'frmZestawienieOcen
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1015, 566)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.chkFunkcja)
    Me.Controls.Add(Me.gbFunkcja)
    Me.Controls.Add(Me.chkAggregate)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.rbSemestr)
    Me.Controls.Add(Me.rbSchoolYear)
    Me.Controls.Add(Me.rbAll)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.dgvZestawienieOcen)
    Me.MinimumSize = New System.Drawing.Size(700, 400)
    Me.Name = "frmZestawienieOcen"
    Me.Text = "Tabelaryczne zestawienie wyników nauczania"
    CType(Me.dgvZestawienieOcen, System.ComponentModel.ISupportInitialize).EndInit()
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.gbFunkcja.ResumeLayout(False)
    Me.gbFunkcja.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents dgvZestawienieOcen As System.Windows.Forms.DataGridView
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents rbSchoolYear As System.Windows.Forms.RadioButton
  Friend WithEvents rbAll As System.Windows.Forms.RadioButton
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label16 As System.Windows.Forms.Label
  Friend WithEvents Label17 As System.Windows.Forms.Label
  Friend WithEvents lblData As System.Windows.Forms.Label
  Friend WithEvents lblIP As System.Windows.Forms.Label
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents Label18 As System.Windows.Forms.Label
  Friend WithEvents chkAggregate As System.Windows.Forms.CheckBox
  Friend WithEvents gbFunkcja As System.Windows.Forms.GroupBox
  Friend WithEvents chkMin As System.Windows.Forms.CheckBox
  Friend WithEvents chkMax As System.Windows.Forms.CheckBox
  Friend WithEvents chkMediana As System.Windows.Forms.CheckBox
  Friend WithEvents chkAvg As System.Windows.Forms.CheckBox
  Friend WithEvents chkFunkcja As System.Windows.Forms.CheckBox
End Class
