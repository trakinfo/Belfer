<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBlokada
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBlokada))
    Me.gbZakresOperacji = New System.Windows.Forms.GroupBox()
    Me.rbRokSzkolny = New System.Windows.Forms.RadioButton()
    Me.rbSemestr = New System.Windows.Forms.RadioButton()
    Me.rbAllType = New System.Windows.Forms.RadioButton()
    Me.gbTypOperacji = New System.Windows.Forms.GroupBox()
    Me.rbUnlock = New System.Windows.Forms.RadioButton()
    Me.rbLock = New System.Windows.Forms.RadioButton()
    Me.txtOutcome = New System.Windows.Forms.TextBox()
    Me.cmdExecute = New System.Windows.Forms.Button()
    Me.cmdClose = New System.Windows.Forms.Button()
    Me.gbZakresOperacji.SuspendLayout()
    Me.gbTypOperacji.SuspendLayout()
    Me.SuspendLayout()
    '
    'gbZakresOperacji
    '
    Me.gbZakresOperacji.Controls.Add(Me.rbRokSzkolny)
    Me.gbZakresOperacji.Controls.Add(Me.rbSemestr)
    Me.gbZakresOperacji.Controls.Add(Me.rbAllType)
    Me.gbZakresOperacji.Location = New System.Drawing.Point(12, 12)
    Me.gbZakresOperacji.Name = "gbZakresOperacji"
    Me.gbZakresOperacji.Size = New System.Drawing.Size(250, 43)
    Me.gbZakresOperacji.TabIndex = 225
    Me.gbZakresOperacji.TabStop = False
    Me.gbZakresOperacji.Text = "Zakres operacji"
    '
    'rbRokSzkolny
    '
    Me.rbRokSzkolny.AutoSize = True
    Me.rbRokSzkolny.Location = New System.Drawing.Point(81, 19)
    Me.rbRokSzkolny.Name = "rbRokSzkolny"
    Me.rbRokSzkolny.Size = New System.Drawing.Size(72, 17)
    Me.rbRokSzkolny.TabIndex = 2
    Me.rbRokSzkolny.Tag = "'C2','R'"
    Me.rbRokSzkolny.Text = "Semestr II"
    Me.rbRokSzkolny.UseVisualStyleBackColor = True
    '
    'rbSemestr
    '
    Me.rbSemestr.AutoSize = True
    Me.rbSemestr.Location = New System.Drawing.Point(6, 19)
    Me.rbSemestr.Name = "rbSemestr"
    Me.rbSemestr.Size = New System.Drawing.Size(69, 17)
    Me.rbSemestr.TabIndex = 1
    Me.rbSemestr.Tag = "'C1','S'"
    Me.rbSemestr.Text = "Semestr I"
    Me.rbSemestr.UseVisualStyleBackColor = True
    '
    'rbAllType
    '
    Me.rbAllType.AutoSize = True
    Me.rbAllType.Location = New System.Drawing.Point(159, 19)
    Me.rbAllType.Name = "rbAllType"
    Me.rbAllType.Size = New System.Drawing.Size(83, 17)
    Me.rbAllType.TabIndex = 0
    Me.rbAllType.Tag = "'C1','S','C2','R'"
    Me.rbAllType.Text = "Rok szkolny"
    Me.rbAllType.UseVisualStyleBackColor = True
    '
    'gbTypOperacji
    '
    Me.gbTypOperacji.Controls.Add(Me.rbUnlock)
    Me.gbTypOperacji.Controls.Add(Me.rbLock)
    Me.gbTypOperacji.Location = New System.Drawing.Point(268, 12)
    Me.gbTypOperacji.Name = "gbTypOperacji"
    Me.gbTypOperacji.Size = New System.Drawing.Size(205, 43)
    Me.gbTypOperacji.TabIndex = 226
    Me.gbTypOperacji.TabStop = False
    Me.gbTypOperacji.Text = "Typ operacji"
    '
    'rbUnlock
    '
    Me.rbUnlock.AutoSize = True
    Me.rbUnlock.Location = New System.Drawing.Point(104, 19)
    Me.rbUnlock.Name = "rbUnlock"
    Me.rbUnlock.Size = New System.Drawing.Size(93, 17)
    Me.rbUnlock.TabIndex = 1
    Me.rbUnlock.Tag = "0"
    Me.rbUnlock.Text = "Odblokowanie"
    Me.rbUnlock.UseVisualStyleBackColor = True
    '
    'rbLock
    '
    Me.rbLock.AutoSize = True
    Me.rbLock.Checked = True
    Me.rbLock.Location = New System.Drawing.Point(6, 19)
    Me.rbLock.Name = "rbLock"
    Me.rbLock.Size = New System.Drawing.Size(92, 17)
    Me.rbLock.TabIndex = 0
    Me.rbLock.TabStop = True
    Me.rbLock.Tag = "1"
    Me.rbLock.Text = "Zablokowanie"
    Me.rbLock.UseVisualStyleBackColor = True
    '
    'txtOutcome
    '
    Me.txtOutcome.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.txtOutcome.Location = New System.Drawing.Point(12, 61)
    Me.txtOutcome.Multiline = True
    Me.txtOutcome.Name = "txtOutcome"
    Me.txtOutcome.ReadOnly = True
    Me.txtOutcome.Size = New System.Drawing.Size(461, 188)
    Me.txtOutcome.TabIndex = 227
    '
    'cmdExecute
    '
    Me.cmdExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdExecute.Image = CType(resources.GetObject("cmdExecute.Image"), System.Drawing.Image)
    Me.cmdExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdExecute.Location = New System.Drawing.Point(483, 61)
    Me.cmdExecute.Name = "cmdExecute"
    Me.cmdExecute.Size = New System.Drawing.Size(117, 36)
    Me.cmdExecute.TabIndex = 229
    Me.cmdExecute.Text = "Wykonaj"
    Me.cmdExecute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdExecute.UseVisualStyleBackColor = True
    '
    'cmdClose
    '
    Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClose.Image = Global.belfer.NET.My.Resources.Resources.close
    Me.cmdClose.Location = New System.Drawing.Point(483, 214)
    Me.cmdClose.Name = "cmdClose"
    Me.cmdClose.Size = New System.Drawing.Size(117, 35)
    Me.cmdClose.TabIndex = 228
    Me.cmdClose.Text = "&Zamknij"
    Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    Me.cmdClose.UseVisualStyleBackColor = True
    '
    'frmBlokada
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(612, 261)
    Me.Controls.Add(Me.cmdExecute)
    Me.Controls.Add(Me.cmdClose)
    Me.Controls.Add(Me.txtOutcome)
    Me.Controls.Add(Me.gbTypOperacji)
    Me.Controls.Add(Me.gbZakresOperacji)
    Me.Name = "frmBlokada"
    Me.Text = "Blokowanie kolumn wynikowych"
    Me.gbZakresOperacji.ResumeLayout(False)
    Me.gbZakresOperacji.PerformLayout()
    Me.gbTypOperacji.ResumeLayout(False)
    Me.gbTypOperacji.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents gbZakresOperacji As System.Windows.Forms.GroupBox
  Friend WithEvents rbRokSzkolny As System.Windows.Forms.RadioButton
  Friend WithEvents rbSemestr As System.Windows.Forms.RadioButton
  Friend WithEvents rbAllType As System.Windows.Forms.RadioButton
  Friend WithEvents gbTypOperacji As System.Windows.Forms.GroupBox
  Friend WithEvents rbUnlock As System.Windows.Forms.RadioButton
  Friend WithEvents rbLock As System.Windows.Forms.RadioButton
  Friend WithEvents txtOutcome As System.Windows.Forms.TextBox
  Friend WithEvents cmdExecute As System.Windows.Forms.Button
  Friend WithEvents cmdClose As System.Windows.Forms.Button
End Class
