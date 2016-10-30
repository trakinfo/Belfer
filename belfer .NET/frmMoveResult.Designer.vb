<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMoveResult
    Inherits System.Windows.Forms.Form

    'Formularz zastępuje metodę dispose, aby wyczyścić listę składników.
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

    'Wymagane przez Projektanta formularzy systemu Windows
    Private components As System.ComponentModel.IContainer

    'UWAGA: Następująca procedura jest wymagana przez Projektanta formularzy systemu Windows
    'Można to modyfikować, używając Projektanta formularzy systemu Windows.  
    'Nie należy modyfikować za pomocą edytora kodu.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.chkVirtual = New System.Windows.Forms.CheckBox()
    Me.cbPrzedmiot = New System.Windows.Forms.ComboBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.cbKlasa = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.chkPrzedmiot = New System.Windows.Forms.CheckBox()
    Me.cbStudent = New System.Windows.Forms.ComboBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.gpbTarget = New System.Windows.Forms.GroupBox()
    Me.txtPrzedmiot = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.chkTargetVirtual = New System.Windows.Forms.CheckBox()
    Me.cbTargetClass = New System.Windows.Forms.ComboBox()
    Me.pbMoveResult = New System.Windows.Forms.ProgressBar()
    Me.cmdExecute = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.GroupBox1.SuspendLayout()
    Me.gpbTarget.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkVirtual
    '
    Me.chkVirtual.AutoSize = True
    Me.chkVirtual.Location = New System.Drawing.Point(311, 21)
    Me.chkVirtual.Name = "chkVirtual"
    Me.chkVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkVirtual.TabIndex = 207
    Me.chkVirtual.Text = "Klasa wirtualna"
    Me.chkVirtual.UseVisualStyleBackColor = True
    '
    'cbPrzedmiot
    '
    Me.cbPrzedmiot.DropDownHeight = 500
    Me.cbPrzedmiot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbPrzedmiot.Enabled = False
    Me.cbPrzedmiot.FormattingEnabled = True
    Me.cbPrzedmiot.IntegralHeight = False
    Me.cbPrzedmiot.Location = New System.Drawing.Point(65, 53)
    Me.cbPrzedmiot.Name = "cbPrzedmiot"
    Me.cbPrzedmiot.Size = New System.Drawing.Size(339, 21)
    Me.cbPrzedmiot.TabIndex = 204
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(6, 56)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(53, 13)
    Me.Label4.TabIndex = 203
    Me.Label4.Text = "Przedmiot"
    '
    'cbKlasa
    '
    Me.cbKlasa.DropDownHeight = 500
    Me.cbKlasa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbKlasa.Enabled = False
    Me.cbKlasa.FormattingEnabled = True
    Me.cbKlasa.IntegralHeight = False
    Me.cbKlasa.Location = New System.Drawing.Point(65, 19)
    Me.cbKlasa.Name = "cbKlasa"
    Me.cbKlasa.Size = New System.Drawing.Size(240, 21)
    Me.cbKlasa.TabIndex = 202
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(6, 22)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(33, 13)
    Me.Label2.TabIndex = 201
    Me.Label2.Text = "Klasa"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.chkPrzedmiot)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.chkVirtual)
    Me.GroupBox1.Controls.Add(Me.cbKlasa)
    Me.GroupBox1.Controls.Add(Me.cbStudent)
    Me.GroupBox1.Controls.Add(Me.cbPrzedmiot)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(816, 89)
    Me.GroupBox1.TabIndex = 208
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Źródło"
    '
    'chkPrzedmiot
    '
    Me.chkPrzedmiot.AutoSize = True
    Me.chkPrzedmiot.Location = New System.Drawing.Point(410, 55)
    Me.chkPrzedmiot.Name = "chkPrzedmiot"
    Me.chkPrzedmiot.Size = New System.Drawing.Size(127, 17)
    Me.chkPrzedmiot.TabIndex = 208
    Me.chkPrzedmiot.Text = "Wszystkie przedmioty"
    Me.chkPrzedmiot.UseVisualStyleBackColor = True
    '
    'cbStudent
    '
    Me.cbStudent.DropDownHeight = 500
    Me.cbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbStudent.Enabled = False
    Me.cbStudent.FormattingEnabled = True
    Me.cbStudent.IntegralHeight = False
    Me.cbStudent.Location = New System.Drawing.Point(458, 19)
    Me.cbStudent.Name = "cbStudent"
    Me.cbStudent.Size = New System.Drawing.Size(352, 21)
    Me.cbStudent.TabIndex = 204
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(414, 23)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(38, 13)
    Me.Label1.TabIndex = 203
    Me.Label1.Text = "Uczeń"
    '
    'gpbTarget
    '
    Me.gpbTarget.Controls.Add(Me.txtPrzedmiot)
    Me.gpbTarget.Controls.Add(Me.Label3)
    Me.gpbTarget.Controls.Add(Me.chkTargetVirtual)
    Me.gpbTarget.Controls.Add(Me.cbTargetClass)
    Me.gpbTarget.Location = New System.Drawing.Point(12, 124)
    Me.gpbTarget.Name = "gpbTarget"
    Me.gpbTarget.Size = New System.Drawing.Size(816, 58)
    Me.gpbTarget.TabIndex = 209
    Me.gpbTarget.TabStop = False
    Me.gpbTarget.Text = "Cel"
    '
    'txtPrzedmiot
    '
    Me.txtPrzedmiot.Enabled = False
    Me.txtPrzedmiot.Location = New System.Drawing.Point(414, 19)
    Me.txtPrzedmiot.Name = "txtPrzedmiot"
    Me.txtPrzedmiot.Size = New System.Drawing.Size(396, 20)
    Me.txtPrzedmiot.TabIndex = 217
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(6, 22)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(33, 13)
    Me.Label3.TabIndex = 208
    Me.Label3.Text = "Klasa"
    '
    'chkTargetVirtual
    '
    Me.chkTargetVirtual.AutoSize = True
    Me.chkTargetVirtual.Enabled = False
    Me.chkTargetVirtual.Location = New System.Drawing.Point(311, 21)
    Me.chkTargetVirtual.Name = "chkTargetVirtual"
    Me.chkTargetVirtual.Size = New System.Drawing.Size(97, 17)
    Me.chkTargetVirtual.TabIndex = 216
    Me.chkTargetVirtual.Text = "Klasa wirtualna"
    Me.chkTargetVirtual.UseVisualStyleBackColor = True
    '
    'cbTargetClass
    '
    Me.cbTargetClass.DropDownHeight = 500
    Me.cbTargetClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cbTargetClass.Enabled = False
    Me.cbTargetClass.FormattingEnabled = True
    Me.cbTargetClass.IntegralHeight = False
    Me.cbTargetClass.Location = New System.Drawing.Point(65, 19)
    Me.cbTargetClass.Name = "cbTargetClass"
    Me.cbTargetClass.Size = New System.Drawing.Size(240, 21)
    Me.cbTargetClass.TabIndex = 209
    '
    'pbMoveResult
    '
    Me.pbMoveResult.Location = New System.Drawing.Point(12, 188)
    Me.pbMoveResult.Name = "pbMoveResult"
    Me.pbMoveResult.Size = New System.Drawing.Size(816, 23)
    Me.pbMoveResult.TabIndex = 211
    '
    'cmdExecute
    '
    Me.cmdExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdExecute.Image = Global.belfer.NET.My.Resources.Resources.Execute22
    Me.cmdExecute.Location = New System.Drawing.Point(584, 242)
    Me.cmdExecute.Name = "cmdExecute"
    Me.cmdExecute.Size = New System.Drawing.Size(119, 35)
    Me.cmdExecute.TabIndex = 212
    Me.cmdExecute.Text = "Wykonaj"
    Me.cmdExecute.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdExecute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdExecute.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(709, 242)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(119, 35)
    Me.cmdClose.TabIndex = 210
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'frmMoveResult
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(840, 289)
    Me.Controls.Add(Me.cmdExecute)
    Me.Controls.Add(Me.pbMoveResult)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.gpbTarget)
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "frmMoveResult"
    Me.Text = "Przepinanie wyników nauczania"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.gpbTarget.ResumeLayout(False)
    Me.gpbTarget.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents chkVirtual As System.Windows.Forms.CheckBox
  Friend WithEvents cbPrzedmiot As System.Windows.Forms.ComboBox
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents cbKlasa As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents cbStudent As System.Windows.Forms.ComboBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents gpbTarget As System.Windows.Forms.GroupBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents chkTargetVirtual As System.Windows.Forms.CheckBox
  Friend WithEvents cbTargetClass As System.Windows.Forms.ComboBox
  Friend WithEvents cmdClose As System.Windows.Forms.Button
  Friend WithEvents pbMoveResult As System.Windows.Forms.ProgressBar
  Friend WithEvents chkPrzedmiot As System.Windows.Forms.CheckBox
  Friend WithEvents cmdExecute As System.Windows.Forms.Button
  Friend WithEvents txtPrzedmiot As System.Windows.Forms.TextBox
End Class
