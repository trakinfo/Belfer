<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgHarmonogram
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
    Me.dtStartTime = New System.Windows.Forms.DateTimePicker()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dtEndTime = New System.Windows.Forms.DateTimePicker()
    Me.nudNrLekcji = New System.Windows.Forms.NumericUpDown()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.nudNrLekcji, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(37, 125)
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
    'dtStartTime
    '
    Me.dtStartTime.CustomFormat = "HH:mm"
    Me.dtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtStartTime.Location = New System.Drawing.Point(104, 38)
    Me.dtStartTime.Name = "dtStartTime"
    Me.dtStartTime.ShowUpDown = True
    Me.dtStartTime.Size = New System.Drawing.Size(79, 20)
    Me.dtStartTime.TabIndex = 2
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(8, 44)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(90, 13)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "Czas rozpoczęcia"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(8, 70)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(93, 13)
    Me.Label2.TabIndex = 4
    Me.Label2.Text = "Czas zakończenia"
    '
    'dtEndTime
    '
    Me.dtEndTime.CustomFormat = "HH:mm"
    Me.dtEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
    Me.dtEndTime.Location = New System.Drawing.Point(104, 64)
    Me.dtEndTime.Name = "dtEndTime"
    Me.dtEndTime.ShowUpDown = True
    Me.dtEndTime.Size = New System.Drawing.Size(79, 20)
    Me.dtEndTime.TabIndex = 5
    '
    'nudNrLekcji
    '
    Me.nudNrLekcji.Location = New System.Drawing.Point(104, 12)
    Me.nudNrLekcji.Maximum = New Decimal(New Integer() {20, 0, 0, 0})
    Me.nudNrLekcji.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudNrLekcji.Name = "nudNrLekcji"
    Me.nudNrLekcji.Size = New System.Drawing.Size(76, 20)
    Me.nudNrLekcji.TabIndex = 6
    Me.nudNrLekcji.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(12, 14)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(45, 13)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Nr lekcji"
    '
    'dlgHarmonogram
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(195, 166)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.nudNrLekcji)
    Me.Controls.Add(Me.dtEndTime)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.dtStartTime)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgHarmonogram"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Nowa jednostka lekcyjna"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.nudNrLekcji, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents dtStartTime As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents dtEndTime As System.Windows.Forms.DateTimePicker
  Friend WithEvents nudNrLekcji As System.Windows.Forms.NumericUpDown
  Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
