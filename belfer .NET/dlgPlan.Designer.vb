<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgPlan
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
    Me.txtPlan = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
    Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.chkLock = New System.Windows.Forms.CheckBox()
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
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 117)
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
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(12, 15)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(40, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Nazwa"
    '
    'txtPlan
    '
    Me.txtPlan.Location = New System.Drawing.Point(95, 12)
    Me.txtPlan.Name = "txtPlan"
    Me.txtPlan.Size = New System.Drawing.Size(325, 20)
    Me.txtPlan.TabIndex = 2
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 44)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(77, 13)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Obowiązuje od"
    '
    'dtpStartDate
    '
    Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.dtpStartDate.Location = New System.Drawing.Point(95, 40)
    Me.dtpStartDate.Name = "dtpStartDate"
    Me.dtpStartDate.Size = New System.Drawing.Size(116, 20)
    Me.dtpStartDate.TabIndex = 4
    '
    'dtpEndDate
    '
    Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
    Me.dtpEndDate.Location = New System.Drawing.Point(304, 40)
    Me.dtpEndDate.Name = "dtpEndDate"
    Me.dtpEndDate.Size = New System.Drawing.Size(116, 20)
    Me.dtpEndDate.TabIndex = 5
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(221, 44)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(77, 13)
    Me.Label3.TabIndex = 6
    Me.Label3.Text = "Obowiązuje do"
    '
    'chkLock
    '
    Me.chkLock.AutoSize = True
    Me.chkLock.Location = New System.Drawing.Point(12, 76)
    Me.chkLock.Name = "chkLock"
    Me.chkLock.Size = New System.Drawing.Size(197, 17)
    Me.chkLock.TabIndex = 7
    Me.chkLock.Text = "Wyłącz możliwość edycji zawartości"
    Me.chkLock.UseVisualStyleBackColor = True
    '
    'dlgPlan
    '
    Me.AcceptButton = Me.OK_Button
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.Cancel_Button
    Me.ClientSize = New System.Drawing.Size(435, 158)
    Me.Controls.Add(Me.chkLock)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.dtpEndDate)
    Me.Controls.Add(Me.dtpStartDate)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtPlan)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgPlan"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "Plan lekcji"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
  Friend WithEvents Cancel_Button As System.Windows.Forms.Button
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents txtPlan As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents chkLock As System.Windows.Forms.CheckBox

End Class
