Public Class dlgNote
  Public Event NewAdded(ByVal InsertedID As String)
  Public InRefresh As Boolean, IdUczen As List(Of Long)
  Public IsNewMode As Boolean
  Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
    If IsNewMode Then AddNew()
    'If AddNew() Then cmdSave.Enabled = False
    'Else
    Me.DialogResult = Windows.Forms.DialogResult.OK
    Me.Close()
    'End If
  End Sub
  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Function AddNew() As Boolean
    AddNew = False
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, U As New UwagiSQL, LastInsertID As Long = 0
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    Try
      For Each ID As String In CType(IdUczen, List(Of Long))
        Dim cmd As MySqlCommand = DBA.CreateCommand(U.InsertNote)
        cmd.Transaction = MySQLTrans
        cmd.Parameters.AddWithValue("?IdUczen", ID)
        cmd.Parameters.AddWithValue("?TrescUwagi", txtUwaga.Text.Trim)
        cmd.Parameters.AddWithValue("?TypUwagi", cbTyp.Text)
        cmd.Parameters.AddWithValue("?Autor", txtAutor.Text.Trim)
        cmd.Parameters.AddWithValue("?Data", dtData.Value.ToString("yyyy-MM-dd"))
        cmd.ExecuteNonQuery()
        LastInsertID = cmd.LastInsertedId
      Next
      MySQLTrans.Commit()
      RaiseEvent NewAdded(LastInsertID.ToString)
      Return True
    Catch myex As MySqlException
      MySQLTrans.Rollback()
      MessageBox.Show(myex.Message & vbCr & myex.Number)
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      'Return False
    End Try
  End Function

  Private Sub txtUwaga_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUwaga.TextChanged, txtAutor.TextChanged, cbTyp.SelectedIndexChanged, dtData.ValueChanged
    If InRefresh Then Exit Sub
    Me.cmdSave.Enabled = CBool(IIf(Me.txtUwaga.Text.Length > 0 AndAlso txtAutor.Text.Length > 0, True, False))
  End Sub
End Class