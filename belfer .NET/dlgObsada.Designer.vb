<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgObsada
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
    Me.lbl2 = New System.Windows.Forms.Label()
    Me.cb2 = New System.Windows.Forms.ComboBox()
    Me.cb1 = New System.Windows.Forms.ComboBox()
    Me.lbl1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.chkGetToAvg = New System.Windows.Forms.CheckBox()
    Me.cbKategoria = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.dtDataAktywacji = New System.Windows.Forms.DateTimePicker()
    Me.nudLiczbaGodzin = New System.Windows.Forms.NumericUpDown()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.lblStudent = New System.Windows.Forms.Label()
    Me.cmdStudent = New System.Windows.Forms.Button()
    Me.pnlObsada = New System.Windows.Forms.Panel()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    CType(Me.nudLiczbaGodzin, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnlObsada.SuspendLayout()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'lbl2
    '
    Me.lbl2.AutoSize = True
    Me.lbl2.Location = New System.Drawing.Point(3, 38)
    Me.lbl2.Name = "lbl2"
    Me.lbl2.Size = New System.Drawing.Size(59, 13)
    Me.lbl2.TabIndex = 187
    Me.lbl2.Text = "Nauczyciel"
    '
    'cb2
    '
    Me.cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cb2.Enabled = False
    Me.cb2.FormattingEnabled = True
    Me.cb2.Location = New System.Drawing.Point(86, 33)
    Me.cb2.Name = "cb2"
    Me.cb2.Size = New System.Drawing.Size(315, 21)
    Me.cb2.TabIndex = 186
    '
    'cb1
    '
    Me.cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cb1.Enabled = False
    Me.cb1.FormattingEnabled = True
    Me.cb1.Location = New System.Drawing.Point(86, 9)
    Me.cb1.Name = "cb1"
    Me.cb1.Size = New System.Drawing.Size(315, 21)
    Me.cb1.TabIndex = 185
    '
    'lbl1
    '
    Me.lbl1.AutoSize = True
    Me.lbl1.Location = New System.Drawing.Point(3, 12)
    Me.lbl1.Name = "lbl1"
    Me.lbl1.Size = New System.Drawing.Size(53, 13)
    Me.lbl1.TabIndex = 184
    Me.lbl1.Text = "Przedmiot"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(4, 65)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(52, 13)
    Me.Label2.TabIndex = 188
    Me.Label2.Text = "Kategoria"
    '
    'chkGetToAvg
    '
    Me.chkGetToAvg.AutoSize = True
    Me.chkGetToAvg.Checked = True
    Me.chkGetToAvg.CheckState = System.Windows.Forms.CheckState.Checked
    Me.chkGetToAvg.Location = New System.Drawing.Point(86, 89)
    Me.chkGetToAvg.Name = "chkGetToAvg"
    Me.chkGetToAvg.Size = New System.Drawing.Size(203, 17)
    Me.chkGetToAvg.TabIndex = 189
    Me.chkGetToAvg.Text = "Uwzględnij przy liczeniu średniej ocen"
    Me.chkGetToAvg.UseVisualStyleBackColor = True
    '
    'cbKategoria
    '
    Me.cbKategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKategoria.FormattingEnabled = True
    Me.cbKategoria.Location = New System.Drawing.Point(86, 62)
    Me.cbKategoria.Name = "cbKategoria"
    Me.cbKategoria.Size = New System.Drawing.Size(119, 21)
    Me.cbKategoria.TabIndex = 190
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(3, 117)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(77, 13)
    Me.Label1.TabIndex = 198
    Me.Label1.Text = "Data aktywacji"
    '
    'dtDataAktywacji
    '
    Me.dtDataAktywacji.Location = New System.Drawing.Point(86, 111)
    Me.dtDataAktywacji.Name = "dtDataAktywacji"
    Me.dtDataAktywacji.Size = New System.Drawing.Size(200, 20)
    Me.dtDataAktywacji.TabIndex = 199
    '
    'nudLiczbaGodzin
    '
    Me.nudLiczbaGodzin.DecimalPlaces = 1
    Me.nudLiczbaGodzin.Location = New System.Drawing.Point(350, 63)
    Me.nudLiczbaGodzin.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nudLiczbaGodzin.Name = "nudLiczbaGodzin"
    Me.nudLiczbaGodzin.Size = New System.Drawing.Size(51, 20)
    Me.nudLiczbaGodzin.TabIndex = 201
    Me.nudLiczbaGodzin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.nudLiczbaGodzin.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(211, 65)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(133, 13)
    Me.Label3.TabIndex = 202
    Me.Label3.Text = "Tygodniowy wymiar godzin"
    '
    'lblStudent
    '
    Me.lblStudent.AutoSize = True
    Me.lblStudent.Location = New System.Drawing.Point(5, 15)
    Me.lblStudent.Name = "lblStudent"
    Me.lblStudent.Size = New System.Drawing.Size(38, 13)
    Me.lblStudent.TabIndex = 204
    Me.lblStudent.Text = "Uczeń"
    Me.lblStudent.Visible = False
    '
    'cmdStudent
    '
    Me.cmdStudent.Location = New System.Drawing.Point(86, 10)
    Me.cmdStudent.Name = "cmdStudent"
    Me.cmdStudent.Size = New System.Drawing.Size(315, 23)
    Me.cmdStudent.TabIndex = 205
    Me.cmdStudent.Text = "Wybierz ucznia"
    Me.cmdStudent.UseVisualStyleBackColor = True
    Me.cmdStudent.Visible = False
    '
    'pnlObsada
    '
    Me.pnlObsada.Controls.Add(Me.lbl1)
    Me.pnlObsada.Controls.Add(Me.cb1)
    Me.pnlObsada.Controls.Add(Me.cb2)
    Me.pnlObsada.Controls.Add(Me.Label1)
    Me.pnlObsada.Controls.Add(Me.dtDataAktywacji)
    Me.pnlObsada.Controls.Add(Me.nudLiczbaGodzin)
    Me.pnlObsada.Controls.Add(Me.Label3)
    Me.pnlObsada.Controls.Add(Me.chkGetToAvg)
    Me.pnlObsada.Controls.Add(Me.lbl2)
    Me.pnlObsada.Controls.Add(Me.cbKategoria)
    Me.pnlObsada.Controls.Add(Me.Label2)
    Me.pnlObsada.Location = New System.Drawing.Point(0, 39)
    Me.pnlObsada.Name = "pnlObsada"
    Me.pnlObsada.Size = New System.Drawing.Size(417, 141)
    Me.pnlObsada.TabIndex = 206
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(261, 186)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 207
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "&Dodaj"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "&Zamknij"
    '
    'dlgObsada
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(419, 225)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Controls.Add(Me.pnlObsada)
    Me.Controls.Add(Me.cmdStudent)
    Me.Controls.Add(Me.lblStudent)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgObsada"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "dlgObsada"
    CType(Me.nudLiczbaGodzin, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnlObsada.ResumeLayout(False)
    Me.pnlObsada.PerformLayout()
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents lbl2 As System.Windows.Forms.Label
  Friend WithEvents cb2 As System.Windows.Forms.ComboBox
  Friend WithEvents cb1 As System.Windows.Forms.ComboBox
  Friend WithEvents lbl1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkGetToAvg As System.Windows.Forms.CheckBox
  Friend WithEvents cbKategoria As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents dtDataAktywacji As System.Windows.Forms.DateTimePicker
  Friend WithEvents nudLiczbaGodzin As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents lblStudent As System.Windows.Forms.Label
  Friend WithEvents cmdStudent As System.Windows.Forms.Button
  Friend WithEvents pnlObsada As System.Windows.Forms.Panel
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button

End Class
