Imports System.Windows.Forms

Public Class dlgKonfiguracja

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    My.Settings.SchoolYear = String.Concat(nudStartYear.Value, "/", nudEndYear.Value)
    My.Settings.IdSchoolType = CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString
    My.Settings.IdSchool = CType(cbSzkola.SelectedItem, CbItem).ID.ToString
    My.Settings.Save()
    SharedKonfiguracja.KonfiguracjaChanged()
    Me.Close()
  End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
  Private Sub nudStartYear_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudStartYear.ValueChanged
    If Not nudStartYear.Created Then Exit Sub
    Me.nudEndYear.Value = Me.nudStartYear.Value + 1
    OK_Button.Enabled = CType(IIf(cbSzkola.SelectedItem IsNot Nothing, True, False), Boolean)
  End Sub

  Private Sub cbTypSzkoly_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTypSzkoly.SelectedIndexChanged
    Dim FCB As New FillComboBox, S As New SzkolaSQL, SH As New SeekHelper
    FCB.AddComboBoxComplexItems(Me.cbSzkola, S.SelectSchoolAlias(CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString))
    cbSzkola.Enabled = CType(IIf(cbSzkola.Items.Count > 0, True, False), Boolean)
    OK_Button.Enabled = False
  End Sub

  Private Sub cbSzkola_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSzkola.SelectedIndexChanged
    OK_Button.Enabled = True
  End Sub

End Class
