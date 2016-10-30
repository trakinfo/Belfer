<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgZastepstwo
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.lblObsadaFilter = New System.Windows.Forms.Label()
    Me.cbNauczyciel = New System.Windows.Forms.ComboBox()
    Me.dtpData = New System.Windows.Forms.DateTimePicker()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.chkStatusLekcji = New System.Windows.Forms.CheckBox()
    Me.cbLekcja = New System.Windows.Forms.ComboBox()
    Me.Panel2 = New System.Windows.Forms.Panel()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.txtSala = New System.Windows.Forms.TextBox()
    Me.chkStatus = New System.Windows.Forms.CheckBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.cbPrzedmiot = New System.Windows.Forms.ComboBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cbNauczyciel2 = New System.Windows.Forms.ComboBox()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.Panel2.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(615, 176)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Zapisz"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Zamknij"
    '
    'lblObsadaFilter
    '
    Me.lblObsadaFilter.AutoSize = True
    Me.lblObsadaFilter.Location = New System.Drawing.Point(338, 6)
    Me.lblObsadaFilter.Name = "lblObsadaFilter"
    Me.lblObsadaFilter.Size = New System.Drawing.Size(124, 13)
    Me.lblObsadaFilter.TabIndex = 205
    Me.lblObsadaFilter.Text = "Nauczyciel zastępowany"
    Me.lblObsadaFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbNauczyciel
    '
    Me.cbNauczyciel.DropDownHeight = 500
    Me.cbNauczyciel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbNauczyciel.Enabled = False
    Me.cbNauczyciel.FormattingEnabled = True
    Me.cbNauczyciel.IntegralHeight = False
    Me.cbNauczyciel.Location = New System.Drawing.Point(468, 3)
    Me.cbNauczyciel.Name = "cbNauczyciel"
    Me.cbNauczyciel.Size = New System.Drawing.Size(278, 21)
    Me.cbNauczyciel.TabIndex = 204
    '
    'dtpData
    '
    Me.dtpData.CustomFormat = "d MMMM yyyy - dddd"
    Me.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtpData.Location = New System.Drawing.Point(95, 4)
    Me.dtpData.Name = "dtpData"
    Me.dtpData.Size = New System.Drawing.Size(237, 20)
    Me.dtpData.TabIndex = 203
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(3, 6)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(86, 13)
    Me.Label1.TabIndex = 202
    Me.Label1.Text = "Data zastępstwa"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(3, 33)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(39, 13)
    Me.Label2.TabIndex = 206
    Me.Label2.Text = "Lekcja"
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.chkStatusLekcji)
    Me.Panel1.Controls.Add(Me.cbLekcja)
    Me.Panel1.Controls.Add(Me.dtpData)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.Label1)
    Me.Panel1.Controls.Add(Me.lblObsadaFilter)
    Me.Panel1.Controls.Add(Me.cbNauczyciel)
    Me.Panel1.Location = New System.Drawing.Point(12, 12)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(749, 61)
    Me.Panel1.TabIndex = 207
    '
    'chkStatusLekcji
    '
    Me.chkStatusLekcji.AutoSize = True
    Me.chkStatusLekcji.Enabled = False
    Me.chkStatusLekcji.Location = New System.Drawing.Point(637, 34)
    Me.chkStatusLekcji.Name = "chkStatusLekcji"
    Me.chkStatusLekcji.Size = New System.Drawing.Size(109, 17)
    Me.chkStatusLekcji.TabIndex = 211
    Me.chkStatusLekcji.Text = "Lekcja odwołana"
    Me.chkStatusLekcji.UseVisualStyleBackColor = True
    '
    'cbLekcja
    '
    Me.cbLekcja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbLekcja.Enabled = False
    Me.cbLekcja.FormattingEnabled = True
    Me.cbLekcja.Location = New System.Drawing.Point(95, 30)
    Me.cbLekcja.Name = "cbLekcja"
    Me.cbLekcja.Size = New System.Drawing.Size(536, 21)
    Me.cbLekcja.TabIndex = 207
    '
    'Panel2
    '
    Me.Panel2.Controls.Add(Me.Label5)
    Me.Panel2.Controls.Add(Me.txtSala)
    Me.Panel2.Controls.Add(Me.chkStatus)
    Me.Panel2.Controls.Add(Me.Label4)
    Me.Panel2.Controls.Add(Me.cbPrzedmiot)
    Me.Panel2.Controls.Add(Me.Label3)
    Me.Panel2.Controls.Add(Me.cbNauczyciel2)
    Me.Panel2.ForeColor = System.Drawing.SystemColors.ControlText
    Me.Panel2.Location = New System.Drawing.Point(12, 96)
    Me.Panel2.Name = "Panel2"
    Me.Panel2.Size = New System.Drawing.Size(749, 57)
    Me.Panel2.TabIndex = 208
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(3, 33)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(57, 13)
    Me.Label5.TabIndex = 208
    Me.Label5.Text = "Komentarz"
    '
    'txtSala
    '
    Me.txtSala.Enabled = False
    Me.txtSala.Location = New System.Drawing.Point(127, 30)
    Me.txtSala.Name = "txtSala"
    Me.txtSala.Size = New System.Drawing.Size(524, 20)
    Me.txtSala.TabIndex = 209
    '
    'chkStatus
    '
    Me.chkStatus.AutoSize = True
    Me.chkStatus.Enabled = False
    Me.chkStatus.Location = New System.Drawing.Point(657, 32)
    Me.chkStatus.Name = "chkStatus"
    Me.chkStatus.Size = New System.Drawing.Size(89, 17)
    Me.chkStatus.TabIndex = 210
    Me.chkStatus.Text = "Zrealizowane"
    Me.chkStatus.UseVisualStyleBackColor = True
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(411, 6)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(53, 13)
    Me.Label4.TabIndex = 209
    Me.Label4.Text = "Przedmiot"
    Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbPrzedmiot
    '
    Me.cbPrzedmiot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPrzedmiot.Enabled = False
    Me.cbPrzedmiot.FormattingEnabled = True
    Me.cbPrzedmiot.Location = New System.Drawing.Point(468, 3)
    Me.cbPrzedmiot.Name = "cbPrzedmiot"
    Me.cbPrzedmiot.Size = New System.Drawing.Size(278, 21)
    Me.cbPrzedmiot.TabIndex = 208
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(3, 6)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(118, 13)
    Me.Label3.TabIndex = 207
    Me.Label3.Text = "Nauczyciel zastępujący"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'cbNauczyciel2
    '
    Me.cbNauczyciel2.DropDownHeight = 600
    Me.cbNauczyciel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbNauczyciel2.Enabled = False
    Me.cbNauczyciel2.FormattingEnabled = True
    Me.cbNauczyciel2.IntegralHeight = False
    Me.cbNauczyciel2.Location = New System.Drawing.Point(127, 3)
    Me.cbNauczyciel2.Name = "cbNauczyciel2"
    Me.cbNauczyciel2.Size = New System.Drawing.Size(278, 21)
    Me.cbNauczyciel2.TabIndex = 206
    '
    'dlgZastepstwo
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(773, 217)
    Me.Controls.Add(Me.Panel2)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgZastepstwo"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Dodawanie nowego zastępstwa"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.Panel2.ResumeLayout(False)
    Me.Panel2.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lblObsadaFilter As System.Windows.Forms.Label
  Friend WithEvents cbNauczyciel As System.Windows.Forms.ComboBox
  Friend WithEvents dtpData As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents cbLekcja As System.Windows.Forms.ComboBox
  Friend WithEvents Panel2 As System.Windows.Forms.Panel
  Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cbPrzedmiot As System.Windows.Forms.ComboBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents cbNauczyciel2 As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents txtSala As System.Windows.Forms.TextBox
  Friend WithEvents chkStatusLekcji As System.Windows.Forms.CheckBox

End Class
