Public Class PrintVariables
  Private Shared FBaseFont As Font = New Font("Times New Roman", 10)
  Private Shared FBoldFont As Font = New Font(FBaseFont, FontStyle.Bold)
  Private Shared FHeaderFont As Font = New Font("Arial", 12, FontStyle.Bold)
  Private Shared FTopMargin As Integer = 79
  Private Shared FLeftMargin As Integer = 79
  Private Shared FBottomMargin As Integer = 79
  Private Shared FRightMargin As Integer = 79
  Private Shared FLandscape As Boolean = False
  Private Shared FColor As Boolean = True

  Public Property BaseFont() As Font
    Get
      Return FBaseFont
    End Get
    Set(ByVal value As Font)
      FBaseFont = value
      FBoldFont = New Font(FBaseFont, FontStyle.Bold)
    End Set
  End Property
  Public ReadOnly Property BoldFont() As Font
    Get
      Return FBoldFont
    End Get
  End Property
  Public Property HeaderFont() As Font
    Get
      Return FHeaderFont
    End Get
    Set(ByVal value As Font)
      FHeaderFont = value
    End Set
  End Property

  Public Property TopMargin() As Integer
    Get
      Return FTopMargin
    End Get
    Set(ByVal value As Integer)
      FTopMargin = value
    End Set
  End Property
  Public Property LeftMargin() As Integer
    Get
      Return FLeftMargin
    End Get
    Set(ByVal value As Integer)
      FLeftMargin = value
    End Set
  End Property
  Public Property BottomMargin() As Integer
    Get
      Return FBottomMargin
    End Get
    Set(ByVal value As Integer)
      FBottomMargin = value
    End Set
  End Property
  Public Property RightMargin() As Integer
    Get
      Return FRightMargin
    End Get
    Set(ByVal value As Integer)
      FRightMargin = value
    End Set
  End Property
  Public Property Landscape() As Boolean
    Get
      Return FLandscape
    End Get
    Set(ByVal value As Boolean)
      FLandscape = value
    End Set
  End Property
  Public Property Color() As Boolean
    Get
      Return FColor
    End Get
    Set(ByVal value As Boolean)
      FColor = value
    End Set
  End Property
End Class
