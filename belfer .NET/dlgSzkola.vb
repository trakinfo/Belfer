Imports System.Windows.Forms

Public Class dlgSzkola
  Public Event NewAdded(ByVal sender As Object, ByVal e As EventArgs, ByVal InsertedID As String)
  Public InRefresh As Boolean = False, IsNewMode As Boolean

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If IsNewMode Then
      If AddNew(Me, e) Then
        ClearFields()
        OK_Button.Enabled = False
      End If
    Else
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    End If
  End Sub
  Private Function AddNew(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, S As New SzkolaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(S.InsertSchool)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      If cbTypSzkoly.SelectedItem IsNot Nothing Then
        cmd.Parameters.AddWithValue("?IdTypSzkoly", CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString)
      Else
        cmd.Parameters.AddWithValue("?IdTypSzkoly", DBNull.Value)
      End If
      cmd.Parameters.AddWithValue("?Nazwa", txtNazwa.Text.Trim)
      cmd.Parameters.AddWithValue("?Alias", txtAlias.Text.Trim)
      cmd.Parameters.AddWithValue("?Nip", IIf(txtNip.Text.Trim.Length > 0, txtNip.Text.Trim, DBNull.Value))
      cmd.Parameters.AddWithValue("?Ulica", txtUlica.Text.Trim)
      cmd.Parameters.AddWithValue("?Nr", txtNr.Text.Trim)
      If cbMiejscowosc.SelectedItem IsNot Nothing Then
        cmd.Parameters.AddWithValue("?IdMiejscowosc", CType(cbMiejscowosc.SelectedItem, CbItem).ID.ToString)
      Else
        cmd.Parameters.AddWithValue("?IdMiejscowosc", DBNull.Value)
      End If
      cmd.Parameters.AddWithValue("?Tel", txtTel.Text.Trim)
      cmd.Parameters.AddWithValue("?Fax", txtFax.Text.Trim)
      cmd.Parameters.AddWithValue("?Email", txtEmail.Text.Trim)
      cmd.ExecuteNonQuery()
      Dim InsertedID As String = cmd.LastInsertedId.ToString
      If chkIsDefault.Checked Then
        cmd.CommandText = S.ResetDefault
        cmd.Parameters.AddWithValue("?TypSzkoly", CType(cbTypSzkoly.SelectedItem, CbItem).ID.ToString)
        cmd.ExecuteNonQuery()

        cmd.CommandText = S.SetDefault()
        cmd.Parameters.AddWithValue("?ID", InsertedID)
        cmd.ExecuteNonQuery()
      End If
      cmd.Parameters.Clear()
      Dim O As New OpcjeSQL
      Dim dtOpcje As DataTable = DBA.GetDataTable(O.SelectOption)
      For Each DR As DataRow In dtOpcje.Rows
        cmd.CommandText = O.InsertOption
        cmd.Parameters.AddWithValue("?Name", DR.Item("Name").ToString)
        cmd.Parameters.AddWithValue("?Value", DR.Item("Value").ToString)
        cmd.Parameters.AddWithValue("?Type", DR.Item("Type").ToString)
        cmd.Parameters.AddWithValue("?Description", DR.Item("Description").ToString)
        cmd.Parameters.AddWithValue("?IdSchool", InsertedID)
        cmd.Parameters.AddWithValue("?StartDate", DR.Item("StartDate").ToString)
        cmd.Parameters.AddWithValue("?EndDate", DR.Item("EndDate").ToString)
        cmd.Parameters.AddWithValue("?Owner", GlobalValues.AppUser.Login)
        cmd.Parameters.AddWithValue("?User", GlobalValues.AppUser.Login)
        cmd.Parameters.AddWithValue("?ComputerIP", GlobalValues.gblIP)
        cmd.ExecuteNonQuery()
        cmd.Parameters.Clear()
      Next
      MySQLTrans.Commit()
      RaiseEvent NewAdded(Me, e, InsertedID)
      'ClearFields()
      txtNazwa.Focus()
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
      'MySQLTrans.Rollback()
      MessageBox.Show(ex.Message)
      Return False
    End Try
  End Function
  Private Sub ClearFields()
    For Each ctrl As Control In Me.pnlSzkola.Controls
      If TypeOf ctrl Is TextBox Then ctrl.Text = ""
    Next
  End Sub
  Public Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub dlgKlasa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'If Not Me.Modal Then AddHandler SharedSchool.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  'Private Sub txtKlasa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNazwa.TextChanged
  '  If Not InRefresh Then OK_Button.Enabled = CType(IIf(txtNazwa.Text.Trim.Length < 1, False, True), Boolean)
  'End Sub

  Private Sub txtAlias_TextChanged(sender As Object, e As EventArgs) Handles txtAlias.TextChanged

    OK_Button.Enabled = CType(IIf(CType(sender, TextBox).Text.Trim.Length > 0, True, False), Boolean)

  End Sub

  Private Sub txtNip_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNip.Validating
    If txtNip.Text.Trim.Length < 1 Then Exit Sub
    Dim CH As New CalcHelper
    If CH.ValidateNIP(txtNip.Text) Then
      e.Cancel = False
    Else
      MessageBox.Show("Numer NIP jest niepoprawny. Wpisz poprawny nr NIP lub pozostaw pole puste.")
      e.Cancel = True
    End If
  End Sub

  Private Sub txtTel_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTel.Validating
    If CType(sender, TextBox).Text.Trim.Length < 1 Then Exit Sub
    Dim SH As New StringHelper
    If SH.DigitOnly(CType(sender, TextBox).Text) Then
      e.Cancel = False
    Else
      e.Cancel = True
      MessageBox.Show("Numer telefonu lub faxu jest niepoprawny. Wpisz poprawny nr lub pozostaw pole puste.")
    End If
  End Sub

  Private Sub cbTypSzkoly_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTypSzkoly.SelectedIndexChanged
    pnlSzkola.Enabled = CType(IIf(cbTypSzkoly.Items.Count > 0, True, False), Boolean)
  End Sub
End Class
