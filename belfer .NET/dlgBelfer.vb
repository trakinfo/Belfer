Imports System.Windows.Forms

Public Class dlgBelfer
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public InRefresh As Boolean = False, IsNewMode As Boolean
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If Me.txtNazwisko.Text.Length > 0 Then
        If AddNew(txtNazwisko, e) Then
          txtNazwisko.Text = ""
          txtImie.Text = ""
          OK_Button.Enabled = False
        End If
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub
  Private Function AddNew(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, B As New BelferSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(B.InsertBelfer)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Nazwisko", txtNazwisko.Text.Trim)
      cmd.Parameters.AddWithValue("?Imie", txtImie.Text.Trim)
      cmd.Parameters.AddWithValue("?Sex", cbPlec.Text)

      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, txtNazwisko.Text.Trim)
      txtNazwisko.Focus()
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
  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub dlgKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'If Not Me.Modal Then AddHandler SharedBelfer.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtKlasa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImie.TextChanged, txtNazwisko.TextChanged
    If Not InRefresh Then OK_Button.Enabled = CType(IIf(txtNazwisko.Text.Trim.Length < 1, False, True), Boolean)
  End Sub

  Private Sub txtImie_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImie.Validated, txtNazwisko.Validated
    Dim SH As New StringHelper
    CType(sender, TextBox).Text = SH.CapitalizeFirst(CType(sender, TextBox).Text.Trim)

  End Sub

  Private Sub txtKlasa_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNazwisko.Validating, txtImie.Validating
    If txtNazwisko.Text.Trim.Length < 1 Then Exit Sub
    Dim SH As New StringHelper
    If SH.AllowedChars(txtNazwisko.Text.Trim, True) AndAlso SH.AllowedChars(txtImie.Text.Trim, True) Then
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
