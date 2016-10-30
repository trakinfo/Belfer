Imports System.Windows.Forms

Public Class dlgOpisWyniku
  Public IsNewMode As Boolean, IdPrzedmiot As String
  Public Event NewAdded(ByVal InsertedID As String)

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    If Not IsNewMode Then
      Me.DialogResult = Windows.Forms.DialogResult.OK
      Me.Close()
    Else
      If AddNew() Then
        txtOpis.Text = ""
        txtOpis.Focus()
        OK_Button.Enabled = False
      End If
    End If
  End Sub
  Private Function AddNew() As Boolean
    Dim MySQLTrans As MySqlTransaction
    Dim DBA As New DataBaseAction, OW As New OpisWynikuSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(OW.InsertOpis)
    MySQLTrans = GlobalValues.gblConn.BeginTransaction()
    cmd.Transaction = MySQLTrans
    Try
      cmd.Parameters.AddWithValue("?Nazwa", txtOpis.Text.Trim)
      cmd.Parameters.AddWithValue("?IdPrzedmiot", IdPrzedmiot)
      cmd.Parameters.AddWithValue("?Kolor", txtKolor.Text)

      cmd.ExecuteNonQuery()
      MySQLTrans.Commit()
      RaiseEvent NewAdded(cmd.LastInsertedId.ToString)
      Return True
    Catch ex As MySqlException
      MySQLTrans.Rollback()
      Select Case ex.Number
        Case 1062
          MessageBox.Show("Wprowadzona wartość już istnieje w bazie danych.")
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
    'If Not Me.Modal Then AddHandler SharedOpisWyniku.OnOwnerClose, AddressOf Cancel_Button_Click

    Me.txtOpis.Focus()
  End Sub

  Private Sub txtKlasa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOpis.TextChanged
    OK_Button.Enabled = CType(IIf(txtOpis.Text.Trim.Length < 1, False, True), Boolean)
  End Sub


  Private Sub cmdKolor_Click(sender As Object, e As EventArgs) Handles cmdKolor.Click
    If dlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
      txtKolor.ForeColor = dlgColor.Color
      txtKolor.Text = String.Format("#{0:X2}{1:X2}{2:X2}", dlgColor.Color.R, dlgColor.Color.G, dlgColor.Color.B)
      cmdKolor.BackColor = dlgColor.Color
      Dim CH As New CalcHelper
      cmdKolor.ForeColor = CH.InvertColor(dlgColor.Color)
      cmdKolor.Text = String.Format("#{0:X2}{1:X2}{2:X2}", dlgColor.Color.R, dlgColor.Color.G, dlgColor.Color.B)

    End If
  End Sub


  Private Sub txtKolor_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtKolor.Validating
    Dim CH As New CalcHelper
    If CH.ValidateHexColor(txtKolor.Text.Trim) Then
      cmdKolor.BackColor = ColorTranslator.FromHtml(txtKolor.Text)
      cmdKolor.ForeColor = CH.InvertColor(cmdKolor.BackColor)
      cmdKolor.Text = txtKolor.Text
      txtKolor.ForeColor = cmdKolor.BackColor
      e.Cancel = False
    Else
      MessageBox.Show("Nieprawidłowa wartość koloru!" & vbNewLine & "Numer koloru powinien składać się z 6 cyfr szesnastkowych (0-9, A-F), poprzedzonych znakiem #.", "Belfer .NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
      e.Cancel = True
    End If
  End Sub
End Class
