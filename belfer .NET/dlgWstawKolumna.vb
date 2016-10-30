Imports System.Windows.Forms

Public Class dlgWstawKolumna
  'Private IdObsada, Typ As String
  'Private DT As DataTable
  Private MaxColNumber, ColNumber As Integer

  Public Event NewColumnAdded(ByVal IdKolumna As String)

  Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    'If Me.Modal Then
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    'Me.Dispose(True)
    Close()
    'Else
    'AddNew()
    'End If
  End Sub

  Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    If Me.Modal Then Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    'Me.Dispose(True)
    Close()
  End Sub

  Public Sub ClassChanged(Klasa As String)
    lblKlasa.Text = "Klasa: " & Klasa
    PrzedmiotChanged(Nothing)
  End Sub

  Public Sub PrzedmiotChanged(Przedmiot As String)
    'IdObsada = Obsada
    lblPrzedmiot.Text = "Przedmiot: " & Przedmiot
    OK_Button.Enabled = CType(IIf(Przedmiot IsNot Nothing, True, False), Boolean)
  End Sub
  Public Sub SemestrChanged(Semestr As String)
    'Typ = TypKolumny
    lblSemestr.Text = "Semestr: " & Semestr
  End Sub
  Public Sub UpdateColData(CurrentColumn As Byte)
    txtKolumnaOdniesienia.Text = CurrentColumn.ToString
    txtDostepnaLiczbaKolumn.Text = (MaxColNumber - ColNumber).ToString
    nudLiczbaKolumn.Maximum = MaxColNumber - ColNumber
  End Sub
  Public Sub SetMaxColNumber()
    Dim OH As New OptionHolder
    MaxColNumber = OH.MaxColNumber
  End Sub
  Public Sub SetColNumber(TotalColumn As Integer)
    ColNumber = TotalColumn
  End Sub
  Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    Dim Separator As New ShapeDrawer
    Separator.DrawSeparator(e.Graphics, Panel1.Left - 10, Panel1.Height - 1, Panel1.Width)
  End Sub
  Public Sub MoveColumn(Pos As Byte, Offset As Byte, IdObsada As String, Typ As String, Tran As MySqlTransaction)
    Dim DBA As New DataBaseAction, K As New KolumnaSQL
    Dim cmd As MySqlCommand = DBA.CreateCommand(K.UpdateNrKolumna(Offset))
    cmd.Transaction = Tran
    cmd.Parameters.AddWithValue("?NrKolumny", Pos)
    cmd.Parameters.AddWithValue("?IdObsada", IdObsada)
    cmd.Parameters.AddWithValue("?Typ", Typ)
    cmd.ExecuteNonQuery()
  End Sub
  Public Function AddNew(Pos As Byte, ColNumber As Byte, IdObsada As String, Typ As String, Tran As MySqlTransaction) As String
    Dim DBA As New DataBaseAction, K As New KolumnaSQL, cmd As MySqlCommand = Nothing
    For i As Byte = 0 To ColNumber - CByte(1)
      cmd = DBA.CreateCommand(K.InsertKolumna)
      cmd.Transaction = Tran
      cmd.Parameters.AddWithValue("?NrKolumny", Pos + i)
      cmd.Parameters.AddWithValue("?IdObsada", IdObsada)
      cmd.Parameters.AddWithValue("?IdOpis", DBNull.Value)
      cmd.Parameters.AddWithValue("?Typ", Typ)
      cmd.Parameters.AddWithValue("?Waga", "1")
      cmd.Parameters.AddWithValue("?Poprawa", chkPoprawa.CheckState)
      cmd.ExecuteNonQuery()
    Next
    Return cmd.LastInsertedId.ToString
  End Function
End Class
