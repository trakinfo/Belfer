Imports System.Windows.Forms

Public Class dlgGrupaPrzedmiotowa
  Public Event NewGroupMemberAdded(ByVal IdLastAdded As String)
  Public Klasa, Przedmiot As String
  'Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
  '    Me.DialogResult = System.Windows.Forms.DialogResult.OK
  '    Me.Close()
  'End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

    If lvStudent.SelectedItems.Count > 0 Then
      If AddNew() Then
        GetData()
        OK_Button.Enabled = False
      End If
    End If

  End Sub

  'Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
  '  Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
  '  Me.Dispose(True)
  'End Sub

  Private Sub dlgPrzydzialKlas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    GetData()
  End Sub
  Private Function AddNew() As Boolean

    Dim DBA As New DataBaseAction, G As New GrupaSQL
    Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
    Try
      For Each item As ListViewItem In lvStudent.SelectedItems
        Dim cmd As MySqlCommand = DBA.CreateCommand(G.InsertGroupMember)
        cmd.Transaction = MySQLTrans
        cmd.Parameters.AddWithValue("?IdSzkolaPrzedmiot", Przedmiot)
        cmd.Parameters.AddWithValue("?IdPrzydzial", item.Text)
        cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
        cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
        cmd.ExecuteNonQuery()
        item.Remove()
      Next


      MySQLTrans.Commit()
      RaiseEvent NewGroupMemberAdded(DBA.GetLastInsertedID)
      Return True

    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wybrana wartość już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
      Return False
    Catch ex As Exception
      'MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function

  Private Sub lvKlasa_DoubleClick(sender As Object, e As EventArgs) Handles lvStudent.DoubleClick
    AddNew()
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvStudent.ItemSelectionChanged
    OK_Button.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)

  End Sub

  Public Sub GetData()
    Dim R As MySqlDataReader = Nothing
    Try
      lvStudent.Items.Clear()
      Dim DBA As New DataBaseAction, G As New GrupaSQL  ', DT As DataTable
      R = DBA.GetReader(G.SelectStudent(Klasa, Przedmiot))
      While R.Read
        lvStudent.Items.Add(R.Item(0).ToString)
        lvStudent.Items(lvStudent.Items.Count - 1).SubItems.Add(R.Item(1).ToString)
      End While
      'DT = DBA.GetDataTable(G.SelectStudent(Klasa, Przedmiot))
      'For Each row As DataRow In DT.Rows
      '  lvStudent.Items.Add(row.Item(0).ToString)
      '  lvStudent.Items(lvStudent.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
      'Next
      lvStudent.Columns(1).Width = CInt(IIf(lvStudent.Items.Count > 17, 381, 400))
      lvStudent.Enabled = CType(IIf(lvStudent.Items.Count > 0, True, False), Boolean)
    Catch mex As MySqlException
      MessageBox.Show(mex.Message)
    Catch ex As Exception
      MessageBox.Show(ex.Message)
    Finally
      R.Close()
    End Try

  End Sub

End Class
