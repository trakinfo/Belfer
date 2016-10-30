<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgWychowawca
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
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cbBelfer = New System.Windows.Forms.ComboBox()
    Me.nudLiczbaGodzin = New System.Windows.Forms.NumericUpDown()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.dtDataAktywacji = New System.Windows.Forms.DateTimePicker()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudLiczbaGodzin, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(209, 129)
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
    'cbKlasa
    '
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.Location = New System.Drawing.Point(77, 12)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(277, 21)
    Me.cbKlasa.TabIndex = 1
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(33, 13)
    Me.Label1.TabIndex = 2
    Me.Label1.Text = "Klasa"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 42)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(59, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Nauczyciel"
    '
    'cbBelfer
    '
    Me.cbBelfer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbBelfer.FormattingEnabled = True
    Me.cbBelfer.Location = New System.Drawing.Point(77, 39)
    Me.cbBelfer.Name = "cbBelfer"
    Me.cbBelfer.Size = New System.Drawing.Size(277, 21)
    Me.cbBelfer.TabIndex = 4
    '
    'nudLiczbaGodzin
    '
    Me.nudLiczbaGodzin.DecimalPlaces = 1
    Me.nudLiczbaGodzin.Location = New System.Drawing.Point(301, 66)
    Me.nudLiczbaGodzin.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
    Me.nudLiczbaGodzin.Name = "nudLiczbaGodzin"
    Me.nudLiczbaGodzin.Size = New System.Drawing.Size(51, 20)
    Me.nudLiczbaGodzin.TabIndex = 203
    Me.nudLiczbaGodzin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.nudLiczbaGodzin.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(78, 68)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(217, 13)
    Me.Label3.TabIndex = 204
    Me.Label3.Text = "Tygodniowy wymiar godzin wychowawczych"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(69, 96)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(77, 13)
    Me.Label4.TabIndex = 205
    Me.Label4.Text = "Data aktywacji"
    '
    'dtDataAktywacji
    '
    Me.dtDataAktywacji.Location = New System.Drawing.Point(152, 92)
    Me.dtDataAktywacji.Name = "dtDataAktywacji"
    Me.dtDataAktywacji.Size = New System.Drawing.Size(200, 20)
    Me.dtDataAktywacji.TabIndex = 206
    '
    'dlgWychowawca
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(367, 170)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.dtDataAktywacji)
    Me.Controls.Add(Me.nudLiczbaGodzin)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.cbBelfer)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.cbKlasa)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgWychowawca"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Przypisz wychowawstwo"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudLiczbaGodzin, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cbBelfer As System.Windows.Forms.ComboBox
  Friend WithEvents nudLiczbaGodzin As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents dtDataAktywacji As System.Windows.Forms.DateTimePicker

End Class
