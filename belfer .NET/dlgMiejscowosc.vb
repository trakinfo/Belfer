Imports System.Windows.Forms

Public Class dlgMiejscowosc
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public IsNewMode As Boolean
  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
    If IsNewMode Then
      If Me.txtNazwa.Text.Length > 0 Then
        AddNew(txtNazwa, e)
        txtNazwa.Text = ""
        txtNazwaMiejscownik.Text = ""
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
  Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, M As New MiejscowoscSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(M.InsertMiejscowosc())
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Nazwa", txtNazwa.Text.Trim)
      cmd.Parameters.AddWithValue("?NazwaMiejscownik", txtNazwaMiejscownik.Text.Trim)
      cmd.Parameters.AddWithValue("?Poczta", txtPoczta.Text.Trim)
      cmd.Parameters.AddWithValue("?KodPocztowy", mskKodPocztowy.Text)
      cmd.Parameters.AddWithValue("?KodWoj", CType(cbWoj.SelectedItem, CbItem).Kod.ToString)
      cmd.Parameters.AddWithValue("?IdKraj", CType(cbKraj.SelectedItem, CbItem).ID.ToString)
      cmd.Parameters.AddWithValue("?Miasto", CType(chkMiasto.CheckState, Integer).ToString)

      cmd.ExecuteNonQuery()
      Dim InsertedID As String = cmd.LastInsertedId.ToString
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, txtNazwa.Text.Trim)
      txtNazwa.Focus()

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

  Private Sub dlgMiejscowosc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'If Not Me.Modal Then AddHandler SharedMiejscowosc.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtNazwa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwa.TextChanged, cbKraj.SelectionChangeCommitted, cbWoj.SelectionChangeCommitted
    'OK_Button.Enabled = CType(IIf(txtNazwa.Text.Trim.Length < 1, False, True), Boolean)
    If txtNazwa.Text.Trim.Length > 0 AndAlso cbWoj.SelectedItem IsNot Nothing AndAlso cbKraj.SelectedItem IsNot Nothing Then
      Dim SH As New StringHelper
      If SH.AllowedChars(txtNazwa.Text.Trim, True) Then OK_Button.Enabled = True
    Else
      OK_Button.Enabled = False
    End If
  End Sub
End Class
