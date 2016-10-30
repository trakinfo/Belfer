<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgWait
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
    Me.Panel1 = New System.Windows.Forms.Panel()
    Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.lblInfo = New System.Windows.Forms.Label()
    Me.Panel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Panel1
    '
    Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.Panel1.Controls.Add(Me.ProgressBar1)
    Me.Panel1.Controls.Add(Me.Label2)
    Me.Panel1.Controls.Add(Me.lblInfo)
    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Panel1.Location = New System.Drawing.Point(0, 0)
    Me.Panel1.Name = "Panel1"
    Me.Panel1.Size = New System.Drawing.Size(386, 135)
    Me.Panel1.TabIndex = 3
    '
    'ProgressBar1
    '
    Me.ProgressBar1.Location = New System.Drawing.Point(11, 85)
    Me.ProgressBar1.MarqueeAnimationSpeed = 15
    Me.ProgressBar1.Minimum = 100
    Me.ProgressBar1.Name = "ProgressBar1"
    Me.ProgressBar1.Size = New System.Drawing.Size(362, 23)
    Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
    Me.ProgressBar1.TabIndex = 3
    Me.ProgressBar1.Value = 100
    '
    'Label2
    '
    Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.Label2.Location = New System.Drawing.Point(11, 50)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(362, 23)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Proszę czekać!"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lblInfo
    '
    Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
    Me.lblInfo.Location = New System.Drawing.Point(11, 27)
    Me.lblInfo.Name = "lblInfo"
    Me.lblInfo.Size = New System.Drawing.Size(362, 23)
    Me.lblInfo.TabIndex = 2
    Me.lblInfo.Text = "Przygotowywanie wydruku ..."
    Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'dlgWait
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(386, 135)
    Me.ControlBox = False
    Me.Controls.Add(Me.Panel1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "dlgWait"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "dlgWait"
    Me.TopMost = True
    Me.Panel1.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Panel1 As System.Windows.Forms.Panel
  Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents lblInfo As System.Windows.Forms.Label

End Class
