<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgNrDz
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
    Me.nudNr = New System.Windows.Forms.NumericUpDown()
    Me.rbSetByHand = New System.Windows.Forms.RadioButton()
    Me.rbAddToEnd = New System.Windows.Forms.RadioButton()
    Me.rbByAlfabet = New System.Windows.Forms.RadioButton()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudNr, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(23, 106)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
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
    Me.Cancel_Button.Text = "&Anuluj"
    '
    'nudNr
    '
    Me.nudNr.Enabled = False
    Me.nudNr.Location = New System.Drawing.Point(116, 59)
    Me.nudNr.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudNr.Name = "nudNr"
    Me.nudNr.Size = New System.Drawing.Size(50, 20)
    Me.nudNr.TabIndex = 8
    Me.nudNr.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'rbSetByHand
    '
    Me.rbSetByHand.AutoSize = True
    Me.rbSetByHand.Location = New System.Drawing.Point(12, 59)
    Me.rbSetByHand.Name = "rbSetByHand"
    Me.rbSetByHand.Size = New System.Drawing.Size(92, 17)
    Me.rbSetByHand.TabIndex = 7
    Me.rbSetByHand.TabStop = True
    Me.rbSetByHand.Text = "Ustaw ręcznie"
    Me.rbSetByHand.UseVisualStyleBackColor = True
    '
    'rbAddToEnd
    '
    Me.rbAddToEnd.AutoSize = True
    Me.rbAddToEnd.Location = New System.Drawing.Point(12, 36)
    Me.rbAddToEnd.Name = "rbAddToEnd"
    Me.rbAddToEnd.Size = New System.Drawing.Size(127, 17)
    Me.rbAddToEnd.TabIndex = 6
    Me.rbAddToEnd.Text = "Dopisz na koniec listy"
    Me.rbAddToEnd.UseVisualStyleBackColor = True
    '
    'rbByAlfabet
    '
    Me.rbByAlfabet.AutoSize = True
    Me.rbByAlfabet.Checked = True
    Me.rbByAlfabet.Location = New System.Drawing.Point(12, 13)
    Me.rbByAlfabet.Name = "rbByAlfabet"
    Me.rbByAlfabet.Size = New System.Drawing.Size(88, 17)
    Me.rbByAlfabet.TabIndex = 5
    Me.rbByAlfabet.TabStop = True
    Me.rbByAlfabet.Text = "Alfabetycznie"
    Me.rbByAlfabet.UseVisualStyleBackColor = True
    '
    'dlgNrDz
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(181, 147)
    Me.Controls.Add(Me.nudNr)
    Me.Controls.Add(Me.rbSetByHand)
    Me.Controls.Add(Me.rbAddToEnd)
    Me.Controls.Add(Me.rbByAlfabet)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgNrDz"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "dlgNrDz"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudNr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents nudNr As System.Windows.Forms.NumericUpDown
  Friend WithEvents rbSetByHand As System.Windows.Forms.RadioButton
  Friend WithEvents rbAddToEnd As System.Windows.Forms.RadioButton
  Friend WithEvents rbByAlfabet As System.Windows.Forms.RadioButton

End Class
