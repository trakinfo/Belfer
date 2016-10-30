Imports System.Windows.Forms

Public Class dlgWoj
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public InRefresh As Boolean = False, IsNewMode As Boolean
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If Me.txtKodWoj.Text.Length > 0 Then
        AddNew(txtKodWoj, e)
        txtKodWoj.Text = ""
        txtNazwaWoj.Text = ""
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, W As New WojSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(W.InsertWoj)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?KodWoj", txtKodWoj.Text.Trim)
      cmd.Parameters.AddWithValue("?Nazwa", txtNazwaWoj.Text.Trim)
      'cmd.Parameters.AddWithValue("?IsDefault", CType(chkIsDefault.CheckState, Integer).ToString)

      cmd.ExecuteNonQuery()
      If chkIsDefault.Checked Then
        cmd.CommandText = W.ResetDefault()
        cmd.ExecuteNonQuery()
        cmd.CommandText = W.SetDefault(1, txtKodWoj.Text.Trim)
        cmd.ExecuteNonQuery()
      Else
        cmd.CommandText = W.SetDefault(0, txtKodWoj.Text.Trim)
        cmd.ExecuteNonQuery()
      End If
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, txtKodWoj.Text.Trim)
      txtKodWoj.Focus()

    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wartość wprowadzona w polu '" & CType(sender, TextBox).Name & "' już istnieje w bazie danych.")
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
    'If Not Me.Modal Then AddHandler SharedWoj.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtKlasa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwaWoj.TextChanged, txtKodWoj.TextChanged
    If Not InRefresh Then OK_Button.Enabled = CType(IIf(txtNazwaWoj.Text.Trim.Length < 1 Or txtKodWoj.Text.Trim.Length < 4, False, True), Boolean)
  End Sub

  Private Sub txtKlasa_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNazwaWoj.Validating, txtKodWoj.Validating
    If txtNazwaWoj.Text.Trim.Length < 1 Then Exit Sub
    Dim SH As New StringHelper
    If SH.AllowedChars(txtNazwaWoj.Text.Trim, True) AndAlso SH.LetterOrDigit(txtKodWoj.Text.Trim) Then
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

  Private Sub chkIsDefault_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsDefault.CheckedChanged
    If Not InRefresh Then OK_Button.Enabled = CType(IIf(txtNazwaWoj.Text.Trim.Length < 1 Or txtKodWoj.Text.Trim.Length < 4, False, True), Boolean)

  End Sub
End Class
