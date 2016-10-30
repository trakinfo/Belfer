Imports System.Windows.Forms

Public Class dlgKraj
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public InRefresh As Boolean = False, IsNewMode As Boolean
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If Me.txtNazwa.Text.Length > 0 Then
        AddNew(txtNazwa, e)
        txtNazwa.Text = ""
        txtFullName.Text = ""
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, K As New KrajSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(K.InsertKraj)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Nazwa", txtNazwa.Text.Trim)
      cmd.Parameters.AddWithValue("?FullName", txtFullName.Text.Trim)

      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, txtNazwa.Text.Trim)
      txtNazwa.Focus()

    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wartość wprowadzona w polu '" & CType(sender, TextBox).Name.Substring(3) & "' już istnieje w bazie danych.")
        Case Else
          MessageBox.Show(ex.Message & vbCr & ex.Number)
      End Select
    Catch ex As Exception
      MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)

    End Try
  End Sub
  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub dlgKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'If Not Me.Modal Then AddHandler SharedKraj.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtKlasa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFullName.TextChanged, txtNazwa.TextChanged
    If Not InRefresh Then OK_Button.Enabled = CType(IIf(txtNazwa.Text.Trim.Length < 1, False, True), Boolean)
  End Sub

  Private Sub txtKlasa_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNazwa.Validating, txtFullName.Validating
    If txtNazwa.Text.Trim.Length < 1 Then Exit Sub
    Dim SH As New StringHelper
    If SH.AllowedChars(txtNazwa.Text.Trim, True) AndAlso SH.AllowedChars(txtFullName.Text.Trim, True) Then
      OK_Button.Enabled = True
    Else
      OK_Button.Enabled = False
      MessageBox.Show("Nazwa zawiera niedozwolone znaki!")
    End If

  End Sub
  'Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
  '    Me.DialogResult = System.Windows.Forms.DialogResult.OK
  '    Me.Close()
  'End Sub

  'Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
  '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
  '    Me.Close()
  'End Sub

End Class
