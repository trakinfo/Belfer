Imports System.Windows.Forms

Public Class dlgPrzydzialPrzedmiotow
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  'Public IdSzkola As String
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

    If lvPrzedmiot.SelectedItems.Count > 0 Then
      AddNew(lvPrzedmiot, e)
      OK_Button.Enabled = False
    End If

  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    'Me.Dispose(True)
  End Sub

  Private Sub dlgPrzydzialKlas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'AddHandler SharedPrzydzialPrzedmiotow.OnOwnerClose, AddressOf Cancel_Button_Click
    GetData()
  End Sub
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL
    Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
    'Dim LastItem As String
    'LastItem = lvPrzedmiot.SelectedItems(lvPrzedmiot.SelectedItems.Count - 1).Text
    Try
      If chkGrupa.Checked Then
        For Each item As ListViewItem In lvPrzedmiot.SelectedItems
          For i As Integer = 1 To CType(nudGrupa.Value, Integer)
            Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPrzydzialPrzedmiot)
            cmd.Transaction = MySQLTrans
            cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
            cmd.Parameters.AddWithValue("?IdPrzedmiot", item.Text)
            cmd.Parameters.AddWithValue("?Grupa", i)
            cmd.Parameters.AddWithValue("?Priorytet", item.SubItems(2).Text)
            cmd.ExecuteNonQuery()
          Next
          item.Remove()
        Next
      Else
        For Each item As ListViewItem In lvPrzedmiot.SelectedItems
          Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPrzydzialPrzedmiot)
          cmd.Transaction = MySQLTrans
          cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
          cmd.Parameters.AddWithValue("?IdPrzedmiot", item.Text)
          cmd.Parameters.AddWithValue("?Grupa", 0)
          cmd.Parameters.AddWithValue("?Priorytet", item.SubItems(2).Text)
          cmd.ExecuteNonQuery()
          item.Remove()
        Next
      End If

      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, DBA.GetLastInsertedID)


    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wartość wprowadzona w polu '" & CType(sender, TextBox).Name & "' już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
    Catch ex As Exception
      'MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)

    End Try
  End Sub

  Private Sub lvKlasa_DoubleClick(sender As Object, e As EventArgs) Handles lvPrzedmiot.DoubleClick
    AddNew(lvPrzedmiot, e)
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvPrzedmiot.ItemSelectionChanged
    OK_Button.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)
    chkGrupa.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)
  End Sub
  'Public Sub SchoolChanged(School As String)
  '  IdSzkola = School
  '  GetData()
  'End Sub
  Public Sub GetData()
    lvPrzedmiot.Items.Clear()
    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL, DT As DataTable
    DT = DBA.GetDataTable(P.SelectPrzedmiotAlias(My.Settings.IdSchool))
    For Each row As DataRow In DT.Rows
      lvPrzedmiot.Items.Add(row.Item(0).ToString)
      lvPrzedmiot.Items(lvPrzedmiot.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      lvPrzedmiot.Items(lvPrzedmiot.Items.Count - 1).SubItems.Add(row.Item(2).ToString)
    Next
    'lvPrzedmiot.Columns(1).Width = CInt(IIf(lvPrzedmiot.Items.Count > 18, 281, 300))
    lvPrzedmiot.Enabled = CType(IIf(lvPrzedmiot.Items.Count > 0, True, False), Boolean)
  End Sub

  Private Sub chkGrupa_CheckedChanged(sender As Object, e As EventArgs) Handles chkGrupa.CheckedChanged
    nudGrupa.Enabled = chkGrupa.Checked
  End Sub
End Class
