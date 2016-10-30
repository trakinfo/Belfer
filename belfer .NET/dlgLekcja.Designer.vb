<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgLekcja
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.OK_Button = New System.Windows.Forms.Button()
    Me.Cancel_Button = New System.Windows.Forms.Button()
    Me.lblKlasa = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cbGodzina = New System.Windows.Forms.ComboBox()
    Me.cbObsada = New System.Windows.Forms.ComboBox()
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.txtSala = New System.Windows.Forms.TextBox()
    Me.cbWeekDay = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.Panel1.SuspendLayout()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 184)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 9
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 0
    Me.OK_Button.Text = "Dodaj"
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
    'lblKlasa
    '
    Me.lblKlasa.AutoSize = True
    Me.lblKlasa.Enabled = False
    Me.lblKlasa.Location = New System.Drawing.Point(6, 5)
    Me.lblKlasa.Name = "lblKlasa"
    Me.lblKlasa.Size = New System.Drawing.Size(39, 13)
    Me.lblKlasa.TabIndex = 2
    Me.lblKlasa.Text = "Klasa: "
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 72)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(88, 13)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "Godzina lekcyjna"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 99)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(39, 13)
    Me.Label2.TabIndex = 5
    Me.Label2.Text = "Lekcja"
    '
    'cbGodzina
    '
    Me.cbGodzina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbGodzina.Enabled = False
    Me.cbGodzina.FormattingEnabled = True
    Me.cbGodzina.Location = New System.Drawing.Point(106, 69)
    Me.cbGodzina.Name = "cbGodzina"
    Me.cbGodzina.Size = New System.Drawing.Size(317, 21)
    Me.cbGodzina.TabIndex = 6
    '
    'cbObsada
    '
    Me.cbObsada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbObsada.Enabled = False
    Me.cbObsada.FormattingEnabled = True
    Me.cbObsada.Location = New System.Drawing.Point(106, 96)
    Me.cbObsada.Name = "cbObsada"
    Me.cbObsada.Size = New System.Drawing.Size(317, 21)
    Me.cbObsada.TabIndex = 7
    '
    'Panel1
    '
    Me.Panel1.Controls.Add(Me.lblKlasa)
    Me.Panel1.Location = New System.Drawing.Point(1, 5)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(434, 31)
    Me.Panel1.TabIndex = 8
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 126)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(28, 13)
    Me.Label3.TabIndex = 9
    Me.Label3.Text = "Sala"
    '
    'txtSala
    '
    Me.txtSala.Enabled = False
    Me.txtSala.Location = New System.Drawing.Point(106, 123)
    Me.txtSala.Name = "txtSala"
    Me.txtSala.Size = New System.Drawing.Size(317, 20)
    Me.txtSala.TabIndex = 8
    '
    'cbWeekDay
    '
    Me.cbWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbWeekDay.FormattingEnabled = True
    Me.cbWeekDay.Location = New System.Drawing.Point(106, 42)
    Me.cbWeekDay.Name = "cbWeekDay"
    Me.cbWeekDay.Size = New System.Drawing.Size(317, 21)
    Me.cbWeekDay.TabIndex = 10
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 45)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(77, 13)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "Dzień tygodnia"
    '
    'dlgLekcja
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(435, 225)
    Me.Controls.Add(Me.cbWeekDay)
    Me.Controls.Add(Me.txtSala)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.cbObsada)
    Me.Controls.Add(Me.cbGodzina)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgLekcja"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Nowa lekcja"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.Panel1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lblKlasa As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cbGodzina As System.Windows.Forms.ComboBox
  Friend WithEvents cbObsada As System.Windows.Forms.ComboBox
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtSala As System.Windows.Forms.TextBox
  Friend WithEvents cbWeekDay As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
