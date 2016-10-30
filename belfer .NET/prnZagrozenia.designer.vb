<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prnZagrozenia
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.pvZagrozenia = New System.Windows.Forms.PrintPreviewControl()
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.gbPrintRange = New System.Windows.Forms.GroupBox()
    Me.cbUczen = New System.Windows.Forms.ComboBox()
    Me.rbAllStudents = New System.Windows.Forms.RadioButton()
    Me.rbSelectedStudent = New System.Windows.Forms.RadioButton()
    Me.gbZoom = New System.Windows.Forms.GroupBox()
    Me.rbZoom200 = New System.Windows.Forms.RadioButton()
    Me.rbZoom150 = New System.Windows.Forms.RadioButton()
    Me.rbZoom100 = New System.Windows.Forms.RadioButton()
    Me.rbZoom75 = New System.Windows.Forms.RadioButton()
    Me.rbZoom50 = New System.Windows.Forms.RadioButton()
    Me.rbZoom25 = New System.Windows.Forms.RadioButton()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.dtData = New System.Windows.Forms.DateTimePicker()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.gbAdresat = New System.Windows.Forms.GroupBox()
    Me.chkOjciec = New System.Windows.Forms.CheckBox()
    Me.chkMatka = New System.Windows.Forms.CheckBox()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gbPrintRange.SuspendLayout()
    Me.gbZoom.SuspendLayout()
    Me.gbAdresat.SuspendLayout()
    Me.SuspendLayout()
    '
    'pvZagrozenia
    '
    Me.pvZagrozenia.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pvZagrozenia.Location = New System.Drawing.Point(0, 0)
    Me.pvZagrozenia.Name = "pvZagrozenia"
    Me.pvZagrozenia.Size = New System.Drawing.Size(613, 638)
    Me.pvZagrozenia.TabIndex = 3
    '
    'nudSemestr
    '
    Me.nudSemestr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nudSemestr.Location = New System.Drawing.Point(828, 106)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(32, 20)
    Me.nudSemestr.TabIndex = 47
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(777, 108)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(45, 13)
    Me.Label2.TabIndex = 46
    Me.Label2.Text = "Semestr"
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(619, 80)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(33, 13)
    Me.Label1.TabIndex = 42
    Me.Label1.Text = "Klasa"
    '
    'gbPrintRange
    '
    Me.gbPrintRange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbPrintRange.Controls.Add(Me.cbUczen)
    Me.gbPrintRange.Controls.Add(Me.rbAllStudents)
    Me.gbPrintRange.Controls.Add(Me.rbSelectedStudent)
    Me.gbPrintRange.Enabled = False
    Me.gbPrintRange.Location = New System.Drawing.Point(624, 165)
    Me.gbPrintRange.Name = "gbPrintRange"
    Me.gbPrintRange.Size = New System.Drawing.Size(236, 93)
    Me.gbPrintRange.TabIndex = 43
    Me.gbPrintRange.TabStop = False
    Me.gbPrintRange.Text = "Zakres wydruku"
    '
    'cbUczen
    '
    Me.cbUczen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbUczen.Enabled = False
    Me.cbUczen.FormattingEnabled = True
    Me.cbUczen.Location = New System.Drawing.Point(23, 65)
    Me.cbUczen.Name = "cbUczen"
    Me.cbUczen.Size = New System.Drawing.Size(207, 21)
    Me.cbUczen.TabIndex = 8
    '
    'rbAllStudents
    '
    Me.rbAllStudents.AutoSize = True
    Me.rbAllStudents.Checked = True
    Me.rbAllStudents.Location = New System.Drawing.Point(6, 19)
    Me.rbAllStudents.Name = "rbAllStudents"
    Me.rbAllStudents.Size = New System.Drawing.Size(117, 17)
    Me.rbAllStudents.TabIndex = 4
    Me.rbAllStudents.TabStop = True
    Me.rbAllStudents.Text = "Wszyscy uczniowie"
    Me.rbAllStudents.UseVisualStyleBackColor = True
    '
    'rbSelectedStudent
    '
    Me.rbSelectedStudent.AutoSize = True
    Me.rbSelectedStudent.Location = New System.Drawing.Point(6, 42)
    Me.rbSelectedStudent.Name = "rbSelectedStudent"
    Me.rbSelectedStudent.Size = New System.Drawing.Size(99, 17)
    Me.rbSelectedStudent.TabIndex = 5
    Me.rbSelectedStudent.Text = "Wybrany uczeñ"
    Me.rbSelectedStudent.UseVisualStyleBackColor = True
    '
    'gbZoom
    '
    Me.gbZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbZoom.Controls.Add(Me.rbZoom200)
    Me.gbZoom.Controls.Add(Me.rbZoom150)
    Me.gbZoom.Controls.Add(Me.rbZoom100)
    Me.gbZoom.Controls.Add(Me.rbZoom75)
    Me.gbZoom.Controls.Add(Me.rbZoom50)
    Me.gbZoom.Controls.Add(Me.rbZoom25)
    Me.gbZoom.Enabled = False
    Me.gbZoom.Location = New System.Drawing.Point(624, 351)
    Me.gbZoom.Name = "gbZoom"
    Me.gbZoom.Size = New System.Drawing.Size(236, 89)
    Me.gbZoom.TabIndex = 44
    Me.gbZoom.TabStop = False
    Me.gbZoom.Text = "Powiêkszenie"
    '
    'rbZoom200
    '
    Me.rbZoom200.AutoSize = True
    Me.rbZoom200.Location = New System.Drawing.Point(179, 65)
    Me.rbZoom200.Name = "rbZoom200"
    Me.rbZoom200.Size = New System.Drawing.Size(51, 17)
    Me.rbZoom200.TabIndex = 5
    Me.rbZoom200.Tag = "2"
    Me.rbZoom200.Text = "200%"
    Me.rbZoom200.UseVisualStyleBackColor = True
    '
    'rbZoom150
    '
    Me.rbZoom150.AutoSize = True
    Me.rbZoom150.Location = New System.Drawing.Point(179, 42)
    Me.rbZoom150.Name = "rbZoom150"
    Me.rbZoom150.Size = New System.Drawing.Size(51, 17)
    Me.rbZoom150.TabIndex = 4
    Me.rbZoom150.Tag = "1,5"
    Me.rbZoom150.Text = "150%"
    Me.rbZoom150.UseVisualStyleBackColor = True
    '
    'rbZoom100
    '
    Me.rbZoom100.AutoSize = True
    Me.rbZoom100.Location = New System.Drawing.Point(179, 19)
    Me.rbZoom100.Name = "rbZoom100"
    Me.rbZoom100.Size = New System.Drawing.Size(51, 17)
    Me.rbZoom100.TabIndex = 3
    Me.rbZoom100.Tag = "1"
    Me.rbZoom100.Text = "100%"
    Me.rbZoom100.UseVisualStyleBackColor = True
    '
    'rbZoom75
    '
    Me.rbZoom75.AutoSize = True
    Me.rbZoom75.Location = New System.Drawing.Point(6, 65)
    Me.rbZoom75.Name = "rbZoom75"
    Me.rbZoom75.Size = New System.Drawing.Size(45, 17)
    Me.rbZoom75.TabIndex = 2
    Me.rbZoom75.Tag = "0,75"
    Me.rbZoom75.Text = "75%"
    Me.rbZoom75.UseVisualStyleBackColor = True
    '
    'rbZoom50
    '
    Me.rbZoom50.AutoSize = True
    Me.rbZoom50.Checked = True
    Me.rbZoom50.Location = New System.Drawing.Point(6, 42)
    Me.rbZoom50.Name = "rbZoom50"
    Me.rbZoom50.Size = New System.Drawing.Size(45, 17)
    Me.rbZoom50.TabIndex = 1
    Me.rbZoom50.TabStop = True
    Me.rbZoom50.Tag = "0,5"
    Me.rbZoom50.Text = "50%"
    Me.rbZoom50.UseVisualStyleBackColor = True
    '
    'rbZoom25
    '
    Me.rbZoom25.AutoSize = True
    Me.rbZoom25.Location = New System.Drawing.Point(6, 19)
    Me.rbZoom25.Name = "rbZoom25"
    Me.rbZoom25.Size = New System.Drawing.Size(45, 17)
    Me.rbZoom25.TabIndex = 0
    Me.rbZoom25.Tag = "0,25"
    Me.rbZoom25.Text = "25%"
    Me.rbZoom25.UseVisualStyleBackColor = True
    '
    'cbKlasa
    '
    Me.cbKlasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(658, 77)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(202, 21)
    Me.cbKlasa.TabIndex = 41
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(621, 9)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(163, 13)
    Me.Label3.TabIndex = 56
    Me.Label3.Text = "Data wystawienia powiadomienia"
    '
    'dtData
    '
    Me.dtData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtData.Location = New System.Drawing.Point(624, 25)
    Me.dtData.Name = "dtData"
    Me.dtData.Size = New System.Drawing.Size(238, 20)
    Me.dtData.TabIndex = 57
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(624, 591)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(85, 35)
    Me.cmdPrint.TabIndex = 123
    Me.cmdPrint.Text = "&Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(775, 591)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(85, 35)
    Me.cmdClose.TabIndex = 124
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'gbAdresat
    '
    Me.gbAdresat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbAdresat.Controls.Add(Me.chkOjciec)
    Me.gbAdresat.Controls.Add(Me.chkMatka)
    Me.gbAdresat.Location = New System.Drawing.Point(624, 276)
    Me.gbAdresat.Name = "gbAdresat"
    Me.gbAdresat.Size = New System.Drawing.Size(236, 49)
    Me.gbAdresat.TabIndex = 125
    Me.gbAdresat.TabStop = False
    Me.gbAdresat.Text = "Adresat"
    '
    'chkOjciec
    '
    Me.chkOjciec.AutoSize = True
    Me.chkOjciec.Location = New System.Drawing.Point(68, 19)
    Me.chkOjciec.Name = "chkOjciec"
    Me.chkOjciec.Size = New System.Drawing.Size(56, 17)
    Me.chkOjciec.TabIndex = 1
    Me.chkOjciec.Text = "Ojciec"
    Me.chkOjciec.UseVisualStyleBackColor = True
    '
    'chkMatka
    '
    Me.chkMatka.AutoSize = True
    Me.chkMatka.Checked = True
    Me.chkMatka.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkMatka.Location = New System.Drawing.Point(6, 19)
    Me.chkMatka.Name = "chkMatka"
    Me.chkMatka.Size = New System.Drawing.Size(56, 17)
    Me.chkMatka.TabIndex = 0
    Me.chkMatka.Text = "Matka"
    Me.chkMatka.UseVisualStyleBackColor = True
    '
    'chkVirtual
    '
    Me.chkVirtual.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(658, 107)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkVirtual.TabIndex = 199
    Me.chkVirtual.Text = "Klasa wirtualna"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'prnZagrozenia
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(867, 638)
    Me.Controls.Add(Me.chkVirtual)
    Me.Controls.Add(Me.gbAdresat)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.dtData)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.gbPrintRange)
    Me.Controls.Add(Me.gbZoom)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.pvZagrozenia)
    Me.Name = "prnZagrozenia"
    Me.Text = "Wydruk powiadomieñ o zagro¿eniu ocenami niedostatecznymi"
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gbPrintRange.ResumeLayout(False)
    Me.gbPrintRange.PerformLayout()
    Me.gbZoom.ResumeLayout(False)
    Me.gbZoom.PerformLayout()
    Me.gbAdresat.ResumeLayout(False)
    Me.gbAdresat.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents pvZagrozenia As System.Windows.Forms.PrintPreviewControl
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents gbPrintRange As System.Windows.Forms.GroupBox
  Friend WithEvents cbUczen As System.Windows.Forms.ComboBox
  Friend WithEvents rbAllStudents As System.Windows.Forms.RadioButton
  Friend WithEvents rbSelectedStudent As System.Windows.Forms.RadioButton
  Friend WithEvents gbZoom As System.Windows.Forms.GroupBox
  Friend WithEvents rbZoom200 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom150 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom100 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom75 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom50 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom25 As System.Windows.Forms.RadioButton
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents dtData As System.Windows.Forms.DateTimePicker
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents gbAdresat As System.Windows.Forms.GroupBox
  Friend WithEvents chkOjciec As System.Windows.Forms.CheckBox
  Friend WithEvents chkMatka As System.Windows.Forms.CheckBox
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
End Class
