<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prnAbsencja
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
    Me.pvAbsencja = New System.Windows.Forms.PrintPreviewControl()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.gbPrintRange = New System.Windows.Forms.GroupBox()
    Me.cbUczen = New System.Windows.Forms.ComboBox()
    Me.rbAllStudents = New System.Windows.Forms.RadioButton()
    Me.rbSelectedStudent = New System.Windows.Forms.RadioButton()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.gbZoom = New System.Windows.Forms.GroupBox()
    Me.nudZoom = New System.Windows.Forms.NumericUpDown()
    Me.rbZoomCustom = New System.Windows.Forms.RadioButton()
    Me.tbZoom = New System.Windows.Forms.TrackBar()
    Me.rbZoom200 = New System.Windows.Forms.RadioButton()
    Me.rbZoom100 = New System.Windows.Forms.RadioButton()
    Me.rbZoom50 = New System.Windows.Forms.RadioButton()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
    Me.cmdPageSetup = New System.Windows.Forms.Button()
    Me.gbPrintRange.SuspendLayout()
    Me.gbZoom.SuspendLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pvAbsencja
    '
    Me.pvAbsencja.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pvAbsencja.Location = New System.Drawing.Point(0, 0)
    Me.pvAbsencja.Name = "pvAbsencja"
    Me.pvAbsencja.Size = New System.Drawing.Size(655, 638)
    Me.pvAbsencja.TabIndex = 2
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(661, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(33, 13)
    Me.Label1.TabIndex = 27
    Me.Label1.Text = "Klasa"
    '
    'gbPrintRange
    '
    Me.gbPrintRange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbPrintRange.Controls.Add(Me.cbUczen)
    Me.gbPrintRange.Controls.Add(Me.rbAllStudents)
    Me.gbPrintRange.Controls.Add(Me.rbSelectedStudent)
    Me.gbPrintRange.Enabled = False
    Me.gbPrintRange.Location = New System.Drawing.Point(664, 146)
    Me.gbPrintRange.Name = "gbPrintRange"
    Me.gbPrintRange.Size = New System.Drawing.Size(236, 93)
    Me.gbPrintRange.TabIndex = 28
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
    'cbKlasa
    '
    Me.cbKlasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(700, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(200, 21)
    Me.cbKlasa.TabIndex = 26
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(815, 591)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(85, 35)
    Me.cmdClose.TabIndex = 126
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(664, 591)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(85, 35)
    Me.cmdPrint.TabIndex = 125
    Me.cmdPrint.Text = "&Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'dtStartDate
    '
    Me.dtStartDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtStartDate.Enabled = False
    Me.dtStartDate.Location = New System.Drawing.Point(687, 61)
    Me.dtStartDate.Name = "dtStartDate"
    Me.dtStartDate.Size = New System.Drawing.Size(213, 20)
    Me.dtStartDate.TabIndex = 9
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(661, 67)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(21, 13)
    Me.Label3.TabIndex = 127
    Me.Label3.Text = "Od"
    '
    'gbZoom
    '
    Me.gbZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbZoom.Controls.Add(Me.nudZoom)
    Me.gbZoom.Controls.Add(Me.rbZoomCustom)
    Me.gbZoom.Controls.Add(Me.tbZoom)
    Me.gbZoom.Controls.Add(Me.rbZoom200)
    Me.gbZoom.Controls.Add(Me.rbZoom100)
    Me.gbZoom.Controls.Add(Me.rbZoom50)
    Me.gbZoom.Location = New System.Drawing.Point(664, 276)
    Me.gbZoom.Name = "gbZoom"
    Me.gbZoom.Size = New System.Drawing.Size(236, 89)
    Me.gbZoom.TabIndex = 21
    Me.gbZoom.TabStop = False
    Me.gbZoom.Text = "Powiêkszenie"
    '
    'nudZoom
    '
    Me.nudZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nudZoom.Enabled = False
    Me.nudZoom.Location = New System.Drawing.Point(156, 19)
    Me.nudZoom.Maximum = New Decimal(New Integer() {400, 0, 0, 0})
    Me.nudZoom.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nudZoom.Name = "nudZoom"
    Me.nudZoom.Size = New System.Drawing.Size(62, 20)
    Me.nudZoom.TabIndex = 158
    Me.nudZoom.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'rbZoomCustom
    '
    Me.rbZoomCustom.AutoSize = True
    Me.rbZoomCustom.Location = New System.Drawing.Point(68, 19)
    Me.rbZoomCustom.Name = "rbZoomCustom"
    Me.rbZoomCustom.Size = New System.Drawing.Size(86, 17)
    Me.rbZoomCustom.TabIndex = 7
    Me.rbZoomCustom.Tag = "0,05"
    Me.rbZoomCustom.Text = "U¿ytkownika"
    Me.rbZoomCustom.UseVisualStyleBackColor = True
    '
    'tbZoom
    '
    Me.tbZoom.Enabled = False
    Me.tbZoom.LargeChange = 10
    Me.tbZoom.Location = New System.Drawing.Point(63, 42)
    Me.tbZoom.Maximum = 400
    Me.tbZoom.Minimum = 50
    Me.tbZoom.Name = "tbZoom"
    Me.tbZoom.Size = New System.Drawing.Size(167, 45)
    Me.tbZoom.TabIndex = 6
    Me.tbZoom.TickFrequency = 10
    Me.tbZoom.Value = 100
    '
    'rbZoom200
    '
    Me.rbZoom200.AutoSize = True
    Me.rbZoom200.Location = New System.Drawing.Point(6, 65)
    Me.rbZoom200.Name = "rbZoom200"
    Me.rbZoom200.Size = New System.Drawing.Size(51, 17)
    Me.rbZoom200.TabIndex = 5
    Me.rbZoom200.Tag = "2"
    Me.rbZoom200.Text = "200%"
    Me.rbZoom200.UseVisualStyleBackColor = True
    '
    'rbZoom100
    '
    Me.rbZoom100.AutoSize = True
    Me.rbZoom100.Checked = True
    Me.rbZoom100.Location = New System.Drawing.Point(6, 42)
    Me.rbZoom100.Name = "rbZoom100"
    Me.rbZoom100.Size = New System.Drawing.Size(51, 17)
    Me.rbZoom100.TabIndex = 3
    Me.rbZoom100.TabStop = True
    Me.rbZoom100.Tag = "1"
    Me.rbZoom100.Text = "100%"
    Me.rbZoom100.UseVisualStyleBackColor = True
    '
    'rbZoom50
    '
    Me.rbZoom50.AutoSize = True
    Me.rbZoom50.Location = New System.Drawing.Point(6, 19)
    Me.rbZoom50.Name = "rbZoom50"
    Me.rbZoom50.Size = New System.Drawing.Size(45, 17)
    Me.rbZoom50.TabIndex = 1
    Me.rbZoom50.Tag = "0,5"
    Me.rbZoom50.Text = "50%"
    Me.rbZoom50.UseVisualStyleBackColor = True
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(661, 93)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(21, 13)
    Me.Label2.TabIndex = 129
    Me.Label2.Text = "Do"
    '
    'dtEndDate
    '
    Me.dtEndDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtEndDate.Enabled = False
    Me.dtEndDate.Location = New System.Drawing.Point(687, 87)
    Me.dtEndDate.Name = "dtEndDate"
    Me.dtEndDate.Size = New System.Drawing.Size(213, 20)
    Me.dtEndDate.TabIndex = 128
    '
    'cmdPageSetup
    '
    Me.cmdPageSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPageSetup.Location = New System.Drawing.Point(664, 550)
    Me.cmdPageSetup.Name = "cmdPageSetup"
    Me.cmdPageSetup.Size = New System.Drawing.Size(85, 35)
    Me.cmdPageSetup.TabIndex = 166
    Me.cmdPageSetup.Text = "Ustawienia strony"
    Me.cmdPageSetup.UseVisualStyleBackColor = True
    '
    'prnAbsencja
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(909, 638)
    Me.Controls.Add(Me.cmdPageSetup)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.dtEndDate)
    Me.Controls.Add(Me.gbZoom)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.dtStartDate)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.gbPrintRange)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.pvAbsencja)
    Me.Name = "prnAbsencja"
    Me.Text = "Wydruk nieobecnoœci uczniów"
    Me.gbPrintRange.ResumeLayout(False)
    Me.gbPrintRange.PerformLayout()
    Me.gbZoom.ResumeLayout(False)
    Me.gbZoom.PerformLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents pvAbsencja As System.Windows.Forms.PrintPreviewControl
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents gbPrintRange As System.Windows.Forms.GroupBox
  Friend WithEvents cbUczen As System.Windows.Forms.ComboBox
  Friend WithEvents rbAllStudents As System.Windows.Forms.RadioButton
  Friend WithEvents rbSelectedStudent As System.Windows.Forms.RadioButton
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents gbZoom As System.Windows.Forms.GroupBox
  Friend WithEvents nudZoom As System.Windows.Forms.NumericUpDown
  Friend WithEvents rbZoomCustom As System.Windows.Forms.RadioButton
  Friend WithEvents tbZoom As System.Windows.Forms.TrackBar
  Friend WithEvents rbZoom200 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom100 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom50 As System.Windows.Forms.RadioButton
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents cmdPageSetup As System.Windows.Forms.Button
End Class
