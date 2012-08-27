Public Class Player
    Inherits PlayerBase

    Public Property IsAIPlayer As Boolean
    Private StartingCities As Integer
    Private Opponents As List(Of PlayerBase)

    Public Overridable Sub PeacetimeTurn()
    End Sub
    Public Overridable Sub WartimeTurn()
    End Sub
    Public Overridable Sub BuildBase()
    End Sub
    Public Overridable Sub Spy()
    End Sub

    Public Sub New()
        Map = New List(Of MapTile)
        Opponents = New List(Of PlayerBase)
        Me.StartingCities = 8
    End Sub

    Public Sub AddOpponent(PlayerName As String, CountryName As String)
        Dim Opponent As New PlayerBase
        Opponent.CountryName = CountryName
        Opponent.PlayerName = PlayerName
        Opponents.Add(Opponent)
    End Sub

    Public Sub SetupMap()

        For x = 0 To 101
            Map.Add(New MapTile)
        Next

        SetMapTileProperties()

        ' Put cities on the map at random locations
        Dim rng As New Random(System.DateTime.Now.Millisecond)
        Dim CityCount As Integer = 0
        Do
            Dim CityTile As MapTile = Map(rng.Next(0, Map.Count - 1))
            If CityTile.TileType <> MapTile.MapTileType.City Then
                CityTile.TileType = MapTile.MapTileType.City
                CityCount += 1
            End If
        Loop Until CityCount = Me.StartingCities

        ' Add a missile base to the map
        Dim BaseCount As Integer = 0
        Do
            Dim MissileBaseTile As MapTile = Map(rng.Next(0, Map.Count - 1))
            If MissileBaseTile.TileType = MapTile.MapTileType.Empty Then
                MissileBaseTile.TileType = MapTile.MapTileType.MissileBase
                BaseCount += 1
            End If
        Loop Until BaseCount = 1

        BaseCount = 0
        Do
            Dim BomberBaseTile As MapTile = Map(rng.Next(0, Map.Count - 1))
            If BomberBaseTile.TileType = MapTile.MapTileType.Empty Then
                BomberBaseTile.TileType = MapTile.MapTileType.BomberBase
                BaseCount += 1
            End If
        Loop Until BaseCount = 1

    End Sub

    Private Sub SetMapTileProperties()
        ' I'm sure there's a more elegant way to do this
        ' For now, I just care about making the map work.
        '
        ' We need to assign rows and columns to each map tile so that we can know which hexes are next to each other
        ' in case a missile goes off target
        For x = 0 To 101
            ' set map rows for each tile
            Select Case x
                Case 0, 2, 5, 9, 14, 20
                    Map(x).MapRow = 0
                Case 1, 4, 8, 13, 19, 25, 31
                    Map(x).MapRow = 1
                Case 3, 7, 12, 18, 24, 30, 36, 42
                    Map(x).MapRow = 2
                Case 6, 11, 17, 23, 29, 35, 41, 47, 53
                    Map(x).MapRow = 3
                Case 10, 16, 22, 28, 34, 40, 46, 52, 58, 64
                    Map(x).MapRow = 4
                Case 15, 21, 27, 33, 39, 45, 51, 57, 63, 69, 75
                    Map(x).MapRow = 5
                Case 26, 32, 38, 44, 50, 56, 62, 68, 74, 80, 86
                    Map(x).MapRow = 6
                Case 37, 43, 49, 55, 61, 67, 73, 79, 85, 91
                    Map(x).MapRow = 7
                Case 48, 54, 60, 66, 72, 78, 84, 90, 95
                    Map(x).MapRow = 8
                Case 59, 65, 71, 77, 83, 89, 94, 98
                    Map(x).MapRow = 9
                Case 70, 76, 82, 88, 93, 97, 100
                    Map(x).MapRow = 10
                Case 81, 87, 92, 96, 99, 101
                    Map(x).MapRow = 11
            End Select

            ' set map columns for each tile
            Select Case x
                Case 15, 26, 37, 48, 59, 70, 81
                    Map(x).MapColumn = 0
                Case 10, 21, 32, 43, 54, 65, 76, 87
                    Map(x).MapColumn = 1
                Case 6, 16, 27, 38, 49, 60, 71, 82, 92
                    Map(x).MapColumn = 2
                Case 3, 11, 22, 33, 44, 55, 66, 77, 88, 96
                    Map(x).MapColumn = 3
                Case 1, 7, 17, 28, 39, 50, 61, 72, 83, 93, 99
                    Map(x).MapColumn = 4
                Case 0, 4, 12, 23, 34, 45, 56, 67, 78, 89, 97, 101
                    Map(x).MapColumn = 5
                Case 2, 8, 18, 29, 40, 51, 62, 73, 84, 94, 100
                    Map(x).MapColumn = 6
                Case 5, 13, 24, 35, 46, 57, 68, 79, 90, 98
                    Map(x).MapColumn = 7
                Case 9, 19, 30, 41, 52, 63, 74, 85, 95
                    Map(x).MapColumn = 8
                Case 14, 25, 36, 47, 58, 69, 80, 91
                    Map(x).MapColumn = 9
                Case 20, 31, 42, 53, 64, 75, 86
                    Map(x).MapColumn = 10
            End Select

            'mark coastal tiles
            'we need to know which tiles are coastal to determine whether or not a sub base can be built there
            Select Case x
                Case 15, 10, 6, 3, 1, 0, 2, 5, 9, 14, 20, 26, 37, 48, 59, 70, 81, 31, 42, 53, 64, 75, 86, 87, 92, 96, 99, 101, 100, 98, 95, 91
                    Map(x).IsCoastal = True
                Case Else
                    Map(x).IsCoastal = False
            End Select

            Map(x).TileType = MapTile.MapTileType.Empty
        Next
    End Sub

    Public Function GetEmptyTileCount() As Integer
        Dim ReturnValue As Integer = 0
        For Each Tile As MapTile In Map
            If Tile.TileType = MapTile.MapTileType.Empty Then
                ReturnValue += 1
            End If
        Next
        Return ReturnValue
    End Function

    Public Function GetEmptyCoastalTileCount() As Integer
        Dim ReturnValue As Integer = 0
        For Each Tile As MapTile In Map
            If Tile.TileType = MapTile.MapTileType.Empty And Tile.IsCoastal Then
                ReturnValue += 1
            End If
        Next
        Return ReturnValue
    End Function

    Public Function GetTileFromCoordinates(Column As Integer, Row As Integer) As MapTile
        Dim ReturnValue As MapTile = Nothing
        For Each Tile As MapTile In Map
            If Tile.MapColumn = Column And Tile.MapRow = Row Then
                ReturnValue = Tile
                Exit For
            End If
        Next
        Return ReturnValue
    End Function
End Class

