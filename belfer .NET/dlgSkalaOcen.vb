Imports System.Windows.Forms

Public Class dlgSkalaOcen

  Public Event NewAdded(ByVal InsertedID As String)
  Public IsNewMode As Boolean

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Close()
    If Not IsNewMode Then
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    Else
      If AddNew() Then
        txtNazwa.Text = ""
        txtNazwaPelna.Text = ""
        txtNazwaSkrot.Text = ""
        txtNazwa.Focus()
        OK_Button.Enabled = False
      End If
    End If

  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.Close()
  End Sub
  Private Function AddNew() As Boolean
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, O As New OcenySQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(O.InsertString)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Ocena", txtNazwa.Text.Trim)
      cmd.Parameters.AddWithValue("?Nazwa", txtNazwaPelna.Text.Trim)
      cmd.Parameters.AddWithValue("?Alias", txtNazwaSkrot.Text.Trim)
      cmd.Parameters.AddWithValue("?Waga", nudWaga.Value)
      cmd.Parameters.AddWithValue("?Typ", CType(cbTyp.SelectedIndex, GlobalValues.GradeType).ToString)
      cmd.Parameters.AddWithValue("?PodTyp", CType(cbPodtyp.SelectedIndex, GlobalValues.GradeSubType).ToString)
      'MessageBox.Show(CType(cbTyp.SelectedIndex, GlobalValues.GradeType).ToString)
      cmd.ExecuteNonQuery()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      'Dim InsertedID As String = cmd.LastInsertedId.ToString
      MySQLTrans.Commit()
      Return True

    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wartość wprowadzona w polu już istnieje w bazie danych.")
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
    'If Not Me.Modal Then AddHandler SharedGrade.OnOwnerClose, AddressOf Cancel_Button_Click

  End Sub

  Private Sub txtNazwa_TextChanged(sender As Object, e As EventArgs) Handles txtNazwa.TextChanged
    OK_Button.Enabled = CType(IIf(txtNazwa.Text.Length > 0, True, False), Boolean)
  End Sub
End Class
