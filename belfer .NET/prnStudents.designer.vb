<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class prnStudents
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.pvListaUczniow = New System.Windows.Forms.PrintPreviewControl()
    Me.gbPrintContent = New System.Windows.Forms.GroupBox()
    Me.chkTel = New System.Windows.Forms.CheckBox()
    Me.chkAdres = New System.Windows.Forms.CheckBox()
    Me.chkPesel = New System.Windows.Forms.CheckBox()
    Me.chkUwagi = New System.Windows.Forms.CheckBox()
    Me.chkImionaRodzicow = New System.Windows.Forms.CheckBox()
    Me.chkDataUr = New System.Windows.Forms.CheckBox()
    Me.chkMiejsceUr = New System.Windows.Forms.CheckBox()
    Me.chkNrArkusza = New System.Windows.Forms.CheckBox()
    Me.chkFullName = New System.Windows.Forms.CheckBox()
    Me.gbZakres = New System.Windows.Forms.GroupBox()
    Me.rbAllStudents = New System.Windows.Forms.RadioButton()
    Me.rbSelectedClass = New System.Windows.Forms.RadioButton()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.gbZoom = New System.Windows.Forms.GroupBox()
    Me.nudZoom = New System.Windows.Forms.NumericUpDown()
    Me.rbZoomCustom = New System.Windows.Forms.RadioButton()
    Me.tbZoom = New System.Windows.Forms.TrackBar()
    Me.rbZoom200 = New System.Windows.Forms.RadioButton()
    Me.rbZoom150 = New System.Windows.Forms.RadioButton()
    Me.rbZoom100 = New System.Windows.Forms.RadioButton()
    Me.rbZoom75 = New System.Windows.Forms.RadioButton()
    Me.rbZoom50 = New System.Windows.Forms.RadioButton()
    Me.rbZoom25 = New System.Windows.Forms.RadioButton()
    Me.gbOpcje = New System.Windows.Forms.GroupBox()
    Me.chkSiatka = New System.Windows.Forms.CheckBox()
    Me.chkPodsumowanie = New System.Windows.Forms.CheckBox()
    Me.chkWychowawca = New System.Windows.Forms.CheckBox()
    Me.chkPageBreak = New System.Windows.Forms.CheckBox()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.cmdPrint = New System.Windows.Forms.Button()
    Me.cmdPageSetup = New System.Windows.Forms.Button()
    Me.gbPrintContent.SuspendLayout()
    Me.gbZakres.SuspendLayout()
    Me.gbZoom.SuspendLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.gbOpcje.SuspendLayout()
    Me.SuspendLayout()
    '
    'pvListaUczniow
    '
    Me.pvListaUczniow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pvListaUczniow.Location = New System.Drawing.Point(0, 0)
    Me.pvListaUczniow.Name = "pvListaUczniow"
    Me.pvListaUczniow.Size = New System.Drawing.Size(655, 636)
    Me.pvListaUczniow.TabIndex = 12
    '
    'gbPrintContent
    '
    Me.gbPrintContent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbPrintContent.Controls.Add(Me.chkTel)
    Me.gbPrintContent.Controls.Add(Me.chkAdres)
    Me.gbPrintContent.Controls.Add(Me.chkPesel)
    Me.gbPrintContent.Controls.Add(Me.chkUwagi)
    Me.gbPrintContent.Controls.Add(Me.chkImionaRodzicow)
    Me.gbPrintContent.Controls.Add(Me.chkDataUr)
    Me.gbPrintContent.Controls.Add(Me.chkMiejsceUr)
    Me.gbPrintContent.Controls.Add(Me.chkNrArkusza)
    Me.gbPrintContent.Controls.Add(Me.chkFullName)
    Me.gbPrintContent.Location = New System.Drawing.Point(667, 133)
    Me.gbPrintContent.Name = "gbPrintContent"
    Me.gbPrintContent.Size = New System.Drawing.Size(336, 94)
    Me.gbPrintContent.TabIndex = 18
    Me.gbPrintContent.TabStop = False
    Me.gbPrintContent.Text = "Zawartość wydruku"
    '
    'chkTel
    '
    Me.chkTel.AutoSize = True
    Me.chkTel.Location = New System.Drawing.Point(138, 65)
    Me.chkTel.Name = "chkTel"
    Me.chkTel.Size = New System.Drawing.Size(86, 17)
    Me.chkTel.TabIndex = 12
    Me.chkTel.Tag = "TRIM(BOTH ';' FROM Concat_WS(';',Tel,TelKom1,TelKom2)) AS Tel"
    Me.chkTel.Text = "Nr telefonów"
    Me.chkTel.UseVisualStyleBackColor = True
    '
    'chkAdres
    '
    Me.chkAdres.AutoSize = True
    Me.chkAdres.Location = New System.Drawing.Point(277, 19)
    Me.chkAdres.Name = "chkAdres"
    Me.chkAdres.Size = New System.Drawing.Size(53, 17)
    Me.chkAdres.TabIndex = 11
    Me.chkAdres.Tag = "TRIM(TRAILING '/' FROM CONCAT_WS('/',CONCAT_WS(' ',IFNULL(TRIM(TRAILING ', ' FROM" & _
    " Concat_WS(', ',m.Nazwa,u.Ulica)),''),u.NrDomu),u.NrMieszkania)) AS Adres"
    Me.chkAdres.Text = "Adres"
    Me.chkAdres.UseVisualStyleBackColor = True
    '
    'chkPesel
    '
    Me.chkPesel.AutoSize = True
    Me.chkPesel.Location = New System.Drawing.Point(277, 42)
    Me.chkPesel.Name = "chkPesel"
    Me.chkPesel.Size = New System.Drawing.Size(52, 17)
    Me.chkPesel.TabIndex = 9
    Me.chkPesel.Tag = "u.Pesel"
    Me.chkPesel.Text = "Pesel"
    Me.chkPesel.UseVisualStyleBackColor = True
    '
    'chkUwagi
    '
    Me.chkUwagi.AutoSize = True
    Me.chkUwagi.Location = New System.Drawing.Point(277, 65)
    Me.chkUwagi.Name = "chkUwagi"
    Me.chkUwagi.Size = New System.Drawing.Size(56, 17)
    Me.chkUwagi.TabIndex = 8
    Me.chkUwagi.Tag = "Uwagi"
    Me.chkUwagi.Text = "Uwagi"
    Me.chkUwagi.UseVisualStyleBackColor = True
    '
    'chkImionaRodzicow
    '
    Me.chkImionaRodzicow.AutoSize = True
    Me.chkImionaRodzicow.Location = New System.Drawing.Point(138, 19)
    Me.chkImionaRodzicow.Name = "chkImionaRodzicow"
    Me.chkImionaRodzicow.Size = New System.Drawing.Size(102, 17)
    Me.chkImionaRodzicow.TabIndex = 7
    Me.chkImionaRodzicow.Tag = "Concat_WS(' ',ImieOjca,ImieMatki) AS ImionaRodzicow"
    Me.chkImionaRodzicow.Text = "Imiona rodziców"
    Me.chkImionaRodzicow.UseVisualStyleBackColor = True
    '
    'chkDataUr
    '
    Me.chkDataUr.AutoSize = True
    Me.chkDataUr.Location = New System.Drawing.Point(6, 42)
    Me.chkDataUr.Name = "chkDataUr"
    Me.chkDataUr.Size = New System.Drawing.Size(98, 17)
    Me.chkDataUr.TabIndex = 6
    Me.chkDataUr.Tag = "DataUr"
    Me.chkDataUr.Text = "Data urodzenia"
    Me.chkDataUr.UseVisualStyleBackColor = True
    '
    'chkMiejsceUr
    '
    Me.chkMiejsceUr.AutoSize = True
    Me.chkMiejsceUr.Location = New System.Drawing.Point(138, 42)
    Me.chkMiejsceUr.Name = "chkMiejsceUr"
    Me.chkMiejsceUr.Size = New System.Drawing.Size(111, 17)
    Me.chkMiejsceUr.TabIndex = 5
    Me.chkMiejsceUr.Tag = "IdMiejsceUr"
    Me.chkMiejsceUr.Text = "Miejsce urodzenia"
    Me.chkMiejsceUr.UseVisualStyleBackColor = True
    '
    'chkNrArkusza
    '
    Me.chkNrArkusza.AutoSize = True
    Me.chkNrArkusza.Location = New System.Drawing.Point(6, 65)
    Me.chkNrArkusza.Name = "chkNrArkusza"
    Me.chkNrArkusza.Size = New System.Drawing.Size(98, 17)
    Me.chkNrArkusza.TabIndex = 1
    Me.chkNrArkusza.Tag = "NrArkusza"
    Me.chkNrArkusza.Text = "Nr ewidencyjny"
    Me.chkNrArkusza.UseVisualStyleBackColor = True
    '
    'chkFullName
    '
    Me.chkFullName.AutoSize = True
    Me.chkFullName.Checked = True
    Me.chkFullName.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkFullName.Enabled = False
    Me.chkFullName.Location = New System.Drawing.Point(6, 19)
    Me.chkFullName.Name = "chkFullName"
    Me.chkFullName.Size = New System.Drawing.Size(110, 17)
    Me.chkFullName.TabIndex = 0
    Me.chkFullName.Tag = "Concat_WS(' ',Nazwisko,Imie,Imie2) AS FullName"
    Me.chkFullName.Text = "Nazwisko i imiona"
    Me.chkFullName.UseVisualStyleBackColor = True
    '
    'gbZakres
    '
    Me.gbZakres.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbZakres.Controls.Add(Me.rbAllStudents)
    Me.gbZakres.Controls.Add(Me.rbSelectedClass)
    Me.gbZakres.Controls.Add(Me.cbKlasa)
    Me.gbZakres.Location = New System.Drawing.Point(667, 12)
    Me.gbZakres.Name = "gbZakres"
    Me.gbZakres.Size = New System.Drawing.Size(336, 96)
    Me.gbZakres.TabIndex = 19
    Me.gbZakres.TabStop = False
    Me.gbZakres.Text = "Zakres wydruku"
    '
    'rbAllStudents
    '
    Me.rbAllStudents.AutoSize = True
    Me.rbAllStudents.Location = New System.Drawing.Point(6, 19)
    Me.rbAllStudents.Name = "rbAllStudents"
    Me.rbAllStudents.Size = New System.Drawing.Size(156, 17)
    Me.rbAllStudents.TabIndex = 4
    Me.rbAllStudents.Text = "Wszyscy uczniowie wg klas"
    Me.rbAllStudents.UseVisualStyleBackColor = True
    '
    'rbSelectedClass
    '
    Me.rbSelectedClass.AutoSize = True
    Me.rbSelectedClass.Checked = True
    Me.rbSelectedClass.Location = New System.Drawing.Point(6, 42)
    Me.rbSelectedClass.Name = "rbSelectedClass"
    Me.rbSelectedClass.Size = New System.Drawing.Size(96, 17)
    Me.rbSelectedClass.TabIndex = 5
    Me.rbSelectedClass.TabStop = True
    Me.rbSelectedClass.Text = "Wybrana klasa"
    Me.rbSelectedClass.UseVisualStyleBackColor = True
    '
    'cbKlasa
    '
    Me.cbKlasa.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(29, 65)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(301, 21)
    Me.cbKlasa.TabIndex = 7
    '
    'gbZoom
    '
    Me.gbZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbZoom.Controls.Add(Me.nudZoom)
    Me.gbZoom.Controls.Add(Me.rbZoomCustom)
    Me.gbZoom.Controls.Add(Me.tbZoom)
    Me.gbZoom.Controls.Add(Me.rbZoom200)
    Me.gbZoom.Controls.Add(Me.rbZoom150)
    Me.gbZoom.Controls.Add(Me.rbZoom100)
    Me.gbZoom.Controls.Add(Me.rbZoom75)
    Me.gbZoom.Controls.Add(Me.rbZoom50)
    Me.gbZoom.Controls.Add(Me.rbZoom25)
    Me.gbZoom.Location = New System.Drawing.Point(667, 253)
    Me.gbZoom.Name = "gbZoom"
    Me.gbZoom.Size = New System.Drawing.Size(336, 89)
    Me.gbZoom.TabIndex = 20
    Me.gbZoom.TabStop = False
    Me.gbZoom.Text = "Powiększenie"
    '
    'nudZoom
    '
    Me.nudZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nudZoom.Enabled = False
    Me.nudZoom.Location = New System.Drawing.Point(220, 19)
    Me.nudZoom.Maximum = New Decimal(New Integer() {400, 0, 0, 0})
    Me.nudZoom.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nudZoom.Name = "nudZoom"
    Me.nudZoom.Size = New System.Drawing.Size(55, 20)
    Me.nudZoom.TabIndex = 158
    Me.nudZoom.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'rbZoomCustom
    '
    Me.rbZoomCustom.AutoSize = True
    Me.rbZoomCustom.Location = New System.Drawing.Point(128, 19)
    Me.rbZoomCustom.Name = "rbZoomCustom"
    Me.rbZoomCustom.Size = New System.Drawing.Size(86, 17)
    Me.rbZoomCustom.TabIndex = 7
    Me.rbZoomCustom.Tag = "0,05"
    Me.rbZoomCustom.Text = "Użytkownika"
    Me.rbZoomCustom.UseVisualStyleBackColor = True
    '
    'tbZoom
    '
    Me.tbZoom.Enabled = False
    Me.tbZoom.LargeChange = 10
    Me.tbZoom.Location = New System.Drawing.Point(128, 37)
    Me.tbZoom.Maximum = 400
    Me.tbZoom.Minimum = 50
    Me.tbZoom.Name = "tbZoom"
    Me.tbZoom.Size = New System.Drawing.Size(202, 45)
    Me.tbZoom.TabIndex = 6
    Me.tbZoom.TickFrequency = 10
    Me.tbZoom.Value = 100
    '
    'rbZoom200
    '
    Me.rbZoom200.AutoSize = True
    Me.rbZoom200.Location = New System.Drawing.Point(71, 65)
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
    Me.rbZoom150.Location = New System.Drawing.Point(71, 42)
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
    Me.rbZoom100.Checked = True
    Me.rbZoom100.Location = New System.Drawing.Point(71, 19)
    Me.rbZoom100.Name = "rbZoom100"
    Me.rbZoom100.Size = New System.Drawing.Size(51, 17)
    Me.rbZoom100.TabIndex = 3
    Me.rbZoom100.TabStop = True
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
    Me.rbZoom50.Location = New System.Drawing.Point(6, 42)
    Me.rbZoom50.Name = "rbZoom50"
    Me.rbZoom50.Size = New System.Drawing.Size(45, 17)
    Me.rbZoom50.TabIndex = 1
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
    'gbOpcje
    '
    Me.gbOpcje.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbOpcje.Controls.Add(Me.chkSiatka)
    Me.gbOpcje.Controls.Add(Me.chkPodsumowanie)
    Me.gbOpcje.Controls.Add(Me.chkWychowawca)
    Me.gbOpcje.Controls.Add(Me.chkPageBreak)
    Me.gbOpcje.Location = New System.Drawing.Point(667, 376)
    Me.gbOpcje.Name = "gbOpcje"
    Me.gbOpcje.Size = New System.Drawing.Size(336, 109)
    Me.gbOpcje.TabIndex = 162
    Me.gbOpcje.TabStop = False
    Me.gbOpcje.Text = "Opcje"
    '
    'chkSiatka
    '
    Me.chkSiatka.AutoSize = True
    Me.chkSiatka.Checked = True
    Me.chkSiatka.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkSiatka.Location = New System.Drawing.Point(6, 88)
    Me.chkSiatka.Name = "chkSiatka"
    Me.chkSiatka.Size = New System.Drawing.Size(105, 17)
    Me.chkSiatka.TabIndex = 13
    Me.chkSiatka.Text = "Drukuj linie siatki"
    Me.chkSiatka.UseVisualStyleBackColor = True
    '
    'chkPodsumowanie
    '
    Me.chkPodsumowanie.AutoSize = True
    Me.chkPodsumowanie.Location = New System.Drawing.Point(6, 65)
    Me.chkPodsumowanie.Name = "chkPodsumowanie"
    Me.chkPodsumowanie.Size = New System.Drawing.Size(131, 17)
    Me.chkPodsumowanie.TabIndex = 12
    Me.chkPodsumowanie.Tag = "Pesel"
    Me.chkPodsumowanie.Text = "Drukuj podsumowanie"
    Me.chkPodsumowanie.UseVisualStyleBackColor = True
    '
    'chkWychowawca
    '
    Me.chkWychowawca.AutoSize = True
    Me.chkWychowawca.Location = New System.Drawing.Point(6, 42)
    Me.chkWychowawca.Name = "chkWychowawca"
    Me.chkWychowawca.Size = New System.Drawing.Size(197, 17)
    Me.chkWychowawca.TabIndex = 11
    Me.chkWychowawca.Tag = "Pesel"
    Me.chkWychowawca.Text = "Drukuj nazwisko i imię wychowawcy"
    Me.chkWychowawca.UseVisualStyleBackColor = True
    '
    'chkPageBreak
    '
    Me.chkPageBreak.AutoSize = True
    Me.chkPageBreak.Location = New System.Drawing.Point(6, 19)
    Me.chkPageBreak.Name = "chkPageBreak"
    Me.chkPageBreak.Size = New System.Drawing.Size(173, 17)
    Me.chkPageBreak.TabIndex = 10
    Me.chkPageBreak.Tag = "Pesel"
    Me.chkPageBreak.Text = "Każda klasa na osobnej stronie"
    Me.chkPageBreak.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(918, 589)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(85, 35)
    Me.cmdClose.TabIndex = 164
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdPrint
    '
    Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrint.Image = Global.belfer.NET.My.Resources.Resources.print_24
    Me.cmdPrint.Location = New System.Drawing.Point(667, 589)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.Size = New System.Drawing.Size(85, 35)
    Me.cmdPrint.TabIndex = 163
    Me.cmdPrint.Text = "&Drukuj"
    Me.cmdPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'cmdPageSetup
    '
    Me.cmdPageSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPageSetup.Location = New System.Drawing.Point(758, 589)
    Me.cmdPageSetup.Name = "cmdPageSetup"
    Me.cmdPageSetup.Size = New System.Drawing.Size(85, 35)
    Me.cmdPageSetup.TabIndex = 165
    Me.cmdPageSetup.Text = "Ustawienia strony"
    Me.cmdPageSetup.UseVisualStyleBackColor = True
    '
    'prnStudents
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(1015, 636)
    Me.Controls.Add(Me.cmdPageSetup)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cmdPrint)
    Me.Controls.Add(Me.gbOpcje)
    Me.Controls.Add(Me.gbPrintContent)
    Me.Controls.Add(Me.gbZakres)
    Me.Controls.Add(Me.gbZoom)
    Me.Controls.Add(Me.pvListaUczniow)
    Me.Name = "prnStudents"
    Me.Text = "Wydruk listy uczniów"
    Me.gbPrintContent.ResumeLayout(False)
    Me.gbPrintContent.PerformLayout()
    Me.gbZakres.ResumeLayout(False)
    Me.gbZakres.PerformLayout()
    Me.gbZoom.ResumeLayout(False)
    Me.gbZoom.PerformLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.gbOpcje.ResumeLayout(False)
    Me.gbOpcje.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents pvListaUczniow As System.Windows.Forms.PrintPreviewControl
  Friend WithEvents gbPrintContent As System.Windows.Forms.GroupBox
  Friend WithEvents chkUwagi As System.Windows.Forms.CheckBox
  Friend WithEvents chkImionaRodzicow As System.Windows.Forms.CheckBox
  Friend WithEvents chkDataUr As System.Windows.Forms.CheckBox
  Friend WithEvents chkMiejsceUr As System.Windows.Forms.CheckBox
  Friend WithEvents chkNrArkusza As System.Windows.Forms.CheckBox
  Friend WithEvents chkFullName As System.Windows.Forms.CheckBox
  Friend WithEvents gbZakres As System.Windows.Forms.GroupBox
  Friend WithEvents rbAllStudents As System.Windows.Forms.RadioButton
  Friend WithEvents rbSelectedClass As System.Windows.Forms.RadioButton
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents gbZoom As System.Windows.Forms.GroupBox
  Friend WithEvents rbZoom200 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom150 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom100 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom75 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom50 As System.Windows.Forms.RadioButton
  Friend WithEvents rbZoom25 As System.Windows.Forms.RadioButton
  Friend WithEvents tbZoom As System.Windows.Forms.TrackBar
  Friend WithEvents rbZoomCustom As System.Windows.Forms.RadioButton
  Friend WithEvents chkPesel As System.Windows.Forms.CheckBox
  Friend WithEvents chkAdres As System.Windows.Forms.CheckBox
  Friend WithEvents chkTel As System.Windows.Forms.CheckBox
  Friend WithEvents nudZoom As System.Windows.Forms.NumericUpDown
  Friend WithEvents gbOpcje As System.Windows.Forms.GroupBox
  Friend WithEvents chkPageBreak As System.Windows.Forms.CheckBox
  Friend WithEvents chkPodsumowanie As System.Windows.Forms.CheckBox
  Friend WithEvents chkWychowawca As System.Windows.Forms.CheckBox
  Friend WithEvents chkSiatka As System.Windows.Forms.CheckBox
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents cmdPrint As System.Windows.Forms.Button
  Friend WithEvents cmdPageSetup As System.Windows.Forms.Button
End Class
