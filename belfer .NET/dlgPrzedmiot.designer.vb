<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPrzedmiot
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
    Me.Label1 = New System.Windows.Forms.Label()
    Me.txtAlias = New System.Windows.Forms.TextBox()
    Me.cbTyp = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.txtNazwa = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(245, 74)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
    Me.TableLayoutPanel1.TabIndex = 6
    '
    'OK_Button
    '
    Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.OK_Button.Enabled = False
    Me.OK_Button.Location = New System.Drawing.Point(3, 3)
    Me.OK_Button.Name = "OK_Button"
    Me.OK_Button.Size = New System.Drawing.Size(67, 23)
    Me.OK_Button.TabIndex = 3
    Me.OK_Button.Text = "Zapisz"
    '
    'Cancel_Button
    '
    Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
    Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
    Me.Cancel_Button.Name = "Cancel_Button"
    Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
    Me.Cancel_Button.TabIndex = 4
    Me.Cancel_Button.Text = "Zamknij"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(29, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Alias"
    '
    'txtAlias
    '
    Me.txtAlias.Location = New System.Drawing.Point(58, 12)
    Me.txtAlias.Name = "txtAlias"
    Me.txtAlias.Size = New System.Drawing.Size(239, 20)
    Me.txtAlias.TabIndex = 0
    '
    'cbTyp
    '
    Me.cbTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbTyp.FormattingEnabled = True
    Me.cbTyp.Location = New System.Drawing.Point(334, 12)
    Me.cbTyp.Name = "cbTyp"
    Me.cbTyp.Size = New System.Drawing.Size(57, 21)
    Me.cbTyp.TabIndex = 1
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(303, 15)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(25, 13)
    Me.Label4.TabIndex = 8
    Me.Label4.Text = "Typ"
    '
    'txtNazwa
    '
    Me.txtNazwa.Location = New System.Drawing.Point(58, 38)
    Me.txtNazwa.Name = "txtNazwa"
    Me.txtNazwa.Size = New System.Drawing.Size(333, 20)
    Me.txtNazwa.TabIndex = 2
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Location = New System.Drawing.Point(12, 41)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(40, 13)
    Me.Label6.TabIndex = 29
    Me.Label6.Text = "Nazwa"
    '
    'dlgPrzedmiot
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(403, 115)
    Me.Controls.Add(Me.txtNazwa)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.cbTyp)
    Me.Controls.Add(Me.txtAlias)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgPrzedmiot"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Dodawanie przedmiotu nauczania"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtAlias As System.Windows.Forms.TextBox
  Friend WithEvents cbTyp As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents txtNazwa As System.Windows.Forms.TextBox
  Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
