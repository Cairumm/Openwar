Public Class Main

    Private PlayerMap As New List(Of MapTile)
    Private AIMap As New List(Of MapTile)

    Public Property PlayerCountryName As String
    Public Property AICountryName As String

    ' A class-level random number generator
    Private rng As Random

    ' When a button is clicked, this variable holds a string that indicates what button was clicked
    Private ButtonAction As String = String.Empty

    ' This is updated by the mouse move event so we can highlight the hex that the mouse is over
    Private CurrentMousePosition As New PointF

    ' The "selection mode" of the form controls how the mouse and graphics behave
    Private Enum SelectMode
        None
        Build
        Target
    End Enum
    Private SelectionMode As SelectMode

    ' The collection that holds the currently-selected hex(es)
    Private SelectedHexes As New List(Of Integer)

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

    ' game control variables
    Dim GameYear As Integer
    Dim GameOver As Boolean
    Dim AtWar As Boolean
    Dim StartingCities As Integer
#End Region

    Private Sub Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.RectangleShape1.Hide()
        Me.RectangleShape2.Hide()

        ' This is necessary to prevent flickering when the maps are redrawn
        Me.DoubleBuffered = True

        Me.rng = New Random(System.DateTime.Now.Millisecond)

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
        Me.StartingCities = 8
        Me.GameYear = SetStartingYear()
        Me.GameOver = False
        Me.AtWar = False

        ' Build and store the list of hex coordinates
        GenerateHexPoints()
        SetupMaps()

        SetVisibility_TurnActionButtons(True)
        SetVisibility_BuildBaseButtons(False)
        SetVisibility_ConfirmSelectionButton(False)

        Me.SetMessage("What will you do this turn?")
    End Sub

    ' This method waits for one of the game buttons to be clicked.
    Private Function GetButtonAction() As String
        Dim ReturnValue As String = String.Empty
        Do
            Application.DoEvents()
            If Me.ButtonAction <> String.Empty Then
                ReturnValue = Me.ButtonAction
            End If
        Loop Until ReturnValue <> String.Empty
        Me.ButtonAction = String.Empty
        Return ReturnValue
    End Function

    Private Sub TurnAction_Build()
        BuildPlayerBase()
        BuildPlayerBase()
    End Sub

    Private Sub BuildPlayerBase()
        SetVisibility_BuildBaseButtons(True)
        SetMessage("What kind of base do you want to build?")

        Dim Action As String = GetButtonAction()
        Dim BaseType As MapTile.MapTileType
        Select Case Action.ToUpper
            Case "MISSILE"
                BaseType = MapTile.MapTileType.MissileBase
            Case "BOMBER"
                BaseType = MapTile.MapTileType.BomberBase
            Case "ABM"
                BaseType = MapTile.MapTileType.ABMBase
            Case "SUB"
                BaseType = MapTile.MapTileType.SubmarineBase
        End Select

        SetVisibility_BuildBaseButtons(False)
        SetVisibility_ConfirmSelectionButton(True)

        Me.SelectionMode = SelectMode.Build
        SetMessage("Select a location for your base and click ""Confirm""")
        Dim Location As Integer = SelectHex()

        Me.PlayerMap(Location).TileType = BaseType
        Me.Invalidate()

        Me.SelectionMode = SelectMode.None
    End Sub

    Private Function SelectHex() As Integer
        Me.SelectedHexes = New List(Of Integer)
        Dim ReturnValue As Integer

        Me.btnConfirmSelection.Enabled = False
        Do
            Application.DoEvents()
        Loop Until Me.ButtonAction = "CONFIRMSELECTION"

        ReturnValue = Me.SelectedHexes(0)

        Me.SelectionMode = SelectMode.None
        SetVisibility_ConfirmSelectionButton(False)
        SetMessage("")

        Me.ButtonAction = String.Empty
        Return ReturnValue

    End Function

    Private Sub Main_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Select Case Me.SelectionMode
            Case SelectMode.Build
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
                        If Me.SelectedHexes.Count = 0 Then
                            Me.SelectedHexes = New List(Of Integer)
                            Me.SelectedHexes.Add(HexCounter)
                            Me.btnConfirmSelection.Enabled = True
                        Else
                            Me.SelectedHexes = New List(Of Integer)
                            Me.btnConfirmSelection.Enabled = False
                        End If
                        Exit For
                    End If
                Next
        End Select
    End Sub

    Private Sub Main_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Me.CurrentMousePosition.X = e.X
        Me.CurrentMousePosition.Y = e.Y
        Debug.WriteLine(e.X & "," & e.Y)
        Me.Invalidate()
    End Sub

    Private Function SetStartingYear() As Integer
        Return Me.rng.Next(1956, 1965)
    End Function

#Region "Map Routines"
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

    Public Sub SetupMaps()
        For x = 0 To 101
            PlayerMap.Add(New MapTile)
            AIMap.Add(New MapTile)
        Next

        SetMapTileProperties(PlayerMap)
        AddStartingMapItems(PlayerMap)

        SetMapTileProperties(AIMap)
        AddStartingMapItems(AIMap)
    End Sub

    Private Sub AddStartingMapItems(Map As List(Of MapTile))
        ' Put cities on the map at random locations
        Dim CityCount As Integer = 0
        Do
            Dim CityTile As MapTile = Map(Me.rng.Next(0, Map.Count - 1))
            If CityTile.TileType <> MapTile.MapTileType.City Then
                CityTile.TileType = MapTile.MapTileType.City
                CityCount += 1
            End If
        Loop Until CityCount = Me.StartingCities

        ' Add a missile base to the map
        Dim BaseCount As Integer = 0
        Do
            Dim MissileBaseTile As MapTile = Map(Me.rng.Next(0, Map.Count - 1))
            If MissileBaseTile.TileType = MapTile.MapTileType.Empty Then
                MissileBaseTile.TileType = MapTile.MapTileType.MissileBase
                BaseCount += 1
            End If
        Loop Until BaseCount = 1

        BaseCount = 0
        Do
            Dim BomberBaseTile As MapTile = Map(Me.rng.Next(0, Map.Count - 1))
            If BomberBaseTile.TileType = MapTile.MapTileType.Empty Then
                BomberBaseTile.TileType = MapTile.MapTileType.BomberBase
                BaseCount += 1
            End If
        Loop Until BaseCount = 1
    End Sub

    Private Sub SetMapTileProperties(Map As List(Of MapTile))
        ' I'm sure there's a more elegant way to do this
        ' For now, I just care about making the map work.
        '
        ' We need to assign rows and columns to each Map tile so that we can know which hexes are next to each other
        ' in case a missile goes off target
        For x = 0 To 101
            Dim ThisTile As MapTile = Map(x)

            ' set Map rows for each tile
            Select Case x
                Case 0, 2, 5, 9, 14, 20
                    ThisTile.MapRow = 0
                Case 1, 4, 8, 13, 19, 25, 31
                    ThisTile.MapRow = 1
                Case 3, 7, 12, 18, 24, 30, 36, 42
                    ThisTile.MapRow = 2
                Case 6, 11, 17, 23, 29, 35, 41, 47, 53
                    ThisTile.MapRow = 3
                Case 10, 16, 22, 28, 34, 40, 46, 52, 58, 64
                    ThisTile.MapRow = 4
                Case 15, 21, 27, 33, 39, 45, 51, 57, 63, 69, 75
                    ThisTile.MapRow = 5
                Case 26, 32, 38, 44, 50, 56, 62, 68, 74, 80, 86
                    ThisTile.MapRow = 6
                Case 37, 43, 49, 55, 61, 67, 73, 79, 85, 91
                    ThisTile.MapRow = 7
                Case 48, 54, 60, 66, 72, 78, 84, 90, 95
                    ThisTile.MapRow = 8
                Case 59, 65, 71, 77, 83, 89, 94, 98
                    ThisTile.MapRow = 9
                Case 70, 76, 82, 88, 93, 97, 100
                    ThisTile.MapRow = 10
                Case 81, 87, 92, 96, 99, 101
                    ThisTile.MapRow = 11
            End Select

            ' set Map columns for each tile
            Select Case x
                Case 15, 26, 37, 48, 59, 70, 81
                    ThisTile.MapColumn = 0
                Case 10, 21, 32, 43, 54, 65, 76, 87
                    ThisTile.MapColumn = 1
                Case 6, 16, 27, 38, 49, 60, 71, 82, 92
                    ThisTile.MapColumn = 2
                Case 3, 11, 22, 33, 44, 55, 66, 77, 88, 96
                    ThisTile.MapColumn = 3
                Case 1, 7, 17, 28, 39, 50, 61, 72, 83, 93, 99
                    ThisTile.MapColumn = 4
                Case 0, 4, 12, 23, 34, 45, 56, 67, 78, 89, 97, 101
                    ThisTile.MapColumn = 5
                Case 2, 8, 18, 29, 40, 51, 62, 73, 84, 94, 100
                    ThisTile.MapColumn = 6
                Case 5, 13, 24, 35, 46, 57, 68, 79, 90, 98
                    ThisTile.MapColumn = 7
                Case 9, 19, 30, 41, 52, 63, 74, 85, 95
                    ThisTile.MapColumn = 8
                Case 14, 25, 36, 47, 58, 69, 80, 91
                    ThisTile.MapColumn = 9
                Case 20, 31, 42, 53, 64, 75, 86
                    ThisTile.MapColumn = 10
            End Select

            'mark coastal tiles
            'we need to know which tiles are coastal to determine whether or not a sub base can be built there
            Select Case x
                Case 15, 10, 6, 3, 1, 0, 2, 5, 9, 14, 20, 26, 37, 48, 59, 70, 81, 31, 42, 53, 64, 75, 86, 87, 92, 96, 99, 101, 100, 98, 95, 91
                    ThisTile.IsCoastal = True
                Case Else
                    ThisTile.IsCoastal = False
            End Select

            ThisTile.TileType = MapTile.MapTileType.Empty
            ThisTile.IsVisibleToOpponent = True
        Next
    End Sub

    Public Function GetEmptyTileCount(Map As List(Of MapTile)) As Integer
        Dim ReturnValue As Integer = 0
        For Each Tile As MapTile In Map
            If Tile.TileType = MapTile.MapTileType.Empty Then
                ReturnValue += 1
            End If
        Next
        Return ReturnValue
    End Function

    Public Function GetEmptyCoastalTileCount(Map As List(Of MapTile)) As Integer
        Dim ReturnValue As Integer = 0
        For Each Tile As MapTile In Map
            If Tile.TileType = MapTile.MapTileType.Empty And Tile.IsCoastal Then
                ReturnValue += 1
            End If
        Next
        Return ReturnValue
    End Function

    Public Function GetTileFromCoordinates(Map As List(Of MapTile), Column As Integer, Row As Integer) As MapTile
        Dim ReturnValue As MapTile = Nothing
        For Each Tile As MapTile In Map
            If Tile.MapColumn = Column And Tile.MapRow = Row Then
                ReturnValue = Tile
                Exit For
            End If
        Next
        Return ReturnValue
    End Function
#End Region

#Region "Graphics/Drawing"
    Private Sub Main_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        ' create two bitmaps and a graphics interface for each
        Dim PlayerMapB As New Bitmap(Me.MapWidth, Me.MapHeight)
        Dim PlayerMapG As Graphics = Graphics.FromImage(PlayerMapB)
        PlayerMapG.FillRectangle(New SolidBrush(Me.MapBackgroundColor), 0, 0, Me.MapWidth, Me.MapHeight)

        Dim AIMapB As New Bitmap(Me.MapWidth, Me.MapHeight)
        Dim AIMapG As Graphics = Graphics.FromImage(AIMapB)
        AIMapG.FillRectangle(New SolidBrush(Me.MapBackgroundColor), 0, 0, Me.MapWidth, Me.MapHeight)

        DrawEmptyHexMaps(PlayerMapG, AIMapG)
        UpdatePlayerMap(PlayerMapG)
        UpdateAIMap(AIMapG)

        Select Case Me.SelectionMode
            Case SelectMode.Build
                HighlightHexUnderMouse(PlayerMapG)
                HighlightSelectedHexes(PlayerMapG)
            Case SelectMode.None
        End Select

        ' bitmaps are all done
        ' put them on the screen
        e.Graphics.DrawImage(PlayerMapB, New Point(Me.PlayerMapXOffset, Me.PlayerMapYOffset))
        e.Graphics.DrawImage(AIMapB, New Point(Me.MapWidth + (2 * Me.PlayerMapXOffset), Me.PlayerMapYOffset))

    End Sub

    Private Sub DrawEmptyHexMaps(ByRef PlayerGraphics As Graphics, ByRef AIGraphics As Graphics)

        PlayerGraphics.DrawString(Me.PlayerCountryName, New Font("Courier New", 12), New SolidBrush(Color.Green), New PointF(1, 1))
        AIGraphics.DrawString(Me.AICountryName, New Font("Courier New", 12), New SolidBrush(Color.Green), New PointF(1, 1))



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

    Private Sub UpdatePlayerMap(ByRef PlayerGraphics As Graphics)
        For TileCounter = 0 To 101
            If PlayerMap(TileCounter).TileType <> MapTile.MapTileType.Empty Then
                Dim TileCharacter As String = String.Empty
                Select Case PlayerMap(TileCounter).TileType
                    Case MapTile.MapTileType.BomberBase
                        TileCharacter = "B"
                    Case MapTile.MapTileType.MissileBase
                        TileCharacter = "M"
                    Case MapTile.MapTileType.SubmarineBase
                        TileCharacter = "S"
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

    Private Sub UpdateAIMap(AIGraphics As Graphics)
        For TileCounter = 0 To 101
            If AIMap(TileCounter).TileType <> MapTile.MapTileType.Empty Then
                Dim TileCharacter As String = String.Empty
                Select Case AIMap(TileCounter).TileType
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

                AIGraphics.DrawString(TileCharacter, New System.Drawing.Font("Courier New", 10), Brushes.Green, Location)
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

    Private Sub HighlightSelectedHexes(ByRef g As Graphics)
        For Each SelectedHex As Integer In SelectedHexes
            Dim UpperLeft As PointF = Me.HexPoints(SelectedHex)
            Dim x As Single = UpperLeft.X
            Dim y As Single = UpperLeft.Y

            Dim Points(5) As PointF
            Points(0) = New PointF(x, y)
            Points(1) = New PointF(x + Me.SideLength, y)
            Points(2) = New PointF(x + Me.SideLength + Me.ShortSide, y + Me.LongSide)
            Points(3) = New PointF(x + Me.SideLength, y + Me.LongSide + Me.LongSide)
            Points(4) = New PointF(x, y + Me.LongSide + Me.LongSide)
            Points(5) = New PointF(x - Me.ShortSide, y + Me.LongSide)

            g.DrawPolygon(New Pen(Brushes.Yellow, 3), Points)
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

    Private Sub SetVisibility_TurnActionButtons(Visible As Boolean)
        Me.btnTurnAction_Build.Visible = Visible
        Me.btnTurnAction_Spy.Visible = Visible
        Me.btnTurnAction_War.Visible = Visible
        Application.DoEvents()
    End Sub

    Private Sub SetVisibility_BuildBaseButtons(Visible As Boolean)
        Me.btnBuildBase_ABM.Visible = Visible
        Me.btnBuildBase_Bomber.Visible = Visible
        Me.btnBuildBase_Missile.Visible = Visible
        Me.btnBuildBase_Sub.Visible = Visible
        Application.DoEvents()
    End Sub

    Private Sub SetVisibility_ConfirmSelectionButton(Visibility As Boolean)
        Me.btnConfirmSelection.Visible = Visibility
        Application.DoEvents()
    End Sub

    Private Sub SetMessage(Message As String)
        Me.lblMessage.Text = Message
        Application.DoEvents()
    End Sub

    Private Sub btnTurnAction_Build_Click(sender As System.Object, e As System.EventArgs) Handles btnTurnAction_Build.Click
        SetVisibility_TurnActionButtons(False)
        TurnAction_Build()
    End Sub

    Private Sub btnBuildBase_Missile_Click(sender As System.Object, e As System.EventArgs) Handles btnBuildBase_Missile.Click
        Me.ButtonAction = "MISSILE"
    End Sub

    Private Sub btnBuildBase_Sub_Click(sender As System.Object, e As System.EventArgs) Handles btnBuildBase_Sub.Click
        Me.ButtonAction = "SUB"
    End Sub

    Private Sub btnBuildBase_ABM_Click(sender As System.Object, e As System.EventArgs) Handles btnBuildBase_ABM.Click
        Me.ButtonAction = "ABM"
    End Sub

    Private Sub btnBuildBase_Bomber_Click(sender As System.Object, e As System.EventArgs) Handles btnBuildBase_Bomber.Click
        Me.ButtonAction = "BOMBER"
    End Sub

    Private Sub btnConfirmSelection_Click(sender As System.Object, e As System.EventArgs) Handles btnConfirmSelection.Click
        Me.ButtonAction = "CONFIRMSELECTION"
    End Sub
End Class