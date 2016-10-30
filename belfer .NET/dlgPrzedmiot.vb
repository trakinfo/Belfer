Imports System.Windows.Forms

Public Class dlgPrzedmiot
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public IsNewMode As Boolean
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
    If IsNewMode Then
      If AddNew(txtAlias, e) Then
        txtAlias.Text = ""
        txtNazwa.Text = ""
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If

  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub
  Private Function AddNew(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, P As New PrzedmiotSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(P.InsertPrzedmiot)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Alias", txtAlias.Text.Trim)
      cmd.Parameters.AddWithValue("?Nazwa", txtNazwa.Text.Trim)
      cmd.Parameters.AddWithValue("?Typ", cbTyp.Text)
      cmd.ExecuteNonQuery()
      Dim InsertedID As String = cmd.LastInsertedId.ToString
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, InsertedID)
      txtAlias.Focus()
      Return True
    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wartość wprowadzona w polu '" & CType(sender, TextBox).Name & "' już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
      Return False
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function

  Private Sub dlgMiejscowosc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'If Not Me.Modal Then AddHandler SharedPrzedmiot.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtNazwa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlias.TextChanged, cbTyp.SelectionChangeCommitted
    'OK_Button.Enabled = CType(IIf(txtNazwa.Text.Trim.Length < 1, False, True), Boolean)
    If txtAlias.Text.Trim.Length > 0 Then
      Dim SH As New StringHelper
      If SH.AllowedChars(txtAlias.Text.Trim, True) Then OK_Button.Enabled = True
    Else
      OK_Button.Enabled = False
    End If
  End Sub

End Class
