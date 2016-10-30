<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prnWyniki
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
    Me.pvOceny = New System.Windows.Forms.PrintPreviewControl()
    Me.nudSemestr = New System.Windows.Forms.NumericUpDown()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.gbScoreType = New System.Windows.Forms.GroupBox()
    Me.chkTableSet = New System.Windows.Forms.CheckBox()
    Me.rbEndingScores = New System.Windows.Forms.RadioButton()
    Me.rbPartialScores = New System.Windows.Forms.RadioButton()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.gbPrintRange = New System.Windows.Forms.GroupBox()
    Me.cbUczen = New System.Windows.Forms.ComboBox()
    Me.rbAllStudents = New System.Windows.Forms.RadioButton()
    Me.rbSelectedStudent = New System.Windows.Forms.RadioButton()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.dtData = New System.Windows.Forms.DateTimePicker()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cmdPageSetup = New System.Windows.Forms.Button()
    Me.cmdFontSetup = New System.Windows.Forms.Button()
    Me.gbOpcje = New System.Windows.Forms.GroupBox()
    Me.chkColor = New System.Windows.Forms.CheckBox()
    Me.chkNote = New System.Windows.Forms.CheckBox()
    Me.chkAbsence = New System.Windows.Forms.CheckBox()
    Me.gbZoom = New System.Windows.Forms.GroupBox()
    Me.nudZoom = New System.Windows.Forms.NumericUpDown()
    Me.rbZoomCustom = New System.Windows.Forms.RadioButton()
    Me.tbZoom = New System.Windows.Forms.TrackBar()
    Me.rbZoom200 = New System.Windows.Forms.RadioButton()
    Me.rbZoom100 = New System.Windows.Forms.RadioButton()
    Me.rbZoom50 = New System.Windows.Forms.RadioButton()
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gbScoreType.SuspendLayout()
    Me.gbPrintRange.SuspendLayout()
    Me.gbOpcje.SuspendLayout()
    Me.gbZoom.SuspendLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pvOceny
    '
    Me.pvOceny.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pvOceny.Location = New System.Drawing.Point(0, 0)
    Me.pvOceny.Name = "pvOceny"
    Me.pvOceny.Size = New System.Drawing.Size(700, 638)
    Me.pvOceny.TabIndex = 2
    '
    'nudSemestr
    '
    Me.nudSemestr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nudSemestr.Enabled = False
    Me.nudSemestr.Location = New System.Drawing.Point(913, 39)
    Me.nudSemestr.Maximum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudSemestr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudSemestr.Name = "nudSemestr"
    Me.nudSemestr.Size = New System.Drawing.Size(32, 20)
    Me.nudSemestr.TabIndex = 32
    Me.nudSemestr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(862, 41)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(45, 13)
    Me.Label2.TabIndex = 31
    Me.Label2.Text = "Semestr"
    '
    'gbScoreType
    '
    Me.gbScoreType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbScoreType.Controls.Add(Me.chkTableSet)
    Me.gbScoreType.Controls.Add(Me.rbEndingScores)
    Me.gbScoreType.Controls.Add(Me.rbPartialScores)
    Me.gbScoreType.Enabled = False
    Me.gbScoreType.Location = New System.Drawing.Point(709, 210)
    Me.gbScoreType.Name = "gbScoreType"
    Me.gbScoreType.Size = New System.Drawing.Size(236, 89)
    Me.gbScoreType.TabIndex = 30
    Me.gbScoreType.TabStop = False
    Me.gbScoreType.Text = "Typ ocen"
    '
    'chkTableSet
    '
    Me.chkTableSet.AutoSize = True
    Me.chkTableSet.Enabled = False
    Me.chkTableSet.Location = New System.Drawing.Point(23, 65)
    Me.chkTableSet.Name = "chkTableSet"
    Me.chkTableSet.Size = New System.Drawing.Size(146, 17)
    Me.chkTableSet.TabIndex = 2
    Me.chkTableSet.Text = "Zestawienie tabelaryczne"
    Me.chkTableSet.UseVisualStyleBackColor = True
    '
    'rbEndingScores
    '
    Me.rbEndingScores.AutoSize = True
    Me.rbEndingScores.Location = New System.Drawing.Point(6, 42)
    Me.rbEndingScores.Name = "rbEndingScores"
    Me.rbEndingScores.Size = New System.Drawing.Size(103, 17)
    Me.rbEndingScores.TabIndex = 1
    Me.rbEndingScores.Text = "Oceny koñcowe"
    Me.rbEndingScores.UseVisualStyleBackColor = True
    '
    'rbPartialScores
    '
    Me.rbPartialScores.AutoSize = True
    Me.rbPartialScores.Checked = True
    Me.rbPartialScores.Location = New System.Drawing.Point(6, 19)
    Me.rbPartialScores.Name = "rbPartialScores"
    Me.rbPartialScores.Size = New System.Drawing.Size(110, 17)
    Me.rbPartialScores.TabIndex = 0
    Me.rbPartialScores.TabStop = True
    Me.rbPartialScores.Text = "Oceny cz¹stkowe"
    Me.rbPartialScores.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(706, 15)
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
    Me.gbPrintRange.Location = New System.Drawing.Point(709, 109)
    Me.gbPrintRange.Name = "gbPrintRange"
    Me.gbPrintRange.Size = New System.Drawing.Size(236, 95)
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
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(745, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(200, 21)
    Me.cbKlasa.TabIndex = 26
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(860, 591)
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
    Me.cmdPrint.Location = New System.Drawing.Point(860, 550)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(85, 35)
    Me.cmdPrint.TabIndex = 125
    Me.cmdPrint.Text = "&Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'dtData
    '
    Me.dtData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.dtData.Enabled = False
    Me.dtData.Location = New System.Drawing.Point(709, 83)
    Me.dtData.Name = "dtData"
    Me.dtData.Size = New System.Drawing.Size(236, 20)
    Me.dtData.TabIndex = 9
    '
    'Label3
    '
    Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(706, 67)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(73, 13)
    Me.Label3.TabIndex = 127
    Me.Label3.Text = "Data wydruku"
    '
    'cmdPageSetup
    '
    Me.cmdPageSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPageSetup.Location = New System.Drawing.Point(709, 591)
    Me.cmdPageSetup.Name = "cmdPageSetup"
    Me.cmdPageSetup.Size = New System.Drawing.Size(116, 35)
    Me.cmdPageSetup.TabIndex = 167
    Me.cmdPageSetup.Text = "Ustawienia strony"
    Me.cmdPageSetup.UseVisualStyleBackColor = True
    '
    'cmdFontSetup
    '
    Me.cmdFontSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdFontSetup.Location = New System.Drawing.Point(709, 550)
    Me.cmdFontSetup.Name = "cmdFontSetup"
    Me.cmdFontSetup.Size = New System.Drawing.Size(116, 35)
    Me.cmdFontSetup.TabIndex = 168
    Me.cmdFontSetup.Text = "Ustawienia czcionki"
    Me.cmdFontSetup.UseVisualStyleBackColor = True
    '
    'gbOpcje
    '
    Me.gbOpcje.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbOpcje.Controls.Add(Me.chkColor)
    Me.gbOpcje.Controls.Add(Me.chkNote)
    Me.gbOpcje.Controls.Add(Me.chkAbsence)
    Me.gbOpcje.Enabled = False
    Me.gbOpcje.Location = New System.Drawing.Point(709, 305)
    Me.gbOpcje.Name = "gbOpcje"
    Me.gbOpcje.Size = New System.Drawing.Size(230, 92)
    Me.gbOpcje.TabIndex = 169
    Me.gbOpcje.TabStop = False
    Me.gbOpcje.Text = "Opcje"
    '
    'chkColor
    '
    Me.chkColor.AutoSize = True
    Me.chkColor.Checked = True
    Me.chkColor.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkColor.Location = New System.Drawing.Point(6, 65)
    Me.chkColor.Name = "chkColor"
    Me.chkColor.Size = New System.Drawing.Size(140, 17)
    Me.chkColor.TabIndex = 2
    Me.chkColor.Text = "Drukuj  oceny w kolorze"
    Me.chkColor.UseVisualStyleBackColor = True
    '
    'chkNote
    '
    Me.chkNote.AutoSize = True
    Me.chkNote.Checked = True
    Me.chkNote.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkNote.Location = New System.Drawing.Point(6, 42)
    Me.chkNote.Name = "chkNote"
    Me.chkNote.Size = New System.Drawing.Size(116, 17)
    Me.chkNote.TabIndex = 1
    Me.chkNote.Text = "Drukuj liczbê uwag"
    Me.chkNote.UseVisualStyleBackColor = True
    '
    'chkAbsence
    '
    Me.chkAbsence.AutoSize = True
    Me.chkAbsence.Checked = True
    Me.chkAbsence.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkAbsence.Location = New System.Drawing.Point(6, 19)
    Me.chkAbsence.Name = "chkAbsence"
    Me.chkAbsence.Size = New System.Drawing.Size(192, 17)
    Me.chkAbsence.TabIndex = 0
    Me.chkAbsence.Text = "Drukuj liczbê opuszczonych godzin"
    Me.chkAbsence.UseVisualStyleBackColor = True
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
    Me.gbZoom.Location = New System.Drawing.Point(709, 418)
    Me.gbZoom.Name = "gbZoom"
    Me.gbZoom.Size = New System.Drawing.Size(236, 89)
    Me.gbZoom.TabIndex = 170
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
    'prnWyniki
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(954, 638)
    Me.Controls.Add(Me.gbZoom)
    Me.Controls.Add(Me.gbOpcje)
    Me.Controls.Add(Me.cmdFontSetup)
    Me.Controls.Add(Me.cmdPageSetup)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.dtData)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.nudSemestr)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.gbScoreType)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.gbPrintRange)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.pvOceny)
    Me.Name = "prnWyniki"
    Me.Text = "Wydruk wyników nauczania"
    CType(Me.nudSemestr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gbScoreType.ResumeLayout(False)
    Me.gbScoreType.PerformLayout()
    Me.gbPrintRange.ResumeLayout(False)
    Me.gbPrintRange.PerformLayout()
    Me.gbOpcje.ResumeLayout(False)
    Me.gbOpcje.PerformLayout()
    Me.gbZoom.ResumeLayout(False)
    Me.gbZoom.PerformLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents pvOceny As System.Windows.Forms.PrintPreviewControl
  Friend WithEvents nudSemestr As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents gbScoreType As System.Windows.Forms.GroupBox
  Friend WithEvents rbEndingScores As System.Windows.Forms.RadioButton
  Friend WithEvents rbPartialScores As System.Windows.Forms.RadioButton
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents gbPrintRange As System.Windows.Forms.GroupBox
  Friend WithEvents cbUczen As System.Windows.Forms.ComboBox
  Friend WithEvents rbAllStudents As System.Windows.Forms.RadioButton
  Friend WithEvents rbSelectedStudent As System.Windows.Forms.RadioButton
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents chkTableSet As System.Windows.Forms.CheckBox
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents dtData As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cmdPageSetup As System.Windows.Forms.Button
  Friend WithEvents cmdFontSetup As System.Windows.Forms.Button
  Friend WithEvents gbOpcje As System.Windows.Forms.GroupBox
  Friend WithEvents chkNote As System.Windows.Forms.CheckBox
  Friend WithEvents chkAbsence As System.Windows.Forms.CheckBox
  Friend WithEvents gbZoom As System.Windows.Forms.GroupBox
  Friend WithEvents nudZoom As System.Windows.Forms.NumericUpDown
  Friend WithEvents rbZoomCustom As System.Windows.Forms.RadioButton
  Friend WithEvents tbZoom As System.Windows.Forms.TrackBar
  Friend WithEvents rbZoom200 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom100 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom50 As System.Windows.Forms.RadioButton
  Friend WithEvents chkColor As System.Windows.Forms.CheckBox
End Class
