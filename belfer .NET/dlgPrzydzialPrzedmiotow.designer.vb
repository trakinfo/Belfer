<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPrzydzialPrzedmiotow
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
    Me.lvPrzedmiot = New System.Windows.Forms.ListView()
    Me.nudGrupa = New System.Windows.Forms.NumericUpDown()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.chkGrupa = New System.Windows.Forms.CheckBox()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudGrupa, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(172, 389)
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
    'lvPrzedmiot
    '
    Me.lvPrzedmiot.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvPrzedmiot.Location = New System.Drawing.Point(12, 12)
    Me.lvPrzedmiot.Name = "lvPrzedmiot"
    Me.lvPrzedmiot.Size = New System.Drawing.Size(306, 336)
    Me.lvPrzedmiot.TabIndex = 1
    Me.lvPrzedmiot.UseCompatibleStateImageBehavior = False
    '
    'nudGrupa
    '
    Me.nudGrupa.Enabled = False
    Me.nudGrupa.Location = New System.Drawing.Point(280, 354)
    Me.nudGrupa.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
    Me.nudGrupa.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudGrupa.Name = "nudGrupa"
    Me.nudGrupa.Size = New System.Drawing.Size(38, 20)
    Me.nudGrupa.TabIndex = 2
    Me.nudGrupa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.nudGrupa.Value = New Decimal(New Integer() {2, 0, 0, 0})
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Enabled = False
    Me.Label1.Location = New System.Drawing.Point(212, 356)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(62, 13)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "Liczba grup"
    '
    'chkGrupa
    '
    Me.chkGrupa.AutoSize = True
    Me.chkGrupa.Enabled = False
    Me.chkGrupa.Location = New System.Drawing.Point(12, 355)
    Me.chkGrupa.Name = "chkGrupa"
    Me.chkGrupa.Size = New System.Drawing.Size(106, 17)
    Me.chkGrupa.TabIndex = 4
    Me.chkGrupa.Text = "Podział na grupy"
    Me.chkGrupa.UseVisualStyleBackColor = True
    '
    'dlgPrzydzialPrzedmiotow
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(330, 430)
    Me.Controls.Add(Me.chkGrupa)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.nudGrupa)
    Me.Controls.Add(Me.lvPrzedmiot)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgPrzydzialPrzedmiotow"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Przydział przedmiotów do szkół"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudGrupa, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents lvPrzedmiot As System.Windows.Forms.ListView
  Friend WithEvents nudGrupa As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents chkGrupa As System.Windows.Forms.CheckBox

End Class
