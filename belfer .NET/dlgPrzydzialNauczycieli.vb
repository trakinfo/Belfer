Imports System.Windows.Forms

Public Class dlgPrzydzialNauczycieli

  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  'Public IdSzkola As String
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

    If lvNauczyciel.SelectedItems.Count > 0 Then
      AddNew(lvNauczyciel, e)
      OK_Button.Enabled = False
    End If

  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Dispose(True)
  End Sub

  Private Sub dlgPrzydzialKlas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'AddHandler SharedPrzydzialNauczycieli.OnOwnerClose, AddressOf Cancel_Button_Click
    GetData()
  End Sub
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim DBA As New DataBaseAction, PN As New PrzydzialNauczycieliSQL
    'Dim cmd As MySqlCommand = DBA.CreateCommand(K.InsertKlasa(txtKlasa.Text.Trim))
    Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
    'cmd.Transaction = MySQLTrans
    'Dim LastItem As String
    'LastItem = lvNauczyciel.SelectedItems(lvNauczyciel.SelectedItems.Count - 1).Text
    Try
      For Each item As ListViewItem In lvNauczyciel.SelectedItems
        Dim cmd As MySqlCommand = DBA.CreateCommand(PN.InsertSchoolTeacher)
        cmd.Transaction = MySQLTrans

        cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
        cmd.Parameters.AddWithValue("?IdNauczyciel", item.Text)
        cmd.ExecuteNonQuery()

        item.Remove()
      Next
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

  Private Sub lvKlasa_DoubleClick(sender As Object, e As EventArgs) Handles lvNauczyciel.DoubleClick
    AddNew(lvNauczyciel, e)
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvNauczyciel.ItemSelectionChanged
    OK_Button.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)
  End Sub
  'Public Sub SchoolChanged(School As String)
  '  IdSzkola = School
  '  GetData()
  'End Sub
  Public Sub GetData()
    lvNauczyciel.Items.Clear()
    Dim DBA As New DataBaseAction, PN As New PrzydzialNauczycieliSQL, DT As DataTable
    DT = DBA.GetDataTable(PN.SelectTeachers(My.Settings.IdSchool))
    For Each row As DataRow In DT.Rows
      lvNauczyciel.Items.Add(row.Item(0).ToString)
      lvNauczyciel.Items(lvNauczyciel.Items.Count - 1).SubItems.Add(row.Item(1).ToString)
    Next
    'lvNauczyciel.Columns(1).Width = CInt(IIf(lvNauczyciel.Items.Count > 18, 321, 340))
    lvNauczyciel.Enabled = CType(IIf(lvNauczyciel.Items.Count > 0, True, False), Boolean)

  End Sub
End Class
