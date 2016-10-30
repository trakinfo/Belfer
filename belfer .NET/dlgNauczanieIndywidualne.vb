Imports System.Windows.Forms

Public Class dlgNauczanieIndywidualne

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
  Public Sub ListViewConfig(ByVal lv As ListView)
    With lv
      .View = View.Details
      '.Enabled = True
      .FullRowSelect = True
      .GridLines = True
      .MultiSelect = False
      .AllowColumnReorder = False
      .AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
      .HideSelection = False
      .Columns.Add("ID", 0, HorizontalAlignment.Center)
      .Columns.Add("Nazwisko i imię", 303, HorizontalAlignment.Left)
      .Items.Clear()
      .Enabled = False
    End With
  End Sub

  Private Overloads Sub GetData()
    Dim R As MySqlDataReader = Nothing, DBA As New DataBaseAction, O As New ObsadaSQL
    Try
      lvStudent.Items.Clear()
      R = DBA.GetReader(O.SelectStudentAllocation(CType(cbKlasa.SelectedItem, CbItem).ID.ToString, My.Settings.SchoolYear))
      While R.Read
        Dim NewItem As New ListViewItem(R.Item("ID").ToString)
        NewItem.UseItemStyleForSubItems = True
        NewItem.SubItems.Add(R.Item("Student").ToString)
        lvStudent.Items.Add(NewItem)
      End While
      lvStudent.Columns(1).Width = CInt(IIf(lvStudent.Items.Count > 13, 284, 303))
      lvStudent.Enabled = CBool(IIf(lvStudent.Items.Count > 0, True, False))

    Catch ex As MySqlException
      MessageBox.Show(ex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try
  End Sub
  Public Sub FillKlasa()
    cbKlasa.Items.Clear()
    Dim FCB As New FillComboBox, O As New ObsadaSQL
    FCB.AddComboBoxComplexItems(cbKlasa, O.SelectClasses(My.Settings.IdSchool, My.Settings.SchoolYear, "0"))
    cbKlasa.Enabled = CType(IIf(cbKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  Private Sub cbKlasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKlasa.SelectedIndexChanged
    GetData()
    'OK_Button.Enabled = True
  End Sub

  Private Sub lvStudent_DoubleClick(sender As Object, e As EventArgs) Handles lvStudent.DoubleClick
    OK_Button_Click(sender, e)
  End Sub

  Private Sub lvStudent_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvStudent.ItemSelectionChanged
    OK_Button.Enabled = If(e.IsSelected, True, False)
  End Sub

End Class
