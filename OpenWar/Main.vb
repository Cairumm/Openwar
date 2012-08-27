Public Class Main

    Private Players As New List(Of Player)

    ' This is updated by the mouse move event so we can highlight the hex that the mouse is over
    Private CurrentMousePosition As New PointF

    Private CurrentGameAction As String = String.Empty

#Region "Hex Map Variables"
    ' This is a list of the coordinates of the upper-left-hand coordinates of each hex
    ' Since they are the same for both the human and AI player, we only need to maintain one list
    Private HexPoints As New List(Of PointF)

    ' variables used in map calculations and drawing
    Dim SideLength As Single
    Dim ShortSide As Single
    Dim LongSide As Single
    Dim HexWidth As Single
    Dim HexHeight As Single
    Dim MapWidth As Integer
    Dim MapHeight As Integer
    Dim MapPadding As Integer
    Dim MapBackgroundColor As Color
    Dim MapLineColor As Color
    Dim PlayerMapXOffset As Integer
    Dim PlayerMapYOffset As Integer

    ' game status variables
    Dim GameYear As Integer
    Dim GameOver As Boolean
    Dim AtWar As Boolean
#End Region

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ' This is necessary to prevent flickering when the maps are redrawn
        Me.DoubleBuffered = True

        ' Initialize game variables
        Me.SideLength = 20
        Me.ShortSide = Convert.ToSingle(System.Math.Sin(30 * System.Math.PI / 180) * Me.SideLength)
        Me.LongSide = Convert.ToSingle(System.Math.Cos(30 * System.Math.PI / 180) * Me.SideLength)
        Me.HexWidth = (2 * Me.ShortSide) + Me.SideLength
        Me.HexHeight = 2 * Me.LongSide
        Me.MapPadding = 10
        Me.MapHeight = (Me.HexHeight * 12) + (2 * MapPadding)
        Me.MapWidth = (Me.HexWidth * 6) + (Me.SideLength * 5) + (2 * Me.MapPadding)
        Me.MapBackgroundColor = Color.Black
        Me.MapLineColor = Color.Green
        Me.PlayerMapXOffset = 10
        Me.PlayerMapYOffset = 10

        Me.GameYear = SetStartingYear()
        GameOver = False
        AtWar = False

        ' Build and store the list of hex coordinates
        GenerateHexPoints()

    End Sub



#Region "Map Coordinate Generation"
    Private Sub GenerateHexPoints()
        Dim RowNumber As Integer = 1
        GenerateHexRow(1, RowNumber)
        GenerateHexRow(2, RowNumber)
        GenerateHexRow(3, RowNumber)
        GenerateHexRow(4, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(6, RowNumber)
        GenerateHexRow(5, RowNumber)
        GenerateHexRow(4, RowNumber)
        GenerateHexRow(3, RowNumber)
        GenerateHexRow(2, RowNumber)
        GenerateHexRow(1, RowNumber)
    End Sub

    Private Sub GenerateHexRow(Hexes As Integer, ByRef RowNumber As Integer)

        ' Calculate x of the starting point of the row
        Dim RowWidth As Single = (Me.HexWidth * Hexes) + (Me.SideLength * (Hexes - 1))
        Dim XOffsetFromCenter As Single = (RowWidth / 2) - Me.ShortSide

        Dim UpperLeft As PointF
        UpperLeft.X = (Me.MapWidth / 2) - XOffsetFromCenter
        UpperLeft.Y = ((RowNumber - 1) * Me.LongSide) + Me.MapPadding

        For CurrentHex = 1 To Hexes
            Me.HexPoints.Add(New PointF(UpperLeft.X, UpperLeft.Y))
            UpperLeft.X += (2 * Me.SideLength) + (2 * Me.ShortSide)
        Next

        RowNumber += 1
    End Sub
#End Region

    Public Sub AddPlayer(CountryName As String, IsAIPlayer As Boolean)
        If IsAIPlayer Then
            Dim NewAIPlayer As New AIPlayer
            NewAIPlayer.CountryName = CountryName
            NewAIPlayer.SetupMap()
            Players.Add(NewAIPlayer)
        Else
            Dim NewHumanPlayer As New HumanPlayer
            NewHumanPlayer.CountryName = CountryName
            NewHumanPlayer.SetupMap()
            Players.Add(NewHumanPlayer)
        End If
    End Sub

    Private Sub Main_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Me.CurrentMousePosition.X = e.X
        Me.CurrentMousePosition.Y = e.Y
        Me.Invalidate()
    End Sub

    Private Function SetStartingYear() As Integer
        Dim rng As New Random(System.DateTime.Now.Millisecond)
        Return rng.Next(1956, 1965)
    End Function




#Region "Graphics/Drawing"
    Private Sub Main_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        ' create two bitmaps and a graphics interface for each
        Dim HumanPlayerMapB As New Bitmap(Me.MapWidth, Me.MapHeight)
        Dim HumanPlayerMapG As Graphics = Graphics.FromImage(HumanPlayerMapB)
        HumanPlayerMapG.FillRectangle(New SolidBrush(Me.MapBackgroundColor), 0, 0, Me.MapWidth, Me.MapHeight)

        Dim AIPlayerMapB As New Bitmap(Me.MapWidth, Me.MapHeight)
        Dim AIPlayerMapG As Graphics = Graphics.FromImage(AIPlayerMapB)
        AIPlayerMapG.FillRectangle(New SolidBrush(Me.MapBackgroundColor), 0, 0, Me.MapWidth, Me.MapHeight)

        DrawEmptyHexMaps(HumanPlayerMapG, AIPlayerMapG)
        DrawPlayerMap(HumanPlayerMapG)
        HighlightHexUnderMouse(HumanPlayerMapG)

        ' bitmaps are all done
        ' put them on the screen
        e.Graphics.DrawImage(HumanPlayerMapB, New Point(Me.PlayerMapXOffset, Me.PlayerMapYOffset))
        e.Graphics.DrawImage(AIPlayerMapB, New Point(Me.MapWidth + (2 * Me.PlayerMapXOffset), Me.PlayerMapYOffset))

    End Sub

    Private Sub DrawEmptyHexMaps(ByRef PlayerGraphics As Graphics, ByRef AIGraphics As Graphics)

        ' iterate through the list of pre-generated coordinates and draw the hexes in
        ' both bitmaps
        For Each UpperLeft As PointF In Me.HexPoints
            Dim x As Single = UpperLeft.X
            Dim y As Single = UpperLeft.Y

            Dim Points(5) As PointF
            Points(0) = New PointF(x, y)
            Points(1) = New PointF(x + Me.SideLength, y)
            Points(2) = New PointF(x + Me.SideLength + Me.ShortSide, y + Me.LongSide)
            Points(3) = New PointF(x + Me.SideLength, y + Me.LongSide + Me.LongSide)
            Points(4) = New PointF(x, y + Me.LongSide + Me.LongSide)
            Points(5) = New PointF(x - Me.ShortSide, y + Me.LongSide)

            PlayerGraphics.DrawPolygon(New Pen(Me.MapLineColor), Points)
            AIGraphics.DrawPolygon(New Pen(Me.MapLineColor), Points)
        Next


    End Sub

    Private Sub DrawPlayerMap(ByRef PlayerGraphics As Graphics)
        For TileCounter = 0 To 101
            If Players(0).Map(TileCounter).TileType <> MapTile.MapTileType.Empty Then
                Dim TileCharacter As String = String.Empty
                Select Case Players(0).Map(TileCounter).TileType
                    Case MapTile.MapTileType.BomberBase
                        TileCharacter = "B"
                    Case MapTile.MapTileType.MissileBase
                        TileCharacter = "M"
                    Case MapTile.MapTileType.City
                        TileCharacter = "C"
                End Select

                Dim XOffset As Single = 4
                Dim YOffset As Single = 4
                Dim Location As New PointF(HexPoints(TileCounter).X + XOffset, HexPoints(TileCounter).Y + YOffset)

                PlayerGraphics.DrawString(TileCharacter, New System.Drawing.Font("Courier New", 10), Brushes.Green, Location)
            End If
        Next
    End Sub

    Private Sub HighlightHexUnderMouse(ByRef PlayerGraphics As Graphics)
        ' Check Player Map
        For HexCounter = 0 To 101
            Dim UpperLeft As PointF = Me.HexPoints(HexCounter)
            Dim x As Single = UpperLeft.X
            Dim y As Single = UpperLeft.Y

            Dim Points(5) As PointF
            Points(0) = New PointF(x, y)
            Points(1) = New PointF(x + Me.SideLength, y)
            Points(2) = New PointF(x + Me.SideLength + Me.ShortSide, y + Me.LongSide)
            Points(3) = New PointF(x + Me.SideLength, y + Me.LongSide + Me.LongSide)
            Points(4) = New PointF(x, y + Me.LongSide + Me.LongSide)
            Points(5) = New PointF(x - Me.ShortSide, y + Me.LongSide)

            Dim OffsetMousePosition As New PointF(Me.CurrentMousePosition.X - Me.PlayerMapXOffset, Me.CurrentMousePosition.Y - Me.PlayerMapYOffset)
            If InsidePolygon(Points, 6, OffsetMousePosition) Then
                PlayerGraphics.DrawPolygon(New Pen(Brushes.Yellow), Points)
                Exit For
            End If
        Next
    End Sub

    Public Shared Function InsidePolygon(polygon As PointF(), N As Integer, p As PointF) As Boolean
        Dim counter As Integer = 0
        Dim i As Integer
        Dim xinters As Double
        Dim p1 As PointF, p2 As PointF

        p1 = polygon(0)
        For i = 1 To N
            p2 = polygon(i Mod N)
            If p.Y > System.Math.Min(p1.Y, p2.Y) Then
                If p.Y <= System.Math.Max(p1.Y, p2.Y) Then
                    If p.X <= System.Math.Max(p1.X, p2.X) Then
                        If p1.Y <> p2.Y Then
                            xinters = (p.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X
                            If p1.X = p2.X OrElse p.X <= xinters Then
                                counter += 1
                            End If
                        End If
                    End If
                End If
            End If
            p1 = p2
        Next

        If counter Mod 2 = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region


    Public Sub PeaceTurnButtons_Show()
        Me.btnBuildBase.Visible = True
        Me.btnDeclareWar.Visible = True
        Me.btnSpy.Visible = True
    End Sub

    Public Sub PeaceTurnButtons_Hide()
        Me.btnBuildBase.Visible = False
        Me.btnDeclareWar.Visible = False
        Me.btnSpy.Visible = False
    End Sub

    Public Sub BaseTypeButtons_Show()
        Me.btnBuildABMBase.Visible = True
        Me.btnBuildBomberBase.Visible = True
        Me.btnBuildMissileBase.Visible = True
        Me.btnBuildSubBase.Visible = True
    End Sub

    Public Sub BaseTypeButtons_Hide()
        Me.btnBuildABMBase.Visible = False
        Me.btnBuildBomberBase.Visible = False
        Me.btnBuildMissileBase.Visible = False
        Me.btnBuildSubBase.Visible = False
    End Sub

    Private Sub SetMessage(Message As String)
        Me.txtMessage.Text = Message
    End Sub

End Class