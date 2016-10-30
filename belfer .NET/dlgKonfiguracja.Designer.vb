<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgKonfiguracja
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
    Me.cbTypSzkoly = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.nudEndYear = New System.Windows.Forms.NumericUpDown()
    Me.nudStartYear = New System.Windows.Forms.NumericUpDown()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.cbSzkola = New System.Windows.Forms.ComboBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudEndYear, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudStartYear, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(258, 129)
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
    Me.OK_Button.Text = "OK"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 1
    Me.Cancel_Button.Text = "Anuluj"
    '
    'cbTypSzkoly
    '
    Me.cbTypSzkoly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbTypSzkoly.Enabled = False
    Me.cbTypSzkoly.FormattingEnabled = True
    Me.cbTypSzkoly.Location = New System.Drawing.Point(83, 51)
    Me.cbTypSzkoly.Name = "cbTypSzkoly"
    Me.cbTypSzkoly.Size = New System.Drawing.Size(323, 21)
    Me.cbTypSzkoly.TabIndex = 197
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 55)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(59, 13)
    Me.Label4.TabIndex = 196
    Me.Label4.Text = "Typ szkoły"
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label6.Location = New System.Drawing.Point(144, 16)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(15, 24)
    Me.Label6.TabIndex = 195
    Me.Label6.Text = "/"
    '
    'nudEndYear
    '
    Me.nudEndYear.Enabled = False
    Me.nudEndYear.Location = New System.Drawing.Point(160, 18)
    Me.nudEndYear.Maximum = New Decimal(New Integer() {2051, 0, 0, 0})
    Me.nudEndYear.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
    Me.nudEndYear.Name = "nudEndYear"
    Me.nudEndYear.Size = New System.Drawing.Size(55, 20)
    Me.nudEndYear.TabIndex = 194
    Me.nudEndYear.Value = New Decimal(New Integer() {2007, 0, 0, 0})
    '
    'nudStartYear
    '
    Me.nudStartYear.Location = New System.Drawing.Point(83, 18)
    Me.nudStartYear.Maximum = New Decimal(New Integer() {2050, 0, 0, 0})
    Me.nudStartYear.Minimum = New Decimal(New Integer() {2000, 0, 0, 0})
    Me.nudStartYear.Name = "nudStartYear"
    Me.nudStartYear.Size = New System.Drawing.Size(55, 20)
    Me.nudStartYear.TabIndex = 193
    Me.nudStartYear.Value = New Decimal(New Integer() {2006, 0, 0, 0})
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(12, 20)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(65, 13)
    Me.Label5.TabIndex = 192
    Me.Label5.Text = "Rok szkolny"
    '
    'cbSzkola
    '
    Me.cbSzkola.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbSzkola.Enabled = False
    Me.cbSzkola.FormattingEnabled = True
    Me.cbSzkola.Location = New System.Drawing.Point(83, 78)
    Me.cbSzkola.Name = "cbSzkola"
    Me.cbSzkola.Size = New System.Drawing.Size(323, 21)
    Me.cbSzkola.TabIndex = 199
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Location = New System.Drawing.Point(12, 81)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(41, 13)
    Me.Label7.TabIndex = 198
    Me.Label7.Text = "Szkoła"
    '
    'dlgKonfiguracja
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(416, 170)
    Me.Controls.Add(Me.cbSzkola)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.cbTypSzkoly)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.nudEndYear)
    Me.Controls.Add(Me.nudStartYear)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgKonfiguracja"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Konfiguracja"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudEndYear, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudStartYear, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents cbTypSzkoly As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label6 As System.Windows.Forms.Label
  Friend WithEvents nudEndYear As System.Windows.Forms.NumericUpDown
  Friend WithEvents nudStartYear As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents cbSzkola As System.Windows.Forms.ComboBox
  Friend WithEvents Label7 As System.Windows.Forms.Label

End Class
