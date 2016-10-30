Imports System.Windows.Forms

Public Class dlgPrzydzialKlas
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  'Public IdSzkola As String
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

    If lvKlasa.SelectedItems.Count > 0 Then
      AddNew(lvKlasa, e)
      OK_Button.Enabled = False
    End If

  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

    Me.Close()
  End Sub

  Private Sub dlgPrzydzialKlas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'AddHandler SharedPrzydzialKlas.OnOwnerClose, AddressOf Cancel_Button_Click
    GetData()
  End Sub
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim DBA As New DataBaseAction, PK As New PrzydzialKlasSQL
    'Dim cmd As MySqlCommand = DBA.CreateCommand(K.InsertKlasa(txtKlasa.Text.Trim))
    Dim MySQLTrans As MySqlTransaction = GlobalValues.gblConn.BeginTransaction()
    'cmd.Transaction = MySQLTrans
    Dim LastItem As String
    LastItem = lvKlasa.SelectedItems(lvKlasa.SelectedItems.Count - 1).Text
    Try
      For Each item As ListViewItem In lvKlasa.SelectedItems
        Dim cmd As MySqlCommand = DBA.CreateCommand(PK.InsertSchoolClass)
        cmd.Transaction = MySQLTrans

        cmd.Parameters.AddWithValue("?IdSzkola", My.Settings.IdSchool)
        cmd.Parameters.AddWithValue("?Kod_Klasy", item.Text)
        cmd.Parameters.AddWithValue("?Nazwa_Klasy", item.Text)
        cmd.Parameters.AddWithValue("?Virtual", chkVirtual.Checked)
        cmd.ExecuteNonQuery()
        item.Remove()
      Next
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, LastItem)

    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wprowadzona wartość już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
    Catch ex As Exception
      'MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)

    End Try
  End Sub

  Private Sub lvKlasa_DoubleClick(sender As Object, e As EventArgs) Handles lvKlasa.DoubleClick
    AddNew(lvKlasa, e)
  End Sub

  Private Sub lvKlasa_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvKlasa.ItemSelectionChanged
    OK_Button.Enabled = CType(IIf(e.IsSelected, True, False), Boolean)
  End Sub
  Public Sub GetData()
    Dim DBA As New DataBaseAction, PK As New PrzydzialKlasSQL, DT As DataTable
    DT = DBA.GetDataTable(PK.SelectClasses(My.Settings.IdSchool))
    lvKlasa.Items.Clear()
    For Each row As DataRow In DT.Rows
      lvKlasa.Items.Add(row.Item(0).ToString)
      Me.lvKlasa.Items(Me.lvKlasa.Items.Count - 1).SubItems.Add(row.Item(0).ToString)
    Next

    'lvKlasa.Columns(0).Width = CInt(IIf(lvKlasa.Items.Count > 18, 120, 139))
    lvKlasa.Enabled = CType(IIf(lvKlasa.Items.Count > 0, True, False), Boolean)
  End Sub
  'Public Sub SchoolChanged(School As String)
  '  IdSzkola = School
  '  GetData()
  'End Sub
End Class
