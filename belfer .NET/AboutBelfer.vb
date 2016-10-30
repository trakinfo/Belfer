Public NotInheritable Class AboutBelfer

  Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' Set the title of the form.
    Dim ApplicationTitle As String
    If My.Application.Info.Title <> "" Then
      ApplicationTitle = My.Application.Info.Title
    Else
      ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
    End If
    Me.Text = String.Format("O programie {0}", ApplicationTitle)
    ' Initialize all of the text displayed on the About Box.
    ' TODO: Customize the application's assembly information in the "Application" pane of the project 
    '    properties dialog (under the "Project" menu).
    Me.LabelProductName.Text = My.Application.Info.ProductName
    Me.LabelVersion.Text = String.Format("Wersja {0}", My.Application.Info.Version.ToString)
    Me.LabelCopyright.Text = My.Application.Info.Copyright
    Me.LabelCompanyName.Text = "Autor programu: " & My.Application.Info.CompanyName
    Me.TextBoxDescription.Text = My.Application.Info.Description
    lblEmail.Text = "pracownia.komputerowa@gimsusz.pl"
    lblWebLink.Text = "http://trakinfo.idsl.pl"
  End Sub

  Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
    Me.Close()
  End Sub

  Private Sub lblWebLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblWebLink.LinkClicked
    System.Diagnostics.Process.Start(lblWebLink.Text)
  End Sub

  Private Sub lblEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblEmail.LinkClicked
    System.Diagnostics.Process.Start("mailto:" & lblEmail.Text)
  End Sub
End Class
