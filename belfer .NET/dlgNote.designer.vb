Partial Public Class dlgNote
  Inherits System.Windows.Forms.Form

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.cbTyp = New System.Windows.Forms.ComboBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.txtAutor = New System.Windows.Forms.TextBox()
    Me.txtUwaga = New System.Windows.Forms.TextBox()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.dtData = New System.Windows.Forms.DateTimePicker()
    Me.cmdSave = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'cbTyp
    '
    Me.cbTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbTyp.FormattingEnabled = True
    Me.cbTyp.Items.AddRange(New Object() {"n", "p"})
    Me.cbTyp.Location = New System.Drawing.Point(284, 39)
    Me.cbTyp.Name = "cbTyp"
    Me.cbTyp.Size = New System.Drawing.Size(75, 21)
    Me.cbTyp.TabIndex = 3
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(254, 42)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(25, 13)
    Me.Label5.TabIndex = 24
    Me.Label5.Text = "Typ"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(10, 42)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(30, 13)
    Me.Label4.TabIndex = 23
    Me.Label4.Text = "Data"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(10, 15)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(32, 13)
    Me.Label3.TabIndex = 22
    Me.Label3.Text = "Autor"
    '
    'txtAutor
    '
    Me.txtAutor.Location = New System.Drawing.Point(48, 12)
    Me.txtAutor.Name = "txtAutor"
    Me.txtAutor.Size = New System.Drawing.Size(311, 20)
    Me.txtAutor.TabIndex = 0
    '
    'txtUwaga
    '
    Me.txtUwaga.Location = New System.Drawing.Point(10, 66)
    Me.txtUwaga.Multiline = True
    Me.txtUwaga.Name = "txtUwaga"
    Me.txtUwaga.Size = New System.Drawing.Size(349, 149)
    Me.txtUwaga.TabIndex = 1
    '
    'cmdClose
    '
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.cmdClose.Location = New System.Drawing.Point(365, 178)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(75, 36)
    Me.cmdClose.TabIndex = 6
    Me.cmdClose.Text = "Zamknij"
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'dtData
    '
    Me.dtData.Location = New System.Drawing.Point(48, 39)
    Me.dtData.Name = "dtData"
    Me.dtData.Size = New System.Drawing.Size(171, 20)
    Me.dtData.TabIndex = 2
    '
    'cmdSave
    '
    Me.cmdSave.Enabled = False
    Me.cmdSave.Image = Global.belfer.NET.My.Resources.Resources.save_24
    Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.cmdSave.Location = New System.Drawing.Point(365, 136)
    Me.cmdSave.Name = "cmdSave"
    Me.cmdSave.Size = New System.Drawing.Size(75, 36)
    Me.cmdSave.TabIndex = 4
    Me.cmdSave.Text = "&Zapisz"
    Me.cmdSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '
    'dlgNote
    '
    Me.ClientSize = New System.Drawing.Size(450, 226)
    Me.Controls.Add(Me.cmdSave)
    Me.Controls.Add(Me.dtData)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.cbTyp)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtAutor)
    Me.Controls.Add(Me.txtUwaga)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgNote"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "frmAddEditNote"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cbTyp As System.Windows.Forms.ComboBox
  Friend WithEvents Label5 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents txtAutor As System.Windows.Forms.TextBox
  Friend WithEvents txtUwaga As System.Windows.Forms.TextBox
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents dtData As System.Windows.Forms.DateTimePicker
  Friend WithEvents cmdSave As System.Windows.Forms.Button
End Class
