Imports System.Windows.Forms
Imports System.Xml
Public Class dlgImportStudent
  Private xmlDoc As XmlDocument
  Private Sub frmPlanLekcji_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    MainForm.ImportToolStripMenuItem.Enabled = True

  End Sub
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    Dim Import As New ImportStudent
    SharedImport.TotalValue = CType(lblTotal.Text, Integer)
    Import.ReadXmlData(xmlDoc, dtDataAktywacji.Value)
    If SharedImport.ErrorValue > 0 Then
      lblInfo.Text = "Ups!, Wystąpiły błędy importu. Rekordy odrzucone: " & SharedImport.ErrorValue.ToString
      lblInfo.ForeColor = Color.Red
    Else
      lblInfo.Text = "Uff, Import zakończony powodzeniem"
      'SharedImport.InfoValue = "Uff, Import zakończony powodzeniem"
      lblInfo.ForeColor = Color.Green
    End If
    lblInfo.Font = New Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point)
    'Me.Close()
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub dlgImportStudent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    AddHandler SharedImport.OnRecordForward, AddressOf RefreshMe
    AddHandler SharedImport.OnpbMaxValueChange, AddressOf pbMaxValue_Change
    AddHandler SharedImport.OnRoutineChange, AddressOf GetInfo
    lblTotal.Text = ""
    lblSuccess.Text = ""
    lblError.Text = ""
    lblInfo.Text = ""
  End Sub
  Private Sub RefreshMe()
    Me.pbImport.Value = SharedImport.pbValue
    Me.lblSuccess.Text = SharedImport.SuccessValue.ToString
    Me.lblError.Text = SharedImport.ErrorValue.ToString
    GetExtendedInfo()
    Me.Refresh()
  End Sub
  Private Sub pbMaxValue_Change()
    pbImport.Value = 0
    Me.pbImport.Maximum = SharedImport.pbMaxValue
    lblInfo.Text = SharedImport.InfoValue
    'lblTotal.Text = SharedImport.TotalValue.ToString
  End Sub
  Private Sub GetInfo()
    lblInfo.Text = SharedImport.InfoValue
    'txtDetails.Text += SharedExport.InfoValue & vbNewLine
    GetExtendedInfo()
  End Sub
  Private Sub GetExtendedInfo()
    'lblInfo.Text = SharedExport.InfoValue
    txtDetails.Text += SharedImport.ExtendedInfoValue & vbNewLine
  End Sub

  Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
    Dim dlgOpen As New OpenFileDialog
    With dlgOpen
      '.InitialDirectory = Application.StartupPath
      .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
      .Multiselect = False
      .Filter = "Wszystkie pliki (*.*)|*.*|Pliki xml (*.xml)|*.xml"
      If .ShowDialog() = Windows.Forms.DialogResult.OK Then
        txtFile.Text = .FileName
        xmlDoc = New XmlDocument
        xmlDoc.Load(.FileName)
        SharedImport.TotalValue = 0
        Dim Total As Integer
        For Each N As System.Xml.XmlNode In xmlDoc.DocumentElement.ChildNodes
          'SharedImport.TotalValue += N.ChildNodes.Count
          Total += N.ChildNodes.Count
        Next
        lblTotal.Text = Total.ToString
        OK_Button.Enabled = True
      End If
    End With
  End Sub
End Class
