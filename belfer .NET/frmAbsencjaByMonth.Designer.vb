<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbsencjaByMonth
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
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dgvAbsencja = New System.Windows.Forms.DataGridView()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    Me.rbSemestr1 = New System.Windows.Forms.RadioButton()
    Me.rbSemestr2 = New System.Windows.Forms.RadioButton()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    CType(Me.dgvAbsencja, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(51, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(261, 21)
    Me.cbKlasa.TabIndex = 26
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 25
    Me.Label2.Text = "Klasa"
    '
    'dgvAbsencja
    '
    Me.dgvAbsencja.AllowUserToAddRows = False
    Me.dgvAbsencja.AllowUserToDeleteRows = False
    Me.dgvAbsencja.AllowUserToResizeColumns = False
    Me.dgvAbsencja.AllowUserToResizeRows = False
    Me.dgvAbsencja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dgvAbsencja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvAbsencja.Enabled = False
    Me.dgvAbsencja.Location = New System.Drawing.Point(12, 39)
    Me.dgvAbsencja.Name = "dgvAbsencja"
    Me.dgvAbsencja.RowHeadersVisible = False
    Me.dgvAbsencja.RowTemplate.Height = 20
    Me.dgvAbsencja.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
    Me.dgvAbsencja.Size = New System.Drawing.Size(980, 480)
    Me.dgvAbsencja.TabIndex = 27
    '
    'chkVirtual
    '
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(318, 14)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkVirtual.TabIndex = 214
    Me.chkVirtual.Text = "Klasa wirtualna"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'rbSemestr1
    '
    Me.rbSemestr1.AutoSize = True
    Me.rbSemestr1.Location = New System.Drawing.Point(498, 13)
    Me.rbSemestr1.Name = "rbSemestr1"
    Me.rbSemestr1.Size = New System.Drawing.Size(72, 17)
    Me.rbSemestr1.TabIndex = 218
    Me.rbSemestr1.TabStop = True
    Me.rbSemestr1.Text = "Semestr 1"
    Me.rbSemestr1.UseVisualStyleBackColor = True
    '
    'rbSemestr2
    '
    Me.rbSemestr2.AutoSize = True
    Me.rbSemestr2.Location = New System.Drawing.Point(576, 13)
    Me.rbSemestr2.Name = "rbSemestr2"
    Me.rbSemestr2.Size = New System.Drawing.Size(72, 17)
    Me.rbSemestr2.TabIndex = 219
    Me.rbSemestr2.TabStop = True
    Me.rbSemestr2.Text = "Semestr 2"
    Me.rbSemestr2.UseVisualStyleBackColor = True
    '
    'rbRokSzkolny
    '
    Me.rbRokSzkolny.AutoSize = True
    Me.rbRokSzkolny.Location = New System.Drawing.Point(654, 13)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(83, 17)
    Me.rbRokSzkolny.TabIndex = 220
    Me.rbRokSzkolny.TabStop = True
    Me.rbRokSzkolny.Text = "Rok szkolny"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(800, 523)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(93, 35)
    Me.cmdPrint.TabIndex = 217
    Me.cmdPrint.Text = "&Drukuj ..."
    Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(899, 523)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(93, 35)
    Me.cmdClose.TabIndex = 208
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'frmAbsencjaByMonth
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1004, 567)
    Me.Controls.Add(Me.rbRokSzkolny)
    Me.Controls.Add(Me.rbSemestr2)
    Me.Controls.Add(Me.rbSemestr1)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.chkVirtual)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.dgvAbsencja)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.Label2)
    Me.Name = "frmAbsencjaByMonth"
    Me.Text = "Absencja na zajęciach lekcyjnych"
    CType(Me.dgvAbsencja, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents dgvAbsencja As System.Windows.Forms.DataGridView
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents rbSemestr1 As System.Windows.Forms.RadioButton
  Friend WithEvents rbSemestr2 As System.Windows.Forms.RadioButton
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
End Class
