<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgOpisWyniku
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
    Me.txtOpis = New System.Windows.Forms.TextBox()
    Me.dlgColor = New System.Windows.Forms.ColorDialog()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cmdKolor = New System.Windows.Forms.Button()
    Me.txtKolor = New System.Windows.Forms.TextBox()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(213, 135)
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
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(64, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Opis wyniku"
    '
    'txtOpis
    '
    Me.txtOpis.Location = New System.Drawing.Point(82, 12)
    Me.txtOpis.MaxLength = 100
    Me.txtOpis.Multiline = True
    Me.txtOpis.Name = "txtOpis"
    Me.txtOpis.Size = New System.Drawing.Size(277, 59)
    Me.txtOpis.TabIndex = 2
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 84)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(63, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Kolor oceny"
    '
    'cmdKolor
    '
    Me.cmdKolor.BackColor = System.Drawing.Color.Black
    Me.cmdKolor.Location = New System.Drawing.Point(82, 79)
    Me.cmdKolor.Name = "cmdKolor"
    Me.cmdKolor.Size = New System.Drawing.Size(143, 23)
    Me.cmdKolor.TabIndex = 5
    Me.cmdKolor.UseVisualStyleBackColor = False
    '
    'txtKolor
    '
    Me.txtKolor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.txtKolor.Location = New System.Drawing.Point(231, 81)
    Me.txtKolor.MaxLength = 7
    Me.txtKolor.Name = "txtKolor"
    Me.txtKolor.Size = New System.Drawing.Size(125, 20)
    Me.txtKolor.TabIndex = 6
    Me.txtKolor.Text = "#000000"
    Me.txtKolor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    '
    'dlgOpisWyniku
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(371, 176)
    Me.Controls.Add(Me.txtKolor)
    Me.Controls.Add(Me.cmdKolor)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtOpis)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgOpisWyniku"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Dodawanie nowego opisu wyniku"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtOpis As System.Windows.Forms.TextBox
  Friend WithEvents dlgColor As System.Windows.Forms.ColorDialog
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents cmdKolor As System.Windows.Forms.Button
  Friend WithEvents txtKolor As System.Windows.Forms.TextBox

End Class
