<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPrintPreview
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
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.pvWydruk = New System.Windows.Forms.PrintPreviewControl()
    Me.gbZoom = New System.Windows.Forms.GroupBox()
    Me.nudZoom = New System.Windows.Forms.NumericUpDown()
    Me.tbZoom = New System.Windows.Forms.TrackBar()
    Me.cmdPageSetup = New System.Windows.Forms.Button()
    Me.cmdFontSetup = New System.Windows.Forms.Button()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.Panel1.SuspendLayout()
    Me.gbZoom.SuspendLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(796, 595)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
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
    Me.OK_Button.Text = "&Drukuj"
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
    'Panel1
    '
    Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Panel1.Controls.Add(Me.pvWydruk)
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(790, 636)
    Me.Panel1.TabIndex = 1
    '
    'pvWydruk
    '
    Me.pvWydruk.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pvWydruk.Location = New System.Drawing.Point(0, 0)
    Me.pvWydruk.Name = "pvWydruk"
    Me.pvWydruk.Size = New System.Drawing.Size(790, 636)
    Me.pvWydruk.TabIndex = 0
    Me.pvWydruk.UseAntiAlias = True
    '
    'gbZoom
    '
    Me.gbZoom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.gbZoom.Controls.Add(Me.nudZoom)
    Me.gbZoom.Controls.Add(Me.tbZoom)
    Me.gbZoom.Location = New System.Drawing.Point(796, 12)
    Me.gbZoom.Name = "gbZoom"
    Me.gbZoom.Size = New System.Drawing.Size(143, 90)
    Me.gbZoom.TabIndex = 23
    Me.gbZoom.TabStop = False
    Me.gbZoom.Text = "Powiększenie"
    '
    'nudZoom
    '
    Me.nudZoom.Dock = System.Windows.Forms.DockStyle.Top
    Me.nudZoom.Location = New System.Drawing.Point(3, 61)
    Me.nudZoom.Maximum = New Decimal(New Integer() {400, 0, 0, 0})
    Me.nudZoom.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nudZoom.Name = "nudZoom"
    Me.nudZoom.Size = New System.Drawing.Size(137, 20)
    Me.nudZoom.TabIndex = 158
    Me.nudZoom.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'tbZoom
    '
    Me.tbZoom.Dock = System.Windows.Forms.DockStyle.Top
    Me.tbZoom.LargeChange = 10
    Me.tbZoom.Location = New System.Drawing.Point(3, 16)
    Me.tbZoom.Maximum = 400
    Me.tbZoom.Minimum = 50
    Me.tbZoom.Name = "tbZoom"
    Me.tbZoom.Size = New System.Drawing.Size(137, 45)
    Me.tbZoom.TabIndex = 6
    Me.tbZoom.TickFrequency = 10
    Me.tbZoom.Value = 100
    '
    'cmdPageSetup
    '
    Me.cmdPageSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPageSetup.Location = New System.Drawing.Point(796, 172)
    Me.cmdPageSetup.Name = "cmdPageSetup"
    Me.cmdPageSetup.Size = New System.Drawing.Size(146, 23)
    Me.cmdPageSetup.TabIndex = 24
    Me.cmdPageSetup.Text = "Ustawienia strony"
    Me.cmdPageSetup.UseVisualStyleBackColor = True
    '
    'cmdFontSetup
    '
    Me.cmdFontSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdFontSetup.Location = New System.Drawing.Point(796, 143)
    Me.cmdFontSetup.Name = "cmdFontSetup"
    Me.cmdFontSetup.Size = New System.Drawing.Size(146, 23)
    Me.cmdFontSetup.TabIndex = 169
    Me.cmdFontSetup.Text = "Ustawienia czcionki"
    Me.cmdFontSetup.UseVisualStyleBackColor = True
    '
    'dlgPrintPreview
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(954, 636)
    Me.Controls.Add(Me.cmdFontSetup)
    Me.Controls.Add(Me.cmdPageSetup)
    Me.Controls.Add(Me.gbZoom)
    Me.Controls.Add(Me.Panel1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.MinimizeBox = False
    Me.Name = "dlgPrintPreview"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Podgląd wydruku"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.Panel1.ResumeLayout(False)
    Me.gbZoom.ResumeLayout(False)
    Me.gbZoom.PerformLayout()
    CType(Me.nudZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tbZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents pvWydruk As System.Windows.Forms.PrintPreviewControl
  Friend WithEvents gbZoom As System.Windows.Forms.GroupBox
  Friend WithEvents nudZoom As System.Windows.Forms.NumericUpDown
  Friend WithEvents tbZoom As System.Windows.Forms.TrackBar
  Friend WithEvents cmdPageSetup As System.Windows.Forms.Button
  Friend WithEvents cmdFontSetup As System.Windows.Forms.Button

End Class
